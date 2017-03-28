//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UserTourLogQueryService.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/8 23:51:17 
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
using Cotide.Domain;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories.Extension;
using Cotide.Domain.Dtos;

namespace Cotide.QueryServices
{
    public class UserTourLogQueryService : IUserTourLogQueryService
    {
        private readonly IDbProxyRepository<UserTourLog> _userTourLogDbProxyRepository;

        public UserTourLogQueryService(IDbProxyRepository<UserTourLog> userTourLogDbProxyRepository)
        {
            _userTourLogDbProxyRepository = userTourLogDbProxyRepository;
        }



        /// <summary>
        /// 查询指定数量的游览过的用户 
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="maxIndex">数据大小</param>
        /// <returns></returns>
        public IList<UserTourLogDto> FindToLookTop(int userId, int maxIndex)
        {
            var query = (from q in _userTourLogDbProxyRepository.FindAll()
                         let u = q.User
                         let uT = q.TourUser
                         where u.Id == userId
                         select CreateUserTourLogDto(q, uT));
            return query.ToList();
        }

        /// <summary>
        /// 查询指定数量的访客用户 
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="maxIndex">数据大小</param>
        /// <returns></returns>
        public IList<UserTourLogDto> FindForLookMeTop(int userId, int maxIndex)
        {
            var query = (from q in _userTourLogDbProxyRepository.FindAll()
                         let u = q.User
                         let uT = q.TourUser
                         where uT.Id == userId
                         select CreateUserTourLogDto(q, u));
            return query.ToList();
        }
         
         
        #region Helper
        private UserTourLogDto CreateUserTourLogDto(UserTourLog t, User u)
        {
            return new UserTourLogDto()
                       {
                           BlogName = u.BlogName,
                           CreateDate = t.CreateDate,
                           Id = t.Id, 
                           UpdateDate = t.LastDateTime,
                           UserId = u.Id,
                           ImgHead = u.ImgHead,
                           SmallImgHead = u.SmallImgHead,
                           StandardImgHead = u.StandardImgHead,
                           UserName = u.UserName,
                           Domain = u.Domain
                       };
        }
        #endregion
         
    }
}
