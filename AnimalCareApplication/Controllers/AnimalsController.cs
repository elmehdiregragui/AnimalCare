using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnimalCareApplication.Models;
using AnimalCareApplication.Security;

namespace AnimalCareApplication.Controllers
{
    [AuthorizeRoleAttribute("Administrateur", "Secrétaire", "Vétérinaire")]
    public class AnimalsController : Controller
    {
        private readonly AnimalCareDbContext _context;

        public AnimalsController(AnimalCareDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var animaux = await _context.Animals
    .Include(a => a.IdProprietaireNavigation)
    .ToListAsync();

            return View(animaux);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.IdProprietaireNavigation)
                .FirstOrDefaultAsync(m => m.IdAnimal == id);

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }
        public IActionResult Create()
        {
            var proprietaires = _context.Proprietaires
                .Select(p => new
                {
                    p.IdProprietaire,
                    NomComplet = p.Prenom + " " + p.Nom
                })
                .ToList();

            ViewData["IdProprietaire"] = new SelectList(
                proprietaires,
                "IdProprietaire",
                "NomComplet"
            );

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAnimal,Nom,Espece,Race,Age,IdProprietaire")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var proprietaires = _context.Proprietaires
                .Select(p => new
                {
                    p.IdProprietaire,
                    NomComplet = p.Prenom + " " + p.Nom
                })
                .ToList();

            ViewData["IdProprietaire"] = new SelectList(
                proprietaires,
                "IdProprietaire",
                "NomComplet",
                animal.IdProprietaire
            );

            return View(animal);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            var proprietaires = _context.Proprietaires
                .Select(p => new
                {
                    p.IdProprietaire,
                    NomComplet = p.Prenom + " " + p.Nom
                })
                .ToList();

            ViewData["IdProprietaire"] = new SelectList(
                proprietaires,
                "IdProprietaire",
                "NomComplet",
                animal.IdProprietaire
            );

            return View(animal);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAnimal,Nom,Espece,Race,Age,IdProprietaire")] Animal animal)
        {
            if (id != animal.IdAnimal)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.IdAnimal))
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

            var proprietaires = _context.Proprietaires
                .Select(p => new
                {
                    p.IdProprietaire,
                    NomComplet = p.Prenom + " " + p.Nom
                })
                .ToList();

            ViewData["IdProprietaire"] = new SelectList(
                proprietaires,
                "IdProprietaire",
                "NomComplet",
                animal.IdProprietaire
            );

            return View(animal);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.IdProprietaireNavigation)
                .FirstOrDefaultAsync(m => m.IdAnimal == id);

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Animals == null)
            {
                return Problem("Entity set 'AnimalCareDbContext.Animals'  is null.");
            }

            var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return (_context.Animals?.Any(e => e.IdAnimal == id)).GetValueOrDefault();
        }
    }
}
