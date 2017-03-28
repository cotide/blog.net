//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ImageActionResult.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/16 10:46:21 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Cotide.Framework.Utility;

namespace Cotide.Framework.ActionResult
{
    /// <summary>
    /// 密匙类
    /// </summary>
    public sealed class GuidKey
    {
        private const string _codeSessionKey = "MASK_KEY";

        /// <summary>
        /// 验证码Session唯一标识
        /// </summary>
        public static string CodeSessionKey
        {
            get { return _codeSessionKey; }
        }
    }

    public class ImageActionResult : System.Web.Mvc.ActionResult
    {

        public override void ExecuteResult(ControllerContext context)
        {

            //输出类型格式(图片格式)
            HttpContext.Current.Response.ContentType = "image/jpeg";
            //不缓存
            HttpContext.Current.Response.AppendHeader("Cache-Control", "no-cache");
            HttpContext.Current.Response.AppendHeader("Pragma", "no-cache");
            HttpContext.Current.Response.AppendHeader("Expires", "0");
            var vc = new ValidateCode(10, new Font("Verdana", 18, FontStyle.Regular), Color.Red, Color.White);
            // ValidateCode vc = new ValidateCode(10, new Font("Verdana", 14), Color.SandyBrown, Color.White); 
            vc.IsRandomFont = true;// 动态生成字体
            string code = null;
            using (Image img = vc.CreateImage(100, 30, out code))
            {
                img.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
            }

            HttpContext.Current.Session[GuidKey.CodeSessionKey] = code.ToLower();
            HttpContext.Current.Response.End();
        }
    }

}
