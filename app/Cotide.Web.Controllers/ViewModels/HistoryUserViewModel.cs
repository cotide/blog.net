using Cotide.Domain.Enum;

namespace Cotide.Web.Controllers.ViewModels
{
    /// <summary>
    /// 当前查看的用户视图
    /// </summary>
    public class HistoryUserViewModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 英文用户昵称
        /// </summary>
        public string EnRealName { get; set; }

        /// <summary>
        /// 域名
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
        /// 微博URL
        /// </summary>
        public string WeiBoUrl { get; set; }

        /// <summary>
        /// 用户性别
        /// </summary>
        public UserSex? Sex { get; set; }

        /// <summary>
        /// 用户头像
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
        /// 用户手机
        /// </summary> 
        public string Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 是否当前登录用户
        /// </summary>
        public bool IsCurrentLoginUser { get; set; }
         

    }
}
