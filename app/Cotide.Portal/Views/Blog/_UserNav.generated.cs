﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17626
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cotide.Portal.Views.Blog
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.5.4.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Blog/_UserNav.cshtml")]
    public partial class UserNav : System.Web.Mvc.WebViewPage<Cotide.Web.Controllers.ViewModels.HistoryUserViewModel>
    {
        public UserNav()
        {
        }
        public override void Execute()
        {

WriteLiteral("<div class=\"navbar-inner\"> <div class=\"container-fluid\">  <a class=\"brand\" href=\"" +
"");


            
            #line 2 "..\..\Views\Blog\_UserNav.cshtml"
                                                                            Write(Url.Action("Index", "Blog"));

            
            #line default
            #line hidden
WriteLiteral("\">");


            
            #line 2 "..\..\Views\Blog\_UserNav.cshtml"
                                                                                                          Write(Model.BlogName);

            
            #line default
            #line hidden
WriteLiteral(" </a>   <div class=\"nav-collapse collapse\">  <ul id=\"mainNav\" class=\"nav\">   <li>" +
"<a href=\"");


            
            #line 2 "..\..\Views\Blog\_UserNav.cshtml"
                                                                                                                                                                                                                   Write(Url.Action("Index", "Blog"));

            
            #line default
            #line hidden
WriteLiteral("\" ><i class=\"icon-home icon-white\"></i> 首页</a></li>   <li ><a href=\"");


            
            #line 2 "..\..\Views\Blog\_UserNav.cshtml"
                                                                                                                                                                                                                                                                                                                   Write(Url.Action("ZuoPin", "Blog"));

            
            #line default
            #line hidden
WriteLiteral("\"> <i class=\"icon-pencil icon-white\"></i>  代表作品</a></li> \r\n");


            
            #line 3 "..\..\Views\Blog\_UserNav.cshtml"
                 if (Model.IsCurrentLoginUser)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <li><a href=\"");


            
            #line 5 "..\..\Views\Blog\_UserNav.cshtml"
                            Write(Url.Action("Index", "BlogManager", new { @domain = Model.Domain }));

            
            #line default
            #line hidden
WriteLiteral("\">网站管理</a></li>\r\n");


            
            #line 6 "..\..\Views\Blog\_UserNav.cshtml"
                }
            
            #line default
            #line hidden
WriteLiteral(@"   </ul>  </div> <ul class=""nav pull-right""> <li class=""dropdown""><a href=""#"" class=""dropdown-toggle"" data-toggle=""dropdown"">分享 <b
                    class=""caret""></b></a> <ul style=""cursor: pointer;"" class=""dropdown-menu"">  <li><a class=""jiathis_button_qzone"">QQ空间</a></li>  <li><a class=""jiathis_button_tsina"">新浪微博</a></li> <li><a class=""jiathis_button_tqq"">腾讯微博</a></li> <li><a class=""jiathis_button_renren"">人人网</a></li> <li><a href=""http://www.jiathis.com/share"" class=""jiathis jiathis_txt jiathis_separator jtico jtico_jiathis""   target=""_blank"">更多</a></li> </ul>
                </li>
");


            
            #line 9 "..\..\Views\Blog\_UserNav.cshtml"
                 if (!string.IsNullOrEmpty(Model.WeiBoUrl) || !string.IsNullOrEmpty(Model.Email))
                  {
            
            #line default
            #line hidden
WriteLiteral("  <li class=\"dropdown\"><a href=\"javascript:void(0);\" class=\"dropdown-toggle\" data" +
"-toggle=\"dropdown\">\r\n                    联系我 <b class=\"caret\"></b></a>  <ul clas" +
"s=\"dropdown-menu\">\r\n");


            
            #line 12 "..\..\Views\Blog\_UserNav.cshtml"
                          if (!string.IsNullOrEmpty(Model.WeiBoUrl))
                           {
            
            #line default
            #line hidden
WriteLiteral("   <li><a target=\"_blank\" href=\"");


            
            #line 13 "..\..\Views\Blog\_UserNav.cshtml"
                                                       Write(Model.WeiBoUrl);

            
            #line default
            #line hidden
WriteLiteral("\"><b>微博: </b>\r\n                            ");


            
            #line 14 "..\..\Views\Blog\_UserNav.cshtml"
                       Write(Model.WeiBoUrl);

            
            #line default
            #line hidden
WriteLiteral("</a></li>  ");


            
            #line 14 "..\..\Views\Blog\_UserNav.cshtml"
                                                      }  

            
            #line default
            #line hidden

            
            #line 15 "..\..\Views\Blog\_UserNav.cshtml"
                         if (!string.IsNullOrEmpty(Model.Email))
                           {
            
            #line default
            #line hidden
WriteLiteral("  <li><a href=\"javascript:void(0);\"><i class=\"icon-envelope\"></i>   ");


            
            #line 16 "..\..\Views\Blog\_UserNav.cshtml"
                                                                                           Write(Model.Email);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n");


            
            #line 17 "..\..\Views\Blog\_UserNav.cshtml"
                         }
            
            #line default
            #line hidden
WriteLiteral("   </ul>  </li>\r\n");


            
            #line 18 "..\..\Views\Blog\_UserNav.cshtml"
                } 

            
            #line default
            #line hidden

            
            #line 19 "..\..\Views\Blog\_UserNav.cshtml"
                 if (ViewBag.IsLogin)
                {
            
            #line default
            #line hidden
WriteLiteral("    <li><a href=\"");


            
            #line 20 "..\..\Views\Blog\_UserNav.cshtml"
                             Write(Url.Action("LoginOut", "Reg"));

            
            #line default
            #line hidden
WriteLiteral("\">退出</a> </li>  ");


            
            #line 20 "..\..\Views\Blog\_UserNav.cshtml"
                                                                                }   
            
            
            #line default
            #line hidden
WriteLiteral("  </ul>  </div> </div>\r\n");


        }
    }
}
#pragma warning restore 1591