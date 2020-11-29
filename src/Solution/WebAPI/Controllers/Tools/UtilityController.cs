using Library.Tool;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.ModelTool;
using System;
using System.Collections.Generic;
using System.Linq;
using static Model.Enum.SystemEnum;

namespace WebAPI.Controllers.Tools
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]   //启用跨域（设置跨域处理的代理）
    public class UtilityController : ControllerBase
    {
        private DataState DataState { get; set; }
        private SexType SexType { get; set; }

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
    }
}