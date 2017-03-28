//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UserLoginViewModel.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/12/30 23:57:51 
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

namespace Cotide.Web.Controllers.ViewModels.User
{
    /// <summary>
    /// 用户登录ViewModel
    /// </summary>
    public class UserLoginViewModel
    { 
        /// <summary>
        /// 账号
        /// </summary> 
        [Required(ErrorMessage = @"您还没输入账号")]
        [DisplayName(@" 用户帐号: ")]
        [StringLength(50, ErrorMessage = @"账号最大长度不能超出3-10个字符", MinimumLength = 3)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DataType(DataType.Password)]
        [Required(ErrorMessage = @"您还没输入密码")]
        [DisplayName(@" 登录密码: ")]
        [StringLength(20, ErrorMessage = @"账号最大长度不能超出5-20个字符", MinimumLength = 5)]
        public string Paw { get; set; } 
    }
}
