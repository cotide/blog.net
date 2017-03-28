//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：CreateArticleHandler.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/1/10 21:48:02 
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
    public class CreateArticleHandle : ICommandHandler<CreateArticleCommand,int>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleTagRepository _articleTagRepository;
        private readonly IArticleTypeRepository _articleTypeRepository;
        private readonly IUserRepository _userRepository;

        public CreateArticleHandle(
            IArticleRepository articleRepository, 
            IArticleTagRepository articleTagRepository, 
            IArticleTypeRepository articleTypeRepository, 
            IUserRepository userRepository)
        {
            _articleRepository = articleRepository;
            _articleTagRepository = articleTagRepository;
            _articleTypeRepository = articleTypeRepository;
            _userRepository = userRepository;
        }

        public int Handle(CreateArticleCommand command)
        {
            var user = _userRepository.Load(command.UserId);
            Guard.IsNotNull(user, "没有该用户");
            
            
            
            var articleTags = new List<ArticleTag>();
            if (command.ArticleTagIds != null)
            {
                articleTags = (from a in _articleTagRepository.FindAll()
                               where command.ArticleTagIds.Contains(a.Id)
                               select a).ToList();
            }

            var article = new Article()
            { 
                Content = HttpUtility.HtmlEncode(command.Content),
                IsShow = command.IsShow,
                ReadCount = 0,
                Title = HttpUtility.HtmlEncode(command.Title),
                UrlQuoteUrl = command.UrlQuoteUrl,
                User = user,
                LastDateTime = DateTime.Now,
                CreateDate = DateTime.Now
            };

            if(string.IsNullOrEmpty(command.ContentDesc))
            {
                if (command.Content != null)
                { 
                    article.ContentDesc = "   " + Utils.CutStringBySuffix(Utils.RemoveHtml(command.Content), 0, 300, "...");
                }
            }
            else
            {
                article.ContentDesc = HttpUtility.HtmlEncode(command.ContentDesc);
            }
             
            if(command.ArticleTypeId!=null && command.ArticleTypeId>0)
            {
                var articleType = _articleTypeRepository.Load(Convert.ToInt32(command.ArticleTypeId));
                Guard.IsNotNull(articleType, "没有该文章类型");
                article.ArticleType = articleType;
            }
              
            article.SetArticleTage(articleTags);
            var result =  _articleRepository.Save(article);
            return result.Id;
        }
         
    }
}
