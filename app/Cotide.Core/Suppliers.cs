using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Framework.Attr.Desc;
using SharpArch.Domain.DomainModel;

namespace Cotide.Domain
{
    /// <summary>
    /// 供应商
    /// </summary>
    [EntityDesc("供应商")]
    public class Suppliers : Entity, IAggregateRoot
    {
        /// <summary>
        /// 供应商名称
        /// </summary>
        [EntityPropertyDesc("供应商名称")]
        public virtual string Name { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [EntityPropertyDesc("联系人")]
        public virtual string User { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [EntityPropertyDesc("联系电话")]
        public virtual string Tel { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [EntityPropertyDesc("手机号码")]
        public virtual string Phone { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        [EntityPropertyDesc("联系地址")]
        public virtual string Address { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [EntityPropertyDesc("备注")]
        public virtual string Remark { get; set; }

    }
}
