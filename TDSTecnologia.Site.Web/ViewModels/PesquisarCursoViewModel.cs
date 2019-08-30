
using System.Collections.Generic;
using TDSTecnologia.Site.Core.Entities;

namespace TDSTecnologia.Site.Web.ViewModels
{
    public class CursoViewModel
    {
        public string Texto { get; set; }

        public IEnumerable<Curso> Cursos { get; set; }
    }
}
