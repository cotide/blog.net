//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：SaveMessageViewModel.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/10 22:40:17 
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

namespace Cotide.Web.Controllers.Blog.ViewModel.Article
{
    public class SaveMessageViewModel
    {
        /// <summary>
        /// 文章ID
        /// </summary>  
        [Required(ErrorMessage = @"文章ID不能为NULL")]
        public int ArticleId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary> 
        [DisplayName(@" 昵称: ")]
        [StringLength(40, ErrorMessage = @"昵称最大长度不能超出20个字符")]
        public string NickName { get; set; }

        /// <summary>
        /// Email
        /// </summary> 
        [DisplayName(@" Email: ")]
        [StringLength(200, ErrorMessage = @"账号最大长度不能超出100个字符")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = @"Email格式不正确")]
        public string Email { get; set; }

        /// <summary>
        /// 个人网站
        /// </summary>
        [DisplayName(@" 个人网站: ")]
        [StringLength(200, ErrorMessage = @"个人网站不能超出200个字符")]
        [RegularExpression(@"[a-zA-Z]+://[^s]*", ErrorMessage = @"个人网站格式不正确")] 
        public string WebSiteUrl { get; set; }

        /// <summary>
        /// 留言内容
        /// </summary>
        public string Comment { get; set; }
    }

}
