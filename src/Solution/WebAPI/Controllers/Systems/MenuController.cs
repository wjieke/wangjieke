using IServices.ISystemServices;
using Microsoft.AspNetCore.Mvc;
using Model.ModelSearch;
using Model.Entity.System;
using System.Collections.Generic;
using WebAPI.Controllers.Bases;
using Model.ModelTool;

namespace WebAPI.Controllers.Systems
{
    public class MenuController : TreeController<Menu, IMenuService>
    {
        public IMenuService MenuService { get; set; }

        #region 查询

        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="o">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        [HttpPost("GetPage")]
        public QueryResultInfo<Menu> GetPage(MenuSearch o)
        {
            return MenuService.GetPage(o);
        }

        /// <summary>
        /// 获取树结构数据
        /// </summary>
        /// <returns>返回树结构数据</returns>
        [HttpGet("GetTree")]
        public List<Menu> GetTree()
        {
            return MenuService.GetTree();
        }

        /// <summary>
        /// 获取树节点路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetPath")]
        public List<Menu> GetPath(int? id)
        {
            return MenuService.GetPath(id);
        }

        #endregion 查询
    }
}