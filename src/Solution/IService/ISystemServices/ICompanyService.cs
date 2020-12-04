using IServices.IBaseServices;
using Model.ModelSearch.System;
using Model.Entity.System;
using System.Threading.Tasks;
using Model.ModelTool;
using Model.ModelView;

namespace IServices.ISystemServices
{
    /// <summary>
    /// 公司服务接口
    /// </summary>
    public interface ICompanyService : IBaseService<Company>
    {
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="searchModel">搜索条件数据</param>
        /// <returns>查询结果信息</returns>
        QueryResultInfo<Company> GetPage(CompanySearchModel searchModel);

        /// <summary>
        /// 查询分页数据（异步）
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        Task<QueryResultInfo<Company>> GetPageAsync(CompanySearchModel searchModel);
    }
}