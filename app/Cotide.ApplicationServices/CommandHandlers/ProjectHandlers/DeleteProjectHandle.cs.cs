//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：DeleteProjectHandle.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/4/7 12:24:30 
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
using Cotide.Tasks.Commands.ProjectCommands;
using SharpArch.Domain.Commands;
using Cotide.Framework.Utility;

namespace Cotide.Tasks.CommandHandlers.ProjectHandlers
{
   public  class DeleteProjectHandle : ICommandHandler<DeleteProjectCommand>
    {
      private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectTypeRepository _projectTypeRepository;

        public DeleteProjectHandle(
            IProjectRepository projectRepository, 
            IUserRepository userRepository,
            IProjectTypeRepository projectTypeRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _projectTypeRepository = projectTypeRepository;
        }

        public void Handle(DeleteProjectCommand command)
        {
            var project =  _projectRepository.Get(command.ProductId);
            Guard.IsNotNull(project,"project");
            _projectRepository.Delete(project);
        }
        
    }
}
