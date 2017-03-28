//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：Article.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/1/10 21:53:00 
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
using Cotide.Framework.Utility;

namespace Cotide.Domain
{
    /// <summary>
    /// 文章
    /// </summary>
    public partial class Article
    {

        /// <summary>
        /// 设置文章所属文章标签
        /// </summary>
        /// <param name="articleTags">文章标签</param>
        public virtual void SetArticleTage(IList<ArticleTag> articleTags)
        {
            ArticleTags.Clear();
            foreach (var articleTag in articleTags)
            {
                SetArticleTage(articleTag);
            }
        }

        /// <summary>
        /// 添加文章留言
        /// </summary>
        /// <param name="articleMessage"></param>
        public virtual void AddArticleMessage(ArticleMessage articleMessage)
        {
            Guard.IsNotNull(articleMessage, "articleMessage");
            ArticleMessages.Add(articleMessage);
        }

        /// <summary>
        /// 删除文章留言
        /// </summary>
        /// <param name="articleMessage"></param>
        public virtual void DeleteArticleMessage(ArticleMessage articleMessage)
        {
            Guard.IsNotNull(articleMessage, "articleMessage");
            ArticleMessages.Remove(articleMessage);
        }

 


        #region Helper 

        /// <summary>
        /// 设置文章所属文章标签
        /// </summary>
        /// <param name="articleTag">文章标签</param>
        private void SetArticleTage(ArticleTag articleTag)
        {
            ArticleTags.Add(articleTag);
        }

       

        #endregion
    }
}
