using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AnimalCareApplication.Models;
using AnimalCareApplication.Security;

namespace AnimalCareApplication.Controllers
{
    [AuthorizeRoleAttribute("Administrateur", "Secrétaire", "Vétérinaire")]
    public class HorairesController : Controller
    {
        private readonly AnimalCareDbContext _context;

        public HorairesController(AnimalCareDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetInt32("UserId");
            var userName = HttpContext.Session.GetString("UserName");

            ViewBag.Role = role;
            ViewBag.UserName = userName;

            IQueryable<Horaire> query = _context.Horaires
                .Include(h => h.IdVeterinaireNavigation)
                .ThenInclude(v => v.IdUtilisateurNavigation);

            if (role == "Vétérinaire" && userId.HasValue)
            {
                var vet = await _context.Veterinaires
                    .FirstOrDefaultAsync(v => v.IdUtilisateur == userId.Value);

                if (vet != null)
                    query = query.Where(h => h.IdVeterinaire == vet.IdVeterinaire);
            }

            var liste = await query.ToListAsync();
            return View(liste);
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var horaire = await _context.Horaires
                .Include(h => h.IdVeterinaireNavigation)
                .ThenInclude(v => v.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.IdHoraire == id);

            if (horaire == null) return NotFound();

            return View(horaire);
        }

        public async Task<IActionResult> Create()
        {
            var role = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Role = role;

            if (role == "Administrateur" || role == "Secrétaire")
            {
                ViewData["IdVeterinaire"] = new SelectList(
                    _context.Veterinaires
                        .Include(v => v.IdUtilisateurNavigation)
                        .Select(v => new
                        {
                            v.IdVeterinaire,
                            NomComplet = v.IdUtilisateurNavigation.Prenom + " " + v.IdUtilisateurNavigation.Nom
                        }),
                    "IdVeterinaire",
                    "NomComplet"
                );

                return View();
            }

            if (role == "Vétérinaire" && userId.HasValue)
            {
                var vet = await _context.Veterinaires
                    .Include(v => v.IdUtilisateurNavigation)
                    .FirstOrDefaultAsync(v => v.IdUtilisateur == userId.Value);

                if (vet == null)
                {
                    TempData["Error"] = "Aucun vétérinaire associé à ce compte.";
                    return RedirectToAction("Index");
                }

                ViewBag.VetName = vet.IdUtilisateurNavigation.Prenom + " " + vet.IdUtilisateurNavigation.Nom;

                var model = new Horaire
                {
                    IdVeterinaire = vet.IdVeterinaire
                };

                return View(model);
            }

            return RedirectToAction("Index");
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Horaire horaire)
        {
            var role = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetInt32("UserId");

            if (role == "Vétérinaire" && userId.HasValue)
            {
                var vet = await _context.Veterinaires
                    .FirstOrDefaultAsync(v => v.IdUtilisateur == userId.Value);

                if (vet == null) return Forbid();

                horaire.IdVeterinaire = vet.IdVeterinaire;
            }

            _context.Horaires.Add(horaire);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var role = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Role = role;

            var horaire = await _context.Horaires
                .Include(h => h.IdVeterinaireNavigation)
                .ThenInclude(v => v.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(h => h.IdHoraire == id);

            if (horaire == null) return NotFound();

            if (role == "Vétérinaire" && userId.HasValue)
            {
                var vet = await _context.Veterinaires
                    .FirstOrDefaultAsync(v => v.IdUtilisateur == userId.Value);

                if (vet == null || horaire.IdVeterinaire != vet.IdVeterinaire)
                    return Forbid();

                ViewBag.VetName = horaire.IdVeterinaireNavigation.IdUtilisateurNavigation.Prenom
                                  + " "
                                  + horaire.IdVeterinaireNavigation.IdUtilisateurNavigation.Nom;

                return View(horaire);
            }

            ViewData["IdVeterinaire"] = new SelectList(
                _context.Veterinaires
                    .Include(v => v.IdUtilisateurNavigation)
                    .Select(v => new
                    {
                        v.IdVeterinaire,
                        NomComplet = v.IdUtilisateurNavigation.Prenom + " " + v.IdUtilisateurNavigation.Nom
                    }),
                "IdVeterinaire",
                "NomComplet",
                horaire.IdVeterinaire
            );

            return View(horaire);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Horaire horaire)
        {
            if (id != horaire.IdHoraire) return NotFound();

            var role = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetInt32("UserId");


            if (role == "Vétérinaire" && userId.HasValue)
            {
                var vet = await _context.Veterinaires
                    .FirstOrDefaultAsync(v => v.IdUtilisateur == userId.Value);

                if (vet == null) return Forbid();

                horaire.IdVeterinaire = vet.IdVeterinaire;
            }

            _context.Update(horaire);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var horaire = await _context.Horaires
                .Include(h => h.IdVeterinaireNavigation)
                .ThenInclude(v => v.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.IdHoraire == id);

            if (horaire == null) return NotFound();

            return View(horaire);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horaire = await _context.Horaires.FindAsync(id);
            if (horaire != null)
            {
                _context.Horaires.Remove(horaire);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
