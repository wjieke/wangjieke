using IService.IBlogServices;
using Model.Entity.Blog;
using Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.BlogServices
{
    public class ArticleService : BaseService<Article>, IArticleService
    {

    }
}
