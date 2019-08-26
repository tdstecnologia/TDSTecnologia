using Microsoft.EntityFrameworkCore;
using System;
using TDSTecnologia.Site.Core.Dominio;
using TDSTecnologia.Site.Core.Entities;

namespace TDSTecnologia.Site.Infrastructure.Data
{
    public class AppContexto : DbContext
    {
        public AppContexto(DbContextOptions<AppContexto> opcoes) : base(opcoes)
        {
        }

        public DbSet<Curso> CursoDao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Curso>()
            .Property(c => c.Turno)
            .HasConversion(
            v => v.ToString(),
            v => (DomTurno)Enum.Parse(typeof(DomTurno), v));
        }
    }
}
