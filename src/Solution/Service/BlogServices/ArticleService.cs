using IService.IBlogServices;
using Model.Entity.Blog;
using Model.ModelSearch.Blog;
using Model.ModelTool;
using Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Service.BlogServices
{
    /// <summary>
    /// 博客文章服务类
    /// </summary>
    public class ArticleService : BaseService<Article>, IArticleService
    {
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="searchModel">搜索条件数据</param>
        /// <returns>查询结果信息</returns>
        public QueryResultInfo<Article> GetPage(ArticleSearchModel searchModel)
        {
            //条件查询表达式
            Expression<Func<Article, bool>> whereFun = null;
            if (!string.IsNullOrEmpty(searchModel.Title))
            {
                whereFun = m => m.Title.Contains(searchModel.Title);
            }
            //排序表达式
            Expression<Func<Article, object>> orderByFun = null;
            orderByFun = m => m.Id;
            try
            {
                return base.GetPage(searchModel.PageIndex, searchModel.PageSize, whereFun, orderByFun, true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
