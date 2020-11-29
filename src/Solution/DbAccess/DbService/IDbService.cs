using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Model.Enum.SystemEnum;

namespace DbAccess.DbService
{
    public interface IDbService<TModel> where TModel : class
    {
        #region 同步

        #region 增删改

        /// <summary>
        /// 增删改（同步）
        /// </summary>
        /// <param name="model">实体模型</param>
        /// <param name="actionType">增删改动作类型</param>
        /// <returns>返回执行结果，数据库执行受影响行数</returns>
        int DbExecuteAction(TModel model, DbActionType actionType);

        #endregion

        #region 查询

        /// <summary>
        /// 根据条件验证数据是否存在
        /// </summary>
        /// <param name="whereFun">条件表达式</param>
        /// <returns>返回bool</returns>
        bool Any(Expression<Func<TModel, bool>> whereFun = null);

        /// <summary>
        /// 根据ID查询数据对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TModel Find(int? id);

        /// <summary>
        /// 根据条件查询数据对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TModel FirstOrDefault(Expression<Func<TModel, bool>> predicate = null);

        /// <summary>
        /// 根据条件查询单个数据对象(包含对象的关联数据)
        /// </summary>
        /// <param name="includeFun">包含表达式</param>
        /// <param name="singleFun">条件表达式</param>
        /// <returns>查询的对象</returns>
        TModel SingleOrDefault(Expression<Func<TModel, List<TModel>>> includeFun = null,
            Expression<Func<TModel, bool>> singleFun = null);

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="whereFun">条件表达式</param>
        /// <param name="includeFun">包含表达式</param>
        /// <param name="orderByFun">排序表达式</param>
        /// <param name="isAsc">是否升序降序</param>
        /// <returns></returns>
        List<TModel> QueryList(Expression<Func<TModel, bool>> whereFun = null, Expression<Func<TModel, List<TModel>>> includeFun = null,
            Expression<Func<TModel, object>> orderByFun = null, bool isAsc = true);

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="count">总条数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="whereFun">条件表达式</param>
        /// <param name="includeFun">包含表达式</param>
        /// <param name="orderByFun">排序表达式</param>
        /// <param name="isAsc">是否升序降序</param>
        /// <returns>对象集合json字符串</returns>
        List<TModel> QueryPage(out int count, out int countPage, int pageIndex = 1, int pageSize = 10,
            Expression<Func<TModel, bool>> whereFun = null,
            Expression<Func<TModel, object>> includeFun = null,
            Expression<Func<TModel, object>> orderByFun = null,
            bool isAsc = true);

        #endregion

        #endregion

        #region 异步

        #region 增删改

        /// <summary>
        /// 增删改（异步）
        /// </summary>
        /// <param name="model">实体模型</param>
        /// <param name="actionType">增删改动作类型</param>
        /// <returns>返回执行结果，数据库执行受影响行数</returns>
        Task<int> DbExecuteActionAsync(TModel model, DbActionType actionType);

        #endregion

        #region 查询

        /// <summary>
        /// 根据条件验证数据是否存在
        /// </summary>
        /// <param name="whereFun">条件表达式</param>
        /// <returns>返回bool</returns>
        Task<bool> AnyAsync(Expression<Func<TModel, bool>> whereFun = null);

        /// <summary>
        /// 根据ID查询数据对象(异步)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TModel> FindAsync(int? id);

        /// <summary>
        /// 根据条件查询数据对象(不包含对象的关联数据)
        /// </summary>
        /// <param name="whereFun"></param>
        /// <returns></returns>
        Task<TModel> FirstOrDefaultAsync(Expression<Func<TModel, bool>> whereFun = null);

        /// <summary>
        /// 根据条件查询单个数据对象(包含对象的关联数据)
        /// </summary>
        /// <param name="includeFun">包含表达式</param>
        /// <param name="singleFun">条件表达式</param>
        /// <returns>查询的对象</returns>
        Task<TModel> SingleOrDefaultAsync(Expression<Func<TModel, List<TModel>>> includeFun = null,
            Expression<Func<TModel, bool>> singleFun = null);

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="whereFun">条件表达式</param>
        /// <param name="includeFun">包含表达式</param>
        /// <param name="orderByFun">排序表达式</param>
        /// <param name="isAsc">是否升序降序</param>
        /// <returns></returns>
        Task<List<TModel>> QueryListAsync(Expression<Func<TModel, bool>> whereFun = null, Expression<Func<TModel, List<TModel>>> includeFun = null,
            Expression<Func<TModel, object>> orderByFun = null, bool isAsc = true);

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="whereFun">条件表达式</param>
        /// <param name="orderByFun">排序表达式</param>
        /// <param name="isAsc">是否升序降序</param>
        /// <returns>返回json字符串</returns>
        Task<ValueTuple<List<TModel>, int, int>> QueryPageAsync(int pageIndex = 1, int pageSize = 10,
            Expression<Func<TModel, bool>> whereFun = null,
            Expression<Func<TModel, object>> includeFun = null,
            Expression<Func<TModel, object>> orderByFun = null,
            bool isAsc = true);

        #endregion

        #endregion
    }
}
