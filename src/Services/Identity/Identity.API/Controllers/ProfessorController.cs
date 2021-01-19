using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    public class ProfessorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}