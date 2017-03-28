namespace Cotide.Web.Controllers.Blog.ViewModel
{
    public  class HistoryArticleViewModel
    {
        public HistoryArticleViewModel()
        {

        }

        /// <summary>
        /// 文章ID
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string ArticleTitle { get; set; }

        /// <summary>
        /// /文章标题(全)
        /// </summary>
        public string FullArticleTitle { get; set; }
    }
}
