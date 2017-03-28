using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Services;
using Cotide.Web.Controllers.Common.ViewModel;

namespace Cotide.Web.Controllers.Common
{
    /// <summary>
    /// 公共Controller
    /// </summary>
    public class CommonController : Controller
    {
        private readonly IUserQueryService _userQueryService; 
        protected readonly IIdentityService IdentityService;
         

        public CommonController(IUserQueryService userQueryService, 
            IIdentityService identityService)
        {
            this._userQueryService = userQueryService;
            IdentityService = identityService;
        }

        /// <summary>
        /// 检查管理员是否存在
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckAdmin(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Json(new AjaxViewModel()
                {
                    IsRight = false,
                    Msg = "非法的用户帐号"
                }, JsonRequestBehavior.AllowGet);
            } 
            var admin = _userQueryService.FindOne(value);
            if (admin != null)
            {
                return Json(new AjaxViewModel()
                {
                    IsRight = false,
                    Msg = "已经存在该用户帐号"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new AjaxViewModel()
            {
                IsRight = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult LoginHandler()
        {  
            if (!IdentityService.IsSignedIn())
            {
                string loginUrl;
                string returnUrl = HttpContext.Request.Params["returnUrl"];
                if (returnUrl!=null &&  returnUrl.Contains("/SiteManager/"))
                {
                    loginUrl = IdentityService.AdminLogin;
                }
                else
                {
                    loginUrl = IdentityService.UserLogin;
                } 

                if (!string.IsNullOrEmpty(HttpContext.Request.Params["returnUrl"]))
                {
                    loginUrl = loginUrl + "?returnUrl=" + HttpUtility.UrlEncode(HttpContext.Request.Params["returnUrl"]);
                }
                return Redirect(loginUrl);
            }
            return Redirect(Url.Action("Login", "Reg"));
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckUser(string value)
        {
            var user = _userQueryService.FindOne(value);
            if (user != null)
            {
                return Json(new AjaxViewModel()
                {
                    IsRight = false,
                    Msg = "已经存在该用户名"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new AjaxViewModel()
            {
                IsRight = true
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 检查用户昵称是否存在
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckUserRealName(string value)
        {
            var user = _userQueryService.FindOneByNiceName(value);
            if (user != null)
            {
                return Json(new AjaxViewModel()
                {
                    IsRight = false,
                    Msg = "已经存在该昵称"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new AjaxViewModel()
            {
                IsRight = true
            }, JsonRequestBehavior.AllowGet);
        }

    }

}
