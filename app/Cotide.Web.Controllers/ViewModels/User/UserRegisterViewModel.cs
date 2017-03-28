//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UserRegisterViewModel.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/12/31 13:34:19 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Cotide.Framework.Attr.Desc;
using SharpArch.Domain.DomainModel;

namespace Cotide.Web.Controllers.ViewModels.User
{
    public class UserRegisterViewModel
    {

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = @"您还没输入用户帐号")]
        [DisplayName(@" 用户帐号: ")]
        [StringLength(50, ErrorMessage = @"用户帐号最大长度不能超出3-50个字符", MinimumLength = 3)]
        public virtual string UserName { get; set; }

        [Required(ErrorMessage = @"您还没输入用户昵称")]
        [DisplayName(@" 中文名称: ")]
        [StringLength(20, ErrorMessage = @"账号最大长度不能超出20个字符")]
        public string NiceName { get; set; }

        [Required(ErrorMessage = @"您还没输入创建密码")]
        [DisplayName(@" 创建密码: ")]
        [StringLength(200, ErrorMessage = @"密码最大长度不能超出100个字符")]
        public string Paw { get; set; }

        [Required(ErrorMessage = @"您还没输入确认密码")]
        [DisplayName(@" 确认密码: ")]
        [StringLength(200, ErrorMessage = @"确认密码最大长度不能超出100个字符")]
        public string RepeatPaw { get; set; }

        //[Required(ErrorMessage = @"您还没输入验证码")]
        //[DisplayName(@" 验证码: ")]
        //public string Code { get; set; }

    }
}
