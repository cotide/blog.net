using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public static class ControlExExtensions
    {
        #region MVC分页实现

        /// <summary>  
        /// 分页Pager显示  
        /// </summary>   
        /// <param name="html"></param>  
        /// <param name="currentPageStr">标识当前页码的QueryStringKey</param>    
        /// <param name="pageSize">每页显示</param>  
        /// <param name="totalCount">总数据量</param>
        /// <param name="requestUrl">请求URL</param>
        /// <returns></returns> 
        public static MvcHtmlString Pager(
            this HtmlHelper html, 
            string currentPageStr,
            int pageSize, 
            int totalCount,
            string requestUrl=null)
        {
            // 当前请求
            var request = HttpContext.Current.Request;

            var requestQueryStr = request.QueryString;
            var parameter = requestQueryStr.AllKeys.
                                            Where(
                                                str =>
                                                !string.IsNullOrEmpty(str) 
                                                && str.ToLower() != currentPageStr.ToLower())
                                           .
                                            Aggregate("",
                                                      (current, str) =>
                                                      current + 
                                                      string.Format("&{0}={1}", str, requestQueryStr[str]));

            // Url地址
            var url = request.CurrentExecutionFilePath;
             
            // 当前页码
            var currentPage = 1; 
            
            if (request[currentPageStr] != null)
            {

                int.TryParse(request[currentPageStr], out currentPage);
            } 

            // 总页数
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1);
            
            if (currentPage <= 0)
            {
                currentPage = 1;
            }
            var output = new StringBuilder("<div pagerId='pageNav' class='pageNav'>");
            if (totalPages > 1)
            {
                //处理首页链接    
                output.AppendFormat("<a href='{0}?{1}={2}{3}'>{4}</a>",
                    url,
                    currentPageStr,
                    "1",
                    parameter,
                    "首页");
                output.Append(" ");
                const int currint = 5;
                var startNum = currentPage - currint <= 1 ? 1 : currentPage - currint;
                var endNum = startNum + (currint * 2) > totalPages ? totalPages : (startNum + (currint * 2));

                for (int i = startNum; i <= endNum; i++)
                {
                    if (i == currentPage)
                    {
                        output.Append(string.Format("<strong>{0}</strong>", currentPage));
                    }
                    else
                    {
                        //一般页处理  
                        output.AppendFormat("<a href='{0}?{1}={2}{3}'>{4}</a>",
                                            url,
                                            currentPageStr,
                                            i,
                                            parameter,
                                            i);

                    }

                } 
                output.AppendFormat("<a href='{0}?{1}={2}{3}'>{4}</a>",
                            url,
                            currentPageStr,
                            totalPages,
                            parameter,
                        "末页"); 

                output.Append(" ");
                output.Append("</div>");
                return new MvcHtmlString(output.ToString());
            }
            return new MvcHtmlString(""); 
        }


        /// <summary>  
        /// 分页Pager显示  
        /// </summary>   
        /// <param name="html"></param>
        /// <param name="panelId">刷新容器ID</param>
        /// <param name="currentPageStr">标识当前页码的QueryStringKey</param>    
        /// <param name="pageSize">每页显示</param>  
        /// <param name="totalCount">总数据量</param>
        /// <param name="requestUrl">请求URL</param>
        /// <returns></returns> 
        public static MvcHtmlString AjaxPager(
            this HtmlHelper html,
            string panelId,
            string currentPageStr,
            int pageSize,
            int totalCount,
            string requestUrl = null)
        {

            // 当前请求
            var request = HttpContext.Current.Request;
            // Url地址
            var url = request.CurrentExecutionFilePath;
            // 当前页码
            var currentPage = 1;
            //总页数
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1);
            // 其他附带请求参数
            int.TryParse(request[currentPageStr], out currentPage);
            if (currentPage <= 0)
            {
                currentPage = 1;
            }
            var output = new StringBuilder(string.Format("<div pagerId='AjaxPageNav' panelId='{0}' class='pageNav'>", panelId));
            if (totalPages > 1)
            {
                //处理首页链接    
                output.AppendFormat("<a tag='{0}?{1}={2}' href='javascript:void(0);'>{3}</a>", url, currentPageStr, 1, "首页");
                output.Append(" ");
                int currint = 5;
                for (int i = 0; i <= 10; i++)
                {
                    //一共最多显示10位页码，前面5位，后面5位  
                    if ((currentPage + i - currint) >= 1 && (currentPage + i - currint) <= totalPages)
                        if (currint == i)
                        {
                            //当前页处理  
                            output.Append(string.Format("<strong>{0}</strong>", currentPage));
                        }
                        else
                        {
                            //一般页处理  
                            output.AppendFormat("<a tag='{0}?{1}={2}' href='javascript:void(0);'>{3}</a>",
                                url,
                                currentPageStr,
                                (currentPage + i - currint),
                                (currentPage + i - currint));

                        }

                }
                output.Append(" ");
                output.AppendFormat("<a tag='{0}?{1}={2}' href='javascript:void(0);'>{3}</a>",
                               url,
                               currentPageStr,
                               totalPages,
                              "末页");
                output.Append(" ");
                // 添加异步脚本 
                output.Append(@"<script type='text/javascript'>");
                output.Append(@"$(function () {");
                output.Append(string.Format(@"$('{0}').AjaxPager();", "div[pagerId=\"AjaxPageNav\"]"));
                output.Append(@"});");
                output.Append(@"</script>");
               
            }
            output.Append("</div>");
            return new MvcHtmlString(output.ToString());
        }



        #endregion

        /// <summary>
        /// 列表控件
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="source"></param>
        /// <param name="option"></param>
        /// <param name="selValue"></param>
        /// <returns></returns>
        public static MvcHtmlString DropDownListExFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, 
            IList<SelectListItem> source,
            DropDownListOption option, 
            string selValue = null,
            object htmlAttributes = null
           )
        {

            var attrs = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
       

            if (!string.IsNullOrEmpty(option.Description))
            {
                attrs.Add("description", option.Description);
            }
            if (!string.IsNullOrEmpty(option.FocusMsg))
            {
                attrs.Add("focusMsg", option.FocusMsg);
            }

            if (option.IsRequired)
            {
                attrs.Add("isEmpty", "false");
                if (!string.IsNullOrEmpty(option.EmptyMsg))
                {
                    attrs.Add("emptyMsg", option.EmptyMsg);
                }
                else
                {
                    attrs.Add("emptyMsg", "请选择选项");
                }
            }
            else
            {
                attrs.Add("isEmpty", "true");
            }
            attrs.Add("validator", "true");
            return htmlHelper.DropDownList(ExpressionHelper.GetExpressionText(expression), source, attrs);
        }


        /// <summary>
        /// 列表控件（无限极）
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="source">绑定数据源</param>
        /// <param name="option">选项</param>
        /// <param name="name"></param>
        /// <param name="selValue">选中值</param>
        /// <returns></returns>
        public static MvcHtmlString DropDownListEx(this System.Web.Mvc.HtmlHelper htmlHelper,
            string name,
            IList<SelectListItem> source,
            DropDownListOption option,
            string selValue = null,
            object htmlAttributes = null
           )
        {
            var attrs = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
       
            if (!string.IsNullOrEmpty(option.Description))
            {
                attrs.Add("description", option.Description);
            }
            if (!string.IsNullOrEmpty(option.FocusMsg))
            {
                attrs.Add("focusMsg", option.FocusMsg);
            }

            if (option.IsRequired)
            {
                attrs.Add("isEmpty", "false");
                if (!string.IsNullOrEmpty(option.EmptyMsg))
                {
                    attrs.Add("emptyMsg", option.EmptyMsg);
                }
                else
                {
                    attrs.Add("emptyMsg", "请选择选项");
                }
            }
            else
            {
                attrs.Add("isEmpty", "true");
            }
            attrs.Add("validator", "true");
            return htmlHelper.DropDownList(ExpressionHelper.GetExpressionText(name), source, attrs); 
        }


        /// <summary>
        /// 验证脚本
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="formId">表单ID</param>
        /// <returns></returns>
        public static MvcHtmlString ValidationExSummary(this System.Web.Mvc.HtmlHelper htmlHelper, string formId)
        {
            if (MvcHtmlString.IsNullOrEmpty(htmlHelper.ValidationSummary()))
            {
                return InitValidatorsScript(formId);
            }
            var result = htmlHelper.ValidationSummary().ToHtmlString() + InitValidatorsScript(formId);
            return new MvcHtmlString(result);
        }

        /// <summary>
        /// 提示容器
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="tip"></param>
        /// <returns></returns>
        public static MvcHtmlString ValidationMessageExFor<TModel, TProperty>(
           this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TProperty>> expression,
           object htmlAttributes = null,
           string tip = "Tip")
        {
            var attrs = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
        
            var oupString = new StringBuilder();
            oupString.Append(string.Format("<div id=\"{0}Tip\" ", ExpressionHelper.GetExpressionText(expression)));
            foreach (var o in attrs)
            {
                oupString.Append(string.Format(" {0}='{1}' ", o.Key, o.Value));
            }
            oupString.Append("></div>");
            return new MvcHtmlString(oupString.ToString());
        }

        /// <summary>
        /// 提示容器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="tip"></param>
        /// <returns></returns>
        public static MvcHtmlString ValidationMessageEx(
            this System.Web.Mvc.HtmlHelper htmlHelper,
            string name,
           object htmlAttributes = null,
           string tip = "Tip")
        {
            var attrs = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
        

            var oupString = new StringBuilder();
            oupString.Append(string.Format("<div id=\"{0}Tip\" ", name));
            foreach (var o in attrs)
            {
                oupString.Append(string.Format(" {0}='{1}' ", o.Key, o.Value));
            }
            oupString.Append("></div>");
            return new MvcHtmlString(oupString.ToString());
        }



        /// <summary>
        /// 验证密码文本框
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static MvcHtmlString PasswordExFor<TModel, TProperty>(
           this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TProperty>> expression,
           object htmlAttributes,
           PassValidateOption option)
        {

            // 拼装验证属性 
            var attrs = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
         
            // 默认描述
            if (!string.IsNullOrEmpty(option.Description))
            {
                attrs.Add("description", option.Description);
            }
            // 失去焦点描述
            if (!string.IsNullOrEmpty(option.FocusMsg))
            {
                attrs.Add("focusMsg", option.FocusMsg);
            }
            // 范围验证
            if (option.Min != null)
            {
                attrs.Add("min", option.Min);
                if (!string.IsNullOrEmpty(option.RangeMsg))
                {
                    attrs.Add("rangeMsg", option.RangeMsg);
                }
            }

            // 非空验证
            if (option.IsEmpty)
            {
                attrs.Add("isEmpty", "true");
            }
            else
            {
                if (!string.IsNullOrEmpty(option.EmptyMsg))
                {
                    attrs.Add("emptyMsg", option.EmptyMsg);
                }
                attrs.Add("isEmpty", "false");
            }

            if (option.Max != null)
            {
                attrs.Add("max", option.Max);
                if (option.RangeMsg != null && attrs.ContainsKey(option.RangeMsg))
                {
                    attrs.Add("rangeMsg", option.RangeMsg);
                }
            }
            if (option.CompareElem != null)
            {
                attrs.Add("compareElemId", option.CompareElem);
                if (!string.IsNullOrEmpty(option.CompareElemMsg))
                {
                    attrs.Add("compareElemMsg", option.CompareElemMsg);
                }
            }


            attrs.Add("validator", "true");
            return htmlHelper.PasswordFor(expression, attrs);
        }

        /// <summary>
        /// 验证文本框
        /// </summary>
        /// <param name="name">验证文本框Name</param>
        /// <param name="htmlAttributes"></param>
        /// <param name="option">验证规则</param>
        /// <returns></returns>
        public static MvcHtmlString TextBoxEx(
            this HtmlHelper htmlHelper,
            string name ,
            object htmlAttributes,
            ValidateOption option = null
          )
        {
            // 拼装验证属性 
            var attrs = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
         


            // 默认描述
            if (!string.IsNullOrEmpty(option.Description))
            {
                attrs.Add("description", option.Description);
            }
            // 失去焦点描述
            if (!string.IsNullOrEmpty(option.FocusMsg))
            {
                attrs.Add("focusMsg", option.FocusMsg);
            }
            // 范围验证
            if (option.Min != null)
            {
                attrs.Add("min", option.Min);
                if (!string.IsNullOrEmpty(option.RangeMsg))
                {
                    attrs.Add("rangeMsg", option.RangeMsg);
                }
            }

            // 非空验证
            if (option.IsEmpty)
            {
                attrs.Add("isEmpty", "true");
            }
            else
            {
                if (!string.IsNullOrEmpty(option.EmptyMsg))
                {
                    attrs.Add("emptyMsg", option.EmptyMsg);
                }
                attrs.Add("isEmpty", "false");
            }


            //正则验证
            if (!string.IsNullOrEmpty(option.Regex))
            {
                attrs.Add("regex", option.Regex);
                attrs.Add("regexMsg", option.RegexMsg);
            }


            if (option.Max != null)
            {
                attrs.Add("max", option.Max);
                if (option.RangeMsg != null && attrs.ContainsKey(option.RangeMsg))
                {
                    attrs.Add("rangeMsg", option.RangeMsg);
                }
            }
            if (option.AjaxUrl != null)
            {
                attrs.Add("ajax", option.AjaxUrl);
            }
            if (option.CompareElem != null)
            {
                attrs.Add("compareElemId", option.CompareElem);
                if (!string.IsNullOrEmpty(option.CompareElemMsg))
                {
                    attrs.Add("compareElemMsg", option.CompareElemMsg);
                }
            }

            attrs.Add("validator", "true");
            return htmlHelper.TextBox(name, null, attrs);
        }


        public static MvcHtmlString TextAreaExFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes,
            ValidateOption option = null
          )
        {
            // 拼装验证属性 
            var attrs = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
         
            // 默认描述
            if (!string.IsNullOrEmpty(option.Description))
            {
                attrs.Add("description", option.Description);
            }
            // 失去焦点描述
            if (!string.IsNullOrEmpty(option.FocusMsg))
            {
                attrs.Add("focusMsg", option.FocusMsg);
            }
            // 范围验证
            if (option.Min != null)
            {
                attrs.Add("min", option.Min);
                if (!string.IsNullOrEmpty(option.RangeMsg))
                {
                    attrs.Add("rangeMsg", option.RangeMsg);
                }
            }


            // 非空验证
            if (option.IsEmpty)
            {
                attrs.Add("isEmpty", "true");
            }
            else
            {
                if (!string.IsNullOrEmpty(option.EmptyMsg))
                {
                    attrs.Add("emptyMsg", option.EmptyMsg);
                }
                attrs.Add("isEmpty", "false");
            }


            //正则验证
            if (!string.IsNullOrEmpty(option.Regex))
            {
                attrs.Add("regex", option.Regex);
                attrs.Add("regexMsg", option.RegexMsg);
            }

            if (option.Max != null)
            {
                attrs.Add("max", option.Max);
                if (option.RangeMsg != null && attrs.ContainsKey(option.RangeMsg))
                {
                    attrs.Add("rangeMsg", option.RangeMsg);
                }
            }
            if (option.AjaxUrl != null)
            {
                attrs.Add("ajax", option.AjaxUrl);
            }
            if (option.CompareElem != null)
            {
                attrs.Add("compareElemId", option.CompareElem);
                if (!string.IsNullOrEmpty(option.CompareElemMsg))
                {
                    attrs.Add("compareElemMsg", option.CompareElemMsg);
                }
            }


            attrs.Add("validator", "true");
            return htmlHelper.TextAreaFor(expression, attrs);
        }




        /// <summary>
        /// 验证文本框
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="option">验证规则</param>
        /// <returns></returns>
        public static MvcHtmlString TextBoxExFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes,
            ValidateOption option = null
          )
        {
            // 拼装验证属性 
            var tagerHtmlAtt = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
           
            // 默认描述
            if (!string.IsNullOrEmpty(option.Description))
            {
                tagerHtmlAtt.Add("description", option.Description);
            }
            // 失去焦点描述
            if (!string.IsNullOrEmpty(option.FocusMsg))
            {
                tagerHtmlAtt.Add("focusMsg", option.FocusMsg);
            }
            // 范围验证
            if (option.Min != null)
            {
                tagerHtmlAtt.Add("min", option.Min);
                if (!string.IsNullOrEmpty(option.RangeMsg))
                {
                    tagerHtmlAtt.Add("rangeMsg", option.RangeMsg);
                }
            }


            // 非空验证
            if (option.IsEmpty)
            {
                tagerHtmlAtt.Add("isEmpty", "true");
            }
            else
            {
                if (!string.IsNullOrEmpty(option.EmptyMsg))
                {
                    tagerHtmlAtt.Add("emptyMsg", option.EmptyMsg);
                }
                tagerHtmlAtt.Add("isEmpty", "false");
            }


            //正则验证
            if (!string.IsNullOrEmpty(option.Regex))
            {
                tagerHtmlAtt.Add("regex",option.Regex);
                tagerHtmlAtt.Add("regexMsg", option.RegexMsg);
            }

            if (option.Max != null)
            {
                tagerHtmlAtt.Add("max", option.Max);
                if (option.RangeMsg != null && tagerHtmlAtt.ContainsKey(option.RangeMsg))
                {
                    tagerHtmlAtt.Add("rangeMsg", option.RangeMsg);
                }
            }
            if (option.AjaxUrl != null)
            {
                tagerHtmlAtt.Add("ajax", option.AjaxUrl);
            }
            if (option.CompareElem != null)
            {
                tagerHtmlAtt.Add("compareElemId", option.CompareElem);
                if (!string.IsNullOrEmpty(option.CompareElemMsg))
                {
                    tagerHtmlAtt.Add("compareElemMsg", option.CompareElemMsg);
                }
            }

           


            tagerHtmlAtt.Add("validator", "true");
            return htmlHelper.TextBoxFor(expression, tagerHtmlAtt);
        }


        #region Helper

        private static IEnumerable<SelectListItem> CreateSelectListItem(
             DataTable source,
             DropDownListOption option,
             string selValue = null)
        {
            var isBindValue = selValue != null;
            var selectListItem = new List<SelectListItem>
                                     {
                                         new SelectListItem()
                                             {
                                                 Selected = true,
                                                 Text = option.TopTitle,
                                                 Value = ""
                                             }
                                     };
            if (source != null && source.Rows.Count > 0)
            {
                var drs = source.Select(string.Format("{0} = {1}", option.BaseIdColumnsName, option.TopParentID));

                foreach (DataRow dr in drs)
                {
                    //获取ID
                    var id = dr[option.IdColumnsName].ToString();
                    //获取类型名称
                    var typeName = dr[option.DisplayColumnsName].ToString();
                    //顶级分类显示形式
                    typeName = option.TopParentStyle + typeName;
                    //绑定数据
                    if (isBindValue && id == selValue)
                    {
                        selectListItem.Add(new SelectListItem() { Text = typeName, Value = id, Selected = true });
                    }
                    else
                    {
                        selectListItem.Add(new SelectListItem() { Text = typeName, Value = id });
                    }
                    var nowID = -1;
                    int.TryParse(id, out nowID);
                    if (nowID > 0)
                    {
                        BindNode(selectListItem, source, nowID, option, option.Subkey, isBindValue, selValue);
                    }

                }
            }
            return selectListItem;
        }


        /// <summary>
        /// 绑定子分类
        /// </summary> 
        private static void BindNode(List<SelectListItem> e,
            DataTable source,
            int topParentID,
            DropDownListOption option,
            string blank,
            bool isBindValue,
            string selValue = null
            )
        {
            var drs = source.Select(string.Format("{0}= {1}", option.BaseIdColumnsName.Trim(), topParentID));
            foreach (var dr in drs)
            {
                //获取ID
                var id = dr[option.IdColumnsName].ToString();
                //获取类型名称
                var typeName = dr[option.DisplayColumnsName].ToString();
                //顶级分类显示形式
                typeName = blank + typeName;

                if (isBindValue && id == selValue)
                {
                    e.Add(new SelectListItem() { Text = typeName, Value = id, Selected = true });
                }
                else
                {
                    e.Add(new SelectListItem() { Text = typeName, Value = id });
                }

                // 设置下一位样式
                var nextBlank = blank + "--";
                var nowID = -1;
                int.TryParse(id, out nowID);
                if (nowID > 0)
                {

                    BindNode(e, source, nowID, option, nextBlank, isBindValue, selValue);
                }
            }

        }

        /// <summary>
        /// 生成客户端验证脚本
        /// </summary> 
        /// <returns></returns>
        private static MvcHtmlString InitValidatorsScript(string formId)
        {
            var outPut = new StringBuilder();
            outPut.Append(@"<script type='text/javascript'>");
            outPut.Append(@"$(function () {");
            outPut.Append(string.Format(@"$('#{0}').formValidator();", formId));
            outPut.Append(@"});");
            outPut.Append(@"</script>");
            return new MvcHtmlString(outPut.ToString());
        }

        #endregion
    }


    /// <summary>
    /// 编辑器类型
    /// </summary>
    public enum HtmlEditorType
    {
        /// <summary>
        /// 基础模式
        /// </summary>
        Basic,
        /// <summary>
        /// 全量模式
        /// </summary>
        Full
    }


}