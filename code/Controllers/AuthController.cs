using Microsoft.AspNetCore.Mvc;
using ProjetFinDeSession.Models;
using ProjetFinDeSession.Services;

namespace ProjetFinDeSession.Controllers
{
    public class AuthController : Controller
    {
        private readonly Singleton _data = Singleton.Instance;

        // PAGE INSCRIPTION
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string nom, string prenom, string email, string motDePasse)
        {
            Singleton.Instance.LogMessage("Tentative d'inscription : " + email);

            if (_data.EmailExiste(email))
            {
                ViewBag.Error = "Email déjà utilisé";
                return View();
            }

            var user = new Utilisateur()
            {
                IdUtilisateur = _data.Utilisateurs.Count + 1,
                Nom = nom,
                Prenom = prenom,
                Email = email,
                MotDePasse = motDePasse
            };

            _data.Utilisateurs.Add(user);

            Singleton.Instance.LogMessage("Utilisateur ajouté : " + email);

            TempData["Success"] = "Compte créé avec succès";
            return RedirectToAction("Login");
        }

        // PAGE CONNEXION
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string motDePasse)
        {
            Singleton.Instance.LogMessage("Tentative connexion : " + email);

            var user = _data.TrouverUtilisateur(email, motDePasse);

            if (user == null)
            {
                Singleton.Instance.LogMessage("Connexion échouée pour : " + email);

                ViewBag.Error = "Identifiants incorrects";
                return View();
            }

            Singleton.Instance.LogMessage("Connexion réussie pour : " + email);

            HttpContext.Session.SetString("user", user.Email);

            return RedirectToAction("Index", "Home");
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Login");
        }
    }
}
