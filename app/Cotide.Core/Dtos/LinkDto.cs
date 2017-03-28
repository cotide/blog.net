using System;

namespace Cotide.Domain.Dtos
{
  
    public class LinkDto
    {
        /// <summary>
        /// 链接ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 链接文本
        /// </summary>
        public string LinkTxt { get; set; }

        /// <summary>
        /// 链接类型名称
        /// </summary>
        public string LinkTypeName { get; set; }

        /// <summary>
        /// 链接类型ID
        /// </summary>
        public int LinkTypeId { get; set; }

        /// <summary>
        /// 链接描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 链接图片
        /// </summary>
        public string Img { get; set; }


        /// <summary>
        /// 前端是否显示
        /// </summary>
        public bool IsShow { get; set; }

         
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public virtual DateTime? LastUpdate { get; set; }  


        /// <summary>
        /// 链接所属用户ID
        /// </summary> 
        public int UserId { get; set; }
    }
}
