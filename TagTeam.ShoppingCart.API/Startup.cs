﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TagTeam.ShoppingCart.Domain;
using TagTeam.ShoppingCart.Service;
using TagTeam.ShoppingCart.Service.Interfaces;

namespace TagTeam.ShoppingCart.API
{
    public class Startup
    {
        private IConfiguration _configuration { get; }
        private static string _connectionString { get; set; }
        private static string _adminConnectionString { get; set; }
        private static string _sCConnectionString { get; set; }
        private static string _DocumentUploadPath { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                             .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                             .AddJsonFile("AppSettings.json");

            _configuration = builder.Build();
            _adminConnectionString = _configuration.GetConnectionString("AdminConnection");
            _sCConnectionString = _configuration.GetConnectionString("SCConnection");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<ITest_Interface>(c => new TestService(_connectionString));
            services.AddTransient<ISettings_interface>(c => new SettingsService(_adminConnectionString, _sCConnectionString));
            services.AddTransient<IRef_Stock_interface>(c => new Ref_StockService(_adminConnectionString, _sCConnectionString));
            services.AddTransient<IRef_StockWiseImages_interface>(c => new Ref_StockWiseImagesService(_adminConnectionString, _sCConnectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin().WithMethods("OPTIONS", "GET", "POST", "PUT", "DELETE"));
            app.UseMvc();
        }
    }
}
