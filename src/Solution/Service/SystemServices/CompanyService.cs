﻿using IServices.ISystemServices;
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
    /// <summary>
    /// 公司服务类
    /// </summary>
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
        /// 查询分页数据
        /// </summary>
        /// <param name="searchModel">搜索条件数据</param>
        /// <returns>查询结果信息</returns>
        public QueryResultInfo<Company> GetPage(CompanySearchModel searchModel)
        {
            //条件查询表达式
            Expression<Func<Company, bool>> whereFun = null;
            if (!string.IsNullOrEmpty(searchModel.CompanyName)) { whereFun = m => m.CompanyName.Contains(searchModel.CompanyName); }

            //排序表达式
            Expression<Func<Company, object>> orderByFun = null;
            orderByFun = m => m.Id;

            try
            {
                return base.GetPage(searchModel.PageIndex, searchModel.PageSize, whereFun);
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
        /// 查询分页数据
        /// </summary>
        /// <param name="searchModel">搜索条件数据</param>
        /// <returns>查询结果信息</returns>
        public async Task<QueryResultInfo<Company>> GetPageAsync(CompanySearchModel searchModel)
        {
            //条件查询表达式
            Expression<Func<Company, bool>> whereFun = null;
            if (!string.IsNullOrEmpty(searchModel.CompanyName)) { whereFun = m => m.CompanyName.Contains(searchModel.CompanyName); }

            //排序表达式
            Expression<Func<Company, object>> orderByFun = null;
            orderByFun = m => m.Id;

            try
            {
                return await base.GetPageAsync(searchModel.PageIndex, searchModel.PageSize, whereFun);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}