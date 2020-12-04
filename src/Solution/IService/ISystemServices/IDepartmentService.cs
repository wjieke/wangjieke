using IServices.IBaseServices;
using Model.ModelSearch.System;
using Model.Entity.System;
using Model.ModelTool;
using System.Collections.Generic;
using Model.ModelView;

namespace IServices.ISystemServices
{
    /// <summary>
    /// 部门服务接口
    /// </summary>
    public interface IDepartmentService : ITreeService<Department>
    {
        #region 查询

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="searchModel">搜索条件数据</param>
        /// <returns>查询结果信息</returns>
        QueryResultInfo<Department> GetPage(DepartmentSearchModel searchModel);


        /// <summary>
        /// 获取树结构数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Department> GetTree();

        /// <summary>
        /// 获取树节点路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Department> GetPath(int? id);

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        QueryResultInfo<Department> GetList();

        #endregion
    }
}
