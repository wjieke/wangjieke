using IServices.IBaseServices;
using Model.ModelSearch.System;
using Model.Entity.System;
using Model.ModelTool;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.ModelView;

namespace IServices.ISystemServices
{
    public interface IPermissionService : IBaseService<Permission>
    {
        #region 同步

        #region 增删改

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="permission">模型类</param>
        /// <returns>返回增加的数据实体类</returns>
        new ActionResultInfo<Permission> AddInfo(Permission permission);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">删除数据的id数组</param>
        /// <param name="tableName">要删除数据的表名</param>
        /// <returns></returns>
        ActionResultInfo<Permission> DelInfo(string[] ids);

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="model">模型类</param>
        /// <returns>返回修改后的数据实体类</returns>
        new ActionResultInfo<Permission> ModInfo(Permission permission);

        #endregion

        #region 查询

        QueryResultInfo<Permission> GetInfo(string permissionId);

        List<Permission> GetPath(string id = null);

        QueryResultInfo<Permission> GetPage(PermissionSearchModel permissionSearch);

        List<Permission> GetNode(string parentId = null);

        List<Permission> GetTree();

        #endregion

        #endregion

        #region 异步

        #region 增删改

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="permission">模型类</param>
        /// <returns>返回增加的数据实体类</returns>
        new Task<ActionResultInfo<Permission>> AddInfoAsync(Permission permission);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">删除数据的id数组</param>
        /// <param name="tableName">要删除数据的表名</param>
        /// <returns></returns>
        Task<ActionResultInfo<Permission>> DelInfoAsync(string[] ids);

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="model">模型类</param>
        /// <returns>返回修改后的数据实体类</returns>
        new Task<ActionResultInfo<Permission>> ModInfoAsync(Permission permission);

        #endregion

        #region 查询

        Task<QueryResultInfo<Permission>> GetInfoAsync(string permissionId);

        Task<List<Permission>> GetPathAsync(string id = null);

        Task<QueryResultInfo<Permission>> GetPageAsync(PermissionSearchModel permissionSearch);

        Task<List<Permission>> GetNodeAsync(string parentId = null);

        Task<List<Permission>> GetTreeAsync();

        #endregion

        #endregion
    }
}