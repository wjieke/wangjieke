using IService.ISystemServices;
using IServices.ISystemServices;
using Library.Tool;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model.Entity.System;
using Model.ModelDto;
using Model.ModelSearch;
using Model.ModelTool;
using Model.ModelView;
using Services.BaseServices;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using static Model.Enum.SystemEnum;

namespace Services.SystemServices
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly JwtSetting _jwtSetting;
        //private readonly IConfiguration _configuration;
        //public UserService(IOptions<JwtSetting> option, IConfiguration configuration)
        public UserService(IOptions<JwtSetting> option)
        {
            _jwtSetting = option.Value;
            //_configuration = configuration;
        }

        public IUserRoleService UserRoleService { get; set; }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="user">模型类</param>
        /// <returns></returns>
        //[TransactionHandle]//标记事务处理方法
        public override ActionResultInfo<User> AddInfo(User user)
        {
            var resultData = new ActionResultInfo<User>() { ResultState = ResultState.Success, Message = "" };
            try
            {
                Expression<Func<User, bool>> whereFun = m => m.UserName.Trim().Equals(user.UserName);
                //如果重复名称数据不存在,添加数据
                if (base.Any(whereFun))
                {
                    resultData.ResultState = ResultState.Failure;
                    resultData.Message = "名称已存在";
                }
                else
                {
                    using (var transaction = Context.Database.BeginTransaction())
                    {
                        try
                        {
                            //密码加密
                            user.Password = EncryptDecryptTool.EncryptPassWord(user.Password);
                            user.CreatorId = CurrentLoginUser.Id;
                            var ret = base.AddInfo(user);
                            if (ret.ResultState == ResultState.Success)
                            {
                                resultData.ResultState = ResultState.Success;
                                resultData.Message = "成功";
                                resultData.Data = user;
                                //添加用户信息成功后，添加用户角色
                                foreach (var rId in user.RoleIds)
                                {
                                    UserRole urModel = new UserRole() { UserId = user.Id, RoleId = rId };
                                    UserRoleService.AddInfo(urModel);
                                }
                                //提交事务
                                transaction.Commit();
                            }
                            else
                            {
                                resultData.ResultState = ResultState.Failure;
                                resultData.Message = "失败";
                                //回滚事务
                                transaction.Rollback();
                            }
                        }
                        catch (Exception er)
                        {
                            resultData.ResultState = ResultState.Failure;
                            resultData.Message = "失败：" + er.Message;
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                resultData.ResultState = ResultState.Error;
                resultData.Message = "错误：" + e.Message;
            }
            return resultData;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override ActionResultInfo<User> ModInfo(User user)
        {
            user.ModifierId = CurrentLoginUser.Id;
            user.ModifyTime = DateTime.Now;

            //using (var transaction = Context.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        var res = base.ModInfo(user);
            //        if (res.ResultState == ResultState.Success)
            //        {
            //            var oldRoleIds = GetInfo(res.Data.Id);
            //            var newRoleIds = user.RoleIds;

            //            var userRole = UserRoleService.GetList(ur => ur.UserId == res.Data.Id).Datas;

            //            string sql1 = $"select * from [dbo].[Sys_UserRole] where UserID ={user.Id} and RoleID in({string.Join(",",newRoleIds)})";
            //            SqlParameter parameter = new SqlParameter("Id", user.Id);
            //            var ps = new[] {
            //                new SqlParameter("UserID", user.Id),
            //                new SqlParameter("RoleID",string.Join(",",newRoleIds))
            //            };
            //            var urs = Context.Set<UserRole>().FromSqlRaw(sql1, ps).ToList();


            //            //var delRoleIds = oldRoleIds.Select(ori=>ori.)

            //            //string sql = $"delete [dbo].[Sys_UserRole] where ID in({string.Join(",", rids)})";
            //            //var paramsObjectArray = new[] {
            //            //    new SqlParameter("DataState", (int)DataState.Deleted),
            //            //    new SqlParameter("ID", string.Join(",", ids))
            //            //};
            //            //var ret = Context.Database.ExecuteSqlRaw(sql, paramsObjectArray);

            //        }
            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }
            //}

            return base.ModInfo(user);
        }

        /// <summary>
        /// 查询(分页数据)
        /// </summary>
        /// <param name="userSearch">搜索条件数据</param>
        /// <returns>Json数据集合</returns>
        public QueryResultInfo<User> GetPage(UserSearchModel userSearch)
        {
            //条件查询表达式
            Expression<Func<User, bool>> whereFun = null;
            if (!string.IsNullOrEmpty(userSearch.UserName))
            {
                whereFun = m => m.UserName.Contains(userSearch.UserName);
            }
            if (userSearch.Birthday != null)
            {
                whereFun = m => m.Birthday.Equals(userSearch.Birthday);
            }
            //排序表达式
            Expression<Func<User, object>> orderByFun = null;
            orderByFun = m => m.Id;
            try
            {
                return base.GetPage(userSearch.PageIndex, userSearch.PageSize, whereFun, orderByFun, true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public string UserLogin(CheckUserDto o)
        {
#if DEBUG
            // 这段代码只为在开发时方便, 正常编译Release之后就不会运行该代码, 只为开发方便
            HttpContextAccessor.HttpContext.Session.SetString("code", o.VeriCode.ToLower());
#endif
            var code = HttpContextAccessor.HttpContext.Session.GetString("code").ToLower();
            if (!string.IsNullOrEmpty(code) && code.Equals(o.VeriCode))
            {
                try
                {
                    //密码加密
                    o.Password = EncryptDecryptTool.EncryptPassWord(o.Password);
                    //AsNoTracking获取非跟踪数据
                    var user = Context.Users.AsNoTracking().Where(m => m.UserName == o.UserName && m.Password == o.Password).FirstOrDefault();
                    if (user != null)
                    {
                        //登录成功保存登录用户信息
                        HttpContextAccessor.HttpContext.Session.Set("CurrentLoginUserInfo", user.ToBytes());

                        //JwtSetting setting = new JwtSetting();
                        //_configuration.Bind("JwtSetting", setting);

                        var claims = new[]{
                            new Claim(ClaimTypes.Name, user.UserName)
                        };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SigningKey));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            issuer: _jwtSetting.Issuer,
                            audience: _jwtSetting.Audience,
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: creds
                        );
                        LoginDto resLogin = new LoginDto()
                        {
                            User = user,
                            Token = new JwtSecurityTokenHandler().WriteToken(token)
                        };
                        return resLogin.ToJsonString();
                    }
                    return "0";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "-1";
        }

        /// <summary>
        /// 查询单个用户信息（带用户角色）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override QueryResultInfo<User> GetInfo(int uId)
        {
            UserViewModel userViewModel = null;

            var user = base.GetInfo(uId).Data;

            userViewModel = Mapper.Map<UserViewModel>(user);

            var userRole = UserRoleService.GetList(ur => ur.UserId == uId).Datas;
            user.RoleIds = userRole.Select(ur => ur.RoleId).ToArray();
            return new QueryResultInfo<User>()
            {
                Message = "成功",
                ResultState = ResultState.Success,
                Data = user
            };
        }
    }
}