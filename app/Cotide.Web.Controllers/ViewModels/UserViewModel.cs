using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Enum;

namespace Cotide.Web.Controllers.ViewModels
{
    /// <summary>
    /// 用户视图
    /// </summary>
    public class UserViewModel
    { 
        /// <summary>
        /// ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary> 
        public string UserName { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 博客名称
        /// </summary>
        public string BlogName { get; set; }

        /// <summary>
        /// 博客描述
        /// </summary>
        public string BlogDesc { get; set; }

        /// <summary>
        /// 用户域名
        /// </summary>
        public string Domain { get; set; }
         
        /// <summary>
        /// 用户角色
        /// </summary>
        public UserLoginRole UserRole { get; set; }
    }
}
