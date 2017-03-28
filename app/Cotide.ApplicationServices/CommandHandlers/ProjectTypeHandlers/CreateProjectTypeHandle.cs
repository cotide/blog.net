//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：CreateProjectTypeHandle.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/4/7 12:11:57 
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
using Cotide.Domain.Contracts.Repositories;
using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.ProjectTypeCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.ProjectTypeHandlers
{
    public class CreateProjectTypeHandle : ICommandHandler<CreateProjectTypeCommand>
    {
        private readonly IProjectTypeRepository _projectTypeRepository;

        private readonly IUserRepository _userRepository;

        public CreateProjectTypeHandle(IProjectTypeRepository projectTypeRepository, IUserRepository userRepository)
        {
            _projectTypeRepository = projectTypeRepository;
            _userRepository = userRepository;
        }
        
        public void Handle(CreateProjectTypeCommand command)
        {
            var user = _userRepository.Load(command.UserId);
            Guard.IsNotNull(user, "user");
            var projectType = new ProjectType()
            {
                IsShow = command.IsShow,
                TypeName = command.TypeName,
                User = user
            };
            _projectTypeRepository.Save(projectType);
        }
         
    }
}
