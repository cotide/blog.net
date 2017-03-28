using System;
using System.Collections.Generic; 
using Cotide.Framework.Attr.Desc;
using SharpArch.Domain.DomainModel;

namespace Cotide.Domain
{
    /// <summary>
    /// 文章留言
    /// </summary>
    public partial class ArticleMessage : Entity 
    { 
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ArticleMessage()
        {
            ArticleMessages = new List<ArticleMessage>();
        }

        /// <summary>
        /// 上级文章留言
        /// </summary>
        public virtual ArticleMessage BaseArticleMessage { get; set; }
         
        /// <summary>
        /// 文章
        /// </summary>
        public virtual Article Article { get; set; }

        /// <summary>
        /// 留言回复
        /// </summary>
        public virtual IList<ArticleMessage> ArticleMessages { get; set; }

        /// <summary>
        /// 留言内容
        /// </summary> 
        public virtual string Content { get; set; }
         
        /// <summary>
        /// 是否前端显示
        /// </summary>
        public virtual bool IsShow { get; set; }

        #region 用户留言字段

        /// <summary>
        /// 所属用户
        /// </summary>
        public virtual User User { get; set; }

        #endregion

        #region 匿名用户留言字段

        /// <summary>
        /// 用户昵称
        /// </summary>
        public virtual string NickName { get; set; }

        /// <summary>
        /// 用户Email
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// 用户个人网站
        /// </summary>
        public virtual string WebSiteUrl { get; set; }
        #endregion 

        
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
