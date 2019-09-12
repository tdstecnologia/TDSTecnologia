using Microsoft.AspNetCore.Authentication.Cookies;
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
using TDSTecnologia.Site.Infrastructure.Services;

namespace TDSTecnologia.Site.Web
{
    public class StartupStaging
    {
        public IConfiguration Configuration { get; }
        private readonly ILogger _logger;

        public StartupStaging(IConfiguration configuration, ILogger<StartupStaging> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public void ConfigureServices(IServiceCollection services)
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

            services.ConfigureApplicationCookie(opcoes =>
            {
                opcoes.Cookie.HttpOnly = true;
                opcoes.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                opcoes.LoginPath = "/Usuarios/Login";
                opcoes.LogoutPath = "/Usuarios/logout";
                opcoes.SlidingExpiration = true;
            });

            _logger.LogInformation("|======| AMBIENTE DE HOMOLOGAÇÃO |======|");
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
