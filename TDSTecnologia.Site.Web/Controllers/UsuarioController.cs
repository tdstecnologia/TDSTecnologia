using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Infrastructure.Integrations.Email;
using TDSTecnologia.Site.Infrastructure.Integrations.Google;
using TDSTecnologia.Site.Infrastructure.Security;
using TDSTecnologia.Site.Infrastructure.Services;
using TDSTecnologia.Site.Web.ViewModels;

namespace TDSTecnologia.Site.Web.Controllers
{
    public class UsuarioController : AppAbstractController
    {

        private readonly UsuarioService _usuarioService;
        private readonly UserManager<Usuario> _userManager;
        private readonly IEmail _email;
        public UsuarioController(UsuarioService usuarioService, UserManager<Usuario> userManager, IEmail email)
        {
            _userManager = userManager;
            _email = email;
            _usuarioService = usuarioService;
        }

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

        [HttpPost]
        public async Task<IActionResult> Cadastro(CadastroUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = model.ConverterParaUsuario();

                IdentityResult result = await _usuarioService.Salvar(usuario, model.Senha);

                if (result.Succeeded)
                {
                    //await _usuarioService.AdicionarPermissao(usuario, "Administrador");
                    await EnviarEmailConfirmacaoCadastroAsync(usuario);
                    //await _usuarioService.Login(usuario, false);
                    //return RedirectToAction("Index", "Home");
                    AddMensagemSucesso("Seu cadastro foi realizado. Verifique seu e-mail para confirmar seu cadastro");
                    return RedirectToAction(nameof(Cadastro));
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
                    if (!usuario.EmailConfirmed)
                    {
                        AddMensagemAlerta("Confirme seu e-mail para poder fazer o login");
                    }
                    else
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
                }
                else
                {
                    ModelState.AddModelError("", "Email inválido");
                }
            }
            return View("Login", model);
        }

        public async Task EnviarEmailConfirmacaoCadastroAsync(Usuario usuario)
        {

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(usuario);
            var callbackUrlAction = Url.Action(
                "ConfirmarEmail", "Usuario", new
                {
                    userId = usuario.Id,
                    code = token
                },
                protocol: Request.Scheme);

            await _email.EnviarEmail(usuario.Email, "Confirmação de Conta",
                $"Confirme sua conta clicando no link <a href='{HtmlEncoder.Default.Encode(callbackUrlAction)}'>Clique Aqui</a>.");
        }

        public async Task<IActionResult> ConfirmarEmail(string userId, string code)
        {
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(code))
            {
                AddMensagemErro(string.Format("Link de confirmação inválido"));
                return View();
            }
            else
            {
                Usuario usuario = await _usuarioService.PesquisarUsuarioPeloIdAsync(userId);
                IdentityResult result = await _userManager.ConfirmEmailAsync(usuario, code);
                if (result.Succeeded)
                {
                    AddMensagemSucesso(string.Format("Seu email {0} foi confirmado", usuario.Email));
                }
                else
                {
                    AddMensagemErro(string.Format("Seu email {0} não pode ser confirmado", usuario.Email));
                }
                return View("ConfirmarEmail", result.Succeeded);
            }
        }
    }
}
