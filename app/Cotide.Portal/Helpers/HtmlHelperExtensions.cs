using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Cotide.Web.Helpers;
using System.Configuration; 

namespace System.Web.Mvc
{
    /// <summary>
    /// System.Web.Mvc.ViewMasterPage 扩展HTML工具类
    /// </summary>
    public static class HtmlHelperExtensions
    {
        private const string BaseRootPath = "~/";

        /// <summary>
        /// 脚本根文件夹位置
        /// </summary>
        private const string ScriptRootPath =BaseRootPath+ "scripts/";

        /// <summary>
        /// 样式根文件夹位置
        /// </summary>
        private const string CssRootPath = BaseRootPath + "content/";

        /// <summary>
        /// 生成CSS的链接
        /// </summary>
        /// <param name="html"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static MvcHtmlString StyleSheet(this HtmlHelper html, string fileName)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var tagBuilder = new TagBuilder("link");

            tagBuilder.MergeAttribute("href", urlHelper.Content(CssRootPath+fileName) +
                "?version=" + ConfigurationManager.AppSettings["Version"]);
            tagBuilder.MergeAttribute("rel", "stylesheet");
            tagBuilder.MergeAttribute("type", "text/css");

            return new MvcHtmlString(tagBuilder.ToString()); 
        }
         
        /// <summary>
        /// 生成Scrip的链接
        /// </summary>
        /// <param name="html"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static MvcHtmlString JavaScript(this HtmlHelper html, string fileName)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var tagBuilder = new TagBuilder("script");
            tagBuilder.MergeAttribute("src", urlHelper.Content(ScriptRootPath+fileName) + 
                "?version=" + ConfigurationManager.AppSettings["Version"]);
            tagBuilder.MergeAttribute("type", "text/javascript");
            return new MvcHtmlString(tagBuilder.ToString()); 
        }

        /// <summary>
        /// 生成Jquery脚本链接
        /// </summary>
        /// <param name="html"></param> 
        /// <returns></returns>
        public static MvcHtmlString JqueryScript(this HtmlHelper html)
        {
            var jqueryUrl = ConfigurationManager.AppSettings["JQueryLib"];
            if (string.IsNullOrEmpty(jqueryUrl))
                return new MvcHtmlString(""); 
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var tagBuilder = new TagBuilder("script"); 
            tagBuilder.MergeAttribute("src", urlHelper.Content(jqueryUrl) +
                "?version=" + ConfigurationManager.AppSettings["Version"]);
            tagBuilder.MergeAttribute("type", "text/javascript"); 
            return new MvcHtmlString(tagBuilder.ToString());
        }

        /// <summary>
        /// 生成CSS的链接
        /// </summary>
        /// <param name="html"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static MvcHtmlString Context(this HtmlHelper html, string fileName)
        {  
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            return new MvcHtmlString(urlHelper.Content(BaseRootPath + fileName) +
                "?version=" + ConfigurationManager.AppSettings["Version"]);
        }
         
 
    }  
}