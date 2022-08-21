using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RFECase.API.Middleware;
using RFECase.API.ViewModel;
using RFECase.Repository;
using RFECase.Repository.Abstract;
using RFECase.Service;
using RFECase.Service.Abstract;

namespace RFECase.API
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
            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.IgnoreNullValues = true;
            })/*.AddFluentValidation(validation =>
            {
                validation.RegisterValidatorsFromAssemblyContaining<Startup>();
            })*/;

            services.AddApiVersioning(version =>
            {
                version.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                version.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<DiffRequestViewModelValidator>();

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "RFE Case Project", Version = "v1" });
            });


            //Singleton is being used because it is kind of database of my project
            services.AddSingleton(typeof(IDiffRepository<>), typeof(DiffRepository<>));

            //it is Transient because For each request, it will be created and disposed.
            services.AddTransient<IDiffService, DiffService>();
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

            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                string clientId = Configuration["Swagger:ClientId"];

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RFE Case v1");
                c.OAuthClientId(clientId);
                c.OAuthAppName("RFE Case");

            });
        }
    }
}
