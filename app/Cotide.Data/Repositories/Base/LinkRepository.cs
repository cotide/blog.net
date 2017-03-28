//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称：  
//文件名称：LinkRepository.cs 
//模块名称：
//模块编号：
//作　　者：cotide 
//创建时间：05/08/2014 19:28:17
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
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Contracts.Repositories.Extension; 
using Cotide.Infrastructure.Repositories.Extension;
namespace Cotide.Infrastructure.Repositories 
{  
    public class LinkRepository : DbProxyRepository<Link>, ILinkRepository
    {
        
    }
}
