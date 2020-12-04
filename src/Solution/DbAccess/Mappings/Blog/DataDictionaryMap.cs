using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entity.System;

namespace DbAccess.Mappings.Blog
{
    /// <summary>
    /// 数据字典映射类
    /// </summary>
    public class DataDictionaryMap: IEntityTypeConfiguration<DataDictionary>
    {
        public void Configure(EntityTypeBuilder<DataDictionary> builder)
        {
            builder.ToTable("Sys_DataDictionary");
            builder.HasKey(t => t.Id);
        }
    }
}
