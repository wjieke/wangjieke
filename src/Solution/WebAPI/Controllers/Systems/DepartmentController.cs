using IServices.ISystemServices;
using Microsoft.AspNetCore.Mvc;
using Model.ModelSearch;
using Model.Entity.System;
using System.Collections.Generic;
using WebAPI.Controllers.Bases;
using Model.ModelTool;

namespace WebAPI.Controllers.Systems
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : TreeController<Department, IDepartmentService>
    {
        public IDepartmentService DepartmentService { get; set; }

        #region 查询

        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="o">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        [HttpPost("GetPage")]
        public QueryResultInfo<Department> GetPage(DepartmentSearch o)
        {
            return DepartmentService.GetPage(o);
        }

        /// <summary>
        /// 获取树结构数据
        /// </summary>
        /// <returns>返回树结构数据</returns>
        [HttpGet("GetTree")]
        public List<Department> GetTree()
        {
            return DepartmentService.GetTree();
        }

        /// <summary>
        /// 获取树节点路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetPath")]
        public List<Department> GetPath(int? id)
        {
            return DepartmentService.GetPath(id);
        }

        #endregion
    }
}