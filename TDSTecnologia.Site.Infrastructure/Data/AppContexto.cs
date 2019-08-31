using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TDSTecnologia.Site.Core.Dominio;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Infrastructure.Map;

namespace TDSTecnologia.Site.Infrastructure.Data
{
    public class AppContexto : IdentityDbContext<Usuario, Permissao, string>
    {
        public AppContexto(DbContextOptions<AppContexto> opcoes) : base(opcoes)
        {
        }

        public DbSet<Curso> CursoDao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("tdstecnologia");
            modelBuilder.ApplyConfiguration(new CursoMapConfiguration());

            modelBuilder
           .Entity<Curso>()
           .Property(c => c.Modalidade)
           .HasConversion(
           v => v.ToString(),
           v => (DomModalidade)Enum.Parse(typeof(DomModalidade), v));

            modelBuilder
           .Entity<Curso>()
           .Property(c => c.Nivel)
           .HasConversion(
           v => v.ToString(),
           v => (DomNivel)Enum.Parse(typeof(DomNivel), v));

            modelBuilder.ApplyConfiguration(new UsuarioMapConfiguration());
            modelBuilder.ApplyConfiguration(new PermissaoMapConfiguration());
        }

    }
}
