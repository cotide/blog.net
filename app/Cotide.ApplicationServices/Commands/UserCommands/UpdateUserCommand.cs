//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UpdateUserCommand.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/6 12:01:52 
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
using Cotide.Framework.Utility;
using SharpArch.Domain.Commands;

namespace Cotide.Tasks.Commands.UserCommands
{
    /// <summary>
    /// 更新用户命令
    /// </summary>
    public class UpdateUserCommand : CommandBase
    { 
        /// <summary>
        /// 用户ID 
        /// </summary>
        public readonly int UserId;

        /// <summary>
        /// 昵称
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 用户头像 原图
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
        /// 邮件
        /// </summary>
        public string Email { get; set; } 

        /// <summary>
        /// 用户手机
        /// </summary>
        public string Phone { get; set; }
         
        /// <summary>
        /// 性别
        /// </summary>
        public UserSex? Sex { get; set; }

        /// <summary>
        /// 是否通过邮箱验证
        /// </summary>
        public bool? EmailValidate { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool? IsIsRecommend { get; set; }

        /// <summary>
        /// 推荐编号
        /// </summary>
        public virtual int? RecommendNo { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Paw { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public UserRole? UserRole { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public UserState? UserState { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 当前登录时间
        /// </summary>
        public DateTime? LoginDate { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// 当前登录IP
        /// </summary>
        public string LoginIp { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 博客名称
        /// </summary>
        public string BlogName { get; set; }

        /// <summary>
        /// 博客描述
        /// </summary>
        public string BlogDesc { get; set; }

        /// <summary>
        /// 微博地址
        /// </summary>
        public string WeiBoUrl { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EnRealName { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userId">用户ID</param>
        public UpdateUserCommand(int userId)
        {
            Guard.IsNotZeroOrNegative(userId, "userId");
            UserId = userId;
        }
    }
}
