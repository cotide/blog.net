//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：AdminQueryService.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/10/6 12:43:47 
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
using Cotide.Domain;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories.Extension;
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;
using Cotide.Framework.Utility;

namespace Cotide.QueryServices
{
    /// <summary>
    /// 查询管理员服务
    /// </summary>
    public class UserQueryService : IUserQueryService
    {

        protected readonly IDbProxyRepository<User> UserDbProxyRepository;

        public UserQueryService(IDbProxyRepository<User> userDbProxyRepository)
        {
            UserDbProxyRepository = userDbProxyRepository;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="id">管理员ID</param>
        /// <returns></returns>
        public UserDto FindOne(int id)
        {
            var query = (from user in UserDbProxyRepository.FindAll()
                         where user.Id == id
                         select CreateUserDto(user));
            return query.SingleOrDefault();
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public UserDto FindOne(string userName)
        {
            var query = (from user in UserDbProxyRepository.FindAll()
                         where user.UserName == userName
                         select CreateUserDto(user));
            return query.SingleOrDefault();
        }

        /// <summary>
        /// 根据域名获取用户实体
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns></returns>
        public UserDto GetUserByDomain(string domain)
        {
            var user = (from u in UserDbProxyRepository.FindAll()
                        where u.Domain == domain
                        select u).SingleOrDefault();
            return user != null ? CreateUserDto(user) : null;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="paw">密码</param>
        /// <returns></returns>
        public UserDto FindOne(string userName, string paw)
        {
            var query = (from user in UserDbProxyRepository.FindAll()
                         where user.UserName == userName
                         && user.Paw == CryptTools.Md5(paw)
                         select CreateUserDto(user));
            return query.SingleOrDefault();
        }

        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public PagedList<UserDto> FindAllPager(
            string userName,
            string realName,
            int pageIndex,
            int pageSize)
        {
            var query = UserDbProxyRepository.FindAll();
            if (!string.IsNullOrEmpty(userName))
            {
                query = query.Where(x => x.UserName.Contains(userName));
            }
            if (!string.IsNullOrEmpty(realName))
            {
                query = query.Where(x => x.RealName.Contains(realName));
            }
            return query.ToPagedList(pageIndex, pageSize)
                .Select(CreateUserDto)
                .ToPagedList(pageIndex,pageSize);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="niceName">用户昵称</param>
        /// <returns></returns>
        public UserDto FindOneByNiceName(string niceName)
        {

            var query = (from user in UserDbProxyRepository.FindAll()
                         where user.RealName == niceName
                         select CreateUserDto(user));
            return query.SingleOrDefault();
        }

        #region Helper
        private UserDto CreateUserDto(User domain)
        {
            return new UserDto()
                       {
                           ID = domain.Id,
                           //AdminLevel = domain.AdminLevel,
                           //AdminPower = domain.AdminPower,
                           UserRole = domain.UserRole,
                           Card = domain.Card,
                           CreateDate = domain.CreateDate,
                           Domain = domain.Domain,
                           Email = domain.Email,
                           EmailValidate = domain.EmailValidate,
                           ImgHead = domain.ImgHead,
                           LastLoginDate = domain.LastLoginDate,
                           LastLoginIp = domain.LastLoginIp,
                           LastUpdate = domain.LastDateTime,
                           LoginDate = domain.LoginDate,
                           LoginIp = domain.LoginIp,
                           Phone = domain.Phone,
                           QQ = domain.QQ,
                           WeiBoUrl =  domain.WeiBoUrl,
                           RealName = domain.RealName,
                           SmallImgHead = domain.SmallImgHead,
                           StandardImgHead = domain.StandardImgHead,
                           UserName = domain.UserName, 
                           UserSex = domain.UserSex,
                           UserState = domain.UserState ,
                           BlogDesc = domain.BlogDesc,
                           BlogName = domain.BlogName,
                           EnRealName = domain.EnRealName
                       };
        }
        #endregion
    }
}
