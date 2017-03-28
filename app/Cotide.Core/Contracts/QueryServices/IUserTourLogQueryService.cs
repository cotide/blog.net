//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：IUserTourLogQueryService.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/8 23:51:38 
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

namespace Cotide.Domain.Contracts.QueryServices
{
    /// <summary>
    /// 游览用户查询服务
    /// </summary>
    public interface IUserTourLogQueryService
    {
        /// <summary>
        /// 查询指定数量的游览过的用户 
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="maxIndex">数据大小</param>
        /// <returns></returns>
        IList<UserTourLogDto> FindToLookTop(int userId,int maxIndex);


        /// <summary>
        /// 查询指定数量的访客用户 
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="maxIndex">数据大小</param>
        /// <returns></returns>
        IList<UserTourLogDto> FindForLookMeTop(int userId, int maxIndex);
    }
}
