using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;

namespace Cotide.Domain.Contracts.QueryServices
{
    /// <summary>
    /// 链接类型查询服务
    /// </summary>
    public interface ILinkTypeQueryService
    {

        /// <summary>
        /// 获取项目类型
        /// </summary>
        /// <param name="linkId">项目类型ID</param>
        /// <returns></returns>
        LinkTypeDto FindOne(int linkId);

        /// <summary>
        /// 获取用户所有链接分类
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="isShow">是否前端显示</param>
        /// <param name="linkTypeId">链接类型</param>
        /// <returns></returns>
        IList<LinkTypeDto> FindAll(int userId,bool? isShow=null,int? linkTypeId=null);

        /// <summary>
        /// 获取项目类型列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="isShow">前端是否显示</param> 
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PagedList<LinkTypeDto> FindAllPager(
            int? userId, 
            bool? isShow,
            int pageIndex=1,
            int pageSize=10); 

    }
}
