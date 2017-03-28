//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：BaseArticleCommentViewModel.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/10 0:37:52 
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

namespace Cotide.Web.Controllers.Blog.ViewModel.Article
{
    public class  ArticleReplyCommentViewModel
    {
        /// <summary>
        /// 回复ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int? UserId { get; set; }
         
        /// <summary>
        /// 昵称
        /// </summary>
        public string TagerNickName { get; set; }

        /// <summary>
        /// 用户域名
        /// </summary>
        public string TagerDomain { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int? TagerUserId { get; set; }

       /// <summary>
       /// 内容
       /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
