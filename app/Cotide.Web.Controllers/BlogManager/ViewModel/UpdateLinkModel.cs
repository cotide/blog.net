namespace Cotide.Web.Controllers.BlogManager.ViewModel
{
    public class UpdateLinkModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 链接文本
        /// </summary>
        public string LinkTxt { get; set; }


        /// <summary>
        /// 链接类型ID
        /// </summary>
        public int LinkTypeId { get; set; }


        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkUrl { get; set; }


        /// <summary>
        /// 前端是否显示
        /// </summary>
        public bool IsShow { get; set; }


    }
}
