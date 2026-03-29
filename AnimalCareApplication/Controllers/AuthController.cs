using Microsoft.AspNetCore.Mvc;
using AnimalCareApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AnimalCareApplication.Patterns.Singleton;


namespace AnimalCareApplication.Controllers
{
    public class AuthController : Controller
    {
        private readonly AnimalCareDbContext _context;

        public AuthController(AnimalCareDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> MesRendezVous()
        {
            int? clientId = HttpContext.Session.GetInt32("ClientId");
            if (clientId == null) return RedirectToAction("Login", "Auth");

            var rvs = await _context.RendezVous
                .Include(r => r.IdAnimalNavigation)
                .Include(r => r.IdVeterinaireNavigation)
                .Where(r => r.IdAnimalNavigation.IdProprietaire == clientId.Value)
                .OrderByDescending(r => r.DateRv)
                .ToListAsync();

            return View(rvs);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(
            string Nom,
            string Prenom,
            string Adresse,
            string Telephone,
            string Email,
            string MotDePasse)
        {
            if (string.IsNullOrWhiteSpace(Nom) ||
                string.IsNullOrWhiteSpace(Prenom) ||
                string.IsNullOrWhiteSpace(Adresse) ||
                string.IsNullOrWhiteSpace(Telephone) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(MotDePasse))
            {
                ViewBag.Error = "Veuillez remplir tous les champs.";
                return View();
            }

            bool emailExistant = await _context.Proprietaires
                .AnyAsync(p => p.Email == Email);

            if (emailExistant)
            {
                ViewBag.Error = "Un compte existe déjà avec cet email.";
                return View();
            }

            var proprietaire = new Proprietaire
            {
                Nom = Nom,
                Prenom = Prenom,
                Adresse = Adresse,
                Telephone = Telephone,
                Email = Email,
                MotDePasse = MotDePasse
            };

            _context.Proprietaires.Add(proprietaire);
            await _context.SaveChangesAsync();
            Singleton.Instance.Log("Nouvel utilisateur inscrit : " + Email);

            TempData["RegisterSuccess"] =
                "Votre compte a été créé. Vous pouvez maintenant vous connecter.";

            return RedirectToAction("Login");
        }


        
        [HttpPost]
        public async Task<IActionResult> Login(string email, string motDePasse)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(motDePasse))
            {
                ViewBag.Error = "Veuillez remplir tous les champs.";
                return View();
            }

            var user = await _context.Utilisateurs
                .Include(u => u.IdRoleNavigation)
                .Include(u => u.Veterinaire)  
                .FirstOrDefaultAsync(u => u.Email == email && u.MotDePasse == motDePasse);

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.IdUtilisateur);
                Singleton.Instance.Log("Utilisateur connecté : " + email);

                HttpContext.Session.SetString("UserName", user.Prenom + " " + user.Nom);
                HttpContext.Session.SetString("UserRole", user.IdRoleNavigation.Nom);

                if (user.Veterinaire != null)
                {
                    HttpContext.Session.SetInt32("IdVeterinaire", user.Veterinaire.IdVeterinaire);
                }

                return user.IdRoleNavigation.Nom switch
                {
                    "Administrateur" => RedirectToAction("Index", "Dashboard"),
                    "Vétérinaire" => RedirectToAction("Index", "RendezVous"),
                    "Secrétaire" => RedirectToAction("Index", "Proprietaires"),
                    _ => RedirectToAction("Index", "Home")
                };
            }

            var proprietaire = await _context.Proprietaires
                .FirstOrDefaultAsync(p => p.Email == email && p.MotDePasse == motDePasse);

            if (proprietaire != null)
            {
                HttpContext.Session.SetInt32("ClientId", proprietaire.IdProprietaire);
                HttpContext.Session.SetString("UserName", proprietaire.Prenom + " " + proprietaire.Nom);
                HttpContext.Session.SetString("UserRole", "Client");

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Email ou mot de passe incorrect.";
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
