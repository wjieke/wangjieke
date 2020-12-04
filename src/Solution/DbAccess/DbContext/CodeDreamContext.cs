using DbAccess.Extensions;
using Microsoft.EntityFrameworkCore;
using Model.Entity.Blog;
using Model.Entity.System;

namespace DbAccess.DbContext
{
    /// <summary>
    /// 数据上下文类
    /// </summary>
    public class CodeDreamContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public CodeDreamContext()
        {

        }
        public CodeDreamContext(DbContextOptions<CodeDreamContext> options) : base(options)
        {

        }

        #region Admin 后台模块

        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public DbSet<Menu> Menus { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public DbSet<Area> Areas { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public DbSet<Company> Companys { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// 角色权限
        /// </summary>
        public DbSet<RolePermission> RolePermissions { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public DbSet<Department> Departments { get; set; }

        /// <summary>
        /// 数据字典
        /// </summary>
        public DbSet<DataDictionary> DataDictionarys { get; set; }

        #endregion

        #region Blog 个人博客

        /// <summary>
        /// 博客文章
        /// </summary>
        public DbSet<Article> Articles { get; set; }

        #endregion

        /// <summary>
        /// 模型创建时构建方法
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasQueryFilter();
            modelBuilder.EntityMappings();

            //映射对应实体配置Fluent Api 
            //modelBuilder.ExecuteConfigurations("");
            //base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfigurationsFromAssembly()
        }
    }
}