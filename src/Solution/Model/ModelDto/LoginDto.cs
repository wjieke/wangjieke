using Model.Entity.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ModelDto
{
    /// <summary>
    /// 用户登录响应数据类
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 请求Token
        /// </summary>
        public string Token { get; set; }
    }
}
