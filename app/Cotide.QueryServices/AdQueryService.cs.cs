//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：AdQueryService.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/23 18:47:08 
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
using Cotide.Domain.Enum;

namespace Cotide.QueryServices
{
    public class AdQueryService : IAdQueryService
    {
        private readonly IDbProxyRepository<Ad> _adDbProxyRepositor;

        public AdQueryService(IDbProxyRepository<Ad> adDbProxyRepositor)
        {
            _adDbProxyRepositor = adDbProxyRepositor;
        }


        public IList<AdDto> FindAll(AdType adType, bool? isShow=null)
        {
            var query = _adDbProxyRepositor.FindAll().Where(x => x.AdType == adType);
            if (isShow != null)
                query = query.Where(x => x.IsShow == isShow);
            return query.OrderByDescending(x=>x.Sort).Select(x => CreateAdDto(x)).ToList();
        }

        public IList<AdDto> FindAll(bool? isShow=null)
        {
            var query = _adDbProxyRepositor.FindAll();
            if (isShow != null)
                query = query.Where(x => x.IsShow == isShow);
            return query.OrderByDescending(x=>x.Sort).Select(x => CreateAdDto(x)).ToList();
        }

        public AdDto FindOne(int adId)
        {
            var query = _adDbProxyRepositor.FindAll().Where(x=>x.Id==adId);
            return query.Select(x => CreateAdDto(x)).FirstOrDefault();
        }

        #region  Helper
        private AdDto CreateAdDto(Ad ad)
        {
            return new AdDto()
                       {
                           Id= ad.Id,
                           AdDesc = ad.AdDesc,
                           AdImg = ad.AdImg,
                           SmallAdImg = ad.SmallAdImg,
                           StandardAdImg = ad.StandardAdImg,
                           AdName = ad.AdName,
                           AdType = ad.AdType,
                           IsShow = ad.IsShow,
                           Sort = ad.Sort
                       };
        }
        #endregion

    }
}
