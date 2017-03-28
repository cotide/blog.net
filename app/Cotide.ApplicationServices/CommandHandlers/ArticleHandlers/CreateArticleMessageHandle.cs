//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：CreateArticleMessageHandle.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/16 0:13:42 
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
using Cotide.Tasks.Commands.ArticleCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.ArticleHandlers
{
    public class CreateArticleMessageHandle : ICommandHandler<CreateArticleMessageCommand>
    {
        protected readonly IArticleRepository ArticleRepository;
        protected readonly IArticleMessageRepository ArticleMessageRepository;
        protected readonly IUserRepository UserRepository;

        public CreateArticleMessageHandle(
            IUserRepository userRepository,
            IArticleRepository articleRepository, 
            IArticleMessageRepository articleMessageRepository)
        {
            UserRepository = userRepository;
            ArticleRepository = articleRepository;
            ArticleMessageRepository = articleMessageRepository;
        }


        public void Handle(CreateArticleMessageCommand command)
        {
            var article = ArticleRepository.Load(command.ArticleId);
            Guard.IsNotNull(article, "article");
            // 用户评论
            if (command.UserId != null)
            { 
                // 文章留言
                var user = UserRepository.Get((int)command.UserId);
                Guard.IsNotNull(user, "user");

                // 用户留言
                var userReply = new ArticleMessage()
                {
                    Content = HttpUtility.HtmlEncode(command.Content),
                    Article = article,
                    CreateDate = DateTime.Now,
                    IsShow = command.IsShow,
                    User = user,
                     LastDateTime =DateTime.Now
                };
                ArticleMessageRepository.Save(userReply); 
            }
            else
            {
                // 匿名用户留言
                var reply = new ArticleMessage()
                {
                    Content = HttpUtility.HtmlEncode(command.Content),
                    Email = command.Email,
                    Article = article,
                    NickName = command.NickName,
                    WebSiteUrl = command.WebSiteUrl,
                    CreateDate = DateTime.Now,
                    IsShow = command.IsShow, 
                    LastDateTime=DateTime.Now
                }; 
                ArticleMessageRepository.Save(reply); 
            } 
               
        }

    }
}
