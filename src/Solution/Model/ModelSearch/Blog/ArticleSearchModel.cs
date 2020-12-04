using System;
using System.Collections.Generic;
using System.Text;
using static Model.Enum.BlogEnum;

namespace Model.ModelSearch.Blog
{
    /// <summary>
    /// 博客文章搜索模型类
    /// </summary>
    public class ArticleSearchModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        public ArticleType ArticleType { get; set; }
    }
}
