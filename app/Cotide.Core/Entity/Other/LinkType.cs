using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using Cotide.Framework.Attr.Desc;
using SharpArch.Domain.DomainModel;


namespace Cotide.Domain
{
    /// <summary>
    /// 链接类型
    /// </summary>
    public class LinkType : Entity
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public virtual string TypeName { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }
          
        /// <summary>
        /// 前端是否显示
        /// </summary>
        public virtual bool IsShow { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort { get; set; }

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
