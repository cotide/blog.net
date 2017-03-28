//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：HandleErrorWithELMAHAttribute.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/12/3 21:49:40 
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
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Cotide.Framework.Exceptions;
using Cotide.Framework.Logging;
using Microsoft.Practices.ServiceLocation;
using log4net.Core;

namespace Cotide.Framework.Attr
{
    /// <summary>
    /// 异常处理 特性
    /// </summary>
    public class HandleErrorWithELMAHAttribute : HandleErrorAttribute
    {

        protected readonly ILogServer LoggerServer;

         

        /// <summary>
        /// 显示注入
        /// </summary>
        public HandleErrorWithELMAHAttribute()
            : this(ServiceLocator.Current.GetInstance<ILogServer>())
        {
        }


        public HandleErrorWithELMAHAttribute(ILogServer loggerServer)
        {
            LoggerServer = loggerServer;
        }



        /// <summary>
        /// 重写OnException方法
        /// </summary>
        /// <param name="context">异常上下文</param>
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var e = context.Exception;
            //if (!context.ExceptionHandled) 
            //    return; 
            // 记录日志
            LogException(e); 
            if ((e is PowerException) || (e is BusinessException))
            {
                HttpContext.Current.Response.Redirect(@"~/Error/Error500?errorMsg=" + e.Message);
            }
            else
            {
                HttpContext.Current.Response.Redirect(@"~/Error/Error500?errorMsg=服务器500错误");
            }
        }

        /// <summary>
        /// 记录日志方法
        /// </summary>
        /// <param name="e">异常</param>
        private void LogException(Exception e)
        {
            var context = HttpContext.Current;
            LoggerServer.Error("全局错误日志", e.Message);
        }
    }
}
