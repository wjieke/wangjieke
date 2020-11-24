using IServices.IBaseServices;
using Microsoft.AspNetCore.Mvc;
using Model.ModelBase;
using Model.ModelTool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Bases
{
    public abstract class TreeController<TTreeModel, TService> : BaseController<TTreeModel, TService>
        where TTreeModel : TreeBase<TTreeModel>
        where TService : ITreeService<TTreeModel>
    {
        #region 同步

        #region 查询

        /// <summary>
        /// 查询(根据ID查询单条数据信息)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetInfo")]
        public new ActionResultInfo<TTreeModel> GetInfo(int id)
        {
            return Service.GetInfo(id);
        }

        /// <summary>
        /// 获取级联器数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetNode")]
        public List<TTreeModel> GetNode(int? id)
        {
            return Service.GetNode(id);
        }

        #endregion

        #endregion

        #region 异步

        #region 查询

        /// <summary>
        /// 查询(根据ID查询单条数据信息)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetInfoAsync")]
        public new async Task<ActionResultInfo<TTreeModel>> GetInfoAsync(int id)
        {
            return await Service.GetInfoAsync(id);
        }

        /// <summary>
        /// 获取级联器数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetNodeAsync")]
        public async Task<List<TTreeModel>> GetNodeAsync(int? id)
        {
            return await Service.GetNodeAsync(id);
        }

        #endregion

        #endregion
    }
}