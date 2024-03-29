using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StarrySkies.Data.Data;
using StarrySkies.Data.Repositories.LocationRepo;
using StarrySkies.Services.Services.Locations;
using StarrySkies.Data.Repositories.WeaponCategoryRepo;
using StarrySkies.Services.Services.WeaponCategories;
using StarrySkies.Data.Repositories.VocationRepo;
using StarrySkies.Services.Services.Vocations;
using StarrySkies.Data.Repositories.SpellRepo;
using StarrySkies.Services.Services.Spells;
using StarrySkies.Data.Repositories.VocationSpellRepo;
using StarrySkies.Services.Services.VocationSpells;

namespace StarrySkies.API
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

            var connectionString = Configuration["connectionStrings:dragonQuestSkies"];
            services.AddDbContext<ApplicationDbContext>(c => c.UseSqlServer(connectionString));

            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IWeaponCategoryRepo, WeaponCategoryRepo>();
            services.AddScoped<IWeaponCategoryService, WeaponCategoryService>();
            services.AddScoped<IVocationRepo, VocationRepo>();
            services.AddScoped<IVocationService, VocationService>();
            services.AddScoped<ISpellRepo, SpellRepo>();
            services.AddScoped<ISpellService, SpellService>();
            services.AddScoped<IVocationSpellRepo, VocationSpellRepo>();
            services.AddScoped<IVocationSpellService, VocationSpellService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StarrySkies.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StarrySkies.API v1"));
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
