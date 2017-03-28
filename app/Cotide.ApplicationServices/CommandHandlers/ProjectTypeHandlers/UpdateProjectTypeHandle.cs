//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UpdateProjectTypeHandle.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/4/7 12:15:29 
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
using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.ProjectTypeCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.ProjectTypeHandlers
{
    public class UpdateProjectTypeHandle : ICommandHandler<UpdateProjectTypeCommand>
    {
        private readonly IProjectTypeRepository _projectTypeRepository;
        private readonly IUserRepository _userRepository;

        public UpdateProjectTypeHandle(
            IUserRepository userRepository, 
            IProjectTypeRepository projectTypeRepository)
        {
            _userRepository = userRepository;
            _projectTypeRepository = projectTypeRepository;
        }

        public void Handle(UpdateProjectTypeCommand command)
        {
            var productType = _projectTypeRepository.Load(command.ProjductTypeId);
            Guard.IsNotNull(productType, "productType");
            if (!string.IsNullOrEmpty(command.TypeName))
            {
                productType.TypeName = command.TypeName;
            }
            if (command.IsShow != null)
            {
                productType.IsShow = bool.Parse(command.IsShow.ToString());
            }
            _projectTypeRepository.SaveOrUpdate(productType);
        }
         
    }
}
