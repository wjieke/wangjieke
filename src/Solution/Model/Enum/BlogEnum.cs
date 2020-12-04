using System;
using System.ComponentModel;

namespace Model.Enum
{
    public class BlogEnum
    {
        /// <summary>
        /// 博客文章类型枚举
        /// </summary>
        [Flags]
        public enum ArticleType
        {
            /// <summary>
            /// 随心日记
            /// </summary>
            [Description("随心日记")]
            Diary = 1,

            /// <summary>
            /// 技术文章
            /// </summary>
            [Description("技术文章")]
            Technology = 2,
        }
    }
}
