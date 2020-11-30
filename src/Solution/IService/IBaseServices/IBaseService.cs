using Autofac.Extras.DynamicProxy;
using DbAccess.DbService;
using Library.Interceptor;
using Model.ModelTool;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IServices.IBaseServices
{

    /// <summary>
    /// 数据传输服务基类接口
    /// </summary>
    /// <typeparam name="TModel">泛型模型类</typeparam>
    [Intercept(typeof(AutofacServiceInterceptor))] //启用服务拦截器（继承该基类的所有服务类都启用拦截器）
    public interface IBaseService<TModel> : IDbService<TModel> where TModel : class
    {
        #region 同步

        #region 增删改

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="model">模型类</param>
        /// <returns>返回增加的数据实体类</returns>
        ActionResultInfo<TModel> AddInfo(TModel model);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">删除数据的id数组</param>
        /// <param name="tableName">要删除数据的表名</param>
        /// <returns></returns>
        ActionResultInfo<TModel> DelInfo(int[] ids);

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="model">模型类</param>
        /// <returns>返回修改后的数据实体类</returns>
        ActionResultInfo<TModel> ModInfo(TModel model);

        #endregion

        #region 查询

        /// <summary>
        /// 根据ID查询信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        QueryResultInfo<TModel> GetInfo(int id);

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <param name="whereFun"></param>
        /// <param name="orderByFun"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        QueryResultInfo<TModel> GetList(Expression<Func<TModel, bool>> whereFun = null, Expression<Func<TModel, object>> orderByFun = null, bool isAsc = true);

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="whereFun">条件表达式</param>
        /// <param name="orderByFun">排序表达式</param>
        /// <param name="isAsc">是否升序降序</param>
        /// <returns>对象集合json字符串</returns>
        QueryResultInfo<TModel> GetPage(int pageIndex = 1, int pageSize = 10,
            Expression<Func<TModel, bool>> whereFun = null,
            Expression<Func<TModel, object>> orderByFun = null,
            bool isAsc = true);

        #endregion

        #endregion

        #region 异步

        #region 增删改

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="model">模型类</param>
        /// <returns>返回增加的数据实体类</returns>
        Task<ActionResultInfo<TModel>> AddInfoAsync(TModel model);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">删除数据的id数组</param>
        /// <param name="tableName">要删除数据的表名</param>
        /// <returns></returns>
        Task<ActionResultInfo<TModel>> DelInfoAsync(int[] ids);

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="model">模型类</param>
        /// <returns>返回修改后的数据实体类</returns>
        Task<ActionResultInfo<TModel>> ModInfoAsync(TModel model);


        #endregion

        #region 查询

        /// <summary>
        /// 根据ID查询信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<QueryResultInfo<TModel>> GetInfoAsync(int id);

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <param name="whereFun"></param>
        /// <param name="orderByFun"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        Task<QueryResultInfo<TModel>> GetListAsync(Expression<Func<TModel, bool>> whereFun = null, Expression<Func<TModel, object>> orderByFun = null, bool isAsc = true);

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="whereFun">条件表达式</param>
        /// <param name="orderByFun">排序表达式</param>
        /// <param name="isAsc">是否升序降序</param>
        /// <returns>查询结果信息</returns>
        Task<QueryResultInfo<TModel>> GetPageAsync(int pageIndex = 1, int pageSize = 10,
            Expression<Func<TModel, bool>> whereFun = null,
            Expression<Func<TModel, object>> orderByFun = null,
            bool isAsc = true);

        #endregion

        #endregion
    }
}