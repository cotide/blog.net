//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UpdateAdminHandle.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/10/6 11:57:12 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  

using System;
using System.Web;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Enum;
using Cotide.Framework.Config;
using Cotide.Tasks.Commands.UserCommands;
using SharpArch.Domain.Commands;
using Cotide.Framework.Utility;

namespace Cotide.Tasks.CommandHandlers.UserHandlers
{
    /// <summary>
    /// 更新用户
    /// </summary>
    public class UpdateUserHandle : ICommandHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(UpdateUserCommand command)
        {
            var user = _userRepository.Get(command.UserId);
            Guard.IsNotNull(user, "user");
            #region /// 更新字段值
            if (!string.IsNullOrEmpty(command.RealName))
            {
                user.RealName = command.RealName;
            }
            if (!string.IsNullOrEmpty(command.ImgHead))
            {
                user.ImgHead = command.ImgHead;
            }
            if (!string.IsNullOrEmpty(command.SmallImgHead))
            {
                user.SmallImgHead = command.SmallImgHead;
            }
            if (!string.IsNullOrEmpty(command.StandardImgHead))
            {
                user.StandardImgHead = command.StandardImgHead;
            }
            user.Email = command.Email;
            user.Phone = command.Phone;
            user.EnRealName = command.EnRealName;
            if (command.Sex != null)
            {
                user.UserSex = command.Sex;
            }
            if (command.EmailValidate != null)
            {
                user.EmailValidate = command.EmailValidate;
            }

            if (!string.IsNullOrEmpty(command.Paw))
            {
                user.Paw = CryptTools.Md5(command.Paw);
            }
            if (command.UserRole != null)
            {
                user.UserRole = (UserRole)command.UserRole;
            }
            if (command.UserState != null)
            {
                user.UserState = (UserState)command.UserState;
            }

            if (!string.IsNullOrEmpty(command.Domain))
            {
                user.Domain = command.Domain;
            }

            if (command.LoginDate != null)
            {
                user.LoginDate = command.LoginDate;
            }
            if (command.LastLoginDate != null)
            {
                user.LastLoginDate = command.LastLoginDate;
            }
            if (!string.IsNullOrEmpty(command.LoginIp))
            {
                user.LoginIp = command.LoginIp;
            }
            if (!string.IsNullOrEmpty(command.LastLoginIp))
            {
                user.LastLoginIp = command.LastLoginIp;
            }
            if (!string.IsNullOrEmpty(command.BlogName))
            {
                user.BlogName = command.BlogName;
            }
            user.BlogDesc = HttpUtility.HtmlEncode(command.BlogDesc);

            if (!string.IsNullOrEmpty(command.WeiBoUrl))
            {
                user.WeiBoUrl = command.WeiBoUrl;
            }
            user.LastDateTime = DateTime.Now;
            #endregion
            _userRepository.SaveOrUpdate(user);
        }


    }
}
