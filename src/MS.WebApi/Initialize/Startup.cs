using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MS.DbContexts;
using MS.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using MS.WebCore;
using Pomelo.EntityFrameworkCore.MySql.Storage;

using MS.Models.Automapper;
using MS.Services;
using Autofac;


namespace MS.WebApi
{
    public class Startup
    {
        /*
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        */
        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IWebHostEnvironment env)
        {
            // In ASP.NET Core 3.0 `env` will be an IWebHostingEnvironment, not IHostingEnvironment.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        //���autofac��DI��������
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //ע��IBaseService��IRoleService�ӿڼ���Ӧ��ʵ����
            builder.RegisterType<BaseService>().As<IBaseService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            //ע��������
            services.AddCorsPolicy(Configuration);
            //ע��webcore������վ��Ҫ���ã�
            services.AddWebCoreService(Configuration);

            services.AddUnitOfWorkService<MSDbContext>(options => 
            { 
                options.UseMySql(Configuration.GetSection("ConectionStrings:MSDbContext").Value);
         
            });

 
            //ע��automapper����
            services.AddAutomapperService();

            /*
            //ע��IBaseService��IRoleService�ӿڼ���Ӧ��ʵ����
            services.AddScoped<IBaseService, BaseService>();
            services.AddScoped<IRoleService, RoleService>();
            */


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
             //   app.UseExceptionHandler("/Error");
            //}

            //app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(WebCoreExtensions.MyAllowSpecificOrigins);  // ��ӿ���

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }



    }
}
