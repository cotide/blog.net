using System.Collections.Generic;
using System.Linq;
using Cotide.Domain;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories.Extension;
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;

namespace Cotide.QueryServices
{
    /// <summary>
    /// 文章类型查询服务
    /// </summary>
    public class ArticleTypeQueryService : IArticleTypeQueryService
    {
        private readonly IDbProxyRepository<ArticleType> _articleTypeDbProxyRepository;

        public ArticleTypeQueryService(IDbProxyRepository<ArticleType> articleTypeDbProxyRepository)
        {
            _articleTypeDbProxyRepository = articleTypeDbProxyRepository;
        }

        /// <summary>
        /// 获取文章类别分页列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pageIndex">开始索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public PagedList<ArticleTypeDto> FindAllPager(int? userId, int pageIndex, int pageSize)
        {
            if(userId==null)
            {
                var query = (from a in _articleTypeDbProxyRepository.FindAll()
                             let u = a.User 
                             orderby a.Id descending
                             select CreaetArticleTypeDto(a, u));
                return query.ToPagedList(pageIndex, pageSize);
            }
            else
            {
                var query = (from a in _articleTypeDbProxyRepository.FindAll()
                             let u = a.User
                             where u.Id == userId
                             orderby a.Id descending
                             select CreaetArticleTypeDto(a, u));
                return query.ToPagedList(pageIndex, pageSize); 
            } 
        }


        /// <summary>
        /// 获取用户文章类别列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public IList<ArticleTypeDto> FindAll(int userId)
        {
            var query = (from a in _articleTypeDbProxyRepository.FindAll()
                         let u = a.User
                         where u.Id == userId
                         orderby a.Id descending
                         select CreaetArticleTypeDto(a, u));
            return query.ToList();
        }

        public IList<ArticleTypeDto> FindAllForShow(int userId)
        {
            var query = (from a in _articleTypeDbProxyRepository.FindAll()
                         let u = a.User
                         where u.Id == userId
                         && a.IsShow 
                         orderby a.Id descending
                         select CreaetArticleTypeDto(a, u));
            return query.ToList();
        }

        /// <summary>
        /// 获取文章类别
        /// </summary>
        /// <param name="articleTypeId">文章类别ID</param>
        /// <returns></returns>
        public ArticleTypeDto FindOne(int articleTypeId)
        {
            var query = (from a in _articleTypeDbProxyRepository.FindAll()
                         let u = a.User
                         where a.Id == articleTypeId
                         orderby a.Id descending
                         select CreaetArticleTypeDto(a, u));
            return query.SingleOrDefault();
        }

        #region Helper
        /// <summary>
        /// 创建ArticleTypeDto对象
        /// </summary>
        /// <param name="articleType">文章类型</param>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public ArticleTypeDto CreaetArticleTypeDto(ArticleType articleType, User user)
        {
            return new ArticleTypeDto()
                       {
                           Id = articleType.Id,
                           TypeName = articleType.TypeName,
                           IsShow = articleType.IsShow,
                           UserId = user.Id,
                           UserName = user.UserName
                       };
        }

        #endregion
    }
}
