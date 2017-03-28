using System.Collections.Generic;
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;

namespace Cotide.Domain.Contracts.QueryServices
{
    /// <summary>
    /// 文章标签查询服务
    /// </summary>
    public interface IArticleTagQueryService
    {
        /// <summary>
        /// 获取文章标签分页列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pageIndex">开始索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        PagedList<ArticleTagDto> FindAllPager(
            int userId,
            int pageIndex,
            int pageSize);

        /// <summary>
        /// 获取文章标签
        /// </summary>
        /// <param name="articleTagId">文章类别ID</param>
        /// <returns></returns>
        ArticleTagDto FindOne(int articleTagId);

        /// <summary>
        /// 获取用户文章标签列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        IList<ArticleTagDto> FindAll(int userId);
         

        /// <summary>
        /// 获取用户TOP N 条文章标签列表
        /// </summary>
        /// <param name="topIndex">N条</param>
        /// <returns></returns>
        IList<ArticleTagDto> FindTop(int topIndex);
    }
}
