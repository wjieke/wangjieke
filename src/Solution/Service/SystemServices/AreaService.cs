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
    public class AreaService : TreeService<Area>, IAreaService
    {
        #region 增删改

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="area">地区类</param>
        /// <returns></returns>
        public override ActionResultInfo<Area> AddInfo(Area area)
        {
            var resultData = new ActionResultInfo<Area>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };

            bool isAdopt = true;
            try
            {
                if (area.ParentId == null)
                {
                    var isExists = base.Any(m => m.AreaName == area.AreaName);
                    if (isExists) { isAdopt = false; }
                }
                else
                {
                    var objList = base.GetList(m => m.ParentId == area.ParentId).Datas;
                    if (objList.Count > 0)
                    {
                        if (objList.Where(m => m.AreaName.Contains(area.AreaName)).ToList().Count > 0) { isAdopt = false; }
                    }
                }

                if (isAdopt)
                {
                    area.Degree = area.ParentId == null ? 1 : Find(area.ParentId).Degree + 1;
                    area.CreatorId = CurrentLoginUser.Id;
                    int ret = base.DbExecuteAction(area,DbActionType.Add);
                    if (ret > 0)
                    {
                        resultData.ResultState = ResultState.Success;
                        resultData.Message = "成功";
                        resultData.Data = area;
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
        /// 修改
        /// </summary>
        /// <param name="area">地区类</param>
        /// <returns></returns>
        public override ActionResultInfo<Area> ModInfo(Area area)
        {
            var resultInfo = new ActionResultInfo<Area>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };
            try
            {
                var ret = base.DbExecuteAction(area,DbActionType.Mod);
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

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public override ActionResultInfo<Area> DelInfo(int[] ids)
        {
            var resultInfo = new ActionResultInfo<Area>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };
            try
            {
                string sql = string.Format("update [dbo].[Sys_Area] set DataState={0} where ID in({1})", (int)DataState.Deleted, string.Join(",", ids));
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
        public QueryResultInfo<Area> GetPage(AreaSearchModel o)
        {
            #region "多条件过滤查询"

            //条件查询表达式
            Expression<Func<Area, bool>> whereFun = null;
            whereFun = m => m.ParentId == o.ParentId;
            //if (o.ParentId.HasValue) { whereFun = m => m.ParentId == o.ParentId; }
            if (!string.IsNullOrEmpty(o.AreaName)) { whereFun = m => m.AreaName.Contains(o.AreaName); }
            if (!string.IsNullOrEmpty(o.Abbreviation)) { whereFun = m => m.Abbreviation.Contains(o.Abbreviation); }

            //排序表达式
            Expression<Func<Area, object>> orderByFun = null;
            orderByFun = m => m.Id;

            #endregion "多条件过滤查询"

            #region "多条件排序查询"

            //升序
            //source.OrderBy(m => m.ID).OrderBy(m => m.AreaName);//根据前面的排序作为基础再排序
            //降序
            //source.OrderByDescending(m => m.ID).ThenByDescending(m => m.AreaName);//根据前面的排序作为基础再排序

            #endregion "多条件排序查询"

            try
            {
                return base.GetPage(o.PageIndex, o.PageSize, whereFun);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取树结构数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Area> GetTree()
        {
            List<Area> list = new List<Area>()
            {
                new Area{
                    Id=0,
                    AreaName="地区管理",
                    Children=base.GetNode()
                }
            };
            return list;
        }

        /// <summary>
        /// 获取树节点路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Area> GetPath(int? id = null)
        {
            List<Area> areaList = new List<Area>();
            var area = Context.Areas.Find(id);
            if (area == null)
            {
                areaList.Add(new Area
                {
                    Id = 0,
                    AreaName = "地区"
                });
            }
            else
            {
                if (area.ParentId == null)
                {
                    areaList.Add(new Area
                    {
                        Id = 0,
                        AreaName = "地区"
                    });
                }
                else
                {
                    var list = GetPath(area.ParentId);
                    foreach (var model in list)
                    {
                        areaList.Add(model);
                    }
                }
                areaList.Add(area);
            }
            return areaList;
        }

        #endregion
    }
}