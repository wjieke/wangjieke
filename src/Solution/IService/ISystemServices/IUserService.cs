using IServices.IBaseServices;
using Model.ModelDto;
using Model.ModelSearch.System;
using Model.Entity.System;
using Model.ModelTool;
using Model.ModelView;

namespace IServices.ISystemServices
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IUserService : IBaseService<User>
    {
        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="searchModel">搜索条件数据</param>
        /// <returns>查询结果信息</returns>
        QueryResultInfo<User> GetPage(UserSearchModel searchModel);

        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        string UserLogin(CheckUserDto o);

        /// <summary>
        /// 查询单个用户信息（带用户角色）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        new QueryResultInfo<User> GetInfo(int uId);
    }
}