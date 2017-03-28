using System.ComponentModel.DataAnnotations;

namespace Cotide.Web.Controllers.BlogManager.ViewModel
{
    public class ArticleTypeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = @"请输入文章分类名称")]
        [StringLength(30, ErrorMessage = @"文章分类名称格式:1-30个字符")] 
        public string TypeName { get; set; }

        public bool IsShow { get; set; }
    }
}
