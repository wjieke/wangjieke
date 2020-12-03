using IServices.ISystemServices;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Model.Entity.System;
using Model.ModelSearch;
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
    public class DepartmentService : TreeService<Department>, IDepartmentService
    {
        #region 增删改

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="department">地区类</param>
        /// <returns></returns>
        public override ActionResultInfo<Department> AddInfo(Department department)
        {
            var resultData = new ActionResultInfo<Department>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };

            bool isAdopt = true;
            try
            {
                if (department.ParentId == null)
                {
                    var isExists = base.Any(m => m.DepartmentName == department.DepartmentName);
                    if (isExists) { isAdopt = false; }
                }
                else
                {
                    var objList = base.GetList(m => m.ParentId == department.ParentId).Datas;
                    if (objList.Count > 0)
                    {
                        if (objList.Where(m => m.DepartmentName.Contains(department.DepartmentName)).ToList().Count > 0) { isAdopt = false; }
                    }
                }

                if (isAdopt)
                {
                    department.Degree = department.ParentId == null ? 1 : Find(department.ParentId).Degree + 1;
                    department.CreatorId = CurrentLoginUser.Id;
                    int ret = base.DbExecuteAction(department,DbActionType.Add);
                    if (ret > 0)
                    {
                        resultData.ResultState = ResultState.Success;
                        resultData.Message = "成功";
                        resultData.Data = department;
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
        /// <param name="department">地区类</param>
        /// <returns></returns>
        public override ActionResultInfo<Department> ModInfo(Department department)
        {
            var resultInfo = new ActionResultInfo<Department>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };
            try
            {
                var ret = base.DbExecuteAction(department,DbActionType.Mod);
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
        public override ActionResultInfo<Department> DelInfo(int[] ids)
        {
            var resultInfo = new ActionResultInfo<Department>()
            {
                ResultState = ResultState.Success,
                Message = ""
            };
            try
            {
                string sql = string.Format("update [dbo].[Com_Department] set DataState={0} where ID in({1})", (int)DataState.Deleted, string.Join(",", ids));
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
        public QueryResultInfo<Department> GetPage(DepartmentSearchModel o)
        {
            #region "多条件过滤查询"

            //条件查询表达式
            Expression<Func<Department, bool>> whereFun = null;
            whereFun = m => m.ParentId == o.ParentId;
            //if (o.ParentId.HasValue) { whereFun = m => m.ParentId == o.ParentId; }
            if (!string.IsNullOrEmpty(o.DepartmentName)) { whereFun = m => m.DepartmentName.Contains(o.DepartmentName); }

            //排序表达式
            Expression<Func<Department, object>> orderByFun = null;
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
        public List<Department> GetTree()
        {
            List<Department> list = new List<Department>()
            {
                new Department{
                    Id=0,
                    DepartmentName="部门管理",
                    Children= base.GetNode()
                }
            };
            return list;
        }

        /// <summary>
        /// 获取树节点路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Department> GetPath(int? id = null)
        {
            List<Department> areaList = new List<Department>();
            var department = Context.Departments.Find(id);
            if (department == null)
            {
                areaList.Add(new Department
                {
                    Id = 0,
                    DepartmentName = "部门"
                });
            }
            else
            {
                if (department.ParentId == null)
                {
                    areaList.Add(new Department
                    {
                        Id = 0,
                        DepartmentName = "部门"
                    });
                }
                else
                {
                    var list = GetPath(department.ParentId);
                    foreach (var model in list)
                    {
                        areaList.Add(model);
                    }
                }
                areaList.Add(department);
            }
            return areaList;
        }

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        public QueryResultInfo<Department> GetList()
        {
            return base.GetList();
        }

        #endregion
    }
}
