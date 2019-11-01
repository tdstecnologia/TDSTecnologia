using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Core.Utilitarios;
using TDSTecnologia.Site.Infrastructure.Integrations.Email;
using TDSTecnologia.Site.Infrastructure.Services;
using TDSTecnologia.Site.Web.ViewModels;
using X.PagedList;


namespace TDSTecnologia.Site.Web.Controllers
{
    public class HomeController : AppAbstractController
    {
 
        private readonly CursoService _cursoService;
        private readonly IEmail _email;
        private readonly ILogger<HomeController> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(CursoService cursoService, ILogger<HomeController> logger, IHostingEnvironment hostingEnvironment)
        {
            _cursoService = cursoService;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            
        }

        public IActionResult Index(int? pagina)
        {
            _logger.LogInformation("Listagem de cursos...");
            _logger.LogInformation("SISTEMA OPERACIONAL:: " + Environment.MachineName);
            _logger.LogInformation("SISTEMA OPERACIONAL:: " + Environment.OSVersion);
            _logger.LogInformation("Request.Scheme:: " + ControllerContext.HttpContext.Request.Scheme);
            _logger.LogInformation("Request.Host:: " + ControllerContext.HttpContext.Request.Host);
            _logger.LogInformation("Request.Path:: " + ControllerContext.HttpContext.Request.Path);
            _logger.LogInformation("Request.PathBase:: " + ControllerContext.HttpContext.Request.PathBase);
            IPagedList<Curso> cursos = _cursoService.ListarComPaginacao(pagina);
            var viewModel = new CursoViewModel
            {
                CursosComPaginacao = cursos
            };
            return View(viewModel);
        }

        public IActionResult Pdf()
        {
            HtmlToPdfConverter converter = new HtmlToPdfConverter();
            WebKitConverterSettings settings = new WebKitConverterSettings();
            if (Environment.OSVersion.ToString().ToLower().Contains("windows"))
            {
                settings.WebKitPath = Path.Combine(_hostingEnvironment.ContentRootPath, "QtBinariesWindows");
            }
            else
            {
                settings.WebKitPath = Path.Combine(_hostingEnvironment.ContentRootPath, "QtBinariesLinux");
            }  

            converter.ConverterSettings = settings;
            string schema = ControllerContext.HttpContext.Request.Scheme;
            string host = ControllerContext.HttpContext.Request.Host.Host;
            string url = "{schema}://{host}:52854/Home/Novo";
            url = url.Replace("{schema}", schema);
            url = url.Replace("{host}", host);
            _logger.LogInformation("URL FINAL: "+url);
            PdfDocument document = converter.Convert(url);
            MemoryStream ms = new MemoryStream();
            document.Save(ms);
            document.Close();
            ms.Position = 0;
            FileStreamResult fileStreamResult = new FileStreamResult(ms, "application/pdf");
            fileStreamResult.FileDownloadName = "Relatorio.pdf";     
            return fileStreamResult;
        }

        public IActionResult PesquisarCurso(CursoViewModel pesquisa)
        {
            if (pesquisa.Texto != null && !String.IsNullOrEmpty(pesquisa.Texto))
            {
                List<Curso> cursos = _cursoService.PesquisarPorNomeDescricao(pesquisa.Texto);
                var viewModel = new CursoViewModel
                {
                    Cursos = cursos
                };
                return View("Index", viewModel);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
           
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Novo([Bind("Id,Nome,Descricao,QuantidadeAula,DataInicio,Turno,Modalidade,Nivel,Vagas")] Curso curso, IFormFile arquivo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    curso.Banner = UtilImagem.ConverterParaByte(arquivo);
                    _cursoService.Salvar(curso);
                    AddMensagemSucesso("Curso Cadastrado");
                    return RedirectToAction(nameof(Novo));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                AddMensagemErro("Falha no cadastro");
            }
            return View(curso);
        }

        public IActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = _cursoService.PesquisarPorId(id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        public IActionResult Alterar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = _cursoService.PesquisarPorId(id);

            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(int id, [Bind("Id,Nome,Descricao,QuantidadeAula,DataInicio,Turno,Modalidade,Nivel,Vagas")] Curso curso)
        {
            if (id != curso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _cursoService.Atualizar(curso);
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        public IActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = _cursoService.PesquisarPorId(id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarExclusao(int id)
        {
            _cursoService.Excluir(id);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Email()
        {
            string assunto = "Mensagem da Aplicação TDSTecnologia";

            string mensagem = string.Format("Email enviado!!!");

            await _email.EnviarEmail("zzzzz@gmail.com", assunto, mensagem);

            return RedirectToAction(nameof(Index));
        }

    }
}
