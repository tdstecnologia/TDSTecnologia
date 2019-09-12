using Microsoft.AspNetCore.Mvc;

namespace TDSTecnologia.Site.Web.Controllers
{
    public class Erros : AppAbstractController
    {
        [HttpGet("Erros/{codigo}")]
        public IActionResult Index(int codigoErro)
        {
            return View(codigoErro);
        }
    }
}
