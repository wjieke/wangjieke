using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static Model.Enum.SystemEnum;

namespace DbAccess.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// 全局查询筛选
        /// </summary>
        /// <param name="modelBuilder">模型生成器</param>
        public static void HasQueryFilter(this ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                return;
            }
            //IMutableEntityType entityType
            //IEnumerable<IMutableProperty> props = entityType.GetProperties();
            //var entityTypes = modelBuilder.Model.GetEntityTypes().Where(e => typeof(SystemBase).IsAssignableFrom(e.ClrType));
            IEnumerable<IMutableEntityType> entityTypes = modelBuilder.Model.GetEntityTypes();
            foreach (var entityType in entityTypes)
            {
                //if (modelBuilder == null) { return; }
                IEnumerable<IMutableProperty> props = entityType.GetProperties();
                if (props.Any(x => x.Name == "DataState"))
                {
                    ParameterExpression parameter = Expression.Parameter(entityType.ClrType, "p");
                    BinaryExpression body = Expression.NotEqual(
                        Expression.Call(
                            typeof(EF),
                            nameof(EF.Property),
                            new[] { typeof(DataState) },
                            parameter,
                            Expression.Constant("DataState")),
                        Expression.Constant(DataState.Deleted));
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));
                }
            }
        }

        /// <summary>
        /// 实体映射配置
        /// 要设置复合主键，需使用fluent API
        /// </summary>
        /// <param name="modelBuilder">模型生成器</param>
        public static void EntityMappings(this ModelBuilder modelBuilder)
        {
            //角色权限表
            //modelBuilder.Entity<RolePermission>(entity =>
            //{
            //    entity.HasKey(rp => new { rp.RoleId, rp.PermissionId });
            //});

            //权限表设置主键
            //modelBuilder.Entity<Permission>(entity =>
            //{
            //    entity.HasKey(p => p.PermissionId);//配置类的属性为主键
            //    entity.Ignore(p => p.Id);//从模型中排队某个属性，使该属性不会映射到数据库
            //});

            var assemblyName = "DbAccess";
            var configurationTypes = Assembly.Load(new AssemblyName(assemblyName)).GetTypes()
                .Where(type => !string.IsNullOrWhiteSpace(type.Namespace))
                .Where(type => type.GetTypeInfo().IsClass)
                .Where(type => type.GetTypeInfo().BaseType != null)
                .Where(type => typeof(IEntityTypeConfiguration<>).IsAssignableFrom(type))
                .ToList();

            var maps = Assembly.Load("DbAccess").GetTypes().Where(type => type.GetInterface("IEntityTypeConfiguration`1") != null);

            foreach (var type in maps)
            {
                //Activator.CreateInstance(type, modelBuilder);
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }


        }

        /// <summary>
        /// 实体映射配置
        /// </summary>
        /// <param name="modelBuilder">模型生成器</param>
        /// <param name="assemblyName">程序及名称</param>
        public static void EntityMappingConfigurations(this ModelBuilder modelBuilder)
        {
            var assemblyName = "DbAccess";
            var configurationTypes = Assembly.Load(new AssemblyName(assemblyName)).GetTypes()
                .Where(type => !string.IsNullOrWhiteSpace(type.Namespace))
                .Where(type => type.GetTypeInfo().IsClass)
                .Where(type => type.GetTypeInfo().BaseType != null)
                .Where(type => typeof(IEntityTypeConfiguration<>).IsAssignableFrom(type))
                .ToList();

            foreach (var type in configurationTypes)
            {
                //Activator.CreateInstance(type, modelBuilder);
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);

            }
        }
    }
}
