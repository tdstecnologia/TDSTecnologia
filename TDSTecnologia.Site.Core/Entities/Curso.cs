using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TDSTecnologia.Site.Core.Dominio;

namespace TDSTecnologia.Site.Core.Entities
{
    [Table("tb01_curso")]
    public class Curso
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("quantidade_aula")]
        public int QuantidadeAula { get; set; }

        [Column("data_inicio")]
        public DateTime DataInicio { get; set; }

        [Column("banner")]
        public byte[] Banner { get; set; }

        [NotMapped]
        public string BannerBase64 {
            get {
                return Banner != null ?  "data:image/png;base64," + Convert.ToBase64String(Banner, 0, Banner.Length) : null ;
            }
        }

        [Column("turno")]
        public DomTurno Turno { get; set; }

        [Column("modalidade")]
        public DomModalidade Modalidade { get; set; }

        [Column("nivel")]
        public DomNivel Nivel { get; set; }

        [Column("vagas")]
        public int Vagas { get; set; }

    }
}
