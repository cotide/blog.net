//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UserArticleMessageViewModel.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/23 12:19:23 
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
    public class UserArticleMessageViewModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserImg { get; set; }

        /// <summary>
        /// 用户域名
        /// </summary>
        public string UserDomain { get; set; }

        private string _nickName;

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName
        {
            get
            {
                return IsUnknown ? _nickName + "(匿名)" : _nickName;
            }
            set { _nickName = value; }
        }

        /// <summary>
        /// 当前评论用户是否是匿名
        /// </summary>
        public bool IsUnknown
        {
            get
            {
                return UserId == null ? true : false;
            }
        }
    }
}
