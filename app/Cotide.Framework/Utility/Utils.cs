using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace Cotide.Framework.Utility
{
    /// <summary>
    /// 公共方法，通用方法类库
    /// </summary>
    public class Utils
    { 
        /// <summary>
        ///  解析含有后缀字符串，返回后缀名
        /// </summary>
        /// <param name="strIn">要解析的字符串</param>
        /// <returns>返回后缀名，如果不包含'.'返回字符串本身</returns>
        public static string GetUrlSuffix(string strIn)
        {
            int index = strIn.LastIndexOf(".");
            if (index > 0)
            {
                return strIn.Substring(index + 1);
            }
            return string.Empty;
        }


        #region /// 文件操作

        /// <summary>
        /// 获取指定文件的扩展名
        /// </summary>
        /// <param name="fileName">指定文件名</param>
        /// <returns>扩展名</returns>
        public static string GetFileExtName(string fileName)
        {
            if (StrIsNullOrEmpty(fileName) || fileName.IndexOf('.') <= 0)
                return "";

            fileName = fileName.ToLower().Trim();

            return fileName.Substring(fileName.LastIndexOf('.'), fileName.Length - fileName.LastIndexOf('.'));
        }

        #endregion

        #region /// 字符串处理

        /// <summary>
        /// 清楚html标签
        /// </summary>
        /// <param name="strHtml">要清除字符串</param>
        /// <returns>返回清除后的字符串</returns>
        public static string ClearHtml(string strHtml)
        {
            if (strHtml != "")
            {
                Regex regex = null;
                Match match = null;
                regex = new Regex(@"<\/?[^>]*>", RegexOptions.IgnoreCase);
                for (match = regex.Match(strHtml); match.Success; match = match.NextMatch())
                {
                    strHtml = strHtml.Replace(match.Groups[0].ToString(), "");
                }
            }
            return strHtml;
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="startIndex">开始位置</param>
        /// <returns></returns>
        public static string CutString(string str, int startIndex)
        {
            return CutString(str, startIndex, str.Length);
        }

        public static string CutStringBySuffix(string str, int startIndex, int length,string suffix )
        {
            var  result = CutString(str, startIndex, length);
            if(str.Length>length)
            {
                result=result + suffix;
            }
            return result;
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="startIndex">开始位置</param>
        /// <param name="length">截取大小</param>
        /// <returns></returns>
        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length *= -1;
                    if ((startIndex - length) < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        startIndex -= length;
                    }
                }
                if (startIndex > str.Length)
                {
                    return "";
                }
            }
            else if ((length >= 0) && ((length + startIndex) > 0))
            {
                length += startIndex;
                startIndex = 0;
            }
            else
            {
                return "";
            }
            if ((str.Length - startIndex) < length)
            {
                length = str.Length - startIndex;
            }
            return str.Substring(startIndex, length);
        }


        #endregion


        /// <summary>
        /// 把数组转换为字符,以逗号分开
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static string ArrayToString(string[] strs)
        {
            var str = "";
            foreach (var str2 in strs)
            {
                str = str + "," + str2;
            }
            return str.Substring(1);
        }

        /// <summary>
        /// 过滤包含非法字符SQL语句 
        /// </summary>
        /// <param name="str">要坚持字符串</param>
        /// <returns>返回</returns>
        public static string ChkSql(string str)
        {
            if (str == null)
            {
                return "";
            }
            str = str.Replace("'", "''");
            return str;
        }

        public static string CleanInput(string strIn)
        {
            return Regex.Replace(strIn.Trim(), @"[^\w\.@-]", "");
        }

        /// <summary>
        /// 清除所有\r\n字符
        /// </summary>
        /// <param name="str">要清除字符串</param>
        /// <returns>返回处理后字符串</returns>
        public static string ClearBr(string str)
        {
            Match match = null;
            var regex = new Regex(@"(\r\n)", RegexOptions.IgnoreCase);
            for (match = regex.Match(str); match.Success; match = match.NextMatch())
            {
                str = str.Replace(match.Groups[0].ToString(), "");
            }
            return str;
        }

        /// <summary>
        /// 清除Cookie
        /// </summary>
        /// <param name="strName">Cookie键值</param>
        /// <param name="domainName">关联的域。</param>
        public static void ClearCookie(string strName, string domainName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null) return;
            if (!domainName.Trim().Equals(""))
            {
                cookie.Domain = domainName;
            }
            cookie.Value = null;
            cookie.Expires = DateTime.Now.AddDays(-1.0);
            cookie.Values.Clear();
            HttpContext.Current.Response.Cookies.Set(cookie);
        }


        /// <summary>
        /// 清除Session对象
        /// </summary>
        /// <param name="sessionName">Session对象键值</param>
        public static void ClearSession(string sessionName)
        {
            HttpContext.Current.Session.Remove(sessionName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CreateDir(string name)
        {
            return MakeSureDirectoryPathExists(name);
        }

  
        public static bool DeleteFile(string fileFullPath)
        {
            if (System.IO.File.Exists(fileFullPath))
            {
                try
                {
                    System.IO.File.Delete(fileFullPath);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public static string EncodeHtml(string strHtml)
        {
            if (strHtml != "")
            {
                strHtml = strHtml.Replace(",", "&def");
                strHtml = strHtml.Replace("'", "&dot");
                strHtml = strHtml.Replace(";", "&dec");
                return strHtml;
            }
            return "";
        }

        public static bool FileExists(string filename)
        {
            return System.IO.File.Exists(filename);
        }

        private static string FilterAHrefScript(string content)
        {
            string input = FilterScript(content);
            const string pattern = @" href[ ^=]*= *[\s\S]*script *:";
            return Regex.Replace(input, pattern, string.Empty, RegexOptions.IgnoreCase);
        }

        private static string FilterFrameset(string content)
        {
            const string pattern = @"(?i)<Frameset([^>])*>(\w|\W)*</Frameset([^>])*>";
            return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
        }

        private static string FilterHtml(string content)
        {
            string input = FilterScript(content);
            const string pattern = "<[^>]*>";
            return Regex.Replace(input, pattern, string.Empty, RegexOptions.IgnoreCase);
        }

        private static string FilterIframe(string content)
        {
            const string pattern = @"(?i)<Iframe([^>])*>(\w|\W)*</Iframe([^>])*>";
            return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
        }

        private static string FilterInclude(string content)
        {
            string input = FilterScript(content);
            const string pattern = @"<[\s\S]*include *(file|virtual) *= *[\s\S]*\.(js|vbs|asp|aspx|php|jsp)[^>]*>";
            return Regex.Replace(input, pattern, string.Empty, RegexOptions.IgnoreCase);
        }

        private static string FilterObject(string content)
        {
            const string pattern = @"(?i)<Object([^>])*>(\w|\W)*</Object([^>])*>";
            return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
        }

        public static string FilterScript(string content)
        {
            const string str = @"(?'comment'<!--.*?--[ \n\r]*>)";
            const string str2 = @"(\/\*.*?\*\/|\/\/.*?[\n\r])";
            var str3 = string.Format(@"(?'script'<[ \n\r]*script[^>]*>(.*?{0}?)*<[ \n\r]*/script[^>]*>)", str2);
            var pattern = string.Format("(?s)({0}|{1})", str, str3);
            return StripScriptAttributesFromTags(Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase));
        }

        private static string FilterSrc(string content)
        {
            var input = FilterScript(content);
            const string pattern = " src *= *['\"]?[^\\.]+\\.(js|vbs|asp|aspx|php|jsp)['\"]";
            return Regex.Replace(input, pattern, "", RegexOptions.IgnoreCase);
        }

        public static string[] FindNoUtf8File(string path)
        {
            var builder = new StringBuilder();
            var files = new DirectoryInfo(path).GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                if (!files[i].Extension.ToLower().Equals(".htm")) continue;
                var sbInputStream = new FileStream(files[i].FullName, FileMode.Open, FileAccess.Read);
                bool flag = IsUtf8(sbInputStream);
                sbInputStream.Close();
                if (!flag)
                {
                    builder.Append(files[i].FullName);
                    builder.Append("\r\n");
                }
            }
            return SplitString(builder.ToString(), "\r\n");
        }

        public static string FormatBytesStr(int bytes)
        {
            if (bytes > 0x40000000)
            {
// ReSharper disable PossibleLossOfFraction
                double num = bytes / 0x40000000;
// ReSharper restore PossibleLossOfFraction
                return (num.ToString("0") + "G");
            }
            if (bytes > 0x100000)
            {
// ReSharper disable PossibleLossOfFraction
                double num2 = bytes / 0x100000;
// ReSharper restore PossibleLossOfFraction
                return (num2.ToString("0") + "M");
            }
            if (bytes > 0x400)
            {
// ReSharper disable PossibleLossOfFraction
                double num3 = bytes / 0x400;
// ReSharper restore PossibleLossOfFraction
                return (num3.ToString("0") + "K");
            }
            return (bytes + "Bytes");
        }

        public static string GetAssemblyProductName()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductName;
        }

        public static string GetAssemblyVersion()
        {
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            return string.Format("{0}.{1}.{2}", versionInfo.FileMajorPart, versionInfo.FileMinorPart, versionInfo.FileBuildPart);
        }

        //public static string GetCookie(string strName)
        //{
        //    if ((HttpContext.Current.Request.Cookies[strName] != null))
        //    {
        //        return JSunescape(HttpContext.Current.Request.Cookies[strName].Value.ToString());
        //    }
        //    return "";
        //}

        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        public static string GetDate(string datetimestr, string replacestr)
        {
            if (datetimestr == null)
            {
                return replacestr;
            }
            if (datetimestr.Equals(""))
            {
                return replacestr;
            }
            try
            {
                datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
            }
            catch
            {
                return replacestr;
            }
            return datetimestr;
        }

        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }

        public static string GetEmailHostName(string strEmail)
        {
            if (strEmail.IndexOf("@") < 0)
            {
                return "";
            }
            return strEmail.Substring(strEmail.LastIndexOf("@")).ToLower();
        }

        public static string GetFilename(string url)
        {
            if (url == null)
            {
                return "";
            }
            string[] strArray = url.Split(new[] { '/' });
            return strArray[strArray.Length - 1].Split(new[] { '?' })[0];
        }

        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }

        public static int GetInArrayId(string strSearch, string[] stringArray)
        {
            return GetInArrayId(strSearch, stringArray, true);
        }

        public static int GetInArrayId(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                    {
                        return i;
                    }
                }
                else if (strSearch == stringArray[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public static string GetMapPath(string strPath)
        {
            return HttpContext.Current.Server.MapPath(strPath);
        }

        public static string GetMoney(string pPrice)
        {
            int index = pPrice.IndexOf(".");
            if ((index > 0) && (pPrice.Length > (index + 2)))
            {
                return pPrice.Substring(0, index + 3);
            }
            return pPrice;
        }

        /// <summary>
        ///  获取请求IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetRealIp()
        {
            return RequestHelper.GetIp();
        }

        public static string GetSession(string sessionName)
        {
            var sessionObj = GetSessionObj(sessionName);
            return sessionObj == null ? "" : sessionObj.ToString();
        }

        public static object GetSessionObj(string sessionName)
        {
            return HttpContext.Current.Session[sessionName];
        }

        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        public static string GetSubString(string pSrcString, int pLength, string pTailString)
        {
            string str = pSrcString;
            if (pLength < 0)
            {
                return str;
            }
            byte[] bytes = Encoding.Default.GetBytes(pSrcString);
            if (bytes.Length <= pLength)
            {
                return str;
            }
            var length = pLength;
            var numArray = new int[pLength];
            var num2 = 0;
            for (var i = 0; i < pLength; i++)
            {
                if (bytes[i] > 0x7f)
                {
                    num2++;
                    if (num2 == 3)
                    {
                        num2 = 1;
                    }
                }
                else
                {
                    num2 = 0;
                }
                numArray[i] = num2;
            }
            if ((bytes[pLength - 1] > 0x7f) && (numArray[pLength - 1] == 1))
            {
                length = pLength + 1;
            }
            var destinationArray = new byte[length];
            Array.Copy(bytes, destinationArray, length);
            return (Encoding.Default.GetString(destinationArray) + pTailString);
        }

        public static string Gettime(string date)
        {
            return date != "" ? Convert.ToDateTime(date).ToString("yyyy-MM-dd") : "";
        }

        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        public static string GetTrueForumPath()
        {
            var path = HttpContext.Current.Request.Path;
            return path.LastIndexOf("/") != path.IndexOf("/") ? path.Substring(path.IndexOf("/"), path.LastIndexOf("/") + 1) : "/";
        }

        public static string GetYear(DateTime begin, DateTime end)
        {
            if (end.Year == begin.Year)
            {
                if (end.Month == begin.Month)
                {
                    if ((end.Day - begin.Day) > 0)
                    {
                        return "1";
                    }
                    return "0";
                }
                if ((end.Month - begin.Month) > 0)
                {
                    return "1";
                }
                if ((end.Day - begin.Day) > 0)
                {
                    return "1";
                }
                return "0";
            }
            if ((end.Year - begin.Year) > 0)
            {
                if (end.Month == begin.Month)
                {
                    return (end.Day - begin.Day) > 0 ? Convert.ToString((end.Year - begin.Year) + 1) : Convert.ToString(end.Year - begin.Year);
                }
                return (end.Day - begin.Day) > 0 ? Convert.ToString((end.Year - begin.Year) + 1) : Convert.ToString(end.Year - begin.Year);
            }
            return "0";
        }

        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        public static string HtmlEncode(string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        public static bool InArray(string str, string[] stringarray)
        {
            return InArray(str, stringarray, false);
        }

        public static bool InArray(string str, string stringarray)
        {
            return InArray(str, SplitString(stringarray, ","), false);
        }

        public static bool InArray(string str, string stringarray, string strsplit)
        {
            return InArray(str, SplitString(stringarray, strsplit), false);
        }

        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            return (GetInArrayId(strSearch, stringArray, caseInsensetive) >= 0);
        }

        public static bool InArray(string str, string stringarray, string strsplit, bool caseInsensetive)
        {
            return InArray(str, SplitString(stringarray, strsplit), caseInsensetive);
        }

        public static bool InIpArray(string ip, string[] iparray)
        {
            var strArray = SplitString(ip, ".");
            for (var i = 0; i < iparray.Length; i++)
            {
                string[] strArray2 = SplitString(iparray[i], ".");
                var num2 = 0;
                for (int j = 0; j < strArray2.Length; j++)
                {
                    if (strArray2[j] == "*")
                    {
                        return true;
                    }
                    if ((strArray.Length <= j) || (strArray2[j] != strArray[j]))
                    {
                        break;
                    }
                    num2++;
                }
                if (num2 == 4)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsBase64String(string str)
        {
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }

        public static bool IsFloat(string strNumber)
        {
            return new Regex(@"^\d+\.{0,1}\d*$").IsMatch(strNumber);
        }

        public static bool IsImgFilename(string filename)
        {
            if (filename.EndsWith(".") || (filename.IndexOf(".") == -1))
            {
                return false;
            }
            string str = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
            if ((str != "jpg" && str != "jpeg") && (str != "png" && str != "bmp"))
            {
                return (str == "gif");
            }
            return true;
        }

        public static string GetFileExtension(string filename)
        {
            if (filename.EndsWith(".") || (filename.IndexOf(".") == -1))
            {
                return "";
            }
            return filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
        }

        public static bool IsIncludeChineseCode(string testString)
        {
            return Regex.IsMatch(testString, @"[\u4e00-\u9fa5]+");
        }

        public static bool IsIp(string ip)
        {
            return new Regex(@"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$").IsMatch(ip);
        }

        public static string IsLength(int length, string str)
        {
            var encoding = new ASCIIEncoding();
            int num = 0;
            string str2 = "";
            byte[] bytes = encoding.GetBytes(str);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0x3f)
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                try
                {
                    str2 = str2 + str.Substring(i, 1);
                }
                catch
                {
                    break;
                }
                if (num > length)
                {
                    break;
                }
            }
            if (Encoding.Default.GetBytes(str).Length > length)
            {
                str2 = str2 + "";
            }
            return str2;
        }

        public static bool IsNumber(string strNumber)
        {
            return new Regex(@"^\d+$").IsMatch(strNumber);
        }

        public static bool IsNumberArray(string[] strNumber)
        {
            if (strNumber == null)
            {
                return false;
            }
            if (strNumber.Length < 1)
            {
                return false;
            }
            foreach (string str in strNumber)
            {
                if (!IsNumber(str))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, "^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }

        private static bool IsUtf8(FileStream sbInputStream)
        {
            bool flag = true;
            long length = sbInputStream.Length;
            byte num2 = 0;
            for (int i = 0; i < length; i++)
            {
                var num4 = (byte)sbInputStream.ReadByte();
                if ((num4 & 0x80) != 0)
                {
                    flag = false;
                }
                if (num2 == 0)
                {
                    if (num4 >= 0x80)
                    {
                        do
                        {
                            num4 = (byte)(num4 << 1);
                            num2 = (byte)(num2 + 1);
                        }
                        while ((num4 & 0x80) != 0);
                        num2 = (byte)(num2 - 1);
                        if (num2 == 0)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if ((num4 & 0xc0) != 0x80)
                    {
                        return false;
                    }
                    num2 = (byte)(num2 - 1);
                }
            }
            if (num2 > 0)
            {
                return false;
            }
            if (flag)
            {
                return false;
            }
            return true;
        }

        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        //public static string JSEscape(string str)
        //{
        //    return GlobalObject.escape(str);
        //}

        //public static string JSunescape(string str)
        //{
        //    return GlobalObject.unescape(str);
        //}

        public static string LableSet(string strName, string strValue)
        {
            return ("┆" + strName + "$" + strValue);
        }


        public static string NoHTML(string Htmlstring) //去除HTML标记
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpUtility.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }

        [DllImport("dbgHelp", SetLastError = true)]
        private static extern bool MakeSureDirectoryPathExists(string name);
        public static string RemoveHtml(string html)
        {
            /*
            html = FilterScript(html);
            html = FilterAHrefScript(html);
            html = FilterSrc(html);
            html = FilterInclude(html);
            html = FilterObject(html);
            html = FilterIframe(html);
            html = FilterFrameset(html);
            html = FilterHtml(html);
            return html;*/
            return NoHTML(html);
        }

         

        public static string[] RepaceSpilthItem(string[] str)
        {
            var list = new ArrayList();
            foreach (string str2 in str)
            {
                if (!list.Contains(str2))
                {
                    list.Add(str2);
                }
            }
            return (string[])list.ToArray(typeof(string));
        }

        public static string ReplaceString(string sourceString, string searchString, string replaceString, bool isCaseInsensetive)
        {
           
            return Regex.Replace(sourceString, Regex.Escape(searchString), replaceString, isCaseInsensetive ? RegexOptions.IgnoreCase : RegexOptions.None);
        }

        public static string ReplaceStrToScript(string str)
        {
            str = str.Replace(@"\", @"\\");
            str = str.Replace("'", @"\'");
            str = str.Replace("\"", "\\\"");
            return str;
        }

        public static void ResponseFile(string filepath, string filename, string filetype)
        {
            Stream stream = null;
            var buffer = new byte[0x2710];
            try
            {
                stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
                var length = stream.Length;
                HttpContext.Current.Response.ContentType = filetype;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + UrlEncode(filename.Trim()));
                while (length > 0L)
                {
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        int count = stream.Read(buffer, 0, 0x2710);
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, count);
                        HttpContext.Current.Response.Flush();
                        buffer = new byte[0x2710];
                        length -= count;
                    }
                    else
                    {
                        length = -1L;
                    }
                }
            }
            catch (Exception exception)
            {
                HttpContext.Current.Response.Write("Error : " + exception.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            HttpContext.Current.Response.End();
        }

        public static string RTrim(string str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                char ch = str[i];
                if (!ch.Equals(" "))
                {
                    char ch2 = str[i];
                    if (!ch2.Equals("\r"))
                    {
                        char ch3 = str[i];
                        if (!ch3.Equals("\n"))
                        {
                            continue;
                        }
                    }
                }
                str.Remove(i, 1);
            }
            return str;
        }

        public static void SetSession(string sessionName, object value)
        {
            if (HttpContext.Current.Session[sessionName] == null)
            {
                HttpContext.Current.Session.Add(sessionName, value);
            }
            else
            {
                HttpContext.Current.Session[sessionName] = value;
            }
        }

        public static string Spaces(int nSpaces)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < nSpaces; i++)
            {
                builder.Append(" &nbsp;&nbsp;");
            }
            return builder.ToString();
        }

        public static string[] SplitString(string strContent, string strSplit)
        {
            if (strContent.IndexOf(strSplit) < 0)
            {
                return new[] { strContent };
            }
            return Regex.Split(strContent, strSplit.Replace(".", @"\."), RegexOptions.IgnoreCase);
        }

        public static string StrFilter(string str, string bantext)
        {
            var strArray = SplitString(bantext, "\r\n");
            for (int i = 0; i < strArray.Length; i++)
            {
                var oldValue = strArray[i].Substring(0, strArray[i].IndexOf("="));
                var newValue = strArray[i].Substring(strArray[i].IndexOf("=") + 1);
                str = str.Replace(oldValue, newValue);
            }
            return str;
        }

        public static string StrFormat(string str)
        {
            if (str == null)
            {
                return "";
            }
            str = str.Replace("\r\n", "<br />");
            str = str.Replace("\n", "<br />");
            return str;
        }

        private static string StripAttributesHandler(Match m)
        {
            if (m.Groups["attribute"].Success)
            {
                return m.Value.Replace(m.Groups["attribute"].Value, "");
            }
            return m.Value;
        }

        private static string StripScriptAttributesFromTags(string content)
        {
            const string str = "on(blur|c(hange|lick)|dblclick|focus|keypress|(key|mouse)(down|up)|(un)?load\r\n                            |mouse(move|o(ut|ver))|reset|s(elect|ubmit))";
            var regex = new Regex(string.Format("(?inx)\r\n                \\<(\\w+)\\s+\r\n                    (\r\n                        (?'attribute'\r\n                        (?'attributeName'{0})\\s*=\\s*\r\n                        (?'delim'['\"]?)\r\n                        (?'attributeValue'[^'\">]+)\r\n                        (\\3)\r\n                    )\r\n                    |\r\n                    (?'attribute'\r\n                        (?'attributeName'href)\\s*=\\s*\r\n                        (?'delim'['\"]?)\r\n                        (?'attributeValue'javascript[^'\">]+)\r\n                        (\\3)\r\n                    )\r\n                    |\r\n                    [^>]\r\n                )*\r\n            \\>", str));
            return regex.Replace(content, new MatchEvaluator(StripAttributesHandler));
        }

        public static float StrToFloat(object strValue, float defValue)
        {
            if ((strValue == null) || (strValue.ToString().Length > 10))
            {
                return defValue;
            }
            float num = defValue;
            if (new Regex(@"^([-]|[0-9])[0-9]*(\.\w*)?$").IsMatch(strValue.ToString()))
            {
                num = Convert.ToSingle(strValue);
            }
            return num;
        }

        public static int StrToInt(object strValue, int defValue)
        {
            if ((strValue == null) || (strValue.ToString().Length > 9))
            {
                return defValue;
            }
            var num = defValue;
            if (new Regex("^([-]|[0-9])[0-9]*$").IsMatch(strValue.ToString()))
            {
                num = Convert.ToInt32(strValue);
            }
            return num;
        }

        public static long StrToInt64(object strValue, int defValue)
        {
            if ((strValue == null) || (strValue.ToString().Length > 9))
            {
                return defValue;
            }
            long num = defValue;
            if (new Regex("^([-]|[0-9])[0-9]*$").IsMatch(strValue.ToString()))
            {
                return Convert.ToInt64(strValue);
            }
            return num;
        }

        public void TransHtml(string path, string outpath)
        {
            FileStream stream;
            var page = new Page();
            var writer = new StringWriter();
            page.Server.Execute(path, writer);
            if (System.IO.File.Exists(page.Server.MapPath("") + @"\" + outpath))
            {
                System.IO.File.Delete(page.Server.MapPath("") + @"\" + outpath);
                stream = System.IO.File.Create(page.Server.MapPath("") + @"\" + outpath);
            }
            else
            {
                stream = System.IO.File.Create(page.Server.MapPath("") + @"\" + outpath);
            }
            byte[] bytes = Encoding.Default.GetBytes(writer.ToString());
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
        }

        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        //public static void WriteCookie(string strName, string strValue)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(strName);
        //    }
        //    strValue = JSEscape(strValue);
        //    cookie.Value = strValue;
        //    HttpContext.Current.Response.AppendCookie(cookie);
        //}

        //public static void WriteCookie(string strName, string strValue, int expires)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(strName);
        //    }
        //    cookie.Value = JSEscape(strValue);
        //    cookie.Expires = DateTime.Now.AddMonths(expires);
        //    HttpContext.Current.Response.AppendCookie(cookie);
        //}

        //public static void WriteCookie(string strName, string strValue, string domain)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(strName);
        //        if (!domain.Trim().Equals(""))
        //        {
        //            cookie.Domain = domain;
        //        }
        //    }
        //    strValue = JSEscape(strValue);
        //    cookie.Value = strValue;
        //    HttpContext.Current.Response.AppendCookie(cookie);
        //}

        //public static void WriteCookie(string strName, string strValue, int expires, string domain)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(strName);
        //        if (!domain.Trim().Equals(""))
        //        {
        //            cookie.Domain = domain;
        //        }
        //    }
        //    cookie.Value = JSEscape(strValue);
        //    cookie.Expires = DateTime.Now.AddMonths(expires);
        //    HttpContext.Current.Response.AppendCookie(cookie);
        //}

        public static void Echo(string str)
        {
            HttpContext.Current.Response.Write(str);
        }



        /// <summary>
        /// 将IP地址转为整数形式
        /// </summary>
        /// <returns>整数</returns>
        public static long IpToLong(IPAddress ip)
        {
            int x = 3;
            long o = 0;
            foreach (byte f in ip.GetAddressBytes())
            {
                o += (long)f << 8 * x--;
            }
            return o;
        }

        /// <summary>
        /// 将整数转为IP地址
        /// </summary>
        /// <returns>IP地址</returns>
        public static IPAddress LongToIp(long l)
        {
            var b = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                b[3 - i] = (byte)(l >> 8 * i & 255);
            }
            return new IPAddress(b);
        }

        /// <summary>
        /// 验证传入IP地址是否应被屏蔽。
        /// </summary>
        /// <returns>是否应被屏蔽</returns>
        public bool CheckIpList(IPAddress iip, long ipstart, long ipend)
        {
            long ip = IpToLong(iip);
            if (ip >= ipstart && ip <= ipend)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检测单个IP是否被屏蔽
        /// </summary>
        /// <param name="iip"></param>
        /// <param name="ipsigle"></param>
        /// <returns></returns>
        public bool CheckIpSigle(IPAddress iip, long ipsigle)
        {
            long ip = IpToLong(iip);
            if (ip.Equals(ipsigle))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 得到分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="pageurl"></param>
        /// <returns></returns>
        public static string GetPager(int page, int pageSize, int count, string pageurl)
        {
            const int pageLine = 10;
            var pageCount = (int)Math.Ceiling((double)count / pageSize);
            var start = ((page - 1) / pageLine) * pageLine + 1;
            var sb = new StringBuilder();
            sb.Append("<div class=\"paginator\">");
            if (pageCount > 1)
            {
                if (start > 1)
                {
                    sb.Append("<a href=\"" + pageurl.Replace("{p}", "1") + "\" title=\"首页\" class=\"a19\">首页</a>");
                    sb.Append("<a href=\"" + pageurl.Replace("{p}", (page - 1).ToString()) + "\" title=\"第" + (page - 1) + "页\" class=\"a19\">&nbsp;上一页&nbsp;</a>");
                }
                for (int i = start; i <= pageCount && i < start + pageLine; i++)
                {
                    if (i == page)
                        sb.Append("<span title=\"当前第" + i + "页\" class=\"cpb\">" + i + "</span>");
                    else
                        sb.Append("<a href=\"" + pageurl.Replace("{p}", i.ToString()) + "\" title=\"第" + i + "页\" class=\"\">" + i + "</a>");
                }
                if (start + pageLine < pageCount)
                {
                    sb.Append("<a href=\"" + pageurl.Replace("{p}", (page + 1).ToString()) + "\" title=\"第" + (page + 1) + "页\" class=\"a19\">&nbsp;下一页&nbsp;</a>");
                    sb.Append("<a href=\"" + pageurl.Replace("{p}", pageCount.ToString()) + "\" title=\"第" + pageCount + "页(尾页)\" class=\"a19\">尾页</a>");
                }
            }
            sb.Append("</div>");
            return sb.ToString();
        }

        /// <summary>
        /// 获得分页代码
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="pageSize">每页显示多少条数据</param>
        /// <param name="count">数据总量</param>
        /// <param name="sub"></param>
        /// <returns>StringBuilder</returns>
        public static string GenPager(int page, int pageSize, int count, string sub)
        {
            const int pageLine = 5;
            var pageCount = (int)Math.Ceiling((double)count / pageSize);
            var start = ((page - 1) / pageLine) * pageLine + 1;
            var sb = new StringBuilder();
            sb.Append("<br><table width=\"229\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" class=\"p4\" style=\"madding-top:6px;\">");
            sb.Append("                  <tr>");
            sb.Append("                     <td align=\"center\"><span class=\"font9\" >分页:" + page + "/" + pageCount);
            if (start > 1)
            {
                sb.Append("<a href=\"javascript:" + sub + "(" + (start - 1) + ")\" title=\"第" + (start - 1) + "页\" class=\"a19\">&nbsp;&lt;&nbsp;</a>");
                sb.Append("<a href=\"javascript:" + sub + "(" + (1) + ")\" title=\"首页\" class=\"a19\">&lt;&lt;</a>");
            }
            sb.Append("&nbsp;<strong>");
            for (int i = start; i <= pageCount && i < start + pageLine; i++)
            {
                if (i == page)
                    sb.Append("<span title=\"当前第" + i + "页\" class=\"a19\">[<font color=red>" + i + "</font>]</span>");
                else
                    sb.Append("<a href=\"javascript:" + sub + "(" + i + ")\" title=\"第" + i + "页\" class=\"a19\">[" + i + "]</a>");
            }
            sb.Append("</strong>");

            if (start + pageLine < pageCount)
            {
                sb.Append("<a href=\"javascript:" + sub + "(" + (start + pageLine) + ")\" title=\"第" + (start + pageLine) + "页\" class=\"a19\">&nbsp;&gt;</a>&nbsp;");
                sb.Append("<a href=\"javascript:" + sub + "(" + pageCount + ")\" title=\"第" + pageCount + "页(末页)\" class=\"a19\">&gt;&gt;</a>");
            }
            sb.Append("                     </span></td>");
            sb.Append("                 </tr>");
            sb.Append("</Table>");
            return sb.ToString();
        }


        /// <summary>
        /// 发送POST请求,返回值
        /// </summary>
        /// <param name="sUrl">地址</param>
        /// <param name="postData">发送数据</param>
        /// <returns></returns>
        public static string SendPost(string sUrl, string postData)
        {
            return SendPost(sUrl, postData, "utf-8");
        }

        public static string SendPost(string sUrl, string postData, string code)
        {
            string sHtml = "";
            HttpWebRequest request;
            HttpWebResponse response = null;
            Stream stream = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(sUrl);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                var encoding = Encoding.GetEncoding(code);
                byte[] data = encoding.GetBytes(postData);
                request.ContentLength = data.Length;
                using (var _stream = request.GetRequestStream())
                {
                    _stream.Write(data, 0, data.Length);
                }
                response = (HttpWebResponse)request.GetResponse();
                stream = response.GetResponseStream();
                if (stream != null) sHtml = new StreamReader(stream, encoding).ReadToEnd();
            }
            catch (Exception e)
            {
                string aa = e.Message;
                if (response != null) response.Close();
            }
            if (stream != null) stream.Close();
            if (response != null) response.Close();
            return sHtml;
        }


        /// <summary>
        /// 将全角数字转换为数字
        /// </summary>
        /// <param name="sbcCase"></param>
        /// <returns></returns>
        public static string SbcCaseToNumberic(string sbcCase)
        {
            char[] c = sbcCase.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length != 2) continue;
                if (b[1] != 255) continue;
                b[0] = (byte)(b[0] + 32);
                b[1] = 0;
                c[i] = Encoding.Unicode.GetChars(b)[0];
            }
            return new string(c);
        }

        /// <summary>
        /// 转换长文件名为短文件名
        /// </summary>
        /// <param name="fullname"></param>
        /// <param name="repstring"></param>
        /// <param name="leftnum"></param>
        /// <param name="rightnum"></param>
        /// <param name="charnum"></param>
        /// <returns></returns>
        public static string ConvertSimpleFileName(string fullname, string repstring, int leftnum, int rightnum, int charnum)
        {
            string simplefilename = "";
            string filename = "";
            string extname = GetFileExtName(fullname);

            if (Utils.StrIsNullOrEmpty(extname))
                throw new Exception("字符串不含有扩展名信息");

            int filelength = 0, dotindex = 0;

            dotindex = fullname.LastIndexOf('.');
            filename = fullname.Substring(0, dotindex);
            filelength = filename.Length;
            if (dotindex > charnum)
            {
                var leftstring = filename.Substring(0, leftnum);
                var rightstring = filename.Substring(filelength - rightnum, rightnum);
                if (string.IsNullOrEmpty(repstring))
                    simplefilename = leftstring + rightstring + "." + extname;
                else
                    simplefilename = leftstring + repstring + rightstring + "." + extname;
            }
            else
                simplefilename = fullname;

            return simplefilename;
        }

        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJSON(DataTable dt)
        {
            return DataTableToJson(dt, true);
        }

        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param> 
        /// <param name="dtDispose">数据表转换结束后是否dispose掉</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJson(DataTable dt, bool dtDispose)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[\r\n");

            //数据表字段名和类型数组
            var dtField = new string[dt.Columns.Count];
            var i = 0;
            var formatStr = "{{";
            var fieldtype = "";
            foreach (DataColumn dc in dt.Columns)
            {
                dtField[i] = dc.Caption.ToLower().Trim();
                formatStr += "'" + dc.Caption.ToLower().Trim() + "':";
                fieldtype = dc.DataType.ToString().Trim().ToLower();
                if (fieldtype.IndexOf("int") > 0 || fieldtype.IndexOf("deci") > 0 ||
                    fieldtype.IndexOf("floa") > 0 || fieldtype.IndexOf("doub") > 0 ||
                    fieldtype.IndexOf("bool") > 0)
                {
                    formatStr += "{" + i + "}";
                }
                else
                {
                    formatStr += "'{" + i + "}'";
                }
                formatStr += ",";
                i++;
            }

            if (formatStr.EndsWith(","))
                formatStr = formatStr.Substring(0, formatStr.Length - 1);//去掉尾部","号

            formatStr += "}},";

            i = 0;
            var objectArray = new object[dtField.Length];
            foreach (DataRow dr in dt.Rows)
            {

#pragma warning disable 168
                foreach (string fieldname in dtField)
#pragma warning restore 168
                {   //对 \ , ' 符号进行转换 
                    objectArray[i] = dr[dtField[i]].ToString().Trim().Replace("\\", "\\\\").Replace("'", "\\'");
                    switch (objectArray[i].ToString())
                    {
                        case "True":
                            {
                                objectArray[i] = "true"; break;
                            }
                        case "False":
                            {
                                objectArray[i] = "false"; break;
                            }
                        default: break;
                    }
                    i++;
                }
                i = 0;
                stringBuilder.Append(string.Format(formatStr, objectArray));
            }
            if (stringBuilder.ToString().EndsWith(","))
                stringBuilder.Remove(stringBuilder.Length - 1, 1);//去掉尾部","号

            if (dtDispose)
                dt.Dispose();

            return stringBuilder.Append("\r\n];");
        }

        /// <summary>
        /// 字段串是否为Null或为""(空)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool StrIsNullOrEmpty(string str)
        {
            if (str == null || str.Trim() == string.Empty)
                return true;

            return false;
        }

        /// <summary>
        /// 检查颜色值是否为3/6位的合法颜色
        /// </summary>
        /// <param name="color">待检查的颜色</param>
        /// <returns></returns>
        public static bool CheckColorValue(string color)
        {
            if (StrIsNullOrEmpty(color))
                return false;

            color = color.Trim().Trim('#');

            if (color.Length != 3 && color.Length != 6)
                return false;

            //不包含0-9  a-f以外的字符
            if (!Regex.IsMatch(color, "[^0-9a-f]", RegexOptions.IgnoreCase))
                return true;

            return false;
        }

        /// <summary>
        /// 根据Url获得源文件内容
        /// </summary>
        /// <param name="url">合法的Url地址</param>
        /// <returns></returns>
        public static string GetSourceTextByUrl(string url)
        {
            var request = WebRequest.Create(url);
            request.Timeout = 20000;//20秒超时
            var response = request.GetResponse();

            var resStream = response.GetResponseStream();
            var sr = new StreamReader(resStream);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// 转换时间为unix时间戳
        /// </summary>
        /// <param name="date">需要传递UTC时间,避免时区误差,例:DataTime.UTCNow</param>
        /// <returns></returns>
        public static double ConvertToUnixTimestamp(DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }


        /// <summary>
        /// Json特符字符过滤，参见http://www.json.org/
        /// </summary>
        /// <param name="sourceStr">要过滤的源字符串</param>
        /// <returns>返回过滤的字符串</returns>
        public static string JsonCharFilter(string sourceStr)
        {
            sourceStr = sourceStr.Replace("\\", "\\\\");
            sourceStr = sourceStr.Replace("\b", "\\\b");
            sourceStr = sourceStr.Replace("\t", "\\\t");
            sourceStr = sourceStr.Replace("\n", "\\\n");
            sourceStr = sourceStr.Replace("\n", "\\\n");
            sourceStr = sourceStr.Replace("\f", "\\\f");
            sourceStr = sourceStr.Replace("\r", "\\\r");
            return sourceStr.Replace("\"", "\\\"");
        } 

        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <returns>合并到的目的字符串</returns>
        public static string MergeString(string source, string target)
        {
            return MergeString(source, target, ",");
        }

        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <param name="mergechar">合并符</param>
        /// <returns>并到字符串</returns>
        public static string MergeString(string source, string target, string mergechar)
        {
            if (StrIsNullOrEmpty(target))
                target = source;
            else
                target += mergechar + source;

            return target;
        } 

        /// <summary>
        /// 清除UBB标签
        /// </summary>
        /// <param name="sDetail">帖子内容</param>
        /// <returns>帖子内容</returns>
        public static string ClearUbb(string sDetail)
        {
            return Regex.Replace(sDetail, @"\[[^\]]*?\]", string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 获取站点根目录URL
        /// </summary>
        /// <returns></returns>
        public static string GetRootUrl(string forumPath)
        {
            var port = HttpContext.Current.Request.Url.Port;
            return string.Format("{0}://{1}{2}{3}",
                                 HttpContext.Current.Request.Url.Scheme,
                                 HttpContext.Current.Request.Url.Host,
                                 (port == 80 || port == 0) ? "" : ":" + port,
                                 forumPath);
        }
      

       /// <summary>
       /// 获取不重复的随机数
       /// </summary>
       /// <param name="num">随机数个数</param>
       /// <param name="minValue">最小值</param>
       /// <param name="maxValue">最大值</param>
       /// <returns>随机数集合</returns>
       public static IList<int> GetRandomNum(int num, int minValue, int maxValue)
       {
           var ra = new Random(unchecked((int)DateTime.Now.Ticks));
           IList<int> arrNum = new List<int>();
           int tmp = 0;
           for (int i = 0; i <= num - 1; i++)
           {
               tmp = ra.Next(minValue, maxValue); //随机取数
               int x = GetNum(arrNum, tmp, minValue, maxValue, ra); //取出值赋到数组中
               if (!arrNum.Contains(x))
               {
                   arrNum.Add(x);
               }
               else
               {
                   i--;
               }
           }
           return arrNum;
       }

       #region Helper
       /// <summary>
       /// 获取随机数
       /// </summary>
       /// <param name="arrNum">当前已有的随机数</param>
       /// <param name="tmp">当前随机生成的随机数</param>
       /// <param name="minValue">最小值</param>
       /// <param name="maxValue">最大值</param>
       /// <param name="ra"></param>
       /// <returns></returns>
       private static int GetNum(IList<int> arrNum, int tmp, int minValue, int maxValue, Random ra)
       {
           int n = 0;
           while (n <= arrNum.Count - 1)
           {
               if (arrNum[n] == tmp) //利用循环判断是否有重复
               {
                   tmp = ra.Next(minValue, maxValue); //重新随机获取。
                   GetNum(arrNum, tmp, minValue, maxValue, ra);//递归:如果取出来的数字和已取得的数字有重复就重新随机获取。
               }
               n++;
           }
           return tmp;
       }
       #endregion
    }
}
