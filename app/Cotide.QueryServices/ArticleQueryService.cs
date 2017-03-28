using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cotide.Domain;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Contracts.Repositories.Extension;
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;
using Cotide.Framework.Enumerable;
using NHibernate.Linq;

namespace Cotide.QueryServices
{
    /// <summary>
    /// 文章查询服务
    /// </summary>
    public class ArticleQueryService : SharpArch.NHibernate.NHibernateQuery, IArticleQueryService
    {
        private readonly IUserRepository _userRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleTypeRepository _articleTypeRepository;



        #region IQC注入
        public ArticleQueryService(
            IArticleRepository articleRepository,
            IArticleTypeRepository articleTypeRepository,
            IUserRepository userRepository)
        {
            _articleRepository = articleRepository;
            _articleTypeRepository = articleTypeRepository;
            _userRepository = userRepository;
        }

        #endregion

        /// <summary>
        /// 获取文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns></returns>
        public ArticleDto FindOne(int articleId)
        {
            var query = (from a in _articleRepository.FindAll()
                         let at = a.ArticleType
                         let u = a.User
                         where a.Id == articleId
                         select CreateArticleDto(a, at, u));
            return query.SingleOrDefault();
        }

        /// <summary>
        /// 获取用户文章列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="articleTypeId">文章类别ID</param>
        /// <param name="tagId">标签ID</param>
        /// <param name="month">月份</param>
        /// <param name="isShow">前端是否显示</param>
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public PagedList<ArticleDto> FindAllPager(
            int? userId,
            int? articleTypeId,
            int? tagId,
            int? year,
            int? month,
            bool? isShow,
            int pageIndex,
            int pageSize)
        {
            /*
          // 构造查询关联对象
          var articles = _articleDbProxyRepository.FindAll();
          var articleTypes = _articleTypeRepository.FindAll();
          var users = _userRepository.FindAll();
          var query = articles.Join(articleTypes, x => x.ArticleType.Id, y => y.Id, (x, y) => x);
          query = query.Join(users, x => x.User.Id, u => u.Id, (x, u) => x);

          // 查询条件
          if (userId != null && userId > 0)
          {
              query = query.Where(x => x.User.Id == userId);
          }
          if (articleTypeId != null && articleTypeId > 0)
          {
              query = query.Where(x => x.ArticleType.Id == articleTypeId);
          }
          if (tagId != null && tagId > 0)
          {
              query = query.Where(x => x.ArticleTags.Any(y => y.Id == tagId));
          }
          if (year != null && year > 0)
          {
              query = query.Where(x => x.CreateDate.Year == year);
          }
          if (month != null && month > 0)
          {
              query = query.Where(x => x.CreateDate.Month == month);
          }
          if (isShow != null)
          {
              query = isShow == true ? query.Where(x => x.IsShow) : query.Where(x => !x.IsShow);
          }
          query = query.OrderByDescending(x => x.CreateDate);
          return query.Select(x => CreateArticleDto(x, x.ArticleType, x.User))
              .ToPagedList(pageIndex, pageSize); 

          */

            // 构造查询关联对象
            var articles = _articleRepository.FindAll();
            var articleTypes = _articleTypeRepository.FindAll();
            var users = _userRepository.FindAll();
            var query = articles.Join(users, x => x.User.Id, u => u.Id, (x, u) => x);
            if (articleTypeId != null && articleTypeId > 0)
            {
                // 关联文章类型
                query = articles.Join(articleTypes, x => x.ArticleType.Id, y => y.Id, (x, y) => x);
            }

            // 查询条件
            if (userId != null && userId > 0)
            {
                query = query.Where(x => x.User.Id == userId);
            }
            if (articleTypeId != null && articleTypeId > 0)
            {
                query = query.Where(x => x.ArticleType.Id == articleTypeId);
            }
            if (tagId != null && tagId > 0)
            {
                query = query.Where(x => x.ArticleTags.Any(y => y.Id == tagId));
            }
            if (year != null && year > 0)
            {
                query = query.Where(x => x.CreateDate.Year == year);
            }
            if (month != null && month > 0)
            {
                query = query.Where(x => x.CreateDate.Month == month);
            }
            if (isShow != null)
            {
                query = isShow == true ? query.Where(x => x.IsShow) : query.Where(x => !x.IsShow);
            }
            query = query.OrderByDescending(x => x.CreateDate);

            var result = query.Select(x => CreateArticleDto(x, x.ArticleType, x.User, Session.Query<ArticleMessage>()
                .Count(r => r.Article.Id == x.Id)));

            return result.ToPagedList(pageIndex, pageSize);
        }



