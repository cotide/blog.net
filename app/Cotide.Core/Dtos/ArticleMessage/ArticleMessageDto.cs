using System;
using System.Collections.Generic;

namespace Cotide.Domain.Dtos.ArticleMessage
{
    /// <summary>
    /// 文章留言
    /// </summary>
    public class ArticleMessageDto
    {
        public ArticleMessageDto()
        {
           
        }

        /// <summary>
        /// 留言ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 文章ID
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// 当前留言用户
        /// </summary>
        public UserArticleMessageDto UserArticleMessageDto { get; set; }

        /// <summary>
        /// 目标留言用户
        /// </summary>
        public TagerUserArticleMessageDto TagerUserArticleMessageDto { get; set; }

        /// <summary>
        /// 父级留言信息ID
        /// </summary>
        public int? BaseArticleMessageId { get; set; }

        ///// <summary>
        ///// 留言回复
        ///// </summary>
        //public IList<ArticleReplyMessageDto> ArticleReplyMessages { get; set; }
         
        /// <summary>
        /// 回复内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    } 
 
}
