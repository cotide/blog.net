using System;
using Cotide.Framework.Attr.Desc;

namespace Cotide.Domain.Base
{
    public abstract class EntityBase : SharpArch.Domain.DomainModel.Entity
    {

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