        /// <summary>
        /// 获取用户文章统计数据
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public IList<ArticleDateDto> GetArticleCount(int userId)
        {


            var query = Session.CreateSQLQuery(
                string.Format("select year([CreateDate]) as Year,month([CreateDate]) AS " +
                              "Month ,Count(ArticleId) as Count from Blog_Articles  as ArticleDateDto  where UserId= {0}  and IsShow =1 " +
                              " group by year([CreateDate]) ,month([CreateDate]) order by Year desc,Month desc", userId));
            //var query = Session.CreateSQLQuery("select year([CreateDate]) as Year,month([CreateDate]) AS " +
            //                 "Month ,Count(ID) as Count from Articles  as ArticleDateDto " +
            //                 " group by year([CreateDate]) ,month([CreateDate])"); 
            var list = query.List();
            return (from object[] obj in list
                    select new ArticleDateDto()
                    {
                        Year = (int)obj[0],
                        Month = (int)obj[1],
                        Count = (int)obj[2]
                    }).ToList();

        }

        /// <summary>
        /// 获取指定条数文章列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="topIndex">显示条数</param>
        /// <returns></returns>
        public IList<ArticleDto> GetTopList(int userId, int topIndex)
        {
            var query = (from a in _articleRepository.FindAll()
                         let at = a.ArticleType
                         let u = a.User
                         where u.Id == userId
                         orderby a.LastDateTime descending
                         select CreateArticleDto(a, at, u)).Take(topIndex);
            return query.ToList();
        }

        #region Helper

        private ArticleDto CreateArticleDto(Article a, ArticleType at, User u)
        {
            var articleDto = new ArticleDto()
            {
                Content = HttpUtility.HtmlDecode(a.Content),
                CreateDate = a.CreateDate,
                Id = a.Id,
                IsShow = a.IsShow,
                LastUpdate = (DateTime)a.LastDateTime,
                ReadCount = a.ReadCount,
                Title = HttpUtility.HtmlDecode(a.Title),
                UrlQuoteUrl = a.UrlQuoteUrl,
                RealName = u.RealName,
                UserName = u.UserName,
                UserHeadImg = u.SmallImgHead,
                Domain = u.Domain,
                UserId = u.Id, 
                ContentDesc = HttpUtility.HtmlDecode(a.ContentDesc)
            };

            if (at != null)
            {
                articleDto.ArticleTypeId = at.Id;
                articleDto.ArticleTypeName = at.TypeName;
            }

            var articleTag = a.ArticleTags.ToList();


            var atList = articleTag.Select(x => new
            {
                @id = x.Id,
                @tagName = x.TagName
            });


            atList.Each(x => articleDto.ArticleTags.Add(
                x.id,
                x.tagName));
            return articleDto;
        }




        private ArticleDto CreateArticleDto(
            Article a,
            ArticleType at,
            User u,
            int commentCount)
        {
            var articleDto = new ArticleDto()
            {

                Content = HttpUtility.HtmlDecode(a.Content),
                CreateDate = a.CreateDate,
                Id = a.Id,
                IsShow = a.IsShow,
                LastUpdate = (DateTime)a.LastDateTime,
                ReadCount = a.ReadCount,
                Title = HttpUtility.HtmlDecode(a.Title),
                UrlQuoteUrl = a.UrlQuoteUrl,
                RealName = u.RealName,
                UserName = u.UserName,
                UserHeadImg = u.SmallImgHead,
                Domain = u.Domain,
                CommentCount = commentCount,
                ContentDesc = HttpUtility.HtmlDecode(a.ContentDesc)
            };
            if (at != null)
            {
                articleDto.ArticleTypeId = at.Id;
                articleDto.ArticleTypeName = at.TypeName;
            }

            var articleTag = a.ArticleTags.ToList();


            var atList = articleTag.Select(x => new
            {
                @id = x.Id,
                @tagName = x.TagName
            });


            atList.Each(x => articleDto.ArticleTags.Add(
                x.id,
                x.tagName));
            return articleDto;
        }

        #endregion
    }
}
