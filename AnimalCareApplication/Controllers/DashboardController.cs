using Microsoft.AspNetCore.Mvc;
using AnimalCareApplication.Security;

namespace AnimalCareApplication.Controllers
{
   
    [AuthorizeRole("Administrateur", "Secrétaire", "Vétérinaire")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
