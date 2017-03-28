//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UpdateArticleHandle.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/1/10 22:44:57 
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
using Cotide.Framework.Exceptions;
using Cotide.Framework.Utility;
using Cotide.Tasks.Commands.ArticleCommands;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.CommandHandlers.ArticleHandlers
{
    public class UpdateArticleHandle : ICommandHandler<UpdateArticleCommand>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleTagRepository _articleTagRepository;
        private readonly IArticleTypeRepository _articleTypeRepository;
        private readonly IUserRepository _userRepository;

        public UpdateArticleHandle(
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

        public void Handle(UpdateArticleCommand command)
        {

            var article = _articleRepository.Get(command.ArticleId);
            Guard.IsNotNull(article, "没有该文章");
            if (command.IsCheckUserState && article.User.Id != command.UserId)
            {
                throw new PowerException("非法操作");
            }
            if (!string.IsNullOrEmpty(command.Title))
            {
                article.Title = HttpUtility.HtmlEncode(command.Title);
            }
            if (!string.IsNullOrEmpty(command.Content))
            {
                article.Content = HttpUtility.HtmlEncode(command.Content);
            }
            if (command.ReadCount > 0)
            {
                article.ReadCount = command.ReadCount;
            }
            if (command.ArticleTagIds != null && command.ArticleTagIds.Count() > 0)
            {
                var articleTags = (from a in _articleTagRepository.FindAll()
                                   where command.ArticleTagIds.Contains(a.Id)
                                   select a).ToList();
                Guard.IsNotNull(articleTags, "找不到该文章标签");
                article.SetArticleTage(articleTags);
            }
            if (command.ArticleTypeId != null && command.ArticleTypeId > 0)
            {
                var articleType = _articleTypeRepository.Load(Convert.ToInt32(command.ArticleTypeId));
                Guard.IsNotNull(articleType, "找不到该文章类型");
                article.ArticleType = articleType;
            }
            else
            {
                article.ArticleType = null;
            }


            if (string.IsNullOrEmpty(command.ContentDesc))
            {
                if (!string.IsNullOrEmpty(command.Content))
                { 
                    article.ContentDesc = "   " + Utils.CutStringBySuffix(Utils.RemoveHtml(command.Content), 0, 300, "...");
                }
            }
            else
            {
                article.ContentDesc = HttpUtility.HtmlEncode(command.ContentDesc);
            }

            if (!string.IsNullOrEmpty(command.UrlQuoteUrl))
            {
                article.UrlQuoteUrl = command.UrlQuoteUrl;
            }

            if (command.IsShow != null)
            {
                article.IsShow = (bool)command.IsShow;
            }

            article.LastDateTime = DateTime.Now;
            _articleRepository.SaveOrUpdate(article);

        }

    }
}
