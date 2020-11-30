using AutoMapper;
using DbAccess.DbContext;
using Library.AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Model.ModelTool;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;

namespace WebAPI.Extensions
{
    /// <summary>
    /// 添加自定义服务，配置扩展类（ConfigureServices 配置服务）
    /// </summary>
    public static class ConfigureExtensions
    {
        /// <summary>
        /// 添加DbContext 配置连接字符串
        /// </summary>
        /// <param name="services">服务容器接口对象</param>
        /// <param name="configuration">配置接口对象</param>
        public static void ConfigureAddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CodeDreamContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        /// <summary>
        /// 配置添加跨域
        /// </summary>
        /// <param name="services">服务容器接口对象</param>
        /// <param name="policyName">策略名称</param>
        public static void ConfigureAddCors(this IServiceCollection services, string policyName)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName, builder =>
                {
                    /*
                     * 备注：
                     * 指定AllowAnyOrigin和AllowCredentials是不安全的配置，可能会导致跨站点请求伪造。
                     * 使用这两种方法配置应用程序时，CORS 服务返回了无效的 CORS 响应。
                     * AllowAnyOrigin允许任何来源的主机访问,默认不允许设置所有主机访问
                     * CORS协议不允许同时指定通配符（任何）源和凭据。如果需要支持凭据，请通过列出各个来源来配置CORS策略。
                     */

                    //builder.AllowAnyOrigin()
                    //指定域名端口的主机访问
                    string[] origins = new string[] {
                        "http://localhost:8080",
                        "http://127.0.0.1:8080",
                        "http://localhost:1337",
                        "http://127.0.0.1:1337",
                        "http://www.wangjk.wang",
                        "http://120.79.196.88"
                    };
                    builder.WithOrigins(origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });
        }

        /// <summary>
        /// 配置添加内存缓存
        /// </summary>
        /// <param name="services">服务容器接口对象</param>
        public static void ConfigureAddMemoryCache(this IServiceCollection services)
        {
            //启用内存缓存(该步骤需在AddSession()调用前使用,启用session之前必须先添加内存缓存)
            services.AddMemoryCache(options =>
            {
                options.ExpirationScanFrequency = TimeSpan.FromMinutes(0.5);
            });//添加内存缓存
            services.AddDistributedMemoryCache();//添加分布式内存缓存
        }

        /// <summary>
        /// 配置添加Session
        /// </summary>
        /// <param name="services">服务容器接口对象</param>
        public static void ConfigureAddSession(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;//这里要改为false，默认是true，true的时候session无效
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(3600000);//设置session的过期时间(1小时)
                options.Cookie.HttpOnly = true;//设置在浏览器不能通过js获得该cookie的值
            });
        }

        /// <summary>
        /// 配置API添加Jwt身份验证
        /// </summary>
        /// <param name="services">服务容器接口对象</param>
        /// <param name="configuration">配置接口对象</param>
        public static void ConfigureAddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            #region 从appsettings.json文件中读取相关配置信息

            services.Configure<JwtSetting>(configuration.GetSection("JwtSetting"));

            JwtSetting jwtSetting = new JwtSetting();
            configuration.Bind("JwtSetting", jwtSetting);

            #endregion

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidIssuer = jwtSetting.Issuer,//Issuer，这两项和前面签发jwt的设置一致
                    ValidAudience = jwtSetting.Audience,//Audience
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SigningKey))//拿到SigningKey
                };
            });
        }

        /// <summary>
        /// 配置注册Swagger生成器，定义一个和多个Swagger文档
        /// Json文件：https://localhost:44372/swagger/v1/swagger.json
        /// UI浏览：https://localhost:44372/swagger/ui/index.html
        /// 线上Json文件：http://www.wangjk.wang:8090/swagger/v1/swagger.json
        /// 线上UI浏览：http://www.wangjk.wang:8090/swagger/ui/index.html
        /// 发布到线上，要修改index.html文件中对应的js代码，地址也指向线上的UI地址
        /// </summary>
        /// <param name="services">服务容器接口对象</param>
        public static void ConfigureAddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "Version 1.0",
                    Title = "代码梦项目的API文档",
                    Description = "作者：王杰轲",
                    //服务条款
                    TermsOfService = new Uri("https://example.com/terms"),
                    //联系人
                    Contact = new OpenApiContact
                    {
                        Name = "王杰轲",
                        Email = string.Empty,
                        Url = new Uri("https://www.cnblogs.com/wangjieke")
                    },
                    //许可证
                    License = new OpenApiLicense
                    {
                        Name = "在作者同意下使用",
                        Url = new Uri("https://user.qzone.qq.com/784622884/main")
                    }
                });
                //为Swagger、JSON和UI设置注释路径。
                //var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath, true);

                #region Swagger
                //string contactName = Configuration.GetSection("SwaggerDoc:ContactName").Value;
                //string contactNameEmail = Configuration.GetSection("SwaggerDoc:ContactEmail").Value;
                //string contactUrl = Configuration.GetSection("SwaggerDoc:ContactUrl").Value;
                //options.SwaggerDoc("v1", new OpenApiInfo
                //{
                //    Version = Configuration.GetSection("SwaggerDoc:Version").Value,
                //    Title = Configuration.GetSection("SwaggerDoc:Title").Value,
                //    Description = Configuration.GetSection("SwaggerDoc:Description").Value,
                //    Contact = new OpenApiContact { Name = contactName, Email = contactNameEmail, Url = new Uri(contactUrl) },
                //    License = new OpenApiLicense { Name = contactName, Url = new Uri(contactUrl) }
                //});

                //var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //var xmlPath = Path.Combine(basePath, "Yuebon.WebApi.xml");
                //options.IncludeXmlComments(xmlPath);
                ////options.DocumentFilter<HiddenApiFilter>(); // 在接口类、方法标记属性 [HiddenApi]，可以阻止【Swagger文档】生成
                //options.OperationFilter<AddHeaderOperationFilter>("correlationId", "Correlation Id for the request", false); // adds any string you like to the request headers - in this case, a correlation id
                //options.OperationFilter<AddResponseHeadersFilter>();
                //options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                //options.OperationFilter<SecurityRequirementsOperationFilter>();
                ////给api添加token令牌证书
                //options.AddSecurityDefinition("jwttoken", new OpenApiSecurityScheme
                //{
                //    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                //    Name = "jwttoken",//jwt默认的参数名称
                //    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                //    Type = SecuritySchemeType.ApiKey
                //});
                #endregion
            });
        }

        /// <summary>
        /// 配置添加ASP.NET Core 3.0 路由方式（控制器、MVC）
        /// </summary>
        /// <param name="services">服务容器接口对象</param>
        public static void ConfigureAddRoute(this IServiceCollection services)
        {
            services.AddControllers()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                    .AddControllersAsServices()// 控制器属性注入
                    .AddJsonOptions(options =>
                    {
                        //.net core3.0 内置Text.Json序列化中文编码设置
                        //options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                    });
            //Microsoft.AspNetCore.Mvc.Newtonsoft
            //services.AddControllers()
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            //    .AddControllersAsServices();// 控制器属性注入;
            //services.AddMvcCore().AddNewtonsoftJson(options => {
            //    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            //});
        }


        /// <summary>
        /// 配置添加Automapper
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapper(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddAutoMapper(typeof(AutoMapperConfig));
            AutoMapperConfig.RegisterMappings();
        }

    }
}
