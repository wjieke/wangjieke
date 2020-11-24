using IServices.IBaseServices;
using Model.ModelDto;
using Model.ModelSearch;
using Model.Entity.System;
using Model.ModelTool;

namespace IServices.ISystemServices
{
    public interface IUserService : IBaseService<User>
    {
        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="searchUser">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        QueryResultInfo<User> GetPage(UserSearch userSearch);

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