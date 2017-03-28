//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：IdentityUser.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/7 12:56:34 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  

using System;
using Cotide.Domain.Enum;

namespace Cotide.Domain.Contracts.Task
{
    public class IdentityUser
    {
         /// <summary>
        /// 默认构造函数
        /// </summary>
        public IdentityUser()
        {
        }

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public UserLoginRole UserLoginRole { get; set; }

        /// <summary>
        /// 用户名
        /// </summary> 
        public string UserName { get; set; }

        /// <summary>
        /// 用户域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 博客名称
        /// </summary>
        public string BlogName { get; set; }

        /// <summary>
        /// 博客描述
        /// </summary>
        public string BlogDesc { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string RealName { get; set; }
  
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

         
    }
}
