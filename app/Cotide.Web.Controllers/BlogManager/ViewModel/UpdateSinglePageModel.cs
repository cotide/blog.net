using System;

namespace Cotide.Web.Controllers.BlogManager.ViewModel
{
    public class UpdateSinglePageModel
    {
        public int Id { get; set; } 

        public bool IsShow { get; set; }

        public string KeyWord { get; set; }

        public string Description { get; set; } 

        public string Title { get; set; }

        public string SinglePageName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastDateTime { get; set; }

        public int UserId { get; set; }  

        public string Content { get; set; } 
         
    }
}
