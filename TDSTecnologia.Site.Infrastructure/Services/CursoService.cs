using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<List<Curso>> ListarTodos()
        {
            return await _cursoRespository.ListarTodos();
        }

        public async Task<int> Salvar(Curso curso)
        {
            _context.Add(curso);
           return await _context.SaveChangesAsync();
        }

        public async Task<Curso> Consultar(int? id)
        {
            return await _cursoRespository.Consultar(id);
        }

        public async Task Atualizar(Curso curso)
        {
            await _cursoRespository.Alterar(curso);
        }

        public async Task Excluir(int? id)
        {
            var curso = await Consultar(id);
            await _cursoRespository.Excluir(curso);
        }

        public List<Curso> PesquisarPorNomeDescricao(string texto)
        {
            List<Curso> cursos = _cursoRespository.PesquisarPorNomeDescricao(texto);

            return cursos;
        }
    }
}
