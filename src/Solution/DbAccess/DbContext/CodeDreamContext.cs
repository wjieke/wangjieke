using DbAccess.Extensions;
using Microsoft.EntityFrameworkCore;
using Model.Entity.System;

namespace DbAccess.DbContext
{
    public class CodeDreamContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public CodeDreamContext()
        {

        }
        public CodeDreamContext(DbContextOptions<CodeDreamContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Department> Departments { get; set; }

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