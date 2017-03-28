using System;
using System.ComponentModel;
using System.Configuration;
using System.Security.Principal;
using System.Web.Mvc;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Services;
using Cotide.Domain.Contracts.Task;
using Cotide.Domain.Dtos;
using Cotide.Framework.Caching.Manager;
using Cotide.Framework.Config;
using Cotide.Framework.Logging;
using Cotide.Framework.Utility; 
using Cotide.Web.Controllers.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System.Text;

namespace Cotide.Web.Controllers.Controllers
{

    /// <summary>
    /// 父类控制器
    /// </summary>
    public abstract class BaseController : Controller
    {

        protected readonly IIdentityService IdentityService;
        protected readonly IUserQueryService UserQueryService;
        protected readonly ILogServer LogServer;


        /// <summary>
        /// 显示注入
        /// </summary>
        protected BaseController()
            : this(ServiceLocator.Current.GetInstance<IIdentityService>(),
                   ServiceLocator.Current.GetInstance<IUserQueryService>(),
                   ServiceLocator.Current.GetInstance<ILogServer>())
        {
        }

        protected BaseController(
          IIdentityService identityService,
          IUserQueryService userQueryService,
          ILogServer logServer)
        {
            IdentityService = identityService;
            UserQueryService = userQueryService;
            LogServer = logServer;
        }



        /// <summary>
        /// 当前登录用户
        /// </summary>
        protected virtual UserViewModel CurrentUser
        {
            get
            {
                var user = HttpContext.User as UserPrincipal;
                return user == null ? null : CreateUserViewModel(user);
            }
        }

        /// <summary>
        /// 判断当前用户是否已登录
        /// </summary>
        protected virtual bool IsLogin
        {
            get { return CurrentUser != null; }

        }
         
        /// <summary>
        /// 当前游览的用户 
        /// </summary>
        protected virtual HistoryUserViewModel HistoryUser
        {
            get
            {
                if (HttpContext.Request.RequestContext.RouteData == null)
                {
                    return null;
                }
                var domain = HttpContext.Request.RequestContext.RouteData.Values["domain"];
                if (domain == null)
                {
                    return null;
                }
                var cacheKey = CacheKeys.HistoryUserKey + domain;
                var user = CacheHelper.GetOrInsert(cacheKey, 600, () => UserQueryService.GetUserByDomain(domain.ToString()));
                if (user == null)
                {
                    return null;
                }
                else
                {
                    TempData["BlogName"] = user.BlogName; 
                    return CreateHistoryUserViewModel(user);
                }
                 
            }
        }


        /// <summary>
        /// 获取头像图片
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected string GetUserHeadImg(string url)
        {
            return string.IsNullOrEmpty(url)
                ? Url.Content(@"~/UploadFile/System/Files/DefaultHead.jpg")
                : url;
        }


        /// <summary>
        /// 弹出窗口页面 返回结果
        /// </summary>
        /// <param name="viewName">返回视图名称</param>
        /// <param name="viewModel">视图数据对象</param>
        /// <param name="dialogType">提示类型</param>
        /// <param name="url">转向页面</param>
        /// <returns></returns>
        protected ActionResult DialogMsgboxResult(
            string viewName,
            object viewModel = null,
            DialogResult dialogType = DialogResult.Right,
            string url = null)
        {
            return Dialog(viewName, dialogType == DialogResult.Right, string.Empty, false, 0, null, viewModel, url);
        }


        /// <summary>
        /// 清除当前登录用户缓存
        /// </summary> 
        protected void ClearNowLoginUserCache()
        {
            var domain = CurrentUser.Domain;
            //// 清除用户缓存
            //var cacheKey = CacheKeys.UserKey + currentUserName;
            //CacheHelper.RemoveCacheObject(cacheKey);
            // 清楚游览用户缓存 
            var historyCacheKey = CacheKeys.HistoryUserKey + domain;
            CacheHelper.RemoveCacheObject(historyCacheKey);
        }


