using System.Collections.Generic;
using System.Linq;
using Cotide.Domain;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories.Extension;
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;

namespace Cotide.QueryServices
{
    public class ArticleTagQueryService : IArticleTagQueryService
    {
        private readonly IDbProxyRepository<ArticleTag> _articleTagDbProxyRepository;

        public ArticleTagQueryService(IDbProxyRepository<ArticleTag>  articleTagDbProxyRepository)
        {
            _articleTagDbProxyRepository = articleTagDbProxyRepository;
        }

        /// <summary>
        /// 获取文章标签分页列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pageIndex">开始索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public PagedList<ArticleTagDto> FindAllPager(int userId, int pageIndex, int pageSize)
        {
            var query = (from a in _articleTagDbProxyRepository.FindAll()
                         let u = a.User
                         where u.Id == userId
                         orderby a.Id descending
                         select CreateArticleTagDto(a, u));
            return query.ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 获取文章标签
        /// </summary>
        /// <param name="articleTagId">文章类别ID</param>
        /// <returns></returns>
        public ArticleTagDto FindOne(int articleTagId)
        {
            var query = (from a in _articleTagDbProxyRepository.FindAll()
                         let u = a.User
                         where a.Id == articleTagId
                         orderby a.Id descending
                         select CreateArticleTagDto(a, u));
            return query.SingleOrDefault();
        }

        /// <summary>
        /// 获取用户文章标签列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public IList<ArticleTagDto> FindAll(int userId)
        {
            var query = (from a in _articleTagDbProxyRepository.FindAll()
                         let u = a.User
                         where u.Id == userId
                         select CreateArticleTagDto(a, u));
            return query.ToList();
        }

        /// <summary>
        /// 获取用户TOP N 条文章标签列表
        /// </summary>
        /// <param name="topIndex">N条</param>
        /// <returns></returns>
        public IList<ArticleTagDto> FindTop(int topIndex)
        {
            var query = (from a in _articleTagDbProxyRepository.FindAll()
                         let u = a.User 
                         select CreateArticleTagDto(a, u)).Take(topIndex);
            return query.ToList();
        }

        #region Helper

        /// <summary>
        /// 创建ArticleTagDto对象
        /// </summary>
        /// <param name="a">文章标签对象</param>
        /// <param name="u">用户对象</param>
        /// <returns></returns>
        private ArticleTagDto CreateArticleTagDto(ArticleTag a, User u)
        {
            return new ArticleTagDto()
                       {
                           Id = a.Id,
                           TagName = a.TagName,
                           UserId = u.Id,
                           UserName = u.UserName,
                           IsShow = a.IsShow
                       };
        }

        #endregion
    }
}
