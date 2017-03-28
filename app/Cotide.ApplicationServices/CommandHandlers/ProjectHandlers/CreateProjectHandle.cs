//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：CreateProjectHandle.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/4/7 12:20:31 
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
using Cotide.Domain;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.ProjectCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.ProjectHandlers
{
    public class CreateProjectHandle : ICommandHandler<CreateProjectCommand>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectTypeRepository _projectTypeRepository;

        public CreateProjectHandle(
            IProjectRepository projectRepository, 
            IUserRepository userRepository,
            IProjectTypeRepository projectTypeRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _projectTypeRepository = projectTypeRepository;
        }

        public void Handle(CreateProjectCommand command)
        {
            var user = _userRepository.Load(command.UserId);
            Guard.IsNotNull(user, "user");
            var projectType = _projectTypeRepository.Load(command.ProductTypeId);
            Guard.IsNotNull(projectType, "projectType");
            var project = new Project
            {
                ProjectName = command.ProjectName,
                Content = HttpUtility.HtmlEncode(command.Content),
                CreateDate = DateTime.Now, 
                IsShow = command.IsShow,
                LastDateTime = DateTime.Now,
                ProjectImg = command.ProjectImg,
                StandardProjectImg = command.StandardProjectImg,
                SmallProjectImg = command.SmallProjectImg,
                User = user,
                WebSite = command.WebSite,
                ProjectType = projectType,
                Sort = GetTypeMaxSort()
            };

            if (string.IsNullOrEmpty(command.Introduction))
            {
                if (command.Content != null)
                {
                    project.Introduction =  Utils.CutStringBySuffix(Utils.RemoveHtml(command.Content), 0, 300, "...");
                }
            }
            else
            {
                project.Introduction = HttpUtility.HtmlEncode(command.Introduction);
            }

            _projectRepository.Save(project);
        }

        private int GetTypeMaxSort()
        {
            var resut = (from project in _projectRepository.FindAll()
                         orderby project.Sort descending
                         select project).FirstOrDefault();

            if (resut != null)
            {
                if (resut.Sort != null)
                    return Convert.ToInt32(resut.Sort);
                return 1;
            }
            return 1;
        }

    }
}
