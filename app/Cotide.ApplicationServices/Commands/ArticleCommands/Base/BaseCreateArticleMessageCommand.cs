//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：BaseCreateArticleMessageCommand.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/17 14:45:33 
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

namespace Cotide.Tasks.Commands.ArticleCommands.Base
{
    public abstract class  BaseCreateArticleMessageCommand : CommandBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public readonly int? UserId;
        /// <summary>
        /// 留言内容
        /// </summary>
        public readonly string Content;

        /// <summary>
        /// 是否前端显示
        /// </summary>
        public bool IsShow { get; set; }

        
         /// <summary>
        /// 构造函数(用户留言)
        /// </summary> 
        /// <param name="userId">用户ID Null为匿名用户留言</param>
        /// <param name="content">留言内容</param> 
        public BaseCreateArticleMessageCommand(int userId, string content)
        { 
            Guard.IsNotZeroOrNegative(userId,"userId");
            Guard.IsNotNullOrEmpty(content, "content"); 
            UserId = userId;
            Content = content; 
            IsShow = true;
        }

          /// <summary>
        /// 构造函数(匿名用户留言)
        /// </summary> 
        /// <param name="nickName">匿名用户名</param>
        /// <param name="content">留言内容</param> 
        public BaseCreateArticleMessageCommand(string nickName, string content)
        { 
            Guard.IsNotNullOrEmpty(nickName, "nickName");
            Guard.IsNotNullOrEmpty(content, "content"); 
            NickName = nickName;
            Content = content; 
            IsShow = true;
        }
        

        #region /// 匿名用户留言字段

        /// <summary>
        /// 用户昵称
        /// </summary>
        public readonly string NickName;
        
      

        /// <summary>
        /// 用户Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户个人网站
        /// </summary>
        public string WebSiteUrl { get; set; }
        #endregion
    }
}

