using System;
using System.Collections.Generic;

namespace Cotide.Domain.Dtos
{
    public class ArticleDto
    {
        public ArticleDto()
        {
            ArticleTags = new Dictionary<int, string>();
        }

        /// <summary>
        /// 文章ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>   
        public string Title { get; set; }

        /// <summary>
        /// 文章所属用户名
        /// </summary> 
        public string UserName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 文章所属用户昵称
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserHeadImg { get; set; }

        /// <summary>
        /// 用户域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 阅读总数
        /// </summary>
        public int ReadCount { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary> 
        public string Content { get; set; }

        /// <summary>
        /// 文章内容简述
        /// </summary>
        public string ContentDesc { get; set; }

        /// <summary>
        /// 文章标签
        /// </summary>
        public IDictionary<int, string> ArticleTags { get; set; }

        /// <summary>
        /// 文章类别
        /// </summary> 
        public int ArticleTypeId { get; set; }

        /// <summary>
        /// 文章类别名称
        /// </summary>
        public string ArticleTypeName { get; set; }

        /// <summary>
        /// 引用通告
        /// </summary>
        public string UrlQuoteUrl { get; set; }

        /// <summary>
        /// 是否前端显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// 评论总数
        /// </summary>
        public int CommentCount { get; set; }
    }
}
