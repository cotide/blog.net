 
using System;
using System.Collections.Generic; 
using Cotide.Domain.Contracts.Repositories;
using Cotide.Framework.Attr.Desc;
using NHibernate.Validator.Constraints;
using SharpArch.Domain.DomainModel;

namespace Cotide.Domain
{
    /// <summary>
    /// 文章
    /// </summary>
    [EntityDesc("文章")]
    public partial class Article : Entity, IAggregateRoot
    {
        /// <summary>
        /// 默认构造函数
        /// </summary> 
        public Article()
        {
            ArticleMessages = new List<ArticleMessage>();
            ArticleTags = new List<ArticleTag>();
        }

        /// <summary>
        /// 文章标题
        /// </summary>  
        [Length(255, Message = "文章标题不能超出255个字符")]
        [NotNullNotEmpty]
        [EntityPropertyDesc("文章标题")]
        public virtual string Title { get; set; }

        /// <summary>
        /// 阅读总数
        /// </summary>
        [EntityPropertyDesc("阅读总数")]
        public virtual int ReadCount { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary> 
        [EntityPropertyDesc("文章内容")]
        public virtual string Content { get; set; }


        /// <summary>
        /// 文章内容简述
        /// </summary>
        [EntityPropertyDesc("文章内容简述")]
        public virtual string ContentDesc { get; set; }

        /// <summary>
        /// 文章类别
        /// </summary>
        [NotNull]
        [EntityPropertyDesc("文章类别")]
        public virtual ArticleType ArticleType { get; set; }

        /// <summary>
        /// 文章标签
        /// </summary>
        [EntityPropertyDesc("文章标签")]
        public virtual IList<ArticleTag> ArticleTags { get; set; }


        /// <summary>
        /// 文章留言
        /// </summary>
        [EntityPropertyDesc("文章留言")]
        public virtual IList<ArticleMessage> ArticleMessages { get; set; }


        /// <summary>
        /// 引用通告
        /// </summary>
        [EntityPropertyDesc("引用通告")]
        public virtual string UrlQuoteUrl { get; set; }

        /// <summary>
        /// 是否前端显示
        /// </summary>
        [NotNull]
        [EntityPropertyDesc("是否前端显示")]
        public virtual bool IsShow { get; set; }


        /// <summary>
        /// 文章所属用户
        /// </summary>
        [NotNull]
        [EntityPropertyDesc("文章所属用户")]
        public virtual User User { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary> 
        [EntityPropertyDesc("创建时间")]
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary> 
        [EntityPropertyDesc("最后修改时间")]
        public virtual DateTime? LastDateTime { get; set; }

    }
}
