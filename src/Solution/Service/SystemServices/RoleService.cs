using IServices.ISystemServices;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.ModelSearch;
using Model.Entity.System;
using Model.ModelTool;
using Model.Enum;
using Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.SystemServices
{
    public class RoleService : TreeService<Role>, IRoleService
    {
        public IRolePermissionService RolePermissionService { get; set; }

        #region 同步

        #region 增删改

        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="role">角色类</param>
        /// <returns></returns>
        public override ActionResultInfo<Role> AddInfo(Role role)
        {
            var resultData = new ActionResultInfo<Role>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };

            bool isAdopt = true;
            try
            {
                if (role.ParentId == null)
                {
                    var isExists = base.Any(m => m.RoleName == role.RoleName);
                    if (isExists) { isAdopt = false; }
                }
                else
                {
                    var objList = base.GetList(m => m.ParentId == role.ParentId).Datas;
                    if (objList.Count > 0)
                    {
                        if (objList.Where(m => m.RoleName.Contains(role.RoleName)).ToList().Count > 0) { isAdopt = false; }
                    }
                }

                if (isAdopt)
                {
                    role.Degree = role.ParentId == null ? 1 : Find(role.ParentId).Degree + 1;
                    role.CreatorId = CurrentUser.Id;
                    using (var transaction = Context.Database.BeginTransaction())
                    {
                        try
                        {
                            int ret = base.DbExecuteAction(role, DbActionType.Add);
                            if (ret > 0)
                            {
                                resultData.ResultState = ResultState.Success;
                                resultData.Message = "成功";
                                resultData.Data = role;

                                //添加角色信息成功后，添加角色权限
                                foreach (var pid in role.PermissionIds)
                                {
                                    RolePermission rpModel = new RolePermission() { RoleId = role.Id, PermissionId = pid };
                                    RolePermissionService.AddInfo(rpModel);
                                }
                                //提交事务
                                transaction.Commit();
                            }
                            else
                            {
                                resultData.ResultState = ResultState.Failure;
                                resultData.Message = "失败";
                                //回滚事务
                                transaction.Rollback();
                            }
                        }
                        catch (Exception er)
                        {
                            resultData.ResultState = ResultState.Failure;
                            resultData.Message = "失败：" + er.Message;
                            transaction.Rollback();
                        }
                    }
                }
                else
                {
                    resultData.ResultState = ResultState.Failure;
                    resultData.Message = "同级节点下不允许有相同名称";
                }
            }
            catch (Exception e)
            {
                resultData.ResultState = ResultState.Error;
                resultData.Message = "错误：" + e.Message;
            }
            return resultData;
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override ActionResultInfo<Role> DelInfo(int[] ids)
        {
            var resultInfo = new ActionResultInfo<Role>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };
            try
            {
                string sql = string.Format("update [dbo].[Sys_Role] set DataState={0} where ID in({1})", (int)DataState.Deleted, string.Join(",", ids));
                var paramsObjectArray = new[] {
                    new SqlParameter("DataState", (int)DataState.Deleted),
                    new SqlParameter("ID", string.Join(",", ids))
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
            catch (Exception e)
            {
                resultInfo.ResultState = ResultState.Error;
                resultInfo.Message = e.Message;
                return resultInfo;
            }
            return resultInfo;
        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="role">角色类</param>
        /// <returns></returns>
        public override ActionResultInfo<Role> ModInfo(Role role)
        {
            var resultInfo = new ActionResultInfo<Role>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };
            try
            {
                var ret = base.DbExecuteAction(role, DbActionType.Mod);
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
        /// 查询(分页数据)
        /// </summary>
        /// <param name="o">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        public QueryResultInfo<Role> GetPage(RoleSearch o)
        {
            //条件查询表达式
            Expression<Func<Role, bool>> whereFun = null;
            whereFun = m => m.ParentId == o.ParentId;
            //if (o.ParentId.HasValue) { whereFun = m => m.ParentId == o.ParentId; }
            if (!string.IsNullOrEmpty(o.RoleName)) { whereFun = m => m.RoleName.Contains(o.RoleName); }

            //排序表达式
            Expression<Func<Role, object>> orderByFun = null;
            orderByFun = m => m.Id;
            try
            {
                return base.GetPage(o.PageIndex, o.PageSize, whereFun, null, orderByFun, true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取树节点路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Role> GetPath(int? id = null)
        {
            List<Role> areaList = new List<Role>();
            var obj = Context.Roles.Find(id);
            if (obj == null)
            {
                areaList.Add(new Role
                {
                    Id = 0,
                    RoleName = "角色"
                });
            }
            else
            {
                if (obj.ParentId == null)
                {
                    areaList.Add(new Role
                    {
                        Id = 0,
                        RoleName = "角色"
                    });
                }
                else
                {
                    var list = GetPath(obj.ParentId);
                    foreach (var model in list)
                    {
                        areaList.Add(model);
                    }
                }
                areaList.Add(obj);
            }
            return areaList;
        }

        /// <summary>
        /// 获取树结构数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Role> GetTree()
        {
            List<Role> list = new List<Role>()
            {
                new Role{
                    Id=0,
                    RoleName="角色管理",
                    Children=base.GetNode()
                }
            };
            return list;
        }

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        public QueryResultInfo<Role> GetList()
        {
            Expression<Func<Role, bool>> whereFun = null;
            whereFun = m => m.ParentId == null;
            return base.GetList(whereFun);
            //var list = base.GetList().Datas.Where(m=>m.ParentId==null).ToList();
            //QueryResultInfo<Role> qri = new QueryResultInfo<Role>();
            //if (list.Count() > 0) {
            //    qri.Datas = list;
            //    qri.Message = "成功";
            //    qri.ResultState = ResultState.Success;
            //}else
            //{
            //    qri.Datas = null;
            //    qri.Message = "失败";
            //    qri.ResultState = ResultState.Failure;
            //}
            //return qri;
        }

        #endregion

        #endregion

        #region 异步

        #region 增删改

        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="role">角色类</param>
        /// <returns></returns>
        public override async Task<ActionResultInfo<Role>> AddInfoAsync(Role role)
        {
            var resultData = new ActionResultInfo<Role>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };

            bool isAdopt = true;
            try
            {
                if (role.ParentId == null)
                {
                    var isExists = await base.AnyAsync(m => m.RoleName == role.RoleName);
                    if (isExists) { isAdopt = false; }
                }
                else
                {
                    var objList = await base.GetListAsync(m => m.ParentId == role.ParentId);
                    if (objList.Datas.Count > 0)
                    {
                        if (objList.Datas.Where(m => m.RoleName.Contains(role.RoleName)).ToList().Count > 0) { isAdopt = false; }
                    }
                }

                if (isAdopt)
                {
                    role.Degree = role.ParentId == null ? 1 : Find(role.ParentId).Degree + 1;
                    role.CreatorId = CurrentUser.Id;
                    using (var transaction = Context.Database.BeginTransaction())
                    {
                        try
                        {
                            int ret = await base.DbExecuteActionAsync(role, DbActionType.Add);
                            if (ret > 0)
                            {
                                resultData.ResultState = ResultState.Success;
                                resultData.Message = "成功";
                                resultData.Data = role;

                                //添加角色信息成功后，添加角色权限
                                foreach (var pid in role.PermissionIds)
                                {
                                    RolePermission rpModel = new RolePermission() { RoleId = role.Id, PermissionId = pid };
                                    await RolePermissionService.DbExecuteActionAsync(rpModel, DbActionType.Add);
                                }
                                //提交事务
                                await transaction.CommitAsync();
                            }
                            else
                            {
                                resultData.ResultState = ResultState.Failure;
                                resultData.Message = "失败";
                                //回滚事务
                                await transaction.RollbackAsync();
                            }
                        }
                        catch (Exception er)
                        {
                            resultData.ResultState = ResultState.Failure;
                            resultData.Message = "失败：" + er.Message;
                            await transaction.RollbackAsync();
                        }
                    }
                }
                else
                {
                    resultData.ResultState = ResultState.Failure;
                    resultData.Message = "同级节点下不允许有相同名称";
                }
            }
            catch (Exception e)
            {
                resultData.ResultState = ResultState.Error;
                resultData.Message = "错误：" + e.Message;
            }
            return resultData;
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override async Task<ActionResultInfo<Role>> DelInfoAsync(int[] ids)
        {
            var resultInfo = new ActionResultInfo<Role>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };
            try
            {
                string sql = string.Format("update [dbo].[Sys_Role] set DataState={0} where ID in({1})", (int)DataState.Deleted, string.Join(",", ids));
                var paramsObjectArray = new[] {
                    new SqlParameter("DataState", (int)DataState.Deleted),
                    new SqlParameter("ID", string.Join(",", ids))
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
            catch (Exception e)
            {
                resultInfo.ResultState = ResultState.Error;
                resultInfo.Message = e.Message;
                return resultInfo;
            }
            return resultInfo;
        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="role">角色类</param>
        /// <returns></returns>
        public override async Task<ActionResultInfo<Role>> ModInfoAsync(Role role)
        {
            var resultInfo = new ActionResultInfo<Role>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };
            try
            {
                var ret = await base.DbExecuteActionAsync(role, DbActionType.Mod);
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
        /// 查询(分页数据)
        /// </summary>
        /// <param name="o">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        public async Task<QueryResultInfo<Role>> GetPageAsync(RoleSearch o)
        {
            //条件查询表达式
            Expression<Func<Role, bool>> whereFun = null;
            whereFun = m => m.ParentId == o.ParentId;
            //if (o.ParentId.HasValue) { whereFun = m => m.ParentId == o.ParentId; }
            if (!string.IsNullOrEmpty(o.RoleName)) { whereFun = m => m.RoleName.Contains(o.RoleName); }

            //排序表达式
            Expression<Func<Role, object>> orderByFun = null;
            orderByFun = m => m.Id;

            try
            {
                return await base.GetPageAsync(o.PageIndex, o.PageSize, whereFun, null, orderByFun, true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取树节点路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Role>> GetPathAsync(int? id = null)
        {
            List<Role> areaList = new List<Role>();
            var obj = await Context.Roles.FindAsync(id);
            if (obj == null)
            {
                areaList.Add(new Role
                {
                    Id = 0,
                    RoleName = "角色"
                });
            }
            else
            {
                if (obj.ParentId == null)
                {
                    areaList.Add(new Role
                    {
                        Id = 0,
                        RoleName = "角色"
                    });
                }
                else
                {
                    var list = await GetPathAsync(obj.ParentId);
                    foreach (var model in list)
                    {
                        areaList.Add(model);
                    }
                }
                areaList.Add(obj);
            }
            return areaList;
        }

        /// <summary>
        /// 获取树结构数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Role>> GetTreeAsync()
        {
            List<Role> list = new List<Role>()
            {
                new Role{
                    Id=0,
                    RoleName="角色管理",
                    Children=await base.GetNodeAsync()
                }
            };
            return list;
        }

        #endregion

        #endregion
    }
}