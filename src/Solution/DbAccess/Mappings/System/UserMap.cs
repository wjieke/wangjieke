using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entity.System;

namespace DbAccess.Mappings.System
{
    /// <summary>
    /// 用户映射类
    /// </summary>
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Sys_User");
            builder.HasKey(t => t.Id);
        }
    }
}
