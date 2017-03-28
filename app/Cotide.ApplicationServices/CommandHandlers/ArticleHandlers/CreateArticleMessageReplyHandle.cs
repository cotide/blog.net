//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：CreateArticleMessageReplyHandle.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/16 21:10:48 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  

using System;
using System.Web;
using Cotide.Domain;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.ArticleCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.ArticleHandlers
{
    public class CreateArticleMessageReplyHandle : ICommandHandler<CreateArticleMessageReplyCommand>
    {

        protected readonly IArticleMessageRepository ArticleMessageRepository;
        //protected readonly IArticleReplyMessageRepository ArticleReplyMessageRepository;
        protected readonly IUserRepository UserRepository;

        public CreateArticleMessageReplyHandle( 
            IUserRepository userRepository, 
            //IArticleReplyMessageRepository articleReplyMessageRepository, 
            IArticleMessageRepository articleMessageRepository)
        { 
            UserRepository = userRepository;
           // ArticleReplyMessageRepository = articleReplyMessageRepository;
            ArticleMessageRepository = articleMessageRepository;
        }

        public void Handle(CreateArticleMessageReplyCommand command)
        {

            var articleMessage = ArticleMessageRepository.Get(command.ArticleMessageId);
            Guard.IsNotNull(articleMessage, "articleMessage");
 
            if(command.UserId!=null)
            {
                // 用户留言回复
                var user = UserRepository.Get((int)command.UserId);
                Guard.IsNotNull(user,"user");
                //// 用户留言
                //var reply = new ArticleReplyMessage()  
                //{
                //    Content = HttpUtility.HtmlEncode(command.Content),
                //    CreateDate = DateTime.Now,
                //    IsShow = command.IsShow,
                //    User = user  
                //    //ArticleMessage = articleMessage
                //};
                // 用户留言
                var reply = new ArticleMessage()
                {
                    Content = HttpUtility.HtmlEncode(command.Content),
                    CreateDate = DateTime.Now,
                    IsShow = command.IsShow,
                    User = user ,
                    Article =  articleMessage.Article, 
                    BaseArticleMessage = articleMessage,
                     LastDateTime =DateTime.Now
                };
               // articleMessage.AddArticleMessage(reply);


                //if(command.BaseArticleReplyMessageId!=null)
                //{
                //    var baseArticleReplyMessage =
                //        ArticleReplyMessageRepository.Get(int.Parse(command.BaseArticleReplyMessageId.ToString()));
                //    Guard.IsNotNull(baseArticleReplyMessage, "baseArticleReplyMessage");
                //    reply.BaseArticleReplyMessage = baseArticleReplyMessage;
                //}
              //  articleMessage.ArticleReplyMessages.Add(reply);
                ArticleMessageRepository.Save(reply); 
            }
            else
            {
                var reply = new ArticleMessage()
                                {
                                    NickName = command.NickName,
                                    Email = command.Email,
                                    WebSiteUrl = command.WebSiteUrl,
                                    Content = HttpUtility.HtmlEncode(command.Content),
                                    CreateDate = DateTime.Now,
                                    IsShow = command.IsShow ,
                                    BaseArticleMessage = articleMessage, 
                                    Article = articleMessage.Article,
                                     LastDateTime =DateTime.Now
                                };
                // articleMessage.AddArticleReplyMessage(reply);
                ArticleMessageRepository.Save(reply); 
            }  

        }
         
    }
}
