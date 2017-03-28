//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ShizongArticleDto.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/23 17:30:40 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cotide.Domain.Dtos
{
    public class ShizongArticleDto
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 文章图片
        /// </summary> 
        public   string ShizongArticleImg { get; set; }

        /// <summary>
        /// 文章图片(标准图) 200 X 250
        /// </summary> 
        public   string StandardShizongArticleImg { get; set; }


        /// <summary>
        /// 文章标题
        /// </summary>   
        public string Title { get; set; }

        /// <summary>
        /// 文章所属用户名
        /// </summary> 
        public string UserName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 文章所属用户昵称
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserHeadImg { get; set; }

        /// <summary>
        /// 用户域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 阅读总数
        /// </summary>
        public int ReadCount { get; set; }

        /// <summary>
        /// 文章内容简述
        /// </summary>
        public string ContentDesc { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary> 
        public string Content { get; set; }

        /// <summary>
        /// 文章类别
        /// </summary> 
        public int ArticleTypeId { get; set; }

        /// <summary>
        /// 文章类别名称
        /// </summary>
        public string ArticleTypeName { get; set; }
         

        /// <summary>
        /// 是否前端显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastUpdate { get; set; }
    }
}
