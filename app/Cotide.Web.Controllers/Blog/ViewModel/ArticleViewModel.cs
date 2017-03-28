using System;
using System.Collections.Generic;

namespace Cotide.Web.Controllers.Blog.ViewModel
{
  
    /// <summary>
    /// 文章视图
    /// </summary>
    public class ArticleViewModel
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// 文章用户昵称
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 文章标题(截取)
        /// </summary>
        public string ArticleTitle { get; set; }

        /// <summary>
        /// 文章标题(全)
        /// </summary>
        public string FullArticleTitle { get; set; }

        /// <summary>
        /// 阅读总数
        /// </summary>
        public int ReadCount { get; set; }

        /// <summary>
        /// 文件内容（全）
        /// </summary>
        public string Content { get; set; }
         
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
        /// 最后修改时间
        /// </summary>
        public DateTime LastUpdate { get; set; }

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserHeadImg { get; set; }

        /// <summary>
        /// 用户域名
        /// </summary>
        public string Domain { get; set; }


        /// <summary>
        /// 评论总数
        /// </summary>
        public int CommentCount { get; set; }
    }
}
