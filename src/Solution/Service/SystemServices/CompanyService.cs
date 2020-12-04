using IServices.ISystemServices;
using Model.Entity.System;
using Model.ModelSearch.System;
using Model.ModelTool;
using Model.ModelView;
using Services.BaseServices;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Model.Enum.SystemEnum;

namespace Services.SystemServices
{
    public class CompanyService : BaseService<Company>, ICompanyService
    {
        #region 同步
        /// <summary>
        /// 添加公司信息
        /// </summary>
        /// <param name="area">模型类</param>
        /// <returns></returns>
        public override ActionResultInfo<Company> AddInfo(Company company)
        {
            var resultData = new ActionResultInfo<Company>() { ResultState = ResultState.Success, Message = "" };
            try
            {
                var isExists = base.Any(m => m.CompanyName == company.CompanyName);
                if (isExists)
                {
                    resultData.ResultState = ResultState.Failure;
                    resultData.Message = "名称已存在";
                }
                else
                {
                    company.CreatorId = CurrentLoginUser.Id;
                    return base.AddInfo(company);
                }
            }
            catch (Exception e)
            {
                resultData.ResultState = ResultState.Error;
                resultData.Message = e.Message;
            }
            return resultData;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public override ActionResultInfo<Company> ModInfo(Company company)
        {
            company.ModifierId = CurrentLoginUser.Id;
            company.ModifyTime = DateTime.Now;
            return base.ModInfo(company);
        }

        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="searchCompany">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        public QueryResultInfo<Company> GetPage(CompanySearchModel companySearch)
        {
            //条件查询表达式
            Expression<Func<Company, bool>> whereFun = null;
            if (!string.IsNullOrEmpty(companySearch.CompanyName)) { whereFun = m => m.CompanyName.Contains(companySearch.CompanyName); }

            //排序表达式
            Expression<Func<Company, object>> orderByFun = null;
            orderByFun = m => m.Id;

            try
            {
                return base.GetPage(companySearch.PageIndex, companySearch.PageSize, whereFun);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region 异步
        /// <summary>
        /// 添加公司信息
        /// </summary>
        /// <param name="area">模型类</param>
        /// <returns></returns>
        public override async Task<ActionResultInfo<Company>> AddInfoAsync(Company company)
        {
            var resultData = new ActionResultInfo<Company>() { ResultState = ResultState.Success, Message = "" };
            try
            {
                var isExists = await base.AnyAsync(m => m.CompanyName == company.CompanyName);
                if (isExists)
                {
                    resultData.ResultState = ResultState.Failure;
                    resultData.Message = "名称已存在";
                }
                else
                {
                    company.CreatorId = CurrentLoginUser.Id;
                    return base.AddInfo(company);
                }
            }
            catch (Exception e)
            {
                resultData.ResultState = ResultState.Error;
                resultData.Message = e.Message;
            }
            return resultData;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public override async Task<ActionResultInfo<Company>> ModInfoAsync(Company company)
        {
            company.ModifierId = CurrentLoginUser.Id;
            company.ModifyTime = DateTime.Now;
            return await base.ModInfoAsync(company);
        }

        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="companySearch">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        public async Task<QueryResultInfo<Company>> GetPageAsync(CompanySearchModel companySearch)
        {
            //条件查询表达式
            Expression<Func<Company, bool>> whereFun = null;
            if (!string.IsNullOrEmpty(companySearch.CompanyName)) { whereFun = m => m.CompanyName.Contains(companySearch.CompanyName); }

            //排序表达式
            Expression<Func<Company, object>> orderByFun = null;
            orderByFun = m => m.Id;

            try
            {
                return await base.GetPageAsync(companySearch.PageIndex, companySearch.PageSize, whereFun);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}