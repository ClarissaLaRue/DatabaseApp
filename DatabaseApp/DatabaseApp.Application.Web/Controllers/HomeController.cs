using Microsoft.AspNetCore.Mvc;

namespace Database.Application.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}