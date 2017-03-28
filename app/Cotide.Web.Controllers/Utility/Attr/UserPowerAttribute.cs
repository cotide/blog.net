//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UserAuthorize.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/12/4 10:54:27 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  

using System;
using System.Web.Mvc;
using Cotide.Domain.Contracts.Task;
using Cotide.Domain.Enum;

namespace Cotide.Web.Controllers.Utility.Attr
{
    /// <summary>
    /// 用户权限特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    internal class UserPowerAttribute : AuthorizeAttribute
    { 
        internal bool PerformAuthorizeCore(System.Web.HttpContextBase httpContext) { return this.AuthorizeCore(httpContext); }

        public readonly UserLoginRole UserRole;

        /// <summary>
        /// 默认构造函数
        /// </summary> 
        public UserPowerAttribute(UserLoginRole userRole)
        {
            UserRole = userRole;
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="httpContext">当前上下文</param>
        /// <returns></returns>
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;
            var user = httpContext.User as UserPrincipal;
            return user != null && UserRole.HasFlag(user.UserRole);
        }  

    }
}
