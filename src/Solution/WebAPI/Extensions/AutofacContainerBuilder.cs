using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using DbAccess.DbContext;
using Library.AutoMapper;
using Library.Interceptor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace WebAPI.Extensions
{
    /// <summary>
    /// Autofac容器生成器
    /// </summary>
    public static class AutofacContainerBuilder
    {
        /// <summary>
        /// Autofac容器生成器扩展方法（注册服务）
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterService(this ContainerBuilder builder)
        {
            //注册EF上下文(Autofac属性方式注入CodeDreamContext)
            builder.RegisterType<CodeDreamContext>().As<DbContext>().InstancePerLifetimeScope().PropertiesAutowired();
            //注册HTTP请求上下文存取器(Autofac属性方式注入HttpContextAccessor)
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope().PropertiesAutowired();

            //注册拦截器类
            //builder.RegisterType<ServiceInterceptor>();

            //注册所有数据服务(Services类库),所有继承IService接口的类
            //builder.RegisterAssemblyTypes(Assembly.Load("Service"))
            //    .Where(type => typeof(IService).IsAssignableFrom(type) && !type.IsAbstract)
            //    .AsImplementedInterfaces()
            //    .PropertiesAutowired()                   //属性注入
            //    .EnableInterfaceInterceptors();          //启用拦截器
            //                                             //.InterceptedBy(typeof(ServiceInterceptor));
            //                                             //注册所有控制器
            //var controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
            //    .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();
            //builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();


            //注册拦截器类
            builder.RegisterType<AutofacServiceInterceptor>();

            //注册AutoMapper
            builder.Register(
                c => new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfile());
                }))
                .AsSelf()
                .SingleInstance();

            builder.Register(
                c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            var assembly = Assembly.Load("Service");
            builder.RegisterAssemblyTypes(assembly).Where(type => type.GetInterface("IBaseService`1") != null)
                .AsImplementedInterfaces()
                //属性注入
                .PropertiesAutowired();
                //启用拦截器(若要启用拦截器放开注释)
                //.EnableInterfaceInterceptors();
                //允许控制器上类上使用拦截器
                //.EnableClassInterceptors();

            var controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();
            builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();
        }
    }
}
