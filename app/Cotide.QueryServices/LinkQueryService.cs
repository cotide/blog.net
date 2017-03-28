using System.Collections.Generic;
using System.Linq;
using Cotide.Domain;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Contracts.Repositories.Extension;
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;

namespace Cotide.QueryServices
{
    /// <summary>
    /// 友情链接查询服务
    /// </summary>
    public class LinkQueryService : ILinkQueryService
    {
        protected readonly IDbProxyRepository<Link> LinkDbProxyRepository;
        protected readonly IDbProxyRepository<User> UserDbProxyRepository;
        protected readonly ILinkTypeRepository LinkTypeRepository;

        #region IOC注入

        public LinkQueryService(
            IDbProxyRepository<Link> linkDbProxyRepository, 
            IDbProxyRepository<User> userDbProxyRepository,
            ILinkTypeRepository linkTypeRepository)
        {
            LinkDbProxyRepository = linkDbProxyRepository;
            UserDbProxyRepository = userDbProxyRepository;
            LinkTypeRepository = linkTypeRepository;
        }

        #endregion

        #region Implementation of ILinkQueryService

        /// <summary>
        /// 获取指定条数链接列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isShow"></param>
        /// <param name="topIndex"></param>
        /// <returns></returns>
        public IList<LinkDto> GetList(
            int? userId, 
            bool? isShow,
            int topIndex)
        {

            var query = LinkDbProxyRepository.FindAll();
            var user = UserDbProxyRepository.FindAll();
            var linkType = LinkTypeRepository.FindAll();
            query = query.Join(user, x => x.User.Id, y => y.Id, (x, y) => x);
            query = query.Join(linkType, x => x.LinkType.Id, z => z.Id, (x, z) => x);
            if (userId != null && userId > 0)
            {
                query = query.Where(x => x.User.Id == userId);
            }
            if (isShow != null)
            {
                query = isShow == true ? query.Where(x => x.IsShow) : query.Where(x => !x.IsShow);
            }
            query = query.OrderByDescending(x => x.LastDateTime);
            return query.Select(x => CreateLinkDto(x, x.User,x.LinkType)).ToList();
        }

        /// <summary>
        /// 获取指定链接 
        /// </summary>
        /// <param name="linkId">链接ID</param>
        /// <returns></returns>
        public LinkDto FindOne(int linkId)
        { 
            var query = (from a in LinkDbProxyRepository.FindAll()
                         let u = a.User
                         let lt = a.LinkType
                         where a.Id == linkId
                         select CreateLinkDto(a, u,lt));
            return query.SingleOrDefault();
        }

        /// <summary>
        /// 获取链接分页列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="isShow">是否显示</param>
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public PagedList<LinkDto> FindAllPager(
            int? userId,
            int? linkTypeId,
            bool? isShow, 
            int pageIndex,
            int pageSize)
        {
            var query = LinkDbProxyRepository.FindAll();
            var user = UserDbProxyRepository.FindAll();
            var linkType = LinkTypeRepository.FindAll();
            query =  query.Join(user, x => x.User.Id, y => y.Id, (x, y) => x);
            query = query.Join(linkType, x => x.LinkType.Id, z => z.Id, (x,z) => x);
            if (userId!=null&& userId>0)
            { 
                query = query.Where(x => x.User.Id == userId);
            }
            if (linkTypeId != null)
            {
                query = query.Where(x => x.LinkType.Id == linkTypeId);
            }
            if (isShow != null)
            {
                query = isShow == true ? query.Where(x => x.IsShow) : query.Where(x => !x.IsShow);
            }
            query = query.OrderByDescending(x => x.LastDateTime);
            return query.Select(x => CreateLinkDto(x, x.User,x.LinkType))
                   .ToPagedList(pageIndex, pageSize);
        }

        #endregion

        #region Helper
        private LinkDto CreateLinkDto(Link l, User u,LinkType lt)
        {
            return new LinkDto()
                       {
                           Id = l.Id,
                           Description = l.Description,
                           Img = l.Img,
                           IsShow = l.IsShow,
                           LinkTxt = l.LinkTxt,
                           LinkTypeName = lt.TypeName,
                           LinkTypeId = lt.Id,
                           LinkUrl = l.LinkUrl,
                           UserId = u.Id, 
                           CreateDate = l.CreateDate,
                           LastUpdate = l.LastDateTime
                       };
        }
        #endregion
    }
}
