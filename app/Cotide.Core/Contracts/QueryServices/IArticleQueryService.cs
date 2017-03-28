using System.Collections.Generic;
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;

namespace Cotide.Domain.Contracts.QueryServices
{
    /// <summary>
    /// 文章查询服务
    /// </summary>
    public interface IArticleQueryService
    {
        /// <summary>
        /// 获取文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns></returns>
        ArticleDto FindOne(int articleId);

        /// <summary>
        /// 获取用户文章列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="articleTypeId">文章类别ID</param>
        /// <param name="tagId">标签ID</param> 
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="isShow">前端是否显示</param>
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PagedList<ArticleDto> FindAllPager(
            int? userId,
            int? articleTypeId,
            int? tagId,
            int? year,
            int? month,
            bool? isShow,
            int pageIndex,
            int pageSize);
         
        /// <summary>
        /// 获取用户文章统计数据
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        IList<ArticleDateDto> GetArticleCount(int userId);
            
            
        /// <summary>
        /// 获取指定条数文章列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="topIndex">显示条数</param>
        /// <returns></returns>
        IList<ArticleDto> GetTopList(
            int userId, 
            int topIndex);
    }
}
