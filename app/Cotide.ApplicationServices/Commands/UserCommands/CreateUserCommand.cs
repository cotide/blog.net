//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：CreateUserCommand.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/6 12:01:13 
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
using Cotide.Domain.Enum;
using SharpArch.Domain.Commands;
using Cotide.Framework.Utility;

namespace Cotide.Tasks.Commands.UserCommands
{
    /// <summary>
    /// 创建用户命令
    /// </summary>
    public class CreateUserCommand : CommandBase
    {

        /// <summary>
        /// 用户名
        /// </summary> 
        public readonly string UserName;

        /// <summary>
        /// 真实名称
        /// </summary>
        public readonly string RealName;


        /// <summary>
        /// 用户状态
        /// </summary>
        public readonly UserState UserState;

        /// <summary>
        /// 密码
        /// </summary>
        public readonly string Paw;

        /// <summary>
        /// 用户英文名
        /// </summary>
        public string EnRealName { get; set; }

        /// <summary>
        /// 博客名称
        /// </summary>
        public string BlogName { get; set; }

        /// <summary>
        /// 博客描述
        /// </summary>
        public string BlogDesc { get; set; }

        /// <summary>
        /// 用户头像(原图)
        /// </summary>
        public string ImgHead { get; set; }

        /// <summary>
        /// 用户头像(小图) 50 X 50
        /// </summary>
        public string SmallImgHead { get; set; }

        /// <summary>
        /// 用户头像(标准图) 150 X 150
        /// </summary>
        public string StandardImgHead { get; set; }

        /// <summary>
        /// 域名地址
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string Card { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public UserSex? UserSex { get; set; }
         

        /// <summary>
        /// 用户手机号码
        /// </summary> 
        public string Phone { get; set; }

        /// <summary>
        /// 个人网站地址
        /// </summary>
        public string WebSiteUrl { get; set; }

        /// <summary>
        /// 是否通过邮箱验证
        /// </summary>
        public bool? EmailValidate { get; set; }

        /// <summary>
        /// QQ
        /// </summary> 
        public string QQ { get; set; }
         
        ///// <summary>
        ///// 管理员权限
        ///// </summary>
        //public AdminPower? AdminPower { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="paw">密码</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="userState">用户状态</param>
        public CreateUserCommand(
            string userName,
            string paw,
            string realName,
            UserState userState)
        {
            Guard.IsNotNullOrEmpty(userName, "userName");
            Guard.IsNotNullOrEmpty(paw, "paw");
            Guard.IsNotNullOrEmpty(realName, "realName");
            UserName = userName;
            Paw = paw;
            RealName = realName;
            UserState = userState;
        }

        /// <summary>
        /// 创建管理员
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="paw">密码</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="userState">用户状态</param>
        /// <param name="adminLevel">管理员等级</param>
        public CreateUserCommand(
            string userName,
            string paw,
            string realName,
            UserState userState,
            AdminLevel adminLevel)
        {
            Guard.IsNotNullOrEmpty(userName, "userName");
            Guard.IsNotNullOrEmpty(paw, "paw");
            Guard.IsNotNullOrEmpty(realName, "realName");
            UserName = userName;
            Paw = paw;
            RealName = realName;
            UserState = userState; 
        }
    }
}
