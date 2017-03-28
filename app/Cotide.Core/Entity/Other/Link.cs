using System; 
using Cotide.Framework.Attr.Desc;
using NHibernate.Validator.Constraints;
using SharpArch.Domain.DomainModel;

namespace Cotide.Domain
{
    /// <summary>
    /// 友情链接
    /// </summary>
    public class Link : Entity
    {
        /// <summary>
        /// 链接文本
        /// </summary>
        public virtual string LinkTxt { get; set; }

        /// <summary>
        /// 链接类型
        /// </summary>
        public virtual LinkType LinkType { get; set; }

        /// <summary>
        /// 链接描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public virtual string LinkUrl { get; set; }

        /// <summary>
        /// 链接图片
        /// </summary>
        public virtual string Img { get; set; }
         

        /// <summary>
        /// 前端是否显示
        /// </summary>
        public virtual bool IsShow { get; set; }
 

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort { get; set; }

        /// <summary>
        /// 链接所属用户
        /// </summary>
        [NotNull]
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
