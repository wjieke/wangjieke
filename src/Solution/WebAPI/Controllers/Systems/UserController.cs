using IServices.ISystemServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.ModelDto;
using Model.ModelSearch.System;
using Model.Entity.System;
using Model.ModelTool;
using WebAPI.Controllers.Bases;
using Model.ModelView;

namespace WebAPI.Controllers.Systems
{
    /// <summary>
    /// 用户API控制器
    /// </summary>
    public class UserController : BaseController<User, IUserService>
    {
        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="o">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        [HttpPost("GetPage")]
        public QueryResultInfo<User> GetPage(UserSearchModel o)
        {
            return Service.GetPage(o);
        }

        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        [HttpPost("UserLogin")]
        [AllowAnonymous]//jwt验证 标记此接口方法不需要身份验证
        public string UserLogin(CheckUserDto o)
        {
            return Service.UserLogin(o);
        }

        /// <summary>
        /// 查询单个用户信息（带用户角色）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("GetInfo")]
        public new QueryResultInfo<User> GetInfo(int id)
        {
            return Service.GetInfo(id);

        }
    }
}