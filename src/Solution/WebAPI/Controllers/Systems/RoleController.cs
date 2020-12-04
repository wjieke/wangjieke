using IServices.ISystemServices;
using Microsoft.AspNetCore.Mvc;
using Model.ModelSearch.System;
using Model.Entity.System;
using Model.ModelTool;
using System.Collections.Generic;
using WebAPI.Controllers.Bases;
using Model.ModelView;

namespace WebAPI.Controllers.Systems
{
    /// <summary>
    /// 角色API控制器
    /// </summary>
    public class RoleController : TreeController<Role, IRoleService>
    {
        public IRoleService RoleService { get; set; }

        #region 查询

        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="o">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        [HttpPost("GetPage")]
        public QueryResultInfo<Role> GetPage(RoleSearchModel o)
        {
            return RoleService.GetPage(o);
        }

        /// <summary>
        /// 获取树结构数据
        /// </summary>
        /// <returns>返回树结构数据</returns>
        [HttpGet("GetTree")]
        public List<Role> GetTree()
        {
            return RoleService.GetTree();
        }

        /// <summary>
        /// 获取树节点路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetPath")]
        public List<Role> GetPath(int? id)
        {
            return RoleService.GetPath(id);
        }


        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetList")]
        public override QueryResultInfo<Role> GetList()
        {
            return RoleService.GetList();
        }

        #endregion
    }
}