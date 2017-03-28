//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：AdminHandle.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/6 11:56:00 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  

using System;
using System.Linq;
using Cotide.Domain;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Framework.Config;
using Cotide.Framework.Exceptions;
using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.UserCommands;
using SharpArch.Domain.Commands;
using Cotide.Domain.Enum;

namespace Cotide.Tasks.CommandHandlers.UserHandlers
{
    /// <summary>
    /// 创建用户
    /// </summary>
    public class CreateUserHandle : ICommandHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int Handle(CreateUserCommand command)
        {

            var admin = (from a in _userRepository.FindAll()
                         where a.UserName == command.UserName ||a.RealName == command.RealName
                         select a).SingleOrDefault();
            if (admin != null)
            {
                throw new BusinessException("创建用户失败：已存在相同的用户帐号/用户昵称");
            } 
            if(string.IsNullOrEmpty(command.Domain))
            {
                command.Domain = command.UserName;
            }

            var user = _userRepository.Save(new User()
                                      { 
                                          Card = command.Card, 
                                          CreateDate = DateTime.Now,
                                          Domain = command.Domain,
                                          Email = command.Email,
                                          EmailValidate = command.EmailValidate,
                                          ImgHead = command.ImgHead,
                                          LastLoginDate = null,
                                          LastLoginIp = "",
                                          LastDateTime = DateTime.Now,
                                          LoginDate = null,
                                          LoginIp = "",
                                          Paw = CryptTools.Md5(command.Paw),
                                          DesPassword = CryptTools.Encrypt(command.Paw, CacheKeys.ECLOGIN_PASSWORD_SECRET),
                                          Phone = command.Phone,
                                          QQ = command.QQ,
                                          RealName = command.RealName,
                                          SmallImgHead = command.SmallImgHead,
                                          StandardImgHead = command.StandardImgHead,
                                          UserName = command.UserName, 
                                          UserSex = command.UserSex,
                                          UserState = command.UserState, 
                                          BlogName = command.BlogName,
                                          BlogDesc = command.BlogDesc,
                                          EnRealName = command.EnRealName,
                                          UserRole = UserRole.User
                                      });
            return user.Id;
        }

    }
}
