//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ActionPowerAttribute.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/2/19 23:06:01 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cotide.Domain.Contracts.Task;
using Cotide.Domain.Enum;

namespace Cotide.Web.Controllers.Utility.Attr
{
    /// <summary>
    /// 方法权限特性
    /// </summary>
   [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)] 
    internal class ActionPowerAttribute : AuthorizeAttribute
    { 
        internal bool PerformAuthorizeCore(System.Web.HttpContextBase httpContext) { return this.AuthorizeCore(httpContext); }

        public readonly UserLoginRole UserRole;

        /// <summary>
        /// 默认构造函数
        /// </summary> 
        public ActionPowerAttribute(UserLoginRole userRole)
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
            return user != null && (user.UserRole & UserRole) != 0;
        }  

    }
}
