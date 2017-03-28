//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：LoginViewModel.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/10/4 21:12:01 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cotide.Web.Controllers.Reg.ViewModel
{
    /// <summary>
    /// 登录实体模型
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 账号
        /// </summary> 
        [Required(ErrorMessage = @"您还没输入账号")]
        [DisplayName(@" 用户帐号: ")]
        [StringLength(50, ErrorMessage = @"用户账号格式:3-50个字符内", MinimumLength = 3)] 
        public string UserName { get; set; }

        /// <summary>
        ///  密码
        /// </summary>  
        [DataType(DataType.Password)]
        [Required(ErrorMessage = @"您还没输入密码")]
        [DisplayName(@"登录密码:")]
        [StringLength(20, ErrorMessage = @"密码格式：3-20个字符内", MinimumLength = 3)] 
        public string Paw { get; set; }
    }
}
