using IServices.ISystemServices;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.Entity.System;
using Model.ModelSearch.System;
using Model.ModelTool;
using Model.ModelView;
using Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Model.Enum.SystemEnum;

namespace Services.SystemServices
{
    public class PermissionService : BaseService<Permission>, IPermissionService
    {
        #region 同步

        #region 增删改

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="permission">地区类</param>
        /// <returns></returns>
        public override ActionResultInfo<Permission> AddInfo(Permission permission)
        {
            var resultData = new ActionResultInfo<Permission>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };

            bool isAdopt = true;
            try
            {
                if (permission.ParentId == null)
                {
                    var isExists = base.Any(m => m.PermissionName == permission.PermissionName);
                    if (isExists) { isAdopt = false; }
                }
                else
                {
                    var objList = base.GetList(m => m.ParentId == permission.ParentId).Datas;
                    if (objList.Count > 0)
                    {
                        if (objList.Where(m => m.PermissionName.Contains(permission.PermissionName)).ToList().Count > 0) { isAdopt = false; }
                    }
                }

                if (isAdopt)
                {
                    Expression<Func<Permission, bool>> whereFun = p => p.PermissionId == permission.ParentId;
                    permission.Degree = permission.ParentId == null ? 1 : Context.Set<Permission>().FirstOrDefault(whereFun).Degree + 1;
                    permission.CreatorId = CurrentLoginUser.Id;
                    int ret = base.DbExecuteAction(permission, DbActionType.Add);
                    if (ret > 0)
                    {
                        resultData.ResultState = ResultState.Success;
                        resultData.Message = "成功";
                        resultData.Data = permission;
                    }
                    else
                    {
                        resultData.ResultState = ResultState.Failure;
                        resultData.Message = "失败";
                    }
                }
                else
                {
                    resultData.ResultState = ResultState.Failure;
                    resultData.Message = "同级节点下不允许有相同地区名";
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
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResultInfo<Permission> DelInfo(string[] ids)
        {
            var resultInfo = new ActionResultInfo<Permission>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };
            try
            {
                if (ids.Length > 0)
                {
                    ids = ids.Select(item => item.Replace(item, $"'{item}'")).ToArray();
                    //string sql = string.Format("update [dbo].[Sys_Permission] set DataState={0} where PermissionID in({1})", (int)DataState.Deleted, string.Join(",", ids));
                    string sql = $"update [dbo].[Sys_Permission] set DataState={(int)DataState.Deleted} where PermissionID in({string.Join(",", ids)})";
                    var paramsObjectArray = new[] {
                        new SqlParameter("DataState", (int)DataState.Deleted),
                        new SqlParameter("PermissionID", string.Join(",", ids))
                    };
                    var ret = Context.Database.ExecuteSqlRaw(sql, paramsObjectArray);
                    if (ret > 0)
                    {
                        #region 递归删除级联子项

                        //foreach (var id in ids)
                        //{
                        //    var objs = await ListAll(o => o.ParentId == id);
                        //    if (objs.Count() > 0)
                        //    {
                        //        int[] oids = objs.Select(o => o.ID).ToArray();
                        //        await DelData(oids);
                        //    }
                        //}

                        #endregion 递归删除级联子项

                        resultInfo.ResultState = ResultState.Success;
                        resultInfo.Message = "成功";
                    }
                    else
                    {
                        resultInfo.ResultState = ResultState.Failure;
                        resultInfo.Message = "失败";
                    }
                }
                else
                {
                    resultInfo.ResultState = ResultState.Error;
                    resultInfo.Message = "参数错误，不能为空";
                }
            }
            catch (Exception e)
            {
                resultInfo.ResultState = ResultState.Error;
                resultInfo.Message = e.Message;
            }
            return resultInfo;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="permission">地区类</param>
        /// <returns></returns>
        public override ActionResultInfo<Permission> ModInfo(Permission permission)
        {
            var resultInfo = new ActionResultInfo<Permission>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };
            try
            {
                var ret = base.DbExecuteAction(permission, DbActionType.Mod);
                if (ret > 0)
                {
                    resultInfo.ResultState = ResultState.Success;
                    resultInfo.Message = "成功";
                }
                else
                {
                    resultInfo.ResultState = ResultState.Failure;
                    resultInfo.Message = "成功";
                }
            }
            catch (Exception e)
            {
                resultInfo.ResultState = ResultState.Error;
                resultInfo.Message = e.Message;
            }
            return resultInfo;
        }

        #endregion

        #region 查询

        /// <summary>
        /// 查询 [根据ID查询单条数据信息]
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public QueryResultInfo<Permission> GetInfo(string permissionId)
        {
            var resultInfo = new QueryResultInfo<Permission>() { ResultState = ResultState.Success, Message = "" };
            try
            {
                var model = base.SingleOrDefault(m => m.PermissionId == permissionId);
                if (model != null)
                {
                    var idList = GetIds(permissionId);
                    model.Ids = idList;
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
        /// 获取对象父id集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<string> GetIds(string id = null)
        {
            List<string> idList = new List<string>();
            var obj = Context.Permissions.Find(id);
            if (obj.ParentId == null)
            {
                idList.Add(obj.PermissionId);
            }
            else
            {
                var ids = GetIds(obj.ParentId);
                foreach (var item in ids)
                {
                    idList.Add(item);
                }
                idList.Add(obj.PermissionId);
            }
            return idList;
        }

        /// <summary>
        /// 获取对象路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Permission> GetPath(string id = null)
        {
            List<Permission> objList = new List<Permission>();
            var obj = Context.Permissions.Find(id);
            if (obj == null)
            {
                objList.Add(new Permission
                {
                    PermissionId = null,
                    PermissionName = "权限"
                });
            }
            else
            {
                if (obj.ParentId == null)
                {
                    objList.Add(new Permission
                    {
                        PermissionId = null,
                        PermissionName = "权限"
                    });
                }
                else
                {
                    var list = GetPath(obj.ParentId);
                    foreach (var model in list)
                    {
                        objList.Add(model);
                    }
                }
                objList.Add(obj);
            }
            return objList;
        }

        /// <summary>
        /// 查询 [分页数据]
        /// </summary>
        /// <param name="o">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        public QueryResultInfo<Permission> GetPage(PermissionSearchModel o)
        {
            //条件查询表达式
            Expression<Func<Permission, bool>> whereFun = null;
            //if (!string.IsNullOrEmpty(o.ParentId)) { whereFun = m => m.ParentId == o.ParentId; }
            whereFun = m => m.ParentId.Equals(o.ParentId);
            if (!string.IsNullOrEmpty(o.PermissionName)) { whereFun = m => m.PermissionName.Contains(o.PermissionName); }

            //排序表达式
            Expression<Func<Permission, object>> orderByFun = null;
            //orderByFun = m => m.Sort;
            try
            {
                return base.GetPage(o.PageIndex, o.PageSize, whereFun, orderByFun, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询根节点列表（包含根节点下的所有子节点）
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<Permission> GetNode(string parentId = null)
        {
            var source = from m in Context.Permissions select m;
            var list = source.ToList();
            var newList = list.Where(m => m.ParentId == parentId).ToList();
            return newList;
        }

        /// <summary>
        /// 获取树数据
        /// </summary>
        /// <returns></returns>
        public List<Permission> GetTree()
        {
            List<Permission> list = new List<Permission>()
            {
                new Permission{
                    PermissionId=null,
                    PermissionName="权限管理",
                    Children= GetNode()
                }
            };
            return list;
        }

        #endregion

        #endregion

        #region 异步

        #region 增删改

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="permission">地区类</param>
        /// <returns></returns>
        public override async Task<ActionResultInfo<Permission>> AddInfoAsync(Permission permission)
        {
            var resultData = new ActionResultInfo<Permission>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };

            bool isAdopt = true;
            try
            {
                if (permission.ParentId == null)
                {
                    var isExists = await base.AnyAsync(m => m.PermissionName == permission.PermissionName);
                    if (isExists) { isAdopt = false; }
                }
                else
                {
                    var objList = await base.GetListAsync(m => m.ParentId == permission.ParentId);
                    if (objList.Datas.Count > 0)
                    {
                        if (objList.Datas.Where(m => m.PermissionName.Contains(permission.PermissionName)).ToList().Count > 0) { isAdopt = false; }
                    }
                }

                if (isAdopt)
                {
                    Expression<Func<Permission, bool>> whereFun = p => p.PermissionId == permission.ParentId;
                    permission.Degree = permission.ParentId == null ? 1 : Context.Set<Permission>().FirstOrDefault(whereFun).Degree + 1;
                    permission.CreatorId = CurrentLoginUser.Id;
                    int ret = await base.DbExecuteActionAsync(permission, DbActionType.Add);
                    if (ret > 0)
                    {
                        resultData.ResultState = ResultState.Success;
                        resultData.Message = "成功";
                        resultData.Data = permission;
                    }
                    else
                    {
                        resultData.ResultState = ResultState.Failure;
                        resultData.Message = "失败";
                    }
                }
                else
                {
                    resultData.ResultState = ResultState.Failure;
                    resultData.Message = "同级节点下不允许有相同地区名";
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
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<ActionResultInfo<Permission>> DelInfoAsync(string[] ids)
        {
            var resultInfo = new ActionResultInfo<Permission>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };
            try
            {
                if (ids.Length > 0)
                {
                    ids = ids.Select(item => item.Replace(item, $"'{item}'")).ToArray();
                    //string sql = string.Format("update [dbo].[Sys_Permission] set DataState={0} where PermissionID in({1})", (int)DataState.Deleted, string.Join(",", ids));
                    string sql = $"update [dbo].[Sys_Permission] set DataState={(int)DataState.Deleted} where PermissionID in({string.Join(",", ids)})";
                    var paramsObjectArray = new[] {
                        new SqlParameter("DataState", (int)DataState.Deleted),
                        new SqlParameter("PermissionID", string.Join(",", ids))
                    };
                    var ret = await Context.Database.ExecuteSqlRawAsync(sql, paramsObjectArray);
                    if (ret > 0)
                    {
                        #region 递归删除级联子项

                        //foreach (var id in ids)
                        //{
                        //    var objs = await ListAll(o => o.ParentId == id);
                        //    if (objs.Count() > 0)
                        //    {
                        //        int[] oids = objs.Select(o => o.ID).ToArray();
                        //        await DelData(oids);
                        //    }
                        //}

                        #endregion 递归删除级联子项

                        resultInfo.ResultState = ResultState.Success;
                        resultInfo.Message = "成功";
                    }
                    else
                    {
                        resultInfo.ResultState = ResultState.Failure;
                        resultInfo.Message = "失败";
                    }
                }
                else
                {
                    resultInfo.ResultState = ResultState.Error;
                    resultInfo.Message = "参数错误，不能为空";
                }
            }
            catch (Exception e)
            {
                resultInfo.ResultState = ResultState.Error;
                resultInfo.Message = e.Message;
            }
            return resultInfo;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="permission">地区类</param>
        /// <returns></returns>
        public override async Task<ActionResultInfo<Permission>> ModInfoAsync(Permission permission)
        {
            var resultInfo = new ActionResultInfo<Permission>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };
            try
            {
                var ret = await base.DbExecuteActionAsync(permission, DbActionType.Mod);
                if (ret > 0)
                {
                    resultInfo.ResultState = ResultState.Success;
                    resultInfo.Message = "成功";
                }
                else
                {
                    resultInfo.ResultState = ResultState.Failure;
                    resultInfo.Message = "成功";
                }
            }
            catch (Exception e)
            {
                resultInfo.ResultState = ResultState.Error;
                resultInfo.Message = e.Message;
            }
            return resultInfo;
        }

        #endregion

        #region 查询

        /// <summary>
        /// 查询 [根据ID查询单条数据信息]
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public async Task<QueryResultInfo<Permission>> GetInfoAsync(string permissionId)
        {
            var resultInfo = new QueryResultInfo<Permission>() { ResultState = ResultState.Success, Message = "" };
            try
            {
                var model = await base.SingleOrDefaultAsync(m => m.PermissionId == permissionId);
                if (model != null)
                {
                    var idList = await GetIdsAsync(permissionId);
                    model.Ids = idList;
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
        /// 获取对象父id集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<List<string>> GetIdsAsync(string id = null)
        {
            List<string> idList = new List<string>();
            var obj = await Context.Permissions.FindAsync(id);
            if (obj.ParentId == null)
            {
                idList.Add(obj.PermissionId);
            }
            else
            {
                var ids = await GetIdsAsync(obj.ParentId);
                foreach (var item in ids)
                {
                    idList.Add(item);
                }
                idList.Add(obj.PermissionId);
            }
            return idList;
        }

        /// <summary>
        /// 获取对象路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Permission>> GetPathAsync(string id = null)
        {
            List<Permission> objList = new List<Permission>();
            var obj = await Context.Permissions.FindAsync(id);
            if (obj == null)
            {
                objList.Add(new Permission
                {
                    PermissionId = null,
                    PermissionName = "权限"
                });
            }
            else
            {
                if (obj.ParentId == null)
                {
                    objList.Add(new Permission
                    {
                        PermissionId = null,
                        PermissionName = "权限"
                    });
                }
                else
                {
                    var list = await GetPathAsync(obj.ParentId);
                    foreach (var model in list)
                    {
                        objList.Add(model);
                    }
                }
                objList.Add(obj);
            }
            return objList;
        }

        /// <summary>
        /// 查询 [分页数据]
        /// </summary>
        /// <param name="o">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        public async Task<QueryResultInfo<Permission>> GetPageAsync(PermissionSearchModel o)
        {
            //条件查询表达式
            Expression<Func<Permission, bool>> whereFun = null;
            //if (!string.IsNullOrEmpty(o.ParentId)) { whereFun = m => m.ParentId == o.ParentId; }
            whereFun = m => m.ParentId.Equals(o.ParentId);
            if (!string.IsNullOrEmpty(o.PermissionName)) { whereFun = m => m.PermissionName.Contains(o.PermissionName); }

            //排序表达式
            Expression<Func<Permission, object>> orderByFun = null;
            //orderByFun = m => m.Sort;
            try
            {
                return await base.GetPageAsync(o.PageIndex, o.PageSize, whereFun, orderByFun, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询根节点列表（包含根节点下的所有子节点）
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<List<Permission>> GetNodeAsync(string parentId = null)
        {
            var source = from m in Context.Permissions select m;
            var list = await source.ToListAsync();
            var newList = list.Where(m => m.ParentId == parentId).ToList();
            return newList;
        }

        /// <summary>
        /// 获取树数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<Permission>> GetTreeAsync()
        {
            List<Permission> list = new List<Permission>()
            {
                new Permission{
                    PermissionId=null,
                    PermissionName="权限管理",
                    Children=await GetNodeAsync()
                }
            };
            return list;
        }

        #endregion

        #endregion
    }
}