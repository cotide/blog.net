using System.Collections.Generic;
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;

namespace Cotide.Domain.Contracts.QueryServices
{
    /// <summary>
    /// 文章类型查询服务
    /// </summary>
    public interface IArticleTypeQueryService
    {
        /// <summary>
        /// 获取文章类别分页列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pageIndex">开始索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        PagedList<ArticleTypeDto> FindAllPager(
            int? userId, 
            int pageIndex,
            int pageSize);

        /// <summary>
        /// 获取用户文章类别列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        IList<ArticleTypeDto> FindAll(int userId);


        /// <summary>
        /// 获取用户文章类别列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        IList<ArticleTypeDto> FindAllForShow(int userId);

       
        /// <summary>
        /// 获取文章类别
        /// </summary>
        /// <param name="articleTypeId">文章类别ID</param>
        /// <returns></returns>
        ArticleTypeDto FindOne(int articleTypeId);
    }
}
