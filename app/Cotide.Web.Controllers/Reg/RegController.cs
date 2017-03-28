//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：RegController.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/4 22:14:46 
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
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Services;
using Cotide.Framework.Commands;
using Cotide.Framework.Exceptions;
using Cotide.Tasks.Commands.UserCommands;
using Cotide.Web.Controllers.Controllers;
using Cotide.Web.Controllers.Reg.ViewModel;
using Cotide.Web.Controllers.ViewModels.User;
using SharpArch.NHibernate.Web.Mvc;
using Cotide.Domain.Enum;

namespace Cotide.Web.Controllers.Reg
{
    public class RegController : Controller
    {
        protected readonly IIdentityService IdentityService;
        protected readonly IUserQueryService UserQueryService; 
        protected readonly ICommandProcessor Processor;

        #region IOC注入
        public RegController(
            IIdentityService identityService,
            IUserQueryService userQueryService,
            ICommandProcessor processor)
        {
            IdentityService = identityService;
            UserQueryService = userQueryService; 
            Processor = processor;
        }
        #endregion


        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [Transaction]
        public ActionResult Login(
            LoginViewModel model,  
            string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = UserQueryService.FindOne(model.UserName, model.Paw);
                if (user != null)
                {
                    IdentityService.SignIn(user.ID, true,user.UserRole);
                    return Redirect(Url.Action("Index", "Blog", new { @domain = user.Domain }));
                }

                ModelState.AddModelError("", @"用户/密码有误");
                return View();
            }
            ModelState.AddModelError("", @"非法的格式");
            return View();
        }
         
       
        /// <summary>
        ///用户退出
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            IdentityService.SignOut();
            return Redirect(Url.Action("Login"));
        }

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private MvcHtmlString GetErrorMsg(string msg)
        {
            var str = new StringBuilder("<div id=\"replyMsg\" class=\"alert alert-error\" >");
            str.AppendFormat(" <strong>警告!</strong>{0}", msg);
            str.Append("</div>");
            return new MvcHtmlString(str.ToString());
        } 
   
    }
}
