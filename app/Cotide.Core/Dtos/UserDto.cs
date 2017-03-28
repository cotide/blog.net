//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UserDto.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/10/6 12:50:03 
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

namespace Cotide.Domain.Dtos
{
    public class UserDto
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public UserDto()
        {
        }

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary> 
        public string UserName { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public UserRole UserRole { get; set; }

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
        /// 用户状态
        /// </summary>
        public UserState UserState { get; set; }

        /// <summary>
        /// 用户权限
        /// </summary>
        public UserPower? UserPower { get; set; }

        /// <summary>
        /// 用户手机号码
        /// </summary> 
        public string Phone { get; set; }

        /// <summary>
        /// 个人网站地址
        /// </summary>
        public string WebSiteUrl { get; set; }


        /// <summary>
        /// 真实名称
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnRealName { get; set; }
         
        /// <summary>
        /// 是否通过邮箱验证
        /// </summary>
        public bool? EmailValidate { get; set; }

        /// <summary>
        /// QQ
        /// </summary> 
        public string QQ { get; set; }

        /// <summary>
        /// 微博
        /// </summary>
        public string WeiBoUrl { get; set; }

        /// <summary>
        /// 博客名称
        /// </summary>
        public string BlogName { get; set; }

        /// <summary>
        /// 博客描述
        /// </summary>
        public string BlogDesc { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastUpdate { get; set; }

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
        /// 管理员等级
        /// </summary>
        public AdminLevel AdminLevel { get; set; }

        ///// <summary>
        ///// 管理员权限
        ///// </summary>
        //public AdminPower? AdminPower { get; set; }
    }
}
