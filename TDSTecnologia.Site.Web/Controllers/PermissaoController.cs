using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Infrastructure.Services;

namespace TDSTecnologia.Site.Web.Controllers
{
    public class PermissaoController : Controller
    {
        private readonly PermissaoService _permissaoService;

        public PermissaoController(PermissaoService permissaoService)
        {
            _permissaoService = permissaoService;
        }

        public IActionResult Index()
        {
            List<Permissao> permissoes = _permissaoService.ListarTodos();
            return View("Index", permissoes);
        }

        public IActionResult Novo()
        {
            return View("Novo");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo([Bind("Descricao,Name")] Permissao permissao)
        {
            if (ModelState.IsValid)
            {
                bool existePermissao = await _permissaoService.ExistePermissao(permissao.Name);

                if (!existePermissao)
                {
                    permissao.NormalizedName = permissao.Name.ToUpper();
                    await _permissaoService.Salvar(permissao);
                    return RedirectToAction("Novo", "Permissao");
                }
            }
            return View(permissao);
        }
    }
}
