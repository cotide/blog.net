//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：DeleteArticleTagHandle.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/1/16 21:23:55 
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
using Cotide.Tasks.Commands.ArticleTagCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.ArticleTagHandlers
{
    public class DeleteArticleTagHandle : ICommandHandler<DeleteArticleTagCommand>
    {
        private readonly IArticleTagRepository _articleTagRepository;

        public DeleteArticleTagHandle(
            IArticleTagRepository articleTagRepository
            )
        {
            _articleTagRepository = articleTagRepository;
        }
         
        public void Handle(DeleteArticleTagCommand command)
        {
            var articleTag = _articleTagRepository.Load(command.ArticleTagId);
            Guard.IsNotNull(articleTag, "articleType");
            _articleTagRepository.Delete(articleTag);
        }  
    }
}
