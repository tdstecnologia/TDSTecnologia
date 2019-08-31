using System.Collections.Generic;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Infrastructure.Data;
using TDSTecnologia.Site.Infrastructure.Repository;

namespace TDSTecnologia.Site.Infrastructure.Services
{
    public class CursoService : BasicService
    {
        private readonly CursoRespository _cursoRespository;

        public CursoService(AppContexto context) : base(context)
        {
            _cursoRespository = new CursoRespository(context);
        }

        public List<Curso> ListarTodos()
        {
            return _cursoRespository.ListarTodos();
        }

        public void Salvar(Curso curso)
        {
            _context.Add(curso);
            SaveChangesApp();
        }

        public Curso Consultar(int? id)
        {
            return _cursoRespository.Consultar(id);
        }

        public void Atualizar(Curso curso)
        {
            _cursoRespository.Alterar(curso);
            SaveChangesApp();
        }

        public void Excluir(int? id)
        {
            var curso = Consultar(id);
            _cursoRespository.Excluir(curso);
            SaveChangesApp();
        }

        public List<Curso> PesquisarPorNomeDescricao(string texto)
        {
            List<Curso> cursos = _cursoRespository.PesquisarPorNomeDescricao(texto);

            return cursos;
        }
    }
}
