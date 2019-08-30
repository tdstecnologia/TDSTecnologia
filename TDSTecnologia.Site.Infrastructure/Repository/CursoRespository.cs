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
            return await _context.CursoDao.ToListAsync();
        }

        public async void Salvar(Curso curso)
        {
            _context.Add(curso);
            await _context.SaveChangesAsync();
        }

        public async Task<Curso> Consultar(int? id)
        {
            return await _context.CursoDao.FindAsync(id);
        }

        public async Task Alterar(Curso curso)
        {
            _context.Update(curso);
            _context.Entry<Curso>(curso).Property(c => c.Banner).IsModified = false;
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(Curso curso)
        {
            _context.CursoDao.Remove(curso);
            await _context.SaveChangesAsync();
        }

    }
}
