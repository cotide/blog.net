using System.ComponentModel.DataAnnotations;

namespace Cotide.Web.Controllers.BlogManager.ViewModel
{ 
    public class ArticleTagModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = @"请输入文章标题")]
        [StringLength(30, ErrorMessage = @"文章标题格式:1-30个字符")] 
        public string TagName { get; set; }

        public bool IsShow { get; set; }
    }
}
