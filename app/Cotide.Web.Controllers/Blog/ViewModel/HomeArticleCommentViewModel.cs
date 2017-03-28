//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：HomeArticleCommentViewModel.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/4/7 0:07:28 
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

namespace Cotide.Web.Controllers.Blog.ViewModel
{
    /// <summary>
    /// 首页评论ViewModel
    /// </summary>
    public class HomeArticleCommentViewModel
    {
        /// <summary>
        /// 文章地址
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户评论内容 (截取)
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 用户评论内容 （全）
        /// </summary>
        public string FullContent { get; set; }
    }
}
