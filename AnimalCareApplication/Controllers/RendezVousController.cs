using System;
using System.Collections.Generic;
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
    [AuthorizeRoleAttribute("Administrateur", "Secrétaire", "Vétérinaire", "Client")]

    public class RendezVousController : Controller
    {
        private readonly AnimalCareDbContext _context;

        public RendezVousController(AnimalCareDbContext context)
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

            IQueryable<RendezVou> query = _context.RendezVous
                .Include(r => r.IdAnimalNavigation)
                .Include(r => r.IdVeterinaireNavigation);

            
            if (role == "Vétérinaire" && userId.HasValue)
            {
                var vet = await _context.Veterinaires
                    .FirstOrDefaultAsync(v => v.IdUtilisateur == userId.Value);

                if (vet != null)
                {
                    query = query.Where(r => r.IdVeterinaire == vet.IdVeterinaire);
                }
            }

            if (role == "Client")
            {
                var clientId = HttpContext.Session.GetInt32("ClientId");

                if (clientId.HasValue)
                {
                    query = query.Where(r =>
                        r.IdAnimalNavigation.IdProprietaire == clientId.Value);
                }
            }

            var liste = await query.ToListAsync();
            return View(liste);
        }


       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RendezVous == null)
            {
                return NotFound();
            }

            var rendezVou = await _context.RendezVous
                .Include(r => r.IdAnimalNavigation)
                .Include(r => r.IdVeterinaireNavigation)
                .FirstOrDefaultAsync(m => m.IdRendezVous == id);
            if (rendezVou == null)
            {
                return NotFound();
            }

            return View(rendezVou);
        }

       
        public async Task<IActionResult> Create()
        {
            var role = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetInt32("UserId");
            var clientId = HttpContext.Session.GetInt32("ClientId");
            ViewBag.Role = role;

            
            IQueryable<Animal> animauxQuery = _context.Animals;

         
            if (role == "Client" && clientId.HasValue)
            {
                animauxQuery = animauxQuery.Where(a => a.IdProprietaire == clientId.Value);
            }

            var animaux = await animauxQuery
                .Select(a => new
                {
                    a.IdAnimal,
                    a.Nom
                })
                .ToListAsync();

            ViewData["IdAnimal"] = new SelectList(animaux, "IdAnimal", "Nom");

            
            if (role == "Administrateur" || role == "Secrétaire" || role == "Client")
            {
                var vets = await _context.Veterinaires
                    .Include(v => v.IdUtilisateurNavigation)
                    .Select(v => new
                    {
                        v.IdVeterinaire,
                        NomComplet = v.IdUtilisateurNavigation.Prenom + " " + v.IdUtilisateurNavigation.Nom
                    })
                    .ToListAsync();

                ViewData["IdVeterinaire"] = new SelectList(vets, "IdVeterinaire", "NomComplet");

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

                ViewBag.VetId = vet.IdVeterinaire;
                ViewBag.VetName = vet.IdUtilisateurNavigation.Prenom + " " + vet.IdUtilisateurNavigation.Nom;

                return View();
            }

           
            return RedirectToAction("Index");
        }


       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRendezVous,DateRv,Heure,Statut,IdAnimal,IdVeterinaire")] RendezVou rendezVou)
        {
            var role = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetInt32("UserId");

            if (role == "Vétérinaire" && userId.HasValue)
            {
                var vet = await _context.Veterinaires
                    .FirstOrDefaultAsync(v => v.IdUtilisateur == userId.Value);

                if (vet != null)
                {
                    rendezVou.IdVeterinaire = vet.IdVeterinaire;
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(rendezVou);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Role = role;
            ViewData["IdAnimal"] = new SelectList(_context.Animals, "IdAnimal", "IdAnimal", rendezVou.IdAnimal);

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
                    "NomComplet",
                    rendezVou.IdVeterinaire
                );
            }
            else if (role == "Vétérinaire" && userId.HasValue)
            {
                var vet = await _context.Veterinaires
                    .Include(v => v.IdUtilisateurNavigation)
                    .FirstOrDefaultAsync(v => v.IdUtilisateur == userId.Value);

                if (vet != null)
                {
                    ViewBag.VetId = vet.IdVeterinaire;
                    ViewBag.VetName = vet.IdUtilisateurNavigation.Prenom + " " + vet.IdUtilisateurNavigation.Nom;
                }
            }

            return View(rendezVou);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RendezVous == null)
            {
                return NotFound();
            }

            var rendezVou = await _context.RendezVous.FindAsync(id);
            if (rendezVou == null)
            {
                return NotFound();
            }
            ViewData["IdAnimal"] = new SelectList(_context.Animals, "IdAnimal", "IdAnimal", rendezVou.IdAnimal);
            ViewData["IdVeterinaire"] = new SelectList(_context.Veterinaires, "IdVeterinaire", "IdVeterinaire", rendezVou.IdVeterinaire);
            return View(rendezVou);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRendezVous,DateRv,Heure,Statut,IdAnimal,IdVeterinaire")] RendezVou rendezVou)
        {
            if (id != rendezVou.IdRendezVous)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rendezVou);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RendezVouExists(rendezVou.IdRendezVous))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAnimal"] = new SelectList(_context.Animals, "IdAnimal", "IdAnimal", rendezVou.IdAnimal);
            ViewData["IdVeterinaire"] = new SelectList(_context.Veterinaires, "IdVeterinaire", "IdVeterinaire", rendezVou.IdVeterinaire);
            return View(rendezVou);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RendezVous == null)
            {
                return NotFound();
            }

            var rendezVou = await _context.RendezVous
                .Include(r => r.IdAnimalNavigation)
                .Include(r => r.IdVeterinaireNavigation)
                .FirstOrDefaultAsync(m => m.IdRendezVous == id);
            if (rendezVou == null)
            {
                return NotFound();
            }

            return View(rendezVou);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RendezVous == null)
            {
                return Problem("Entity set 'AnimalCareDbContext.RendezVous'  is null.");
            }
            var rendezVou = await _context.RendezVous.FindAsync(id);
            if (rendezVou != null)
            {
                _context.RendezVous.Remove(rendezVou);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RendezVouExists(int id)
        {
            return (_context.RendezVous?.Any(e => e.IdRendezVous == id)).GetValueOrDefault();
        }
    }
}
