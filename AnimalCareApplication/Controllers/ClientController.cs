using AnimalCareApplication.Models;
using AnimalCareApplication.Patterns.Singleton;
using AnimalCareApplication.Patterns.Strategy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        private string GetJourFrancais(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday: return "Lundi";
                case DayOfWeek.Tuesday: return "Mardi";
                case DayOfWeek.Wednesday: return "Mercredi";
                case DayOfWeek.Thursday: return "Jeudi";
                case DayOfWeek.Friday: return "Vendredi";
                case DayOfWeek.Saturday: return "Samedi";
                case DayOfWeek.Sunday: return "Dimanche";
                default: return "";
            }
        }

        private List<string> GenererCreneaux(TimeSpan debut, TimeSpan fin, int dureeMinutes = 30)
        {
            var creneaux = new List<string>();
            var courant = debut;

            while (courant < fin)
            {
                creneaux.Add(courant.ToString(@"hh\:mm"));
                courant = courant.Add(TimeSpan.FromMinutes(dureeMinutes));
            }

            return creneaux;
        }

        private async Task RechargerListesAsync(ClientRendezVousViewModel model, int clientId)
        {
            var animaux = await _context.Animals
                .Where(a => a.IdProprietaire == clientId)
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
        }

        private async Task EnvoyerNotificationAsync(int idUtilisateur, string message)
        {
            var notification = new Notification
            {
                IdUtilisateur = idUtilisateur,
                Message = message,
                EstLue = false,
                DateCreation = DateTime.Now
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        private async Task NotifierVeterinaireEtAdminsAsync(int idVeterinaire, string message)
        {
            var vet = await _context.Veterinaires
                .Include(v => v.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(v => v.IdVeterinaire == idVeterinaire);

            if (vet != null && vet.IdUtilisateur > 0)
            {
                await EnvoyerNotificationAsync(vet.IdUtilisateur, message);
            }

            var admins = await _context.Utilisateurs
                .Include(u => u.IdRoleNavigation)
                .Where(u => u.IdRoleNavigation.Nom == "Administrateur")
                .ToListAsync();

            foreach (var admin in admins)
            {
                await EnvoyerNotificationAsync(admin.IdUtilisateur, message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> PrendreRendezVous()
        {
            var clientId = GetClientId();
            if (clientId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var model = new ClientRendezVousViewModel
            {
                DateRv = DateTime.Today.AddDays(1),
                Heure = new TimeSpan(9, 0, 0)
            };

            await RechargerListesAsync(model, clientId.Value);

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

            await RechargerListesAsync(model, clientId.Value);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.DateRv.Date < DateTime.Today)
            {
                ModelState.AddModelError("", "Vous ne pouvez pas choisir une date passée.");
                return View(model);
            }

            var animalExiste = await _context.Animals
                .AnyAsync(a => a.IdAnimal == model.IdAnimal && a.IdProprietaire == clientId.Value);

            if (!animalExiste)
            {
                ModelState.AddModelError("", "Animal invalide.");
                return View(model);
            }

            var jour = GetJourFrancais(model.DateRv.Date);

            var dansHoraire = await _context.Horaires.AnyAsync(h =>
                h.IdVeterinaire == model.IdVeterinaire &&
                h.Jour == jour &&
                model.Heure >= h.HeureDebut &&
                model.Heure < h.HeureFin);

            if (!dansHoraire)
            {
                ModelState.AddModelError("", "Le vétérinaire n'est pas disponible à cette date et à cette heure.");
                return View(model);
            }

            var conflit = await _context.RendezVous.AnyAsync(r =>
                r.IdVeterinaire == model.IdVeterinaire &&
                r.DateRv == model.DateRv.Date &&
                r.Heure == model.Heure);

            if (conflit)
            {
                ModelState.AddModelError("", "Ce créneau est déjà pris. Veuillez choisir une autre heure.");
                return View(model);
            }

            var rdv = new RendezVou
            {
                IdAnimal = model.IdAnimal,
                IdVeterinaire = model.IdVeterinaire,
                DateRv = model.DateRv.Date,
                Heure = model.Heure,
            };
            IRendezVousStrategy strategy;

            if (rdv.DateRv.Date == DateTime.Today)
            {
                strategy = new RendezVousUrgentStrategy();
            }
            else
            {
                strategy = new RendezVousNormalStrategy();
            }

            strategy.AppliquerStrategie(rdv);

            _context.RendezVous.Add(rdv);
            await _context.SaveChangesAsync();


            Singleton.Instance.Log("Création d'un rendez-vous par un client");

            TempData["RdvSuccess"] = "Votre rendez-vous a été enregistré avec succès.";

            await NotifierVeterinaireEtAdminsAsync(
                rdv.IdVeterinaire,
                "Un nouveau rendez-vous a été créé par un client."
            );

            return RedirectToAction("MesRendezVous");
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
            if (clientId == null)
                return RedirectToAction("Login", "Auth");

            var rv = await _context.RendezVous
                .Include(r => r.IdAnimalNavigation)
                .FirstOrDefaultAsync(r => r.IdRendezVous == id);

            if (rv == null)
                return NotFound();

            if (rv.IdAnimalNavigation.IdProprietaire != clientId.Value)
                return Forbid();

            var model = new ClientRendezVousViewModel
            {
                IdRendezVous = rv.IdRendezVous,
                DateRv = rv.DateRv,
                Heure = rv.Heure,
                IdAnimal = rv.IdAnimal,
                IdVeterinaire = rv.IdVeterinaire
            };

            await RechargerListesAsync(model, clientId.Value);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifierRendezVous(ClientRendezVousViewModel model)
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Auth");

            var rv = await _context.RendezVous
                .Include(r => r.IdAnimalNavigation)
                .FirstOrDefaultAsync(r => r.IdRendezVous == model.IdRendezVous);

            if (rv == null)
                return NotFound();

            if (rv.IdAnimalNavigation.IdProprietaire != clientId.Value)
                return Forbid();

            await RechargerListesAsync(model, clientId.Value);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.DateRv.Date < DateTime.Today)
            {
                ModelState.AddModelError("", "Vous ne pouvez pas choisir une date passée.");
                return View(model);
            }

            var animalExiste = await _context.Animals
                .AnyAsync(a => a.IdAnimal == model.IdAnimal && a.IdProprietaire == clientId.Value);

            if (!animalExiste)
            {
                ModelState.AddModelError("", "Animal invalide.");
                return View(model);
            }

            var jour = GetJourFrancais(model.DateRv.Date);

            var dansHoraire = await _context.Horaires.AnyAsync(h =>
                h.IdVeterinaire == model.IdVeterinaire &&
                h.Jour == jour &&
                model.Heure >= h.HeureDebut &&
                model.Heure < h.HeureFin);

            if (!dansHoraire)
            {
                ModelState.AddModelError("", "Le vétérinaire n'est pas disponible à cette date et à cette heure.");
                return View(model);
            }

            var conflit = await _context.RendezVous.AnyAsync(r =>
                r.IdVeterinaire == model.IdVeterinaire &&
                r.DateRv == model.DateRv.Date &&
                r.Heure == model.Heure &&
                r.IdRendezVous != model.IdRendezVous);

            if (conflit)
            {
                ModelState.AddModelError("", "Ce créneau est déjà pris. Veuillez choisir une autre heure.");
                return View(model);
            }

            rv.IdAnimal = model.IdAnimal;
            rv.IdVeterinaire = model.IdVeterinaire;
            rv.DateRv = model.DateRv.Date;
            rv.Heure = model.Heure;


            await _context.SaveChangesAsync();

            Singleton.Instance.Log("Modification d'un rendez-vous");

            await NotifierVeterinaireEtAdminsAsync(
                rv.IdVeterinaire,
                "Un rendez-vous a été modifié par un client."
            );

            TempData["RdvSuccess"] = "Votre rendez-vous a été modifié.";

            return RedirectToAction("MesRendezVous");
        }

        [HttpGet]
        public async Task<IActionResult> SupprimerRendezVous(int id)
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Auth");

            var rv = await _context.RendezVous
                .Include(r => r.IdAnimalNavigation)
                .Include(r => r.IdVeterinaireNavigation)
                    .ThenInclude(v => v.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(r => r.IdRendezVous == id);

            if (rv == null)
                return NotFound();

            if (rv.IdAnimalNavigation.IdProprietaire != clientId.Value)
                return Forbid();

            return View(rv);
        }

        [HttpPost, ActionName("SupprimerRendezVous")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SupprimerRendezVousConfirmed(int id)
        {
            var clientId = GetClientId();
            if (clientId == null)
                return RedirectToAction("Login", "Auth");

            var rv = await _context.RendezVous
                .Include(r => r.IdAnimalNavigation)
                .FirstOrDefaultAsync(r => r.IdRendezVous == id);

            if (rv == null)
                return NotFound();

            if (rv.IdAnimalNavigation.IdProprietaire != clientId.Value)
                return Forbid();

            int idVet = rv.IdVeterinaire;
            string dateRv = rv.DateRv.ToString("dd/MM/yyyy");
            string heureRv = rv.Heure.ToString(@"hh\:mm");
            string nomAnimal = rv.IdAnimalNavigation?.Nom ?? "Animal inconnu";

            _context.RendezVous.Remove(rv);
            await _context.SaveChangesAsync();

            Singleton.Instance.Log("Suppression d'un rendez-vous par un client");

            TempData["RdvSuccess"] = "Votre rendez-vous a été supprimé.";

            await NotifierVeterinaireEtAdminsAsync(
                idVet,
                $"Le rendez-vous de {nomAnimal} du {dateRv} à {heureRv} a été supprimé par un client."
            );

            TempData["RdvSuccess"] = "Votre rendez-vous a été supprimé.";
            return RedirectToAction("MesRendezVous");
        }

        [HttpGet]
        public async Task<IActionResult> GetCreneauxDisponibles(int idVeterinaire, DateTime dateRv, int? idRendezVous = null)
        {
            var jour = GetJourFrancais(dateRv);

            var horaires = await _context.Horaires
                .Where(h => h.IdVeterinaire == idVeterinaire && h.Jour == jour)
                .OrderBy(h => h.HeureDebut)
                .ToListAsync();

            var rendezVousPris = await _context.RendezVous
                .Where(r => r.IdVeterinaire == idVeterinaire
                            && r.DateRv == dateRv.Date
                            && (idRendezVous == null || r.IdRendezVous != idRendezVous.Value))
                .Select(r => r.Heure)
                .ToListAsync();

            var liste = new List<object>();

            foreach (var horaire in horaires)
            {
                var creneaux = GenererCreneaux(horaire.HeureDebut, horaire.HeureFin, 30);

                foreach (var creneau in creneaux)
                {
                    var heureSpan = TimeSpan.Parse(creneau);
                    bool estPris = rendezVousPris.Contains(heureSpan);

                    liste.Add(new
                    {
                        heure = creneau,
                        disponible = !estPris
                    });
                }
            }

            return Json(liste);
        }

        [HttpGet]
        public async Task<IActionResult> Notifications()
        {
            var clientId = GetClientId();
            if (clientId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var notifications = await _context.Notifications
                .Where(n => n.IdUtilisateur == clientId.Value)
                .OrderByDescending(n => n.DateCreation)
                .ToListAsync();

            return View(notifications);
        }
    }
}