        /// <summary>
        /// 消息提示
        /// </summary>
        /// <param name="msg">提示</param>
        /// <param name="returnUrl">跳转地址</param>
        protected void AlterForRightGoTo(string msg , string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                var scriptStr = new StringBuilder("<script type=\"text/javascript\"  >setTimeout(function(){ ");
                scriptStr.Append(string.Format("window.location.href=\"{0}\";", returnUrl));
                scriptStr.Append("},1000);</script>");
                TempData["alter"] = new MvcHtmlString(string.Format("<div id=\"replyMsg\" class=\"alert alert-success\">{0}</div>{1}", msg, scriptStr)); ;
            }

            TempData["alter"] = new MvcHtmlString(string.Format("<div id=\"replyMsg\" class=\"alert alert-success\">{0}</div>", msg)); 
        }


        /// <summary>
        /// 生成正确提示
        /// </summary>
        /// <param name="msg">提示</param>
        /// <param name="setTimeHide">自动隐藏提示 单位：毫秒</param>
        /// <returns></returns>
        protected void AlterForRight(string msg, int setTimeHide=0)
        {
            if (setTimeHide < 1000)
            {
                setTimeHide = 1000;
            }

            var str = new StringBuilder(string.Format(@"<div id='replyMsg' class='alert alert-success'>{0}</div>", msg));
            str.Append("<script language=\"javascript\"> ");
            str.Append("setTimeout(function(){");
            str.Append("$('#replyMsg').remove();}");
            str.Append(string.Format(",{0}", setTimeHide));
            str.Append(")</script>");  
            TempData["alter"] = new MvcHtmlString(str.ToString());
        }


        /// <summary>
        /// 生成错误提示
        /// </summary>
        /// <param name="msg">消息</param>
        protected void AlterForError(string msg)
        {
            TempData["alter"] = new MvcHtmlString(
                string.Format( "<div id=\"replyMsg\" class=\"alert alert-warning\"><strong>警告!</strong> {0}</div>",
                        msg));
        }


        #region Helper
         
      

        private UserViewModel CreateUserViewModel(UserPrincipal userPrincipal)
        {
            return new UserViewModel()
                       {
                           Domain = userPrincipal.Domain,
                           UserRole = userPrincipal.UserRole,
                           UserId = userPrincipal.UserId,
                           UserName = userPrincipal.UserName,
                           BlogDesc = userPrincipal.BlogDesc,
                           BlogName = userPrincipal.BlogName,
                           RealName = userPrincipal.RealName
                       };
        }

        private HistoryUserViewModel CreateHistoryUserViewModel(UserDto u)
        {
            var historyUserViewModel = new HistoryUserViewModel()
            {
                Domain = u.Domain,
                UserId = u.ID,
                UserName = u.UserName,
                RealName = u.RealName,
                ImgHead = GetUserHeadImg(u.ImgHead),
                SmallImgHead = GetUserHeadImg(u.SmallImgHead),
                StandardImgHead = GetUserHeadImg(u.StandardImgHead),
                WeiBoUrl = u.WeiBoUrl,
                BlogDesc = u.BlogDesc,
                BlogName = u.BlogName,
                Sex = u.UserSex,
                Phone = u.Phone,
                Email = u.Email,
                EnRealName = u.EnRealName,
                IsCurrentLoginUser = false
            };
            if (CurrentUser != null && u.ID == CurrentUser.UserId)
            {
                historyUserViewModel.IsCurrentLoginUser = true;
            }
            return historyUserViewModel;
        }


        /// <summary>
        /// 弹出窗口页面 返回结果
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isClose">是否关闭</param>
        /// <param name="colsed">关闭时间</param>
        /// <param name="data">返回数据</param>
        /// <param name="viewModel">视图对象</param>
        /// <param name="success"></param>
        /// <param name="viewName">视图名称</param>
        /// <returns></returns>
        private ActionResult Dialog(
           string viewName,
            bool success,
            string message,
            bool isClose,
            int colsed,
            object data,
            object viewModel = null,
            string url = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = (success ? "操作成功" : "系统繁忙,请稍后再试.");
            }
            ViewData["DialogResult"] = new DialogResultViewModel
            {
                Colsed = colsed,
                Data = data,
                IsClose = isClose,
                Message = message,
                Success = success,
                ReturnUrl = !string.IsNullOrEmpty(url) ? url : ""
            };
            return View(viewName, viewModel);
        }

        #endregion
    }


}
