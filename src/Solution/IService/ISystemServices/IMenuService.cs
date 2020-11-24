using IServices.IBaseServices;
using Model.ModelSearch;
using Model.Entity.System;
using System.Collections.Generic;
using Model.ModelTool;

namespace IServices.ISystemServices
{
    public interface IMenuService : ITreeService<Menu>
    {
        #region 查询

        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="searchMenu">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        QueryResultInfo<Menu> GetPage(MenuSearch menuSearch);

        /// <summary>
        /// 获取树节点路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Menu> GetPath(int? id = null);

        /// <summary>
        /// 获取树结构数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Menu> GetTree();

        #endregion 查询
    }
}