using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TDSTecnologia.Site.Infrastructure.Data;
using TDSTecnologia.Site.Infrastructure.Services;

namespace TDSTecnologia.Site.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkNpgsql()
         .AddDbContext<AppContexto>(options => options.UseNpgsql(Configuration.GetConnectionString("AppConnection")));

            services.AddScoped<CursoService, CursoService>();
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkNpgsql()
         .AddDbContext<AppContexto>(options => options.UseNpgsql(Databases.Instance.Conexao));

            services.AddScoped<CursoService, CursoService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
