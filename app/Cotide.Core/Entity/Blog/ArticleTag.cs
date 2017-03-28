using System;
using System.Collections.Generic; 
using Cotide.Domain.Contracts.Repositories; 
using Cotide.Framework.Attr.Desc;
using NHibernate.Validator.Constraints;
using SharpArch.Domain.DomainModel;

namespace Cotide.Domain
{
    /// <summary>
    /// 文章标签
    /// </summary> 
    [EntityDesc("文章标签")]
    public class ArticleTag : Entity 
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        [Length(30, Message = "文章标签名称不能超出30个字符")]
        [NotNullNotEmpty]
        [EntityPropertyDesc("标签名称")]
        public virtual string TagName { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        [EntityPropertyDesc("是否显示")]
        public virtual bool IsShow { get; set; }

        /// <summary>
        /// 标签下的文章列表
        /// </summary>
        [EntityPropertyDesc("标签下的文章列表")]
        public virtual IList<Article> Articles { get; set; }

        /// <summary>
        /// 所属用户
        /// </summary>
        [NotNull]
        [EntityPropertyDesc("所属用户")]
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
