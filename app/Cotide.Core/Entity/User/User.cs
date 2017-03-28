//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：User.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/4 16:08:07 
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
using Cotide.Domain.Contracts.Repositories; 
using Cotide.Domain.Enum;
using Cotide.Framework.Attr.Desc;
using NHibernate.Validator.Constraints;
using SharpArch.Domain.DomainModel;

namespace Cotide.Domain
{

    /// <summary>
    /// 用户
    /// </summary> 
    [EntityDesc("用户，用于记录平台用户信息")]
    public class User : Entity, IAggregateRoot
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [DomainSignature]
        [Length(255, Message = "用户名不能超出255个字符")]
        [NotNullNotEmpty]
        [EntityPropertyDesc("用户名")]
        public virtual string UserName { get; set; }

        /// <summary>
        /// 用户编码 
        /// </summary>
        [EntityPropertyDesc("用户编码")]
        public virtual string UserNo { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        [EntityPropertyDesc("用户角色")]
        public virtual UserRole UserRole { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Length(255, Message = "用户名不能超出255个字符")]
        [NotNullNotEmpty]
        [EntityPropertyDesc("密码")]
        public virtual string Paw { get; set; }

        /// <summary>
        /// 用户头像(原图)
        /// </summary>
        [EntityPropertyDesc("用户头像(原图)")]
        public virtual string ImgHead { get; set; }

        /// <summary>
        /// 用户头像(小图) 50 X 50
        /// </summary>
        [EntityPropertyDesc("用户头像(小图) 50 X 50")]
        public virtual string SmallImgHead { get; set; }

        /// <summary>
        /// 用户头像(标准图) 150 X 150
        /// </summary>
        [EntityPropertyDesc("用户头像(标准图) 150 X 150")]
        public virtual string StandardImgHead { get; set; }

        /// <summary>
        /// 域名地址
        /// </summary>
        [EntityPropertyDesc("域名地址")]
        public virtual string Domain { get; set; }

        /// <summary>
        /// 邮件
        /// </summary>
        [EntityPropertyDesc("邮件")]
        public virtual string Email { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [EntityPropertyDesc("身份证")]
        public virtual string Card { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [EntityPropertyDesc("性别")]
        public virtual UserSex? UserSex { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        [EntityPropertyDesc("用户状态")]
        public virtual UserState UserState { get; set; }

        /// <summary>
        /// 用户手机号码
        /// </summary>
        [Length(20, Message = "手机号码不能超出22个字符")]
        [EntityPropertyDesc("用户手机号码")]
        public virtual string Phone { get; set; }

        /// <summary>
        /// 明文密码
        /// </summary>
        [EntityPropertyDesc("明文密码")]
        public virtual string DesPassword { get; set; }

        /// <summary>
        /// 真实名称
        /// </summary>
        [EntityPropertyDesc("真实名称")]
        public virtual string RealName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        [EntityPropertyDesc("英文名称")]
        public virtual string EnRealName { get; set; }

        /// <summary>
        /// 是否通过邮箱验证
        /// </summary>
        [EntityPropertyDesc("是否通过邮箱验证")]
        public virtual bool? EmailValidate { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        [EntityPropertyDesc("QQ")]
        [Length(20, Message = "QQ不能超出22个字符")]
        public virtual string QQ { get; set; }

        /// <summary>
        /// 微博地址
        /// </summary>
        [Length(255, Message = "微博地址不能超出255个字符")]
        public virtual string WeiBoUrl { get; set; }

        /// <summary>
        /// 博客名称
        /// </summary>
        [Length(50, Message = "博客名称不能超出50个字符")]
        public virtual string BlogName { get; set; }

        /// <summary>
        /// 博客描述
        /// </summary>
        [Length(255, Message = "博客描述不能超出255个字符")]
        public virtual string BlogDesc { get; set; }
         
        /// <summary>
        /// 当前登录时间
        /// </summary>
        [EntityPropertyDesc("当前登录时间")]
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        [EntityPropertyDesc("上次登录时间")]
        public virtual DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// 当前登录IP
        /// </summary>
        [EntityPropertyDesc("当前登录IP")]
        public virtual string LoginIp { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        [EntityPropertyDesc("最后登录IP")]
        public virtual string LastLoginIp { get; set; }


        /// <summary>
        /// 用户游览记录
        /// </summary>
        [EntityPropertyDesc("用户游览记录")]
        public virtual IList<UserTourLog> UserTourLogs { get; set; }

        /// <summary>
        /// 用户文章
        /// </summary>
        [EntityPropertyDesc("用户文章")]
        public virtual IList<Article> Articles { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary> 
        [EntityPropertyDesc("创建时间")]
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary> 
        [EntityPropertyDesc("最后修改时间")]
        public virtual DateTime? LastDateTime { get; set; }
         

    }
}
