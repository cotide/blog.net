//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ArticleCommentViewModel.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/10 0:37:24 
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
    /// <summary>
    /// 文章评论
    /// </summary>
    public class ArticleCommentViewModel
    {
        public ArticleCommentViewModel()
        {
            ArticleReplyCommentViewModel = new List<ArticleCommentViewModel>();
        }

         
        /// <summary>
        /// 留言ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 当前留言用户
        /// </summary>
        public UserArticleMessageViewModel UserArticleMessageViewModel { get; set; }

        /// <summary>
        /// 目标留言用户
        /// </summary>
        public TagerUserArticleMessageViewModel TagerUserArticleMessageViewModel { get; set; }
 

        /// <summary>
        /// 回复内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 是否指定目标留言用户
        /// </summary>
        public bool IsNoNodeArticleMessage
        {
            get { return TagerUserArticleMessageViewModel != null; }
        }

        public IList<ArticleCommentViewModel> ArticleReplyCommentViewModel { get; set; }
         

        /// <summary>
        /// 是否可以删除该评论
        /// </summary>
        public bool IsCanDelete { get; set; }
         

    }
}
