using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}