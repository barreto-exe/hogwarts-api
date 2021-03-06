using hogwarts_api.Responses;
using hogwarts_core.Interfaces;
using hogwarts_infrastructure.Data;
using hogwarts_infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace hogwarts_api
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
            services.AddControllers();

            //Registrar los mapeos de DTOs
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Contexto de BD
            services.AddDbContext<HogwartsContext>(options => options.UseNpgsql(Configuration.GetConnectionString("HogwartsPgsql")));

            //Repositorios genéricos
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ApiResponse>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
