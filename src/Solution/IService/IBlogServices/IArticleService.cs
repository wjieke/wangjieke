using IServices.IBaseServices;
using Model.Entity.Blog;
using Model.ModelSearch.Blog;
using Model.ModelTool;
using System;
using System.Collections.Generic;
using System.Text;

namespace IService.IBlogServices
{
    /// <summary>
    /// 博客文章服务接口
    /// </summary>
    public interface IArticleService : IBaseService<Article>
    {
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="searchModel">搜索条件数据</param>
        /// <returns>查询结果信息</returns>
        QueryResultInfo<Article> GetPage(ArticleSearchModel searchModel);
    }
}
