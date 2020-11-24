using Library.Tool;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.ModelTool;
using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Controllers.Tools
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]   //启用跨域（设置跨域处理的代理）
    public class UtilityController : ControllerBase
    {
        /// <summary>
        /// 获取终端类别枚举
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetClientCategory")]
        public List<EnumInfo> GetClientCategory()
        {
            var enumList = CommonTool.GetEnumList<ClientCategory>();
            return enumList;
        }

        /// <summary>
        /// 获取数据状态枚举
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDataState")]
        public List<EnumInfo> GetDataState()
        {
            var enumList = CommonTool.GetEnumList<DataState>();
            string deleted = CommonTool.GetEnumDescription(DataState.Deleted);
            enumList = enumList.Where(o => !o.Label.Contains(deleted)).ToList();
            return enumList;
        }

        /// <summary>
        /// 获取性别类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSexType")]
        public List<EnumInfo> GetSexType()
        {
            var enumList = CommonTool.GetEnumList<SexType>();
            string unknown = CommonTool.GetEnumDescription(SexType.Unknown);
            enumList = enumList.Where(o => !o.Label.Contains(unknown)).ToList();
            return enumList;
        }
    }
}