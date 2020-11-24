using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entity.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbAccess.Mappings.System
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Sys_User");
            builder.HasKey(t => t.Id);
        }
    }
}
