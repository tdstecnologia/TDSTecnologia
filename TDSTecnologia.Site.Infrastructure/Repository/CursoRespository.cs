using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Infrastructure.Data;

namespace TDSTecnologia.Site.Infrastructure.Repository
{
    public class CursoRespository : BasicRepository
    {

        public CursoRespository(AppContexto context) : base(context)
        {
        }

        public async Task<List<Curso>> ListarTodos()
        {
            List<Curso> cursos = await _context.CursoDao.ToListAsync();

            cursos.ForEach(c =>
            {
                if (c.Banner != null)
                {
                    //c.BannerBase64 = "data:image/png;base64," + Convert.ToBase64String(c.Banner, 0, c.Banner.Length);
                }
            });

            return cursos;
        }

    }
}
