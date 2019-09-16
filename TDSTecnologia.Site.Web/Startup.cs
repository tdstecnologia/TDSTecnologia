﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using TDSTecnologia.Site.Core.Entities;
using TDSTecnologia.Site.Infrastructure.Data;
using TDSTecnologia.Site.Infrastructure.Integrations.Email;
using TDSTecnologia.Site.Infrastructure.Integrations.Google;
using TDSTecnologia.Site.Infrastructure.Services;

namespace TDSTecnologia.Site.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
            _logger.LogInformation("ARQUIVO Construtor: " + Configuration.GetValue<string>("Arquivo"));
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkNpgsql()
         .AddDbContext<AppContexto>(options => options.UseNpgsql(Databases.Instance.Conexao));

            services.AddIdentity<Usuario, Permissao>()
                            .AddDefaultUI(UIFramework.Bootstrap4)
                            .AddEntityFrameworkStores<AppContexto>();

            services.AddScoped<CursoService, CursoService>();
            services.AddScoped<UsuarioService, UsuarioService>();
            services.AddScoped<PermissaoService, PermissaoService>();

            services.Configure<ConfiguracoesEmail>(Configuration.GetSection("ConfiguracoesEmail"));
            services.AddScoped<IEmail, Email>();
            services.AddLogging();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();

            services.Configure<GoogleReCaptcha>(Configuration.GetSection("GoogleReCaptcha"));

            services.ConfigureApplicationCookie(opcoes =>
            {
                opcoes.Cookie.HttpOnly = true;
                opcoes.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                opcoes.LoginPath = "/Usuarios/Login";
                opcoes.LogoutPath = "/Usuarios/logout";
                opcoes.SlidingExpiration = true;
            });

        }

        public void ConfigureStagingServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkNpgsql()
         .AddDbContext<AppContexto>(options => options.UseNpgsql(Databases.Instance.Conexao));

            services.AddIdentity<Usuario, Permissao>()
                            .AddDefaultUI(UIFramework.Bootstrap4)
                            .AddEntityFrameworkStores<AppContexto>();

            services.AddScoped<CursoService, CursoService>();
            services.AddScoped<UsuarioService, UsuarioService>();
            services.AddScoped<PermissaoService, PermissaoService>();

            services.Configure<ConfiguracoesEmail>(Configuration.GetSection("ConfiguracoesEmail"));
            services.AddScoped<IEmail, Email>();
            services.AddLogging();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();

            services.Configure<GoogleReCaptcha>(Configuration.GetSection("Googl	eReCaptcha"));

            services.ConfigureApplicationCookie(opcoes =>
            {
                opcoes.Cookie.HttpOnly = true;
                opcoes.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                opcoes.LoginPath = "/Usuarios/Login";
                opcoes.LogoutPath = "/Usuarios/logout";
                opcoes.SlidingExpiration = true;
            });

        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opcoes =>
            {
                opcoes.Cookie.HttpOnly = true;
                opcoes.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                opcoes.Cookie.Name = "TDSTecnologiaSite";
                opcoes.LoginPath = new PathString("/Acesso/Usuario/Login");
                opcoes.LogoutPath = new PathString("/Usuario/Logout");
                opcoes.AccessDeniedPath = new PathString("/Usuario/AcessoNegado");
                opcoes.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                opcoes.SlidingExpiration = true;
            });

            services.AddEntityFrameworkNpgsql()
         .AddDbContext<AppContexto>(options => options.UseNpgsql(Databases.Instance.Conexao));

            services.AddIdentity<Usuario, Permissao>()
                            .AddDefaultUI(UIFramework.Bootstrap4)
                            .AddEntityFrameworkStores<AppContexto>();

            services.AddScoped<CursoService, CursoService>();
            services.AddScoped<UsuarioService, UsuarioService>();
            services.AddScoped<PermissaoService, PermissaoService>();

            services.Configure<ConfiguracoesEmail>(Configuration.GetSection("ConfiguracoesEmail"));
            services.AddScoped<IEmail, Email>();

            services.Configure<GoogleReCaptcha>(Configuration.GetSection("GoogleReCaptcha"));

        }

        public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env)
        {
            _logger.LogInformation("AMBIENTE: " + env.EnvironmentName);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }

        public void ConfigureStaging(IApplicationBuilder app, IHostingEnvironment env)
        {
            _logger.LogInformation("AMBIENTE: " + env.EnvironmentName);
            _logger.LogInformation("ARQUIVO: " + Configuration.GetValue<string>("Arquivo"));
            _logger.LogInformation("Texto: " + Configuration.GetValue<string>("Texto"));

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }

        public void ConfigureProduction(IApplicationBuilder app, IHostingEnvironment env)
        {
            _logger.LogInformation("AMBIENTE: " + env.EnvironmentName);
            _logger.LogInformation("ARQUIVO: " + Configuration.GetValue<string>("Arquivo"));
            app.UseStatusCodePagesWithReExecute("/Erros/{0}");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
