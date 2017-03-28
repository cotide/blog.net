using System.ComponentModel;
using System.Security.Principal;
using Cotide.Domain.Enum; 
using UserPrincipal = Cotide.Domain.Contracts.Task.UserPrincipal;

namespace Cotide.Domain.Contracts.Services
{
    /// <summary>
    /// 用户状态管理服务接口
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// 用户登录地址
        /// </summary>
        string UserLogin { get; }

        /// <summary>
        /// 后台用户登录地址
        /// </summary>
        string AdminLogin { get; }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userId">用户ID</param> 
        /// <param name="createPersistentCookies">是否跨游览器保存凭据</param>
        /// <param name="userRole">用户角色</param>
        void SignIn(
            int userId,
            bool createPersistentCookies,
            UserRole userRole = UserRole.User);

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="createPersistentCookies">是否跨游览器保存凭据</param>
        void AdminSingnIn(
            int userId,
            bool createPersistentCookies);


        /// <summary>
        /// 注销
        /// </summary> 
        void SignOut();


        /// <summary>
        /// 是否已经登录
        /// </summary> 
        /// <returns></returns>
        bool IsSignedIn();

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        IIdentity GetCurrentIdentity();

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        UserPrincipal GetCurrentUser();

    }
}
