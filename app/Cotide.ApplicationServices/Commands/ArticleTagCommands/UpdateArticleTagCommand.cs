//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UpdateArticleTagCommand.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/1/15 23:46:42 
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
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.ArticleTagCommands
{
    /// <summary>
    /// 修改文章标签命令 
    /// </summary>
    public class UpdateArticleTagCommand : CommandBase
    {
        /// <summary>
        /// 文章标签ID
        /// </summary>
        public readonly int ArticleTagId;

        /// <summary>
        /// 当前用户ID
        /// </summary>
        public readonly int CurrentUserId;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="articleTagId">文章标签ID</param>
        /// <param name="currentUserId">当前用户ID</param>
        public UpdateArticleTagCommand(int articleTagId, int currentUserId)
        {
            Guard.IsNotZeroOrNegative(articleTagId, "articleTagId");
            Guard.IsNotZeroOrNegative(currentUserId, "currentUserId");
            ArticleTagId = articleTagId;
            CurrentUserId = currentUserId;
        }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 是否前端显示 默认为True
        /// </summary>
        public bool? IsShow { get; set; }
    }
}
