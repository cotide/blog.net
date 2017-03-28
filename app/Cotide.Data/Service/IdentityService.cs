using System;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Contracts.Services;
using Cotide.Domain.Contracts.Task;
using Cotide.Framework.Caching.Manager;
using Cotide.Framework.Config;
using Cotide.Framework.Exceptions;
using Cotide.Framework.Utility;
using Cotide.Domain.Enum;

namespace Cotide.Infrastructure.Service
{
    /// <summary>
    /// 用户状态管理服务
    /// </summary>
    public class IdentityService : IIdentityService
    {
        protected readonly IUserRepository UserRepository; 
        private readonly string _userLogin = ConfigurationManager.AppSettings["UserLogin"]; 
        private readonly string _adminLogin = ConfigurationManager.AppSettings["AdminLogin"];
         
        /// <summary>
        /// 用户登录地址
        /// </summary>
        public string UserLogin
        {
            get { return _userLogin; }
        }

        /// <summary>
        /// 后台用户登录地址
        /// </summary>
        public string AdminLogin
        {
            get { return _adminLogin; }
        }
         
        /// <summary>
        /// 用户凭证加密密钥
        /// </summary>
        protected string Secret = ConfigurationManager.AppSettings["UserSecretKey"];

        public IdentityService(
            IUserRepository userRepository )
        {
            UserRepository = userRepository; 
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userId">用户ID</param> 
        /// <param name="createPersistentCookie">是否跨游览器保存凭据</param>
        /// <param name="userRole">用户角色</param>
        public void SignIn(int userId, bool createPersistentCookie, UserRole userRole = UserRole.User)
        {
            var ticket = CreateTicket(userId.ToString(), GetUserLoginRole(userRole)); 
            FormsAuthentication.SetAuthCookie(ticket, createPersistentCookie);
        }

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="createPersistentCookies">是否跨游览器保存凭据</param>
        public void AdminSingnIn(int userId, bool createPersistentCookies)
        {
            var ticket = CreateTicket(userId.ToString(), UserLoginRole.Admin);
            FormsAuthentication.SetAuthCookie(ticket, createPersistentCookies);
        }

        /// <summary>
        /// 注销
        /// </summary> 
        public void SignOut()
        {  
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// 是否已经登录
        /// </summary> 
        /// <returns></returns>
        public bool IsSignedIn()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public IIdentity GetCurrentIdentity()
        {
            return HttpContext.Current.User.Identity;
        }

        /// <summary>
        /// 跳到登录界面
        /// </summary>
        public void RedirectLogin()
        {
            HttpContext.Current.Response.Redirect("~/Common/LoginHandler"); 
        } 
          
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public UserPrincipal GetCurrentUser()
        {

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var userInfo = GetCurrentIdentityUser();
                return new UserPrincipal(
                    HttpContext.Current.User.Identity, userInfo);
            }
            return null;
        }


        #region Helper

        /// <summary>
        /// 生成包含用户角色的验证票据 (加密处理)
        /// </summary>
        /// <param name="key">用户ID</param>
        /// <param name="userRole">用户角色</param>
        /// <returns></returns>
        private string CreateTicket(
            string key,
            UserLoginRole userRole)
        {
            var ticket = key + "|" + userRole;
            var encryptTicket = CryptTools.Encrypt(ticket, Secret);
            return encryptTicket;
        }

        private UserLoginRole GetUserLoginRole(UserRole  userRole)
        {
            if(userRole == UserRole.User)
            {
                return UserLoginRole.User;
            }
            else if(userRole == UserRole.Admin)
            {
                return UserLoginRole.Admin;
            }
            throw new BusinessException("无效的用户角色!");
        }


        private IdentityUser GetCurrentIdentityUser()
        {
            // 获取验证票
            var ticket = HttpContext.Current.User.Identity.Name;
            var decryptTicket = "";
            try
            {
                // 解密后的验证票
                decryptTicket = CryptTools.Decrypt(ticket, Secret);
            }
            catch (ArgumentException ex)
            {
                //throw new BusinessException("无效的用户凭证");
                SignOut();
                RedirectLogin();
            } 

            var userContent = decryptTicket.Split('|');
            if (userContent.Count() <= 0 || userContent.Count() != 2)
            {
               // throw new BusinessException("无效的用户凭证");
                SignOut();
                RedirectLogin(); 
            }

           
            var userRole = (UserLoginRole)Enum.Parse(typeof(UserLoginRole), userContent[1]);
            /*if (userRole == UserLoginRole.Admin)
            {
                throw new BusinessException("暂不支持管理员凭证");
              
            }*/
            if (userRole == UserLoginRole.User || userRole == UserLoginRole.Admin)
            {
                // 用户
                var userId = int.Parse(userContent[0]);
                return UserRepository.FindAll().Where(m => m.Id == userId).
                    Select(user => new IdentityUser
                                       {
                                           CreateDate = user.CreateDate,
                                           ID = user.Id,
                                           RealName = user.RealName,
                                           UserName = user.UserName,
                                           Domain = user.Domain,
                                           BlogDesc = user.BlogDesc,
                                           BlogName = user.BlogName,
                                           UserLoginRole =GetUserLoginRole(user.UserRole)
                                       }).FirstOrDefault();
            }
            
            SignOut();
            throw new BusinessException("无效的用户凭证");
        } 

        #endregion
    }
}
