using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Infrastructure.Services;
using TDSTecnologia.Site.Web.ViewModels;

namespace TDSTecnologia.Site.Web.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly UsuarioService _usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View("Cadastro");
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(CadastroUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    Email = model.Email,
                    PasswordHash = model.Senha,
                    CPF = "02477439170",
                    UserName = "Dherkyan",
                    Nome = "Dherkyan Ribeiro da Silva",
                    Telefone = "(65) 9 9952 5652"
                };

                IdentityResult result = await _usuarioService.Salvar(usuario, model.Senha);

                if (result.Succeeded)
                {
                    await _usuarioService.AdicionarPermissao(usuario, "Administrador");
                    await _usuarioService.Login(usuario,false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var erro in result.Errors)
                        ModelState.AddModelError("", erro.Description.ToString());
                    return View("Cadastro");
                } 
            }
            else
            {
                return View("Cadastro");
            }
        }
    }
}
