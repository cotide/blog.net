//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：DeleteAdminHandle.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/6 11:57:43 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  

using Cotide.Domain.Contracts.Repositories;
using Cotide.Tasks.Commands.UserCommands;
using SharpArch.Domain.Commands;
using System.Linq;
using Cotide.Domain.Enum;

namespace Cotide.Tasks.CommandHandlers.UserHandlers
{
    /// <summary>
    /// 删除用户
    /// </summary>
    public class DeleteUserHandle : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _adminRepository;

        public DeleteUserHandle(IUserRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public void Handle(DeleteUserCommand command)
        {
            if (command.UserIds != null && command.UserIds.Count() > 0)
            {
                foreach (var id in command.UserIds)
                {
                    var admin = _adminRepository.Get(id); 
                    _adminRepository.Delete(admin); 
                }
            }
        } 
       
    }
}
