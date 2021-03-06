﻿using Model.ModelBase;
using static Model.Enum.BlogEnum;

namespace Model.Entity.Blog
{
    /// <summary>
    /// 文章
    /// </summary>
    public class Article: SystemBase
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        public ArticleType ArticleType { get; set; }
    }
}
