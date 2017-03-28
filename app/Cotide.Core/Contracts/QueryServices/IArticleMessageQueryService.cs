using System.Collections.Generic;
using Cotide.Domain.Dtos;
using Cotide.Domain.Dtos.ArticleMessage;
using Cotide.Framework.Collections;

namespace Cotide.Domain.Contracts.QueryServices
{
    /// <summary>
    /// 文章留言查询服务
    /// </summary>
    public interface IArticleMessageQueryService
    {
        /// <summary>
        /// 获取文章留言
        /// </summary>
        /// <param name="articleId">文章ID</param> 
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PagedList<ArticleMessageDto> FindAllByArticleIdPager(
            int articleId,  
            int pageIndex, 
            int pageSize);
         
        /// <summary>
        /// 获取文章留言（状态为显示）
        /// </summary>
        /// <param name="articleId">文章ID</param> 
        /// <returns></returns>
        IList<ArticleMessageDto> FindAllByArticleId(int articleId);

        /// <summary>
        /// 获取文章留言（状态为显示）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="topIndex">前N条</param>
        /// <returns></returns>
        IList<ArticleMessageDto> FindTop(int userId, int topIndex);
    }
}
