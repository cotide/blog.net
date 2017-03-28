//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：DeleteProjectTypeHandle.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/4/7 12:13:41 
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
using Cotide.Domain.Contracts.Repositories;
using Cotide.Tasks.Commands.ProjectTypeCommands;
using SharpArch.Domain.Commands;
using Cotide.Framework.Utility;

namespace Cotide.Tasks.CommandHandlers.ProjectTypeHandlers
{
    public class DeleteProjectTypeHandle : ICommandHandler<DeleteProjectTypeCommand>
    {
        private readonly IProjectTypeRepository _projectTypeRepository; 
        private readonly IUserRepository _userRepository;

        public DeleteProjectTypeHandle(IUserRepository userRepository, 
            IProjectTypeRepository projectTypeRepository)
        {
            _userRepository = userRepository;
            _projectTypeRepository = projectTypeRepository;
        }


        public void Handle(DeleteProjectTypeCommand command)
        {
            var projectType = _projectTypeRepository.Get(command.ProductTypeId);
            Guard.IsNotNull(projectType, "projectType");
            _projectTypeRepository.Delete(projectType);
        }

    }
}
