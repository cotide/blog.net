//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：IAdQueryService.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/23 18:47:35 
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
using Cotide.Domain.Dtos;
using Cotide.Domain.Enum;

namespace Cotide.Domain.Contracts.QueryServices
{
    public interface IAdQueryService
    {
        IList<AdDto> FindAll(AdType adType,bool? isShow=null);

        IList<AdDto> FindAll(bool? isShow=null);

        AdDto FindOne(int adId);
    }
}
