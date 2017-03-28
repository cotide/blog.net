//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ErrorController.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/1/25 20:12:52 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Cotide.Web.Controllers.Error
{
    public class ErrorController: Controller
    {
        [HttpGet]
        public ViewResult Error500(string errorMsg)
        {
            ViewBag.Error = errorMsg;
            return View();
        }
    }
}
