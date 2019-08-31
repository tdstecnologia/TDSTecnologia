using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Curso> ListarTodos()
        {
            return  _context.CursoDao.ToList();
        }

        public void Salvar(Curso curso)
        {
            _context.Add(curso);
        }

        public Curso Consultar(int? id)
        {
            return _context.CursoDao.Find(id);
        }

        public List<Curso> PesquisarPorNomeDescricao(string texto)
        {
            List<Curso> cursos = _context.CursoDao.Where(x => EF.Functions.ILike(x.Nome, $"%{texto}%") || EF.Functions.ILike(x.Descricao, $"%{texto}%")).OrderBy(x => x.Nome).ToList();

            return cursos;
        }

        public void Alterar(Curso curso)
        {
            _context.Update(curso);
            _context.Entry<Curso>(curso).Property(c => c.Banner).IsModified = false;
        }

        public void Excluir(Curso curso)
        {
            _context.CursoDao.Remove(curso);
        }

    }
}
