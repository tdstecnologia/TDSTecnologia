﻿using Microsoft.EntityFrameworkCore;
using System;
using TDSTecnologia.Site.Core.Dominio;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Infrastructure.Map;

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

            modelBuilder.ApplyConfiguration(new CursoMapConfiguration());

            modelBuilder
            .Entity<Curso>()
            .Property(c => c.Turno)
            .HasConversion(
            v => v.ToString(),
            v => (DomTurno)Enum.Parse(typeof(DomTurno), v));

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
        }

    }
}
