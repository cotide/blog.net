using System;
using System.Collections.Generic;

namespace Cotide.Web.Controllers.BlogManager.ViewModel
{
    public class ArticleModel
    {
        public int Id { get; set; }

        public int ArticleTypeId { get; set; }

        public string ArticleTypeName { get; set; }

        public IDictionary<int, string> ArticleTags { get; set; }

        public bool IsShow { get; set; }

        public int ReadCount { get; set; }

        public string UrlQuoteUrl { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
