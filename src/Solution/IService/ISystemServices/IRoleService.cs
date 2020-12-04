using IServices.IBaseServices;
using Model.ModelSearch.System;
using Model.Entity.System;
using Model.ModelTool;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.ModelView;

namespace IServices.ISystemServices
{
    public interface IRoleService : ITreeService<Role>
    {
        #region 同步

        #region 查询

        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="roleSearch">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        QueryResultInfo<Role> GetPage(RoleSearchModel roleSearch);

        /// <summary>
        /// 获取树结构数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Role> GetTree();

        /// <summary>
        /// 获取树节点路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Role> GetPath(int? id);

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        QueryResultInfo<Role> GetList();

        #endregion

        #endregion

        #region 异步

        #region 查询

        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="roleSearch">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        Task<QueryResultInfo<Role>> GetPageAsync(RoleSearchModel roleSearch);

        /// <summary>
        /// 获取树结构数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<Role>> GetTreeAsync();

        /// <summary>
        /// 获取树节点路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<Role>> GetPathAsync(int? id);

        #endregion

        #endregion
    }
}