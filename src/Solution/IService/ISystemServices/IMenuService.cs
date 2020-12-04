using IServices.IBaseServices;
using Model.ModelSearch.System;
using Model.Entity.System;
using System.Collections.Generic;
using Model.ModelTool;
using Model.ModelView;

namespace IServices.ISystemServices
{
    public interface IMenuService : ITreeService<Menu>
    {
        #region 查询

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="searchModel">搜索条件数据</param>
        /// <returns>查询结果信息</returns>
        QueryResultInfo<Menu> GetPage(MenuSearchModel searchModel);

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