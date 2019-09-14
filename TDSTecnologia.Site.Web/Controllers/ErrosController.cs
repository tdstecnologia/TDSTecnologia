﻿using Microsoft.AspNetCore.Mvc;

namespace TDSTecnologia.Site.Web.Controllers
{
    public class ErrosController : AppAbstractController
    {
        [HttpGet("Erros/{codigo}")]
        public IActionResult Index(int codigoErro)
        {
            return View(codigoErro);
        }
    }
}
