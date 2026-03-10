using System.Linq;
using System.Threading.Tasks;
using AnimalCareApplication.Models;
using AnimalCareApplication.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AnimalCareApplication.Controllers
{
    [AuthorizeRoleAttribute("Administrateur")]
    public class VeterinairesController : Controller
    {
        private readonly AnimalCareDbContext _context;

        public VeterinairesController(AnimalCareDbContext context)
        {
            _context = context;
        }

       
       
        [AuthorizeRoleAttribute("Administrateur")]
        public async Task<IActionResult> Index()
        {
            var vets = await _context.Veterinaires
                .Include(v => v.IdUtilisateurNavigation)
                .ToListAsync();

            return View(vets);
        }


    
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var veterinaire = await _context.Veterinaires
                .Include(v => v.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(v => v.IdVeterinaire == id);

            if (veterinaire == null) return NotFound();

            return View(veterinaire);
        }

        
        public IActionResult Create()
        {
            return View();
        }



     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVeterinaireViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            
            var roleVet = await _context.Roles.FirstAsync(r => r.Nom == "Vétérinaire");

            
            var user = new Utilisateur
            {
                Prenom = model.Prenom,
                Nom = model.Nom,
                Email = model.Email,
                MotDePasse = model.MotDePasse,
                IdRole = roleVet.IdRole  
            };

            _context.Utilisateurs.Add(user);
            await _context.SaveChangesAsync(); 

            
            var veterinaire = new Veterinaire
            {
                IdUtilisateur = user.IdUtilisateur,
                Specialite = model.Specialite
            };

            _context.Veterinaires.Add(veterinaire);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var veterinaire = await _context.Veterinaires
                .Include(v => v.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(v => v.IdVeterinaire == id);

            if (veterinaire == null) return NotFound();

            return View(veterinaire);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string Specialite)
        {
            var vet = await _context.Veterinaires.FindAsync(id);
            if (vet == null)
                return NotFound();

          
            vet.Specialite = Specialite;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var veterinaire = await _context.Veterinaires
                .Include(v => v.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(v => v.IdVeterinaire == id);

            if (veterinaire == null) return NotFound();

            return View(veterinaire);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veterinaire = await _context.Veterinaires.FindAsync(id);

            if (veterinaire != null)
            {
                _context.Veterinaires.Remove(veterinaire);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
