using DbAccess.DbService;
using IServices.IBaseServices;
using Library.Tool;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.Entity.System;
using Model.Enum;
using Model.ModelTool;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Services.BaseServices
{
    /// <summary>
    /// 数据传输服务基类
    /// </summary>
    /// <typeparam name="TModel">泛型模型类</typeparam>
    public abstract class BaseService<TModel> : DbService<TModel>, IBaseService<TModel> where TModel : class
    {
        /// <summary>
        /// HTTP上下文访问器
        /// </summary>
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        public User CurrentUser
        {
            get
            {
                try
                {
                    //return HttpContextAccessor.HttpContext.Session.Get("CurrentUser").ToClass<User>();

                    var jso = new JsonSerializerOptions()
                    {
                        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                        ReadCommentHandling = JsonCommentHandling.Skip,//跳过自我循环引用
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase//默认大写，配置驼峰命名
                    };
                    byte[] byteUser = HttpContextAccessor.HttpContext.Session.Get("CurrentUser");
                    string strUser = System.Text.Encoding.Default.GetString(byteUser);
                    User user = JsonSerializer.Deserialize<User>(strUser, jso);
                    return user;
                }
                catch (Exception ex)
                {
                    throw new Exception($"错误信息：{ex.Message}，请重新登录。");
                }
            }
        }

        #region 同步

        #region 增删改

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="model">模型类</param>
        /// <returns>返回增加的数据实体类</returns>
        public virtual ActionResultInfo<TModel> AddInfo(TModel model)
        {
            var resultInfo = new ActionResultInfo<TModel>() { ResultState = ResultState.Success, Message = "" };
            try
            {
                int ret = base.DbExecuteAction(model,DbActionType.Add);
                if (ret > 0)
                {
                    resultInfo.ResultState = ResultState.Success;
                    resultInfo.Message = "成功";
                    resultInfo.Data = model;
                }
                else
                {
                    resultInfo.ResultState = ResultState.Failure;
                    resultInfo.Message = "失败";
                }
            }
            catch (Exception ex)
            {
                resultInfo.ResultState = ResultState.Error;
                resultInfo.Message = "错误：" + ex.Message;
            }
            return resultInfo;
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="model">模型类</param>
        /// <returns>返回修改后的数据实体类</returns>
        public virtual ActionResultInfo<TModel> ModInfo(TModel model)
        {
            var resultInfo = new ActionResultInfo<TModel>() { ResultState = ResultState.Success, Message = "" };
            try
            {
                int ret = base.DbExecuteAction(model, DbActionType.Mod);
                if (ret > 0)
                {
                    resultInfo.ResultState = ResultState.Success;
                    resultInfo.Message = "成功";
                    resultInfo.Data = model;
                }
                else
                {
                    resultInfo.ResultState = ResultState.Failure;
                    resultInfo.Message = "失败";
                }
            }
            catch (Exception ex)
            {
                resultInfo.ResultState = ResultState.Error;
                resultInfo.Message = "错误：" + ex.Message;
            }
            return resultInfo;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">删除数据的id数组</param>
        /// <param name="tableName">要删除数据的表名</param>
        /// <returns></returns>
        public virtual ActionResultInfo<TModel> DelInfo(int[] ids)
        {
            string tableName = string.Empty;
            var resultInfo = new ActionResultInfo<TModel>() { ResultState = ResultState.Success, Message = "" };
            try
            {
                Type objType = typeof(TModel);
                var objs = objType.GetCustomAttributes(true);
                foreach (var item in objs)
                {
                    if (item is System.ComponentModel.DataAnnotations.Schema.TableAttribute obj)
                    {
                        tableName = obj.Name;
                    }
                }
                string sql = $"update [dbo].[{tableName}] set DataState={(int)DataState.Deleted} where ID in({string.Join(",", ids)})";
                var paramsObjectArray = new[] {
                    new SqlParameter("DataState", (int)DataState.Deleted),
                    new SqlParameter("ID", string.Join(",", ids))
                };
                var ret = Context.Database.ExecuteSqlRaw(sql, paramsObjectArray);
                if (ret > 0)
                {
                    resultInfo.ResultState = ResultState.Success;
                    resultInfo.Message = "成功";
                }
                else
                {
                    resultInfo.ResultState = ResultState.Failure;
                    resultInfo.Message = "失败";
                }
            }
            catch (Exception ex)
            {
                resultInfo.ResultState = ResultState.Error;
                resultInfo.Message = "错误：" + ex.Message;
            }
            return resultInfo;
        }

        #endregion

        #region 查询

        /// <summary>
        /// 根据ID查询信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual QueryResultInfo<TModel> GetInfo(int id)
        {
            QueryResultInfo<TModel> resultInfo = new QueryResultInfo<TModel>() { ResultState = ResultState.Success, Message = "" };
            try
            {
                var model = base.Find(id);
                if (model != null)
                {
                    resultInfo.ResultState = ResultState.Success;
                    resultInfo.Message = "成功";
                    resultInfo.Data = model;
                }
                else
                {
                    resultInfo.ResultState = ResultState.Failure;
                    resultInfo.Message = "失败";
                }
            }
            catch (Exception ex)
            {
                resultInfo.ResultState = ResultState.Error;
                resultInfo.Message = "错误" + ex.Message;
            }
            return resultInfo;
        }

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <param name="whereFun"></param>
        /// <param name="includeFun"></param>
        /// <param name="orderByFun"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public virtual QueryResultInfo<TModel> GetList(Expression<Func<TModel, bool>> whereFun = null, Expression<Func<TModel, List<TModel>>> includeFun = null,
            Expression<Func<TModel, object>> orderByFun = null, bool isAsc = true)
        {
            List<TModel> list = base.QueryList(whereFun, includeFun, orderByFun, isAsc);
            QueryResultInfo<TModel> queryResultInfo = new QueryResultInfo<TModel>();
            if (list.Count > 0)
            {
                queryResultInfo.Datas = list;
                queryResultInfo.ResultState = ResultState.Success;
                queryResultInfo.Message = "成功";
            }
            else
            {
                queryResultInfo.ResultState = ResultState.Failure;
                queryResultInfo.Message = "无记录";
            }
            return queryResultInfo;
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="whereFun">条件表达式</param>
        /// <param name="includeFun">包含表达式</param>
        /// <param name="orderByFun">排序表达式</param>
        /// <param name="isAsc">是否升序降序</param>
        /// <returns>对象集合json字符串</returns>
        public virtual QueryResultInfo<TModel> GetPage(int pageIndex = 1, int pageSize = 10,
            Expression<Func<TModel, bool>> whereFun = null,
            Expression<Func<TModel, object>> includeFun = null,
            Expression<Func<TModel, object>> orderByFun = null,
            bool isAsc = true)
        {
            List<TModel> list = base.QueryPage(out int count, out int countPage, pageIndex, pageSize, whereFun, includeFun, orderByFun);
            QueryResultInfo<TModel> queryResultInfo = new QueryResultInfo<TModel>();
            if (list.Count > 0)
            {
                queryResultInfo.Datas = list;                       //数据集
                queryResultInfo.Total = count;                      //总条数
                queryResultInfo.TotalPage = countPage;              //总页数
                queryResultInfo.ResultState = ResultState.Success;
                queryResultInfo.Message = "查询成功";
            }
            else
            {
                queryResultInfo.ResultState = ResultState.Failure;
                queryResultInfo.Message = "查询无记录";
            }
            return queryResultInfo;
        }

        #endregion

        #endregion

        #region 异步

        #region 增删改

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="model">模型类</param>
        /// <returns>返回增加的数据实体类</returns>
        public virtual async Task<ActionResultInfo<TModel>> AddInfoAsync(TModel model)
        {
            ActionResultInfo<TModel> resultInfo = new ActionResultInfo<TModel>() { ResultState = ResultState.Success, Message = "" };
            try
            {
                int ret = await base.DbExecuteActionAsync(model, DbActionType.Add);
                if (ret > 0)
                {
                    resultInfo.ResultState = ResultState.Success;
                    resultInfo.Message = "成功";
                    resultInfo.Data = model;
                }
                else
                {
                    resultInfo.ResultState = ResultState.Failure;
                    resultInfo.Message = "失败";
                }
            }
            catch (Exception ex)
            {
                resultInfo.ResultState = ResultState.Error;
                resultInfo.Message = "错误：" + ex.Message;
            }
            return resultInfo;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">删除数据的id数组</param>
        /// <param name="tableName">要删除数据的表名</param>
        /// <returns></returns>
        public virtual async Task<ActionResultInfo<TModel>> DelInfoAsync(int[] ids)
        {
            string tableName = string.Empty;
            ActionResultInfo<TModel> resultInfo = new ActionResultInfo<TModel>() { ResultState = ResultState.Success, Message = "" };
            try
            {
                Type objType = typeof(TModel);
                var objs = objType.GetCustomAttributes(true);
                foreach (var item in objs)
                {
                    if (item is System.ComponentModel.DataAnnotations.Schema.TableAttribute obj)
                    {
                        tableName = obj.Name;
                    }
                }
                string sql = $"update [dbo].[{tableName}] set DataState={(int)DataState.Deleted} where ID in({string.Join(",", ids)})";
                var paramsObjectArray = new[] {
                    new SqlParameter("DataState", (int)DataState.Deleted),
                    new SqlParameter("ID", string.Join(",", ids))
                };
                var ret = await Context.Database.ExecuteSqlRawAsync(sql, paramsObjectArray);
                if (ret > 0)
                {
                    resultInfo.ResultState = ResultState.Success;
                    resultInfo.Message = "成功";
                }
                else
                {
                    resultInfo.ResultState = ResultState.Failure;
                    resultInfo.Message = "失败";
                }
            }
            catch (Exception ex)
            {
                resultInfo.ResultState = ResultState.Error;
                resultInfo.Message = "错误：" + ex.Message;
            }
            return resultInfo;
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="model">模型类</param>
        /// <returns>返回修改后的数据实体类</returns>
        public virtual async Task<ActionResultInfo<TModel>> ModInfoAsync(TModel model)
        {
            ActionResultInfo<TModel> resultInfo = new ActionResultInfo<TModel>() { ResultState = ResultState.Success, Message = "" };
            try
            {
                int ret = await base.DbExecuteActionAsync(model, DbActionType.Mod);
                if (ret > 0)
                {
                    resultInfo.ResultState = ResultState.Success;
                    resultInfo.Message = "成功";
                    resultInfo.Data = model;
                }
                else
                {
                    resultInfo.ResultState = ResultState.Failure;
                    resultInfo.Message = "失败";
                }
            }
            catch (Exception ex)
            {
                resultInfo.ResultState = ResultState.Error;
                resultInfo.Message = "错误：" + ex.Message;
            }
            return resultInfo;
        }

        #endregion

        #region 查询

        /// <summary>
        /// 根据ID查询信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<QueryResultInfo<TModel>> GetInfoAsync(int id)
        {
            QueryResultInfo<TModel> resultInfo = new QueryResultInfo<TModel>() { ResultState = ResultState.Success, Message = "" };
            try
            {
                var model = await base.FindAsync(id);
                if (model != null)
                {
                    resultInfo.ResultState = ResultState.Success;
                    resultInfo.Message = "成功";
                    resultInfo.Data = model;
                }
                else
                {
                    resultInfo.ResultState = ResultState.Failure;
                    resultInfo.Message = "失败";
                }
            }
            catch (Exception ex)
            {
                resultInfo.ResultState = ResultState.Error;
                resultInfo.Message = "错误" + ex.Message;
            }
            return resultInfo;
        }

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <param name="whereFun"></param>
        /// <param name="includeFun"></param>
        /// <param name="orderByFun"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public virtual async Task<QueryResultInfo<TModel>> GetListAsync(Expression<Func<TModel, bool>> whereFun = null, Expression<Func<TModel, List<TModel>>> includeFun = null,
            Expression<Func<TModel, object>> orderByFun = null, bool isAsc = true)
        {
            List<TModel> list = await base.QueryListAsync(whereFun, includeFun, orderByFun, isAsc);
            QueryResultInfo<TModel> queryResultInfo = new QueryResultInfo<TModel>();
            if (list.Count > 0)
            {
                queryResultInfo.Datas = list;
                queryResultInfo.ResultState = ResultState.Success;
                queryResultInfo.Message = "成功";
            }
            else
            {
                queryResultInfo.ResultState = ResultState.Failure;
                queryResultInfo.Message = "无记录";
            }
            return queryResultInfo;
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="whereFun">条件表达式</param>
        /// <param name="includeFun">包含表达式</param>
        /// <param name="orderByFun">排序表达式</param>
        /// <param name="isAsc">是否升序降序</param>
        /// <returns>对象集合json字符串</returns>
        public virtual async Task<QueryResultInfo<TModel>> GetPageAsync(int pageIndex = 1, int pageSize = 10,
            Expression<Func<TModel, bool>> whereFun = null,
            Expression<Func<TModel, object>> includeFun = null,
            Expression<Func<TModel, object>> orderByFun = null,
            bool isAsc = true)
        {
            ValueTuple<List<TModel>, int, int> tupleData = await base.QueryPageAsync(pageIndex, pageSize, whereFun, includeFun, orderByFun, isAsc);
            QueryResultInfo<TModel> queryResultInfo = new QueryResultInfo<TModel>();
            if (tupleData.Item1.Count > 0)
            {
                queryResultInfo.Datas = tupleData.Item1;                  //数据集
                queryResultInfo.Total = tupleData.Item2;                  //总条数
                queryResultInfo.TotalPage = tupleData.Item3;              //总页数
                queryResultInfo.ResultState = ResultState.Success;
                queryResultInfo.Message = "查询成功";
            }
            else
            {
                queryResultInfo.ResultState = ResultState.Failure;
                queryResultInfo.Message = "查询失败";
            }
            return queryResultInfo;
        }

        #endregion

        #endregion

    }
}