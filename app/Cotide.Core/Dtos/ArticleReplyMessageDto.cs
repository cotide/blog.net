//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ArticleReplyMessageDto.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/17 10:25:19 
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
    public class ArticleReplyMessageDto
    { 
        public ArticleReplyMessageDto()
        {
            
        }

        /// <summary>
        /// 回复ID
        /// </summary>
        public int Id { get; set; }

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

        /// <summary>
        /// 留言者昵称
        /// </summary>
        public string NickName { get; set; }


        /// <summary>
        /// 用户ID
        /// </summary>
        public int? TagerUserId { get; set; }


        /// <summary>
        /// 用户头像
        /// </summary>
        public string TagerUserImg { get; set; }

        /// <summary>
        /// 用户域名
        /// </summary>
        public string TagerUserDomain { get; set; }

        /// <summary>
        /// 留言者昵称
        /// </summary>
        public string TagerNickName { get; set; }
 

        /// <summary>
        /// 回复内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

    }
}
