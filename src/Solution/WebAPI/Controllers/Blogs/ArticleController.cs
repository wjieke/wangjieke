using IService.IBlogServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entity.Blog;
using Model.ModelSearch.Blog;
using Model.ModelTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers.Bases;

namespace WebAPI.Controllers.Blogs
{
    public class ArticleController : BaseController<Article, IArticleService>
    {
        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="searchModel">搜索条件数据</param>
        /// <returns>查询结果信息</returns>
        [HttpPost("GetPage")]
        public QueryResultInfo<Article> GetPage(ArticleSearchModel searchModel)
        {
            return Service.GetPage(searchModel);
        }
    }
}
