using System.Collections.Generic;
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;

namespace Cotide.Web.Controllers.Blog.ViewModel
{
    public class ZupPinViewModel
    {
        public IList<ProjectTypeDto> ProjectTypes { get; set; }

        public PagedList<ProjectViewModel> Projects { get; set; }

        public string Domain { get; set; }
        
        public ZupPinViewModel()
        {
            ProjectTypes = new List<ProjectTypeDto>(); 
        }

    }
}
