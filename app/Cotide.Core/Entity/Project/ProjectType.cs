using System; 
using Cotide.Framework.Attr.Desc;
using SharpArch.Domain.DomainModel;

namespace Cotide.Domain
{
    /// <summary>
    /// 项目类型
    /// </summary>
    public class ProjectType : Entity
    {
        /// <summary>
        /// 项目所属用户
        /// </summary> 
        public virtual User User { get; set; } 

        /// <summary>
        /// 类型名称
        /// </summary>
        public virtual string TypeName { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public virtual bool IsShow { get; set; }


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
