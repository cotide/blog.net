//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UserArticleMessageDto.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/23 11:20:23 
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
    /// <summary>
    /// 用户留言DTO
    /// </summary>
    public class UserArticleMessageDto
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

        /// <summary>
        /// 留言者昵称
        /// </summary>
        public string NickName { get; set; }
         
      
    }
}
