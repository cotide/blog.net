using System.Collections.Generic;
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;

namespace Cotide.Domain.Contracts.QueryServices
{
    /// <summary>
    /// 链接查询服务
    /// </summary>
    public interface ILinkQueryService
    {
        /// <summary>
        /// 获取指定条数链接列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isShow"></param>
        /// <param name="topIndex"></param>
        /// <returns></returns>
        IList<LinkDto> GetList(int? userId,bool? isShow, int topIndex);

        /// <summary>
        /// 获取指定链接 
        /// </summary>
        /// <param name="linkId">链接ID</param>
        /// <returns></returns>
        LinkDto FindOne(int linkId);

        /// <summary>
        /// 获取链接分页列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="linkTypeId">连接类型</param>
        /// <param name="isShow">是否显示</param>
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PagedList<LinkDto> FindAllPager(int? userId, int? linkTypeId,bool? isShow, int pageIndex, int pageSize);

         
    }
}
