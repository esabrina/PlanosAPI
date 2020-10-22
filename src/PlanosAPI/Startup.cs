using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlanosAPI.Models;
using PlanosAPI.Repositories;

namespace PlanosAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PlanoDBContext>(opt => opt.UseInMemoryDatabase("PlanosDB"));
            services.AddTransient<IPlanoRepository, PlanoRepository>();
            services.AddTransient<ITipoPlanoRepository, TipoPlanoRepository>();
            services.AddTransient<IDDDRepository, DDDRepository>();
            services.AddTransient<IOperadoraRepository, OperadoraRepository>();
            services.AddControllers();
            services.AddMvcCore(options =>
            {
                options.EnableEndpointRouting = false;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // enable OData capabilities 
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.EnableDependencyInjection();
                routeBuilder.Select().Filter().Count();
            });

            // Carga de dados experimental
            CargaTeste.Initialize(app.ApplicationServices);
        }
    }
}
