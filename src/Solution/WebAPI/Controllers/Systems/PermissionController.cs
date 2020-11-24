using IServices.ISystemServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.ModelSearch;
using Model.Entity.System;
using Model.ModelTool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Systems
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]     //启用跨域（设置跨域处理的代理）
    [Authorize]             //启用jwt验证 (添加Authorize标签，可以加在方法上，也可以加在类上)
    public class PermissionController : ControllerBase
    {
        public IPermissionService PermissionService { get; set; }

        #region 增删改

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="model">模型类</param>
        /// <returns>返回增加的数据实体类</returns>
        [HttpPost("AddInfo")]
        public ActionResult<ActionResultInfo<Permission>> AddInfo(Permission model)
        {
            return PermissionService.AddInfo(model);
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="model">模型类</param>
        /// <returns>返回修改后的数据实体类</returns>
        [HttpPut("ModInfo")]
        public ActionResult<ActionResultInfo<Permission>> ModInfo(Permission model)
        {
            return PermissionService.ModInfo(model);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">删除数据的id数组</param>
        /// <returns></returns>
        [HttpDelete("DelInfo")]
        public ActionResult<ActionResultInfo<Permission>> DelInfo(string[] ids)
        {
            return PermissionService.DelInfo(ids);
        }

        #endregion

        #region 查询

        /// <summary>
        /// 查询(根据ID查询单条数据信息)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetInfo")]
        public QueryResultInfo<Permission> GetInfo(string id)
        {
            return PermissionService.GetInfo(id);
        }

        /// <summary>
        /// 获取对象路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("GetPath")]
        public List<Permission> GetPath(string id = null)
        {
            return PermissionService.GetPath(id);
        }

        /// <summary>
        /// 查询 [分页数据]
        /// </summary>
        /// <param name="o">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        [HttpPost("GetPage")]
        public QueryResultInfo<Permission> GetPage(PermissionSearch o)
        {
            return PermissionService.GetPage(o);
        }

        /// <summary>
        /// 获得节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpPost("GetNode")]
        public List<Permission> GetNode(string parentId = null)
        {
            return PermissionService.GetNode(parentId);
        }

        /// <summary>
        /// 获得树
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetTree")]
        public List<Permission> GetTree()
        {
            return PermissionService.GetTree();
        }

        #endregion
    }
}