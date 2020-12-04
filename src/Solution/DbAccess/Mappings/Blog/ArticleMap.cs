using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entity.Blog;

namespace DbAccess.Mappings.Blog
{
    /// <summary>
    /// 博客文章映射类
    /// </summary>
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Blog_Article");
            builder.HasKey(t => t.Id);
        }
    }
}
