//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UpdateProject.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/4/7 12:22:52 
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
using System.Web;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.ProjectCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.ProjectHandlers
{
    public class UpdateProjectHandle : ICommandHandler<UpdateProjectCommand>
	{
       private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectTypeRepository _projectTypeRepository;

        public UpdateProjectHandle(
            IProjectRepository projectRepository, 
            IUserRepository userRepository,
            IProjectTypeRepository projectTypeRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _projectTypeRepository = projectTypeRepository;
        }

        public void Handle(UpdateProjectCommand command)
        {
           var project = _projectRepository.Load(command.ProjectId);
            Guard.IsNotNull(project, "project");
            if (command.ProjectTypeId != null)
            {
                var projectType = _projectTypeRepository.Get(int.Parse(command.ProjectTypeId.ToString()));
                Guard.IsNotNull(projectType, "projectType");
                project.ProjectType = projectType;
            }
            if (!string.IsNullOrEmpty(command.ProjectName))
            {
                project.ProjectName = command.ProjectName;
            }
            if (!string.IsNullOrEmpty(command.ProjectImg))
            {
                project.ProjectImg = command.ProjectImg;
            }
            if(!string.IsNullOrEmpty(command.StandardProjectImg))
            {
                project.StandardProjectImg = command.StandardProjectImg;
            }
            if(!string.IsNullOrEmpty(command.SmallProjectImg))
            {
                project.SmallProjectImg = command.SmallProjectImg;
            }
            if (!string.IsNullOrEmpty(command.WebSite))
            {
                project.WebSite = command.WebSite;
            }



            if (string.IsNullOrEmpty(command.Introduction))
            {
                if (!string.IsNullOrEmpty(command.Content))
                {
                    project.Introduction = "   " + Utils.CutStringBySuffix(Utils.RemoveHtml(command.Content), 0, 300, "...");
                }
            }
            else
            {
                project.Introduction = HttpUtility.HtmlEncode(command.Introduction);
            }

            if (command.IsShow != null)
            {
                project.IsShow = bool.Parse(command.IsShow.ToString());
            }
            if (!string.IsNullOrEmpty(command.Content))
            {
                project.Content = HttpUtility.HtmlEncode(command.Content);
            }
            if(command.Sort!=null && command.Sort>0)
            {
                project.Sort = command.Sort;
            }
            project.LastDateTime = DateTime.Now;
            _projectRepository.SaveOrUpdate(project);
        }
         
	}
}
