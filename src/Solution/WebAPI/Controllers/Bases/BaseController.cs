using IServices.IBaseServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.ModelTool;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Bases
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")] //启用跨域（设置跨域处理的代理）
    [Authorize]         //启用jwt验证 (添加Authorize标签，可以加在方法上，也可以加在类上，[AllowAnonymous] 标记此接口方法不需要身份验证)
    public abstract class BaseController<TModel, TService> : ControllerBase
        where TModel : class
        where TService : IBaseService<TModel>
    {
        public TService Service { get; set; }

        #region 同步

        #region 增删改

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="model">模型类</param>
        /// <returns>返回增加的数据实体类</returns>
        [HttpPost("AddInfo")]
        public virtual ActionResult<ActionResultInfo<TModel>> AddInfo(TModel model)
        {
            return Service.AddInfo(model);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">删除数据的id数组</param>
        /// <returns></returns>
        [HttpDelete("DelInfo")]
        public virtual ActionResult<ActionResultInfo<TModel>> DelInfo(int[] ids)
        {
            return Service.DelInfo(ids);
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="model">模型类</param>
        /// <returns>返回修改后的数据实体类</returns>
        [HttpPut("ModInfo")]
        public virtual ActionResult<ActionResultInfo<TModel>> ModInfo(TModel model)
        {
            return Service.ModInfo(model);
        }

        #endregion

        #region 查询

        /// <summary>
        /// 查询(根据ID查询单条数据信息)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetInfo")]
        public virtual QueryResultInfo<TModel> GetInfo(int id)
        {
            return Service.GetInfo(id);
        }

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetList")]
        public virtual QueryResultInfo<TModel> GetList()
        {
            return Service.GetList();
        }


        #endregion

        #endregion

        #region 异步

        #region 增删改

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="model">模型类</param>
        /// <returns>返回增加的数据实体类</returns>
        [HttpPost("AddInfoAsync")]
        public async virtual Task<ActionResult<ActionResultInfo<TModel>>> AddInfoAsync(TModel model)
        {
            return await Service.AddInfoAsync(model);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">删除数据的id数组</param>
        /// <returns></returns>
        [HttpDelete("DelInfoAsync")]
        public async virtual Task<ActionResult<ActionResultInfo<TModel>>> DelInfoAsync(int[] ids)
        {
            return await Service.DelInfoAsync(ids);
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="model">模型类</param>
        /// <returns>返回修改后的数据实体类</returns>
        [HttpPut("ModInfoAsync")]
        public async virtual Task<ActionResult<ActionResultInfo<TModel>>> ModInfoAsync(TModel model)
        {
            return await Service.ModInfoAsync(model);
        }

        #endregion

        #region 查询

        /// <summary>
        /// 查询(根据ID查询单条数据信息)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetInfoAsync")]
        public virtual async Task<QueryResultInfo<TModel>> GetInfoAsync(int id)
        {
            return await Service.GetInfoAsync(id);
        }

        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetListAsync")]
        public virtual async Task<QueryResultInfo<TModel>> GetListAsync()
        {
            return await Service.GetListAsync();
        }

        #endregion

        #endregion
    }
}