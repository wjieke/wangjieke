using Autofac;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using WebAPI.Extensions;
using WebAPI.Filters;

namespace WebAPI
{
    /// <summary>
    /// 程序启动类
    /// </summary>
    public class Startup
    {
        private readonly string Any = "any";
        public static ILoggerRepository LoggerRepository { get; set; }
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 此方法由运行时调用，使用此方法向容器添加服务。
        /// </summary>
        /// <param name="services">向容器添加服务的接口对象</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureAddDbContext(Configuration);
            services.ConfigureAddCors(Any);
            services.ConfigureAddMemoryCache();
            services.ConfigureAddSession();
            services.ConfigureAddJwt(Configuration);
            services.ConfigureAddSwagger();
            services.ConfigureAddRoute();
            services.AddScoped<ModelValidationAttribute>();
            services.AddAutoMapper();
        }

        /// <summary>
        /// 此方法由运行时调用。使用此方法配置HTTP请求管道。
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //log4net
            LoggerRepository = LogManager.CreateRepository("NETCoreRepository");
            //log4net从log4net.config文件中读取配置信息
            XmlConfigurator.Configure(LoggerRepository, new FileInfo("log4net.config"));
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
                options.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseCors(Any);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //自定义异常中间件扩展
            app.UseCustomExceptionMiddleware();
        }

        /// <summary>
        /// Autofac 依赖注入容器
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterService();
        }
    }
}