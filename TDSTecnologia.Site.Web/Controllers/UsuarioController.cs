﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Infrastructure.Integrations.Google;
using TDSTecnologia.Site.Infrastructure.Security;
using TDSTecnologia.Site.Infrastructure.Services;
using TDSTecnologia.Site.Web.ViewModels;

namespace TDSTecnologia.Site.Web.Controllers
{
    [Area("Acesso")]
    [Route("Usuario")]
    public class UsuarioController : AppAbstractController
    {

        private readonly UsuarioService _usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _usuarioService.Logout();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Cadastro()
        {
            return View("Cadastro");
        }

        [HttpGet]
        [Route("AcessoNegado")]
        public IActionResult AcessoNegado()
        {
            AddMensagemErro("Acesso Negado");
            return View("AcessoNegado");
        }

        [HttpPost]
        public async Task<IActionResult> Cadastro(CadastroUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = model.ConverterParaUsuario();

                IdentityResult result = await _usuarioService.Salvar(usuario, model.Senha);

                if (result.Succeeded)
                {
                    await _usuarioService.AdicionarPermissao(usuario, "Administrador");
                    await _usuarioService.Login(usuario, false);
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

        [AllowAnonymous]
        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _usuarioService.Logout();
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            string recaptcha = HttpContext.Request.Form["g-recaptcha-response"];
            if (string.IsNullOrEmpty(recaptcha))
            {
                AddMensagemErro("ReCaptcha inválido");
                return View(model);
            }
            else if(!GoogleReCaptchaService.IsReCaptchaValido(recaptcha))
            {
                AddMensagemErro("ReCaptcha inválido, Tente novamente!");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var usuario = await _usuarioService.PesquisarUsuarioPeloEmail(model.Email);

                if (usuario != null)
                {

                    if (SecurityUtil.CompararSenhas(usuario, model.Senha))
                    {
                        await _usuarioService.Login(usuario, false);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Senha inválida");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email inválido");
                }
            }
            return View(model);
        }
    }
}
