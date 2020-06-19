using Microsoft.AspNetCore.Mvc;

namespace Database.Application.Web.Controllers
{
    public class TrainerController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}