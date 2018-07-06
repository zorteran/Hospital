using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hospital.Core;
using Hospital.Core.Interfaces;
using Hospital.Data;
using Hospital.Data.DbManagers;
using Hospital.Data.IRepositories;
using Hospital.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Hospital.Api
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
            services.AddTransient<ICouchDbManager, CouchDbManager>();

            services.AddTransient<IRepository<Doctor>, GenericCouchDbRepository<Doctor>>();
            services.AddTransient<IRepository<Patient>, GenericCouchDbRepository<Patient>>();

            services.AddTransient<IPatientRepository, PatientCouchDbGenericCouchDbRepository>();
            services.AddTransient<IDoctorRepository, DoctorCouchDbGenericCouchDbRepository>();

            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<IPatientService, PatientService>();

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Hospital API",
                    Version = "v1",
                    Description = "Fictional Hospital API to play with CouchDB",
                    Contact = new Contact()
                    {
                        Name = "Maciej Szymczyk",
                        Email = "maciej.szymczyk@outlook.com",
                        Url = "https://mszymczyk.com"
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hospital API V1"); });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();



        }
    }
}
