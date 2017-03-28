//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称：  
//文件名称：ILinkRepository.cs 
//模块名称：
//模块编号：
//作　　者：cotide 
//创建时间：03/15/2014 20:46:51
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
using Cotide.Domain.Contracts.Repositories.Extension; 
namespace Cotide.Domain.Contracts.Repositories 
{ 
    public interface ILinkRepository :  IDbProxyRepository<Link> 
    {
        
    }
}
