namespace Cotide.Web.Controllers.BlogManager.ViewModel
{
    public class UpdateProjectTypeModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
    }
}
