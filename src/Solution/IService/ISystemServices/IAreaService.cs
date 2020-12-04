using IServices.IBaseServices;
using Model.ModelSearch.System;
using Model.Entity.System;
using System.Collections.Generic;
using Model.ModelTool;
using Model.ModelView;

namespace IServices.ISystemServices
{
    /// <summary>
    /// 地区服务接口
    /// </summary>
    public interface IAreaService : ITreeService<Area>
    {
        #region 查询

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="searchModel">搜索条件数据</param>
        /// <returns>查询结果信息</returns>
        QueryResultInfo<Area> GetPage(AreaSearchModel searchModel);

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