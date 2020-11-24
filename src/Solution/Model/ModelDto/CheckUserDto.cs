using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ModelDto
{
    /// <summary>
    /// 用户登录验证类
    /// </summary>
    public class CheckUserDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string VeriCode { get; set; }
    }
}
