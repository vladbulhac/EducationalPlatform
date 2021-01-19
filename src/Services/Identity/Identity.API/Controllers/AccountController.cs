using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUri)
        {
        }
    }
}