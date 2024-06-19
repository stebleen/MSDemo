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
using MS.Models.ViewModel;
using System.Net.WebSockets;

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
        //添加autofac的DI配置容器
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //注册IBaseService和IRoleService接口及对应的实现类
            builder.RegisterType<BaseService>().As<IBaseService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<WeChatService>().As<IWeChatService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<DishService>().As<IDishService>().InstancePerLifetimeScope();
            builder.RegisterType<SetmealService>().As<ISetmealService>().InstancePerLifetimeScope();
            builder.RegisterType<ShoppingCartService>().As<IShoppingCartService>().InstancePerLifetimeScope();
            builder.RegisterType<AddressBookService>().As<IAddressBookService>().InstancePerLifetimeScope();
            builder.RegisterType<AddressService>().As<IAddressService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerLifetimeScope();
            builder.RegisterType<BusinessDataService>().As<IBusinessDataService>().InstancePerLifetimeScope();
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            //注册跨域策略
            services.AddCorsPolicy(Configuration);
            //注册webcore服务（网站主要配置）
            services.AddWebCoreService(Configuration);

            services.AddUnitOfWorkService<MSDbContext>(options => 
            { 
                options.UseMySql(Configuration.GetSection("ConectionStrings:MSDbContext").Value);
         
            });

 
            //注册automapper服务
            services.AddAutomapperService();

            /*
            //注册IBaseService和IRoleService接口及对应的实现类
            services.AddScoped<IBaseService, BaseService>();
            services.AddScoped<IRoleService, RoleService>();
            */

            // 配置阿里云OSS
            services.Configure<AliOssOptions>(Configuration.GetSection("AliOss"));
            services.AddScoped<IAliOssService, AliOssService>();

            /*
            // 注册WebSocketManager服务 
            services.AddSingleton<WebSocketManager>();
            services.AddSingleton<WebSocketServerMiddleware>();
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

            
            /*
            app.UseWebSockets();
            app.UseWebSocketServer();
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws") // 检查请求是否为WebSocket请求路径
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        // 当请求是 WebSocket 请求时，处理 WebSocket 连接
                        var webSocketManager = app.ApplicationServices.GetService<WebSocketManager>();
                        await webSocketManager.HandleWebSocketAsync(context); // 直接传递 context
                    }
                    else
                    {
                        // 如果不是 WebSocket 请求，将请求传递给下一个中间件
                        await next();
                    }
                }
                else
                {
                    await next();
                }
            });
            */

            app.UseRouting();

            app.UseCors(WebCoreExtensions.MyAllowSpecificOrigins);  // 添加跨域

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
          

        }



    }
}
