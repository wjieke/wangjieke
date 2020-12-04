using IServices.IBaseServices;
using Model.ModelSearch.System;
using Model.Entity.System;
using Model.ModelTool;
using System.Collections.Generic;
using Model.ModelView;

namespace IServices.ISystemServices
{
    public interface IDepartmentService : ITreeService<Department>
    {
        #region 查询

        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="searchArea">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        QueryResultInfo<Department> GetPage(DepartmentSearchModel searchDepartment);


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
