using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    public class UserSettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}