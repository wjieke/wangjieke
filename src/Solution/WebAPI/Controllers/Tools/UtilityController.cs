using Library.Tool;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.ModelTool;
using System;
using System.Collections.Generic;
using System.Linq;
using static Model.Enum.BlogEnum;
using static Model.Enum.SystemEnum;

namespace WebAPI.Controllers.Tools
{
    /// <summary>
    /// 公共API控制器接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]   //启用跨域（设置跨域处理的代理）
    public class UtilityController : ControllerBase
    {
        #region 枚举列表接口

        #region 枚举属性
        /// <summary>
        /// 数据状态
        /// </summary>
        private DataState DataState { get; set; }

        /// <summary>
        /// 性别类型
        /// </summary>
        private SexType SexType { get; set; }

        /// <summary>
        /// 文章类型
        /// </summary>
        private ArticleType ArticleType { get; set; }
        #endregion

        #region 系统枚举
        /// <summary>
        /// 获取终端类别枚举
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetClientCategory")]
        public List<EnumInfo> GetClientCategory()
        {
            return new ClientCategory().GetEnumList();
        }

        /// <summary>
        /// 获取数据状态枚举
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDataState")]
        public List<EnumInfo> GetDataState()
        {
            return DataState.GetEnumList().Where(o => o.Value != (Convert.ToInt32(DataState.Deleted))).ToList();
        }

        /// <summary>
        /// 获取性别类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSexType")]
        public List<EnumInfo> GetSexType()
        {
            return SexType.GetEnumList().Where(i => i.Value != (Convert.ToInt32(SexType.Unknown))).ToList();
        }
        #endregion

        #region 博客枚举

        /// <summary>
        /// 获取文章类型枚举
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetArticleType")]
        public List<EnumInfo> GetArticleType()
        {
            return ArticleType.GetEnumList();
        }

        #endregion

        #endregion
    }
}