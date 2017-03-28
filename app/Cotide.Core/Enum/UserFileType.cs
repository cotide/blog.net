using System.ComponentModel;

namespace Cotide.Domain.Enum
{
    /// <summary>
    /// 用户文件类型
    /// </summary>
    public enum UserFileType
    {
        /// <summary>
        /// 用户资料
        /// </summary>
        [Description("用户资料")]
        Profile,
        /// <summary>
        /// 图片
        /// </summary>  
        [Description("图片")]
        Image,
        /// <summary>
        ///文档
        /// </summary>  
        [Description("文档")]
        Document,
        /// <summary>
        ///媒体文件
        /// </summary>  
        [Description("媒体文件")]
        Media
    }
}
