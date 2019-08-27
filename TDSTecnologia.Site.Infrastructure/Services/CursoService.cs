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

        public CursoService(AppContexto context): base(context)
        {
            _cursoRespository = new CursoRespository(context);
        }

        public async Task<List<Curso>> ListarTodos()
        {
            return await _cursoRespository.ListarTodos();
        }
    }
}
