using Microsoft.AspNetCore.Mvc;

namespace TDSTecnologia.Site.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
