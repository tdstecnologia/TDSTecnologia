﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Core.Utilitarios;
using TDSTecnologia.Site.Infrastructure.Services;
using TDSTecnologia.Site.Web.ViewModels;

namespace TDSTecnologia.Site.Web.Controllers
{
    public class HomeController : Controller
    {
 
        private readonly CursoService _cursoService;

        public HomeController(CursoService cursoService)
        {
            _cursoService = cursoService;
        }

        public IActionResult Index()
        {
            List<Curso> cursos = _cursoService.ListarTodos();
            var viewModel = new CursoViewModel
            {
                Cursos = cursos
            };
            return View(viewModel);
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
            if (ModelState.IsValid)
            {
                curso.Banner = UtilImagem.ConverterParaByte(arquivo);
               _cursoService.Salvar(curso);
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        public IActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = _cursoService.Consultar(id);
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

            var curso = _cursoService.Consultar(id);

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

            var curso = _cursoService.Consultar(id);
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

    }
}
