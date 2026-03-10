using AnimalCareApplication.Models;
using AnimalCareApplication.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;                    
using System.Threading.Tasks;

namespace AnimalCareApplication.Controllers
{
    [AuthorizeRoleAttribute("Administrateur")]
    public class UtilisateursController : Controller
    {
        private readonly AnimalCareDbContext _context;

        public UtilisateursController(AnimalCareDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public IActionResult Create(string? role, string? redirectToVet)
        {
            var roles = _context.Roles.ToList();

            int? selectedRoleId = null;
            if (!string.IsNullOrEmpty(role))
            {
                var r = roles.FirstOrDefault(x => x.Nom == role);   
                if (r != null) selectedRoleId = r.IdRole;
            }

            ViewData["IdRole"] = new SelectList(roles, "IdRole", "Nom", selectedRoleId);
            ViewBag.RedirectToVet = redirectToVet; 

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Utilisateur user)
        {
            if (!ModelState.IsValid)
                return View(user);

           
            var roleVet = await _context.Roles
                .Where(r => r.Nom == "Vétérinaire")
                .Select(r => r.IdRole)
                .FirstAsync();

            user.IdRole = roleVet;   

            _context.Utilisateurs.Add(user);
            await _context.SaveChangesAsync();

            
            return RedirectToAction("Create", "Veterinaires");
        }


    }
}
