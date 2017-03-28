using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories.Extension;
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;

namespace Cotide.QueryServices
{
    public class LinkTypeQueryService : ILinkTypeQueryService
    {
        private readonly IDbProxyRepository<LinkType> _linkTypeDbProxyRepository;
        private readonly IDbProxyRepository<User> _userDbProxyRepository;

        public LinkTypeQueryService(
            IDbProxyRepository<LinkType> linkTypeDbProxyRepository,
            IDbProxyRepository<User> userDbProxyRepository)
        {
            this._linkTypeDbProxyRepository = linkTypeDbProxyRepository;
            this._userDbProxyRepository = userDbProxyRepository;
        }

        public LinkTypeDto FindOne(int linkId)
        {
            var query = (from l in _linkTypeDbProxyRepository.FindAll()
                         let u = l.User
                         where l.Id == linkId
                         select CreateLinkTypeDto(l, u));
            return query.FirstOrDefault();
        }

        public IList<LinkTypeDto> FindAll(int userId, bool? isShow = null, int? linkTypeId = null)
        {
            var query = _linkTypeDbProxyRepository.FindAll();
            var userQuery = _userDbProxyRepository.FindAll();
            query = query.Join(userQuery, x => x.User.Id, y => y.Id, (x, y) => x);
            query = query.Where(x => x.User.Id == userId);
            if (isShow != null)
            {
                query = query.Where(x => x.IsShow == isShow);
            }
            if (linkTypeId != null)
            {
                query = query.Where(x => x.Id == linkTypeId);
            }
            query = query.OrderByDescending(x => x.Sort);
            var result = query.Select(x => CreateLinkTypeDto(x, x.User));

            return result.ToList();
        }
 

        public PagedList<LinkTypeDto> FindAllPager(
            int? userId,
            bool? isShow,
            int pageIndex = 1,
            int pageSize = 10)
        {
            var query = _linkTypeDbProxyRepository.FindAll();
            var userQuery = _userDbProxyRepository.FindAll();
            query = query.Join(userQuery, x => x.User.Id, y => y.Id, (x, y) => x);

            if (userId != null)
            {
                query = query.Where(x => x.User.Id == userId);
            }
            if (isShow != null)
            {
                query = query.Where(x => x.IsShow == isShow);
            }
            query = query.OrderByDescending(x => x.Sort);
            var result = query.Select(x => CreateLinkTypeDto(x, x.User));

            return result.ToPagedList(pageIndex, pageSize);
        }

        #region Helper

        private LinkTypeDto CreateLinkTypeDto(LinkType linkType, User u)
        {
            return new LinkTypeDto()
            {
                Id = linkType.Id,
                CreateDate = linkType.CreateDate,
                LastUpdate = linkType.LastDateTime,
                TypeName = linkType.TypeName,
                UserId = u.Id,
                UserName = u.UserName,
                IsShow = linkType.IsShow,
                Sort = linkType.Sort
            };
        }

        #endregion
    }
}
