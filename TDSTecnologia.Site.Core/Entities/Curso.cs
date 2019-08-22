using System;
using System.Collections.Generic;
using System.Text;

namespace TDSTecnologia.Site.Core.Entities
{
    public class Curso
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public int QuantidadeAula { get; set; }

        public DateTime DataInicio { get; set; }
    }
}
