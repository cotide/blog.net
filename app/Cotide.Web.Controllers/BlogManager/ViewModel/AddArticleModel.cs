using System.ComponentModel.DataAnnotations;

namespace Cotide.Web.Controllers.BlogManager.ViewModel
{
    public class AddArticleModel
    {
        public int Id { get; set; }

        public int? ArticleTypeId { get; set; }

        public bool IsShow { get; set; }

        public string UrlQuoteUrl { get; set; }

        [Required(ErrorMessage = @"请输入文章标题")]
        [StringLength(255, ErrorMessage = @"文章标题格式:1-255个字符")] 
        public string Title { get; set; }

        public string ContentDesc { get; set; }

        public string Content { get; set; }
    }
}
