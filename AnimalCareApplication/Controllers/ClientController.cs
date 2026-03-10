using AnimalCareApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalCareApplication.Controllers
{
    public class ClientController : Controller
    {
        private readonly AnimalCareDbContext _context;

        public ClientController(AnimalCareDbContext context)
        {
            _context = context;
        }

        private int? GetClientId()
        {
            return HttpContext.Session.GetInt32("ClientId");
        }

        [HttpGet]
        public async Task<IActionResult> PrendreRendezVous()
        {
            var clientId = GetClientId();
            if (clientId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var animaux = await _context.Animals
                .Where(a => a.IdProprietaire == clientId.Value)
                .ToListAsync();

            var veterinaires = await _context.Veterinaires
                .Include(v => v.IdUtilisateurNavigation)
                .ToListAsync();

            var model = new ClientRendezVousViewModel
            {
                DateRv = DateTime.Today.AddDays(1),
                Heure = new TimeSpan(9, 0, 0),
                Animaux = animaux.Select(a => new SelectListItem
                {
                    Value = a.IdAnimal.ToString(),
                    Text = a.Nom + " (" + a.Espece + ")"
                }).ToList(),
                Veterinaires = veterinaires.Select(v => new SelectListItem
                {
                    Value = v.IdVeterinaire.ToString(),
                    Text = v.IdUtilisateurNavigation.Prenom + " " + v.IdUtilisateurNavigation.Nom
                }).ToList()
            };

            if (!model.Animaux.Any())
            {
                ViewBag.Message = "Vous n'avez encore enregistré aucun animal dans la clinique. Veuillez contacter la clinique.";
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PrendreRendezVous(ClientRendezVousViewModel model)
        {
            var clientId = GetClientId();
            if (clientId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            if (!ModelState.IsValid)
            {
                var animaux = await _context.Animals
                    .Where(a => a.IdProprietaire == clientId.Value)
                    .ToListAsync();
                var veterinaires = await _context.Veterinaires
                    .Include(v => v.IdUtilisateurNavigation)
                    .ToListAsync();

                model.Animaux = animaux.Select(a => new SelectListItem
                {
                    Value = a.IdAnimal.ToString(),
                    Text = a.Nom + " (" + a.Espece + ")"
                }).ToList();

                model.Veterinaires = veterinaires.Select(v => new SelectListItem
                {
                    Value = v.IdVeterinaire.ToString(),
                    Text = v.IdUtilisateurNavigation.Prenom + " " + v.IdUtilisateurNavigation.Nom
                }).ToList();

                return View(model);
            }

            var rdv = new RendezVou
            {
                IdAnimal = model.IdAnimal,
                IdVeterinaire = model.IdVeterinaire,
                DateRv = model.DateRv.Date,
                Heure = model.Heure,
                Statut = "Prévu"
            };

            _context.RendezVous.Add(rdv);
            await _context.SaveChangesAsync();

            TempData["RdvSuccess"] = "Votre rendez-vous a été enregistré. Nous vous contacterons pour confirmation.";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> MesRendezVous()
        {
            var clientId = GetClientId();
            if (clientId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var rvs = await _context.RendezVous
                .Include(r => r.IdAnimalNavigation)
                .Include(r => r.IdVeterinaireNavigation)
                    .ThenInclude(v => v.IdUtilisateurNavigation)
                .Where(r => r.IdAnimalNavigation.IdProprietaire == clientId.Value)
                .OrderByDescending(r => r.DateRv)
                .ToListAsync();

            return View(rvs);
        }

        [HttpGet]
        public async Task<IActionResult> ModifierRendezVous(int id)
        {
            var clientId = GetClientId();
            if (clientId == null) return RedirectToAction("Login", "Auth");

            var rv = await _context.RendezVous
                .Include(r => r.IdAnimalNavigation)
                .FirstOrDefaultAsync(r => r.IdRendezVous == id);

            if (rv == null) return NotFound();

            if (rv.IdAnimalNavigation.IdProprietaire != clientId.Value)
                return Forbid();

            // listes pour les dropdowns
            var animaux = await _context.Animals
                .Where(a => a.IdProprietaire == clientId.Value)
                .ToListAsync();

            var veterinaires = await _context.Veterinaires
                .Include(v => v.IdUtilisateurNavigation)
                .ToListAsync();

            var model = new ClientRendezVousViewModel
            {
                IdRendezVous = rv.IdRendezVous, // ➜ ajoute cette propriété dans ton VM si pas déjà
                DateRv = rv.DateRv,
                Heure = rv.Heure,
                IdAnimal = rv.IdAnimal,
                IdVeterinaire = rv.IdVeterinaire,
                Animaux = animaux.Select(a => new SelectListItem
                {
                    Value = a.IdAnimal.ToString(),
                    Text = a.Nom + " (" + a.Espece + ")"
                }).ToList(),
                Veterinaires = veterinaires.Select(v => new SelectListItem
                {
                    Value = v.IdVeterinaire.ToString(),
                    Text = v.IdUtilisateurNavigation.Prenom + " " + v.IdUtilisateurNavigation.Nom
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifierRendezVous(ClientRendezVousViewModel model)
        {
            var clientId = GetClientId();
            if (clientId == null) return RedirectToAction("Login", "Auth");

            var rv = await _context.RendezVous
                .Include(r => r.IdAnimalNavigation)
                .FirstOrDefaultAsync(r => r.IdRendezVous == model.IdRendezVous);

            if (rv == null) return NotFound();

            if (rv.IdAnimalNavigation.IdProprietaire != clientId.Value)
                return Forbid();

            if (!ModelState.IsValid)
            {
                // recharger les listes
                var animaux = await _context.Animals
                    .Where(a => a.IdProprietaire == clientId.Value)
                    .ToListAsync();

                var veterinaires = await _context.Veterinaires
                    .Include(v => v.IdUtilisateurNavigation)
                    .ToListAsync();

                model.Animaux = animaux.Select(a => new SelectListItem
                {
                    Value = a.IdAnimal.ToString(),
                    Text = a.Nom + " (" + a.Espece + ")"
                }).ToList();

                model.Veterinaires = veterinaires.Select(v => new SelectListItem
                {
                    Value = v.IdVeterinaire.ToString(),
                    Text = v.IdUtilisateurNavigation.Prenom + " " + v.IdUtilisateurNavigation.Nom
                }).ToList();

                return View(model);
            }

            // update
            rv.IdAnimal = model.IdAnimal;
            rv.IdVeterinaire = model.IdVeterinaire;
            rv.DateRv = model.DateRv.Date;
            rv.Heure = model.Heure;

            // Option : remettre "Prévu" après modification
            rv.Statut = "Prévu";

            await _context.SaveChangesAsync();

            TempData["RdvSuccess"] = "Votre rendez-vous a été modifié.";
            return RedirectToAction("MesRendezVous");
        }

        [HttpGet]
        public async Task<IActionResult> SupprimerRendezVous(int id)
        {
            var clientId = GetClientId();
            if (clientId == null) return RedirectToAction("Login", "Auth");

            var rv = await _context.RendezVous
                .Include(r => r.IdAnimalNavigation)
                .Include(r => r.IdVeterinaireNavigation).ThenInclude(v => v.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(r => r.IdRendezVous == id);

            if (rv == null) return NotFound();

            if (rv.IdAnimalNavigation.IdProprietaire != clientId.Value)
                return Forbid();

            return View(rv);
        }

        [HttpPost, ActionName("SupprimerRendezVous")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SupprimerRendezVousConfirmed(int id)
        {
            var clientId = GetClientId();
            if (clientId == null) return RedirectToAction("Login", "Auth");

            var rv = await _context.RendezVous
                .Include(r => r.IdAnimalNavigation)
                .FirstOrDefaultAsync(r => r.IdRendezVous == id);

            if (rv == null) return NotFound();

            if (rv.IdAnimalNavigation.IdProprietaire != clientId.Value)
                return Forbid();

            _context.RendezVous.Remove(rv);
            await _context.SaveChangesAsync();

            TempData["RdvSuccess"] = "Votre rendez-vous a été supprimé.";
            return RedirectToAction("MesRendezVous");
        }

    }

}
