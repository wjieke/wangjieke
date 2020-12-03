using IServices.ISystemServices;
using Microsoft.AspNetCore.Mvc;
using Model.ModelSearch;
using Model.Entity.System;
using System.Threading.Tasks;
using WebAPI.Controllers.Bases;
using Model.ModelTool;
using Model.ModelView;

namespace WebAPI.Controllers.Systems
{
    /// <summary>
    /// 公司API控制器
    /// </summary>
    public class CompanyController : BaseController<Company, ICompanyService>
    {
        public ICompanyService CompanyService { get; set; }

        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="o">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        [HttpPost("GetPage")]
        public QueryResultInfo<Company> GetPage(CompanySearchModel o)
        {
            return CompanyService.GetPage(o);
        }
    }
}