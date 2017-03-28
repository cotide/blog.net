using Cotide.Domain.Enum;

namespace Cotide.Web.Controllers.BlogManager.ViewModel
{
    public class UserAccountModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 英文真实名称
        /// </summary>
        public string EnRealName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string ImgHead { get; set; }


        /// <summary>
        /// 博客名称
        /// </summary>
        public string BlogName { get; set; }

        /// <summary>
        /// 个人网站地址
        /// </summary>
        public string WebSiteUrl { get; set; }


        /// <summary>
        /// 博客描述
        /// </summary> 
        public string BlogDesc { get; set; }

        /// <summary>
        /// 微博地址
        /// </summary> 
        public string WeiBoUrl { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Emali { get; set; }

        /// <summary>
        /// 用户手机
        /// </summary> 
        public string Phone { get; set; }

         

    }
}
