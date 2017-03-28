using System; 
using Cotide.Framework.Attr.Desc;
using SharpArch.Domain.DomainModel;

namespace Cotide.Domain
{
    public class Project : Entity
    {
        /// <summary>
        /// 项目名称
        /// </summary> 
        public virtual string ProjectName { get; set; }

        /// <summary>
        /// 项目图片(原图)
        /// </summary>
        public virtual string ProjectImg { get; set; }

        /// <summary>
        /// 项目图片(小图)
        /// </summary>
        public virtual string SmallProjectImg { get; set; }

        /// <summary>
        /// 项目图片(标准图)
        /// </summary>
        public virtual string StandardProjectImg { get; set; }

        /// <summary>
        /// 网址
        /// </summary> 
        public virtual string WebSite { get; set; }

        /// <summary>
        /// 项目简介
        /// </summary>
        public virtual string Introduction { get; set; }

        /// <summary>
        /// 项目内容
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public virtual ProjectType ProjectType { get; set; }

        /// <summary>
        /// 项目所属用户
        /// </summary> 
        public virtual User User { get; set; }
         
        /// <summary>
        /// 是否显示
        /// </summary>
        public virtual bool IsShow { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? Sort { get; set; }


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
