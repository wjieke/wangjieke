using IServices.ISystemServices;
using Microsoft.AspNetCore.Mvc;
using Model.ModelSearch;
using Model.Entity.System;
using System.Collections.Generic;
using WebAPI.Controllers.Bases;
using Model.ModelTool;
using Model.ModelView;

namespace WebAPI.Controllers.Systems
{
    /// <summary>
    /// 地区API控制器
    /// </summary>
    public class AreaController : TreeController<Area, IAreaService>
    {
        public IAreaService AreaService { get; set; }

        #region 查询

        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="o">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        [HttpPost("GetPage")]
        public QueryResultInfo<Area> GetPage(AreaSearchModel o)
        {
            return AreaService.GetPage(o);
        }

        /// <summary>
        /// 获取树结构数据
        /// </summary>
        /// <returns>返回树结构数据</returns>
        [HttpGet("GetTree")]
        public List<Area> GetTree()
        {
            return AreaService.GetTree();
        }

        /// <summary>
        /// 获取树节点路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetPath")]
        public List<Area> GetPath(int? id)
        {
            return AreaService.GetPath(id);
        }

        #endregion
    }
}