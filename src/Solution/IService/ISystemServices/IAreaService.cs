using IServices.IBaseServices;
using Model.ModelSearch;
using Model.Entity.System;
using System.Collections.Generic;
using Model.ModelTool;
using Model.ModelView;

namespace IServices.ISystemServices
{
    public interface IAreaService : ITreeService<Area>
    {
        #region 查询

        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="searchArea">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        QueryResultInfo<Area> GetPage(AreaSearchModel areaSearch);

        /// <summary>
        /// 获取树结构数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Area> GetTree();

        /// <summary>
        /// 获取树节点路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Area> GetPath(int? id);

        #endregion
    }
}