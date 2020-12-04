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
using static Model.Enum.SystemEnum;

namespace Services.SystemServices
{
    /// <summary>
    /// 菜单服务类
    /// </summary>
    public class MenuService : TreeService<Menu>, IMenuService
    {
        #region 增删改

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="area">模型类</param>
        /// <returns></returns>
        public override ActionResultInfo<Menu> AddInfo(Menu o)
        {
            var resultData = new ActionResultInfo<Menu>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };

            bool isAdopt = true;
            try
            {
                if (o.ParentId == null)
                {
                    var isExists = base.Any(m => m.MenuName == o.MenuName);
                    if (isExists) { isAdopt = false; }
                }
                else
                {
                    var areaList = base.GetList(m => m.ParentId == o.ParentId).Datas;
                    if (areaList.Count > 0)
                    {
                        if (areaList.Where(m => m.MenuName.Contains(o.MenuName)).ToList().Count > 0) { isAdopt = false; }
                    }
                }

                if (isAdopt)
                {
                    o.Degree = o.ParentId == null ? 1 : Find(o.ParentId).Degree + 1;
                    o.CreatorId = CurrentLoginUser.Id;
                    int ret = base.DbExecuteAction(o,DbActionType.Add);
                    if (ret > 0)
                    {
                        resultData.ResultState = ResultState.Success;
                        resultData.Message = "成功";
                        resultData.Data = o;
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
                return resultData;
            }
            catch (Exception e)
            {
                resultData.ResultState = ResultState.Error;
                resultData.Message = e.Message;
                return resultData;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public override ActionResultInfo<Menu> ModInfo(Menu o)
        {
            var resultInfo = new ActionResultInfo<Menu>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };
            try
            {
                var ret = base.DbExecuteAction(o,DbActionType.Mod);
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
                return resultInfo;
            }
            catch (Exception e)
            {
                resultInfo.ResultState = ResultState.Error;
                resultInfo.Message = e.Message;
                return resultInfo;
            }
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override ActionResultInfo<Menu> DelInfo(int[] ids)
        {
            var resultInfo = new ActionResultInfo<Menu>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };
            try
            {
                string sql = string.Format("update [dbo].[Sys_Menu] set DataState={0} where ID in({1})", (int)DataState.Deleted, string.Join(",", ids));
                var paramsObjectArray = new[] {
                    new SqlParameter("DataState", (int)DataState.Deleted),
                    new SqlParameter("ID", string.Join(",", ids))
                };
                //Context.Database.ExecuteSqlCommand("sqlStr",params); //.net core2.1
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

        #endregion 增删改

        #region 查询

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="searchModel">搜索条件数据</param>
        /// <returns>查询结果信息</returns>
        public QueryResultInfo<Menu> GetPage(MenuSearchModel searchModel)
        {
            //条件查询表达式
            Expression<Func<Menu, bool>> whereFun = null;
            whereFun = m => m.ParentId == searchModel.ParentId;
            //if (o.ParentId.HasValue) { whereFun = m => m.ParentId == o.ParentId; }
            if (!string.IsNullOrEmpty(searchModel.MenuName)) { whereFun = m => m.MenuName.Contains(searchModel.MenuName); }

            //排序表达式
            Expression<Func<Menu, object>> orderByFun = null;
            orderByFun = m => m.Id;

            try
            {
                return base.GetPage(searchModel.PageIndex, searchModel.PageSize, whereFun, orderByFun, true);
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
        public List<Menu> GetPath(int? id = null)
        {
            List<Menu> areaList = new List<Menu>();
            var obj = base.Context.Menus.Find(id);
            if (obj == null)
            {
                areaList.Add(new Menu
                {
                    Id = 0,
                    MenuName = "菜单"
                });
            }
            else
            {
                if (obj.ParentId == null)
                {
                    areaList.Add(new Menu
                    {
                        Id = 0,
                        MenuName = "菜单"
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
        public List<Menu> GetTree()
        {
            List<Menu> list = new List<Menu>()
            {
                new Menu{
                    Id=0,
                    MenuName="菜单管理",
                    Children=base.GetNode()
                }
            };
            return list;
        }

        #endregion 查询
    }
}