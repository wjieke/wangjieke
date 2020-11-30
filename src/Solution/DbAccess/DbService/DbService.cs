using DbAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Model.Enum.SystemEnum;

namespace DbAccess.DbService
{
    /// <summary>
    /// 数据上下文服务类
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class DbService<TModel> : IDbService<TModel> where TModel : class
    {
        public CodeDreamContext Context { get; set; }

        #region 同步

        #region 增删改

        /// <summary>
        /// 增删改(同步)
        /// </summary>
        /// <param name="model">实体模型</param>
        /// <param name="actionType">增删改动作类型</param>
        /// <returns>返回执行结果，数据库执行受影响行数</returns>
        public virtual int DbExecuteAction(TModel model, DbActionType actionType)
        {
            switch (actionType)
            {
                case DbActionType.Add:
                    Context.Entry(model).State = EntityState.Added;
                    break;
                case DbActionType.Del:
                    Context.Entry(model).State = EntityState.Deleted;
                    break;
                case DbActionType.Mod:
                    Context.Entry(model).State = EntityState.Modified;
                    break;
                default:
                    break;
            }
            return Context.SaveChanges();
        }

        #endregion

        #region 查询

        /// <summary>
        /// 根据条件验证数据是否存在
        /// </summary>
        /// <param name="whereFun">条件表达式</param>
        /// <returns>返回bool</returns>
        public virtual bool Any(Expression<Func<TModel, bool>> whereFun = null)
        {
            return Context.Set<TModel>().Any(whereFun);
        }

        /// <summary>
        /// 根据ID查询数据对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TModel Find(int? id)
        {
            return Context.Set<TModel>().Find(id);
        }

        /// <summary>
        /// 根据条件查询数据对象
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public virtual TModel FirstOrDefault(Expression<Func<TModel, bool>> predicate = null)
        {
            return Context.Set<TModel>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 根据条件查询单个数据对象
        /// </summary>
        /// <param name="singleFun">条件表达式</param>
        /// <returns>查询的对象</returns>
        public virtual TModel SingleOrDefault(Expression<Func<TModel, bool>> singleFun = null)
        {
            return Context.Set<TModel>().SingleOrDefault(singleFun);
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="whereFun">条件表达式</param>
        /// <param name="orderByFun">排序表达式</param>
        /// <param name="isAsc">是否升序降序</param>
        /// <returns>对象集合</returns>
        public virtual List<TModel> QueryList(Expression<Func<TModel, bool>> whereFun = null, Expression<Func<TModel, object>> orderByFun = null, bool isAsc = true)
        {
            IQueryable<TModel> source = Context.Set<TModel>();
            if (whereFun != null)
            {
                source = source.Where(whereFun);
            }
            if (orderByFun != null)
            {
                source = isAsc == true ? source.OrderBy(orderByFun) : source.OrderByDescending(orderByFun);
            }
            return source.ToList();
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="count">总条数</param>
        /// <param name="countPage">总页数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="whereFun">条件表达式</param>
        /// <param name="orderByFun">排序表达式</param>
        /// <param name="isAsc">是否升序降序</param>
        /// <returns>分页数据对象集合</returns>
        public virtual List<TModel> QueryPage(out int count, out int countPage, int pageIndex = 1, int pageSize = 10,
            Expression<Func<TModel, bool>> whereFun = null,
            Expression<Func<TModel, object>> orderByFun = null,
            bool isAsc = true)
        {
            IQueryable<TModel> source = Context.Set<TModel>();
            int startIndex = pageSize * (pageIndex - 1);
            if (whereFun != null)
            {
                source = source.Where(whereFun);
            }
            if (orderByFun != null)
            {
                source = isAsc == true ? source.OrderBy(orderByFun) : source.OrderByDescending(orderByFun);
            }
            //总条数
            count = source.Count();
            //总页数
            countPage = (int)Math.Ceiling(count / (double)pageSize);
            //分页数据集
            //Skip:跳过集合的前n个元素(从第几页开始取数据)，Take：获取集合的前n个元素(取多少条数据,即只返回限定数量的结果集)
            source = source.Skip(startIndex).Take(pageSize);
            List<TModel> list = source.ToList();
            return list;
        }

        #endregion

        #endregion

        #region 异步

        #region 增删改
        /// <summary>
        /// 增删改(异步)
        /// </summary>
        /// <param name="model">实体模型</param>
        /// <param name="actionType">增删改动作类型</param>
        /// <returns>返回执行结果，数据库执行受影响行数</returns>
        public virtual async Task<int> DbExecuteActionAsync(TModel model, DbActionType actionType)
        {
            switch (actionType)
            {
                case DbActionType.Add:
                    Context.Entry(model).State = EntityState.Added;
                    break;
                case DbActionType.Del:
                    Context.Entry(model).State = EntityState.Deleted;
                    break;
                case DbActionType.Mod:
                    Context.Entry(model).State = EntityState.Modified;
                    break;
                default:
                    break;
            }
            return await Context.SaveChangesAsync();
        }

        #endregion

        #region 查询

        /// <summary>
        /// 根据条件验证数据是否存在
        /// </summary>
        /// <param name="whereFun">条件表达式</param>
        /// <returns>返回bool</returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<TModel, bool>> whereFun = null)
        {
            return await Context.Set<TModel>().AnyAsync(whereFun);
        }

        /// <summary>
        /// 根据ID查询数据对象(异步)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TModel> FindAsync(int? id)
        {
            return await Context.Set<TModel>().FindAsync(id);
        }

        /// <summary>
        /// 根据条件查询数据对象
        /// </summary>
        /// <param name="whereFun">条件表达式</param>
        /// <returns></returns>
        public virtual async Task<TModel> FirstOrDefaultAsync(Expression<Func<TModel, bool>> whereFun = null)
        {
            return await Context.Set<TModel>().FirstOrDefaultAsync(whereFun);
        }

        /// <summary>
        /// 根据条件查询单个数据对象
        /// </summary>
        /// <param name="singleFun">条件表达式</param>
        /// <returns>查询的对象</returns>
        public virtual async Task<TModel> SingleOrDefaultAsync(Expression<Func<TModel, bool>> singleFun = null)
        {
            return await Context.Set<TModel>().SingleOrDefaultAsync(singleFun);
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="whereFun">条件表达式</param>
        /// <param name="orderByFun">排序表达式</param>
        /// <param name="isAsc">是否升序降序</param>
        /// <returns>对象集合</returns>
        public virtual async Task<List<TModel>> QueryListAsync(Expression<Func<TModel, bool>> whereFun = null, Expression<Func<TModel, object>> orderByFun = null, bool isAsc = true)
        {
            IQueryable<TModel> source = Context.Set<TModel>();
            if (whereFun != null)
            {
                source = source.Where(whereFun);
            }
            if (orderByFun != null)
            {
                source = isAsc == true ? source.OrderBy(orderByFun) : source.OrderByDescending(orderByFun);
            }
            return await source.ToListAsync();
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="whereFun">条件表达式</param>
        /// <param name="orderByFun">排序表达式</param>
        /// <param name="isAsc">是否升序降序</param>
        /// <returns>元组数据</returns>
        public virtual async Task<ValueTuple<List<TModel>, int, int>> QueryPageAsync(int pageIndex = 1, int pageSize = 10,
            Expression<Func<TModel, bool>> whereFun = null,
            Expression<Func<TModel, object>> orderByFun = null,
            bool isAsc = true)
        {
            IQueryable<TModel> source = Context.Set<TModel>();
            int startIndex = pageSize * (pageIndex - 1);
            if (whereFun != null)
            {
                source = source.Where(whereFun);
            }
            if (orderByFun != null)
            {
                source = isAsc == true ? source.OrderBy(orderByFun) : source.OrderByDescending(orderByFun);
            }
            //总条数
            var count = await source.CountAsync();
            //总页数
            var countPage = (int)Math.Ceiling(count / (double)pageSize);
            //分页数据集
            //Skip:跳过集合的前n个元素(从第几页开始取数据)，Take：获取集合的前n个元素(取多少条数据,即只返回限定数量的结果集)
            source = source.Skip(startIndex).Take(pageSize);
            List<TModel> list = await source.ToListAsync();
            ValueTuple<List<TModel>, int, int> tupleData = new ValueTuple<List<TModel>, int, int>(list, count, countPage);
            return tupleData;
        }

        #endregion

        #endregion
    }
}
