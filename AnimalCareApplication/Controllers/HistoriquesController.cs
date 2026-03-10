using AnimalCareApplication.Models;
using AnimalCareApplication.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalCareApplication.Controllers
{
    [AuthorizeRoleAttribute("Administrateur", "Secrétaire", "Vétérinaire")]
    public class HistoriquesController : Controller
    {
        private readonly AnimalCareDbContext _context;

        public HistoriquesController(AnimalCareDbContext context)
        {
            _context = context;
        }


        private string? GetCurrentRole()
            => HttpContext.Session.GetString("UserRole");

        private int? GetCurrentVeterinaireId()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue) return null;

            var vet = _context.Veterinaires
                              .FirstOrDefault(v => v.IdUtilisateur == userId.Value);

            return vet?.IdVeterinaire;
        }


        public async Task<IActionResult> Index()
        {
            var role = GetCurrentRole();
            var idVet = GetCurrentVeterinaireId();

            IQueryable<Historique> query = _context.Historiques
                .Include(h => h.IdAnimalNavigation)
                .Include(h => h.IdVeterinaireNavigation);

           
            if (role == "Vétérinaire" && idVet.HasValue)
            {
                query = query.Where(h => h.IdVeterinaire == idVet.Value);
            }
         

            var model = await query
                .OrderByDescending(h => h.DateSoin)
                .ToListAsync();

            return View(model);
        }

   

        private SelectList BuildVeterinaireSelectList(int? selectedId, string? role, int? idVet)
        {
            
            if (role == "Vétérinaire" && idVet.HasValue)
            {
                var vets = _context.Veterinaires
                    .Where(v => v.IdVeterinaire == idVet.Value)
                    .Select(v => new
                    {
                        v.IdVeterinaire,
                        NomComplet = "Vétérinaire #" + v.IdVeterinaire
                    })
                    .ToList();

                return new SelectList(vets, "IdVeterinaire", "NomComplet", selectedId);
            }
            else
            {
                var vets = _context.Veterinaires
                    .Select(v => new
                    {
                        v.IdVeterinaire,
                        NomComplet = "Vétérinaire #" + v.IdVeterinaire
                    })
                    .ToList();

                return new SelectList(vets, "IdVeterinaire", "NomComplet", selectedId);
            }
        }


        public IActionResult Create()
        {
            var role = GetCurrentRole();
            var idVet = GetCurrentVeterinaireId();

            ViewData["IdAnimal"] = new SelectList(_context.Animals, "IdAnimal", "Nom");
            ViewData["IdVeterinaire"] = BuildVeterinaireSelectList(null, role, idVet);

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DateTime DateSoin, string Description, int IdAnimal)
        {
            var idVet = HttpContext.Session.GetInt32("IdVeterinaire");

            if (idVet == null)
                return RedirectToAction("Login", "Auth");

            var historique = new Historique
            {
                DateSoin = DateSoin,
                Description = Description,
                IdAnimal = IdAnimal,
                IdVeterinaire = idVet.Value
            };

            _context.Historiques.Add(historique);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var historique = await _context.Historiques.FindAsync(id);
            if (historique == null) return NotFound();

            var role = GetCurrentRole();
            var idVet = GetCurrentVeterinaireId();

            if (role == "Vétérinaire" && idVet.HasValue && historique.IdVeterinaire != idVet.Value)
                return Forbid();

            ViewData["IdAnimal"] = new SelectList(_context.Animals, "IdAnimal", "Nom", historique.IdAnimal);
            ViewData["IdVeterinaire"] = BuildVeterinaireSelectList(historique.IdVeterinaire, role, idVet);

            return View(historique);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHistorique,DateSoin,Description,IdAnimal,IdVeterinaire")] Historique historique)
        {
            if (id != historique.IdHistorique) return NotFound();

            var role = GetCurrentRole();
            var idVet = GetCurrentVeterinaireId();

            if (role == "Vétérinaire" && idVet.HasValue)
            {
                historique.IdVeterinaire = idVet.Value;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historique);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Historiques.Any(h => h.IdHistorique == historique.IdHistorique))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdAnimal"] = new SelectList(_context.Animals, "IdAnimal", "Nom", historique.IdAnimal);
            ViewData["IdVeterinaire"] = BuildVeterinaireSelectList(historique.IdVeterinaire, role, idVet);

            return View(historique);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var historique = await _context.Historiques
                .Include(h => h.IdAnimalNavigation)
                .Include(h => h.IdVeterinaireNavigation)
                .FirstOrDefaultAsync(m => m.IdHistorique == id);

            if (historique == null) return NotFound();

            var role = GetCurrentRole();
            var idVet = GetCurrentVeterinaireId();

            if (role == "Vétérinaire" && idVet.HasValue && historique.IdVeterinaire != idVet.Value)
                return Forbid();

            return View(historique);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historique = await _context.Historiques.FindAsync(id);
            if (historique == null) return NotFound();

            var role = GetCurrentRole();
            var idVet = GetCurrentVeterinaireId();

            if (role == "Vétérinaire" && idVet.HasValue && historique.IdVeterinaire != idVet.Value)
                return Forbid();

            _context.Historiques.Remove(historique);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
