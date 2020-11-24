using IServices.IBaseServices;
using Model.ModelSearch;
using Model.Entity.System;
using System.Threading.Tasks;
using Model.ModelTool;

namespace IServices.ISystemServices
{
    public interface ICompanyService : IBaseService<Company>
    {
        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="searchCompany">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        QueryResultInfo<Company> GetPage(CompanySearch companySearch);

        Task<QueryResultInfo<Company>> GetPageAsync(CompanySearch companySearch);
    }
}