//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ArticleCommentReplyViewModel.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/10 0:36:59 
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
using Cotide.Framework.Collections;

namespace Cotide.Web.Controllers.Blog.ViewModel.Article
{
    public class ArticleCommentReplyViewModel
    {
        public ArticleCommentReplyViewModel()
        {

        }

        public IList<ArticleCommentViewModel> ArticleCommentViewModels { get; set; }

        public bool IsLogin { get; set; }

        public int ArticleId { get; set; }

        public int? CurrentUserId { get; set; }

         
    }
}
