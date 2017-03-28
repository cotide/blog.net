using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Cotide.Framework.ActionResult;
using Cotide.Framework.Utility;
using Cotide.Web.Controllers.Controllers;

namespace Cotide.Web.Controllers.Utility
{
    
    [HandleError]
    public class CodeController : BaseController
    {

        [HttpGet]
        public ImageActionResult GetCode()
        {
            #region///资源访问事件
            return new ImageActionResult();  
            #endregion
        } 
    }
}
