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
    [AuthorizeRoleAttribute("Administrateur", "Secrétaire")]

    public class ProprietairesController : Controller
    {
        private readonly AnimalCareDbContext _context;

        public ProprietairesController(AnimalCareDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.Proprietaires != null ? 
                          View(await _context.Proprietaires.ToListAsync()) :
                          Problem("Entity set 'AnimalCareDbContext.Proprietaires'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Proprietaires == null)
            {
                return NotFound();
            }

            var proprietaire = await _context.Proprietaires
                .FirstOrDefaultAsync(m => m.IdProprietaire == id);
            if (proprietaire == null)
            {
                return NotFound();
            }

            return View(proprietaire);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProprietaire,Nom,Prenom,Adresse,Telephone,Email")] Proprietaire proprietaire)
        {
            if (ModelState.IsValid)
            {
                proprietaire.MotDePasse = "temp123";

                _context.Add(proprietaire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proprietaire);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Proprietaires == null)
            {
                return NotFound();
            }

            var proprietaire = await _context.Proprietaires.FindAsync(id);
            if (proprietaire == null)
            {
                return NotFound();
            }
            return View(proprietaire);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
    int id,
    [Bind("IdProprietaire,Nom,Prenom,Adresse,Telephone,Email")] Proprietaire proprietaire)
        {
            if (id != proprietaire.IdProprietaire)
                return NotFound();

            if (ModelState.IsValid)
            { 
                var original = await _context.Proprietaires.FindAsync(id);
                if (original == null)
                    return NotFound();

                original.Nom = proprietaire.Nom;
                original.Prenom = proprietaire.Prenom;
                original.Adresse = proprietaire.Adresse;
                original.Telephone = proprietaire.Telephone;
                original.Email = proprietaire.Email;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(proprietaire);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Proprietaires == null)
            {
                return NotFound();
            }

            var proprietaire = await _context.Proprietaires
                .FirstOrDefaultAsync(m => m.IdProprietaire == id);
            if (proprietaire == null)
            {
                return NotFound();
            }

            return View(proprietaire);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Proprietaires == null)
            {
                return Problem("Entity set 'AnimalCareDbContext.Proprietaires'  is null.");
            }
            var proprietaire = await _context.Proprietaires.FindAsync(id);
            if (proprietaire != null)
            {
                _context.Proprietaires.Remove(proprietaire);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProprietaireExists(int id)
        {
          return (_context.Proprietaires?.Any(e => e.IdProprietaire == id)).GetValueOrDefault();
        }
    }
}
