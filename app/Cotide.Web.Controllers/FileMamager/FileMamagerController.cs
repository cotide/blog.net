using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Cotide.Framework.Enumerable;
using Cotide.Framework.Utility;
using Cotide.Web.Controllers.Controllers;
using LitJson;

namespace Cotide.Web.Controllers.FileMamager
{
    /// <summary>
    /// 文件上传管理
    /// </summary>
    public class FileMamagerController : BaseController 
    {

        public ActionResult MyFile()
        {
            var aspxUrl = ConfigurationManager.AppSettings["UploadFilePath"] + CurrentUser.UserId + "/";
             
            //文件保存目录URL
            var rootPath = aspxUrl + "EditorUpload/";
             
            //图片扩展名
            var fileTypes = "gif,jpg,jpeg,png,bmp";

            var currentPath = "";
            var currentUrl = "";
            var currentDirPath = "";
            var moveupDirPath = "";

            var dirPath = HttpContext.Server.MapPath(rootPath);
            var dirName = HttpContext.Request.QueryString["dir"];
            if (!String.IsNullOrEmpty(dirName))
            {
                if (Array.IndexOf("image,flash,media,file".Split(','), dirName) == -1)
                {
                    HttpContext.Response.Write("Invalid Directory name.");
                    HttpContext.Response.End();
                }
                dirPath += dirName + "/";
                rootPath += dirName + "/";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
            }

            //根据path参数，设置各路径和URL
            String path = HttpContext.Request.QueryString["path"];
            path = String.IsNullOrEmpty(path) ? "" : path;
            if (path == "")
            {
                currentPath = dirPath;
                currentUrl = rootPath;
                currentDirPath = "";
                moveupDirPath = "";
            }
            else
            {
                currentPath = dirPath + path;
                currentUrl = rootPath + path;
                currentDirPath = path;
                moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
            }

            //排序形式，name or size or type
            String order = HttpContext.Request.QueryString["order"];
            order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

            //不允许使用..移动到上一级目录
            if (Regex.IsMatch(path, @"\.\."))
            { 
                return Content("Access is not allowed.");
            }
            //最后一个字符不是/
            if (path != "" && !path.EndsWith("/"))
            { 
                return Content("Parameter is not valid.");
            }
            //目录不存在或不是目录
            if (!Directory.Exists(currentPath)) 
            {
                return Content("Directory does not exist.");
            }

            //遍历目录取得文件信息
            string[] dirList = Directory.GetDirectories(currentPath);
            string[] fileList = Directory.GetFiles(currentPath);

            switch (order)
            {
                case "size":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new SizeSorter());
                    break;
                case "type":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new TypeSorter());
                    break;
                case "name":
                default:
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new NameSorter());
                    break;
            }

            var result = new Hashtable();
            result["moveup_dir_path"] = moveupDirPath;
            result["current_dir_path"] = currentDirPath;
            result["current_url"] = currentUrl;
            result["total_count"] = dirList.Length + fileList.Length;
            var dirFileList = new List<Hashtable>();
            result["file_list"] = dirFileList;
            foreach (string t in dirList)
            {
                var dir = new DirectoryInfo(t);
                var hash = new Hashtable();
                hash["is_dir"] = true;
                hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
                hash["filesize"] = 0;
                hash["is_photo"] = false;
                hash["filetype"] = "";
                hash["filename"] = dir.Name;
                hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            for (int i = 0; i < fileList.Length; i++)
            {
                var file = new FileInfo(fileList[i]);
                var hash = new Hashtable();
                hash["is_dir"] = false;
                hash["has_file"] = false;
                hash["filesize"] = file.Length;
                hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
                hash["filetype"] = file.Extension.Substring(1);
                hash["filename"] = file.Name;
                hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            HttpContext.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
            return Content(JsonMapper.ToJson(result)); 
        }

        /// <summary>
        /// 编辑器文件上传处理
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadFile()
        {
            var aspxUrl = ConfigurationManager.AppSettings["UploadFilePath"] + CurrentUser.UserId + "/";

            //文件保存目录路径
           // const string savePath = "EditorUpload/";  
            //文件保存目录URL
            var saveUrl = aspxUrl + "EditorUpload/";
            var soureUlr = saveUrl + "soureImg/";

            //定义允许上传的文件扩展名
            var extTable = new Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            extTable.Add("flash", "swf,flv");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

            //最大文件大小
            int maxSize = 1000000;
             
            var imgFile = HttpContext.Request.Files["imgFile"];
            if (imgFile == null)
            {
               return  ShowError("请选择文件。"); 
            }

            var dirPath = HttpContext.Server.MapPath(saveUrl);
            if (!Directory.Exists(dirPath))
            {
                //showError("上传目录不存在。");
                // 检查是否有该文件夹，没有则创建  
                Directory.CreateDirectory(dirPath); 
            }

            var dirName = HttpContext.Request.QueryString["dir"];
            if (String.IsNullOrEmpty(dirName))
            {
                dirName = "image";
            }
            if (!extTable.ContainsKey(dirName))
            {
                return   ShowError("目录名不正确。");
            }

            String fileName = imgFile.FileName;
            String fileExt = Path.GetExtension(fileName).ToLower();

            if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            {
               return   ShowError("上传文件大小超过限制。");
            } 

            if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                return ShowError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
            }

          

            //创建文件夹
            dirPath += dirName + "/";
            saveUrl += dirName + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            var ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + "/";
            saveUrl += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            } 
            var newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            var filePath = dirPath + newFileName;

            var image = Image.FromStream(imgFile.InputStream);
            if(image.Width>850)
            {
                // 图片宽度超出850 
                // 保存原图
                var soureDirPath = HttpContext.Server.MapPath(soureUlr);
                if (!Directory.Exists(soureDirPath))
                {
                    Directory.CreateDirectory(soureDirPath);
                }
                image.Save(soureDirPath + newFileName);

                // 进行图片处理
                var outPutImg =   ImgHelper.MakeThumbnail(imgFile.InputStream, 850, 0, ImgModel.W, ImageFormat.Jpeg);
                var saveImg =  ImgHelper.ByteArrayToImage(outPutImg);
                saveImg.Save(filePath); 
                // 响应结果
                var fileUrl = saveUrl + newFileName;
                var hash = new Hashtable();
                hash["error"] = 0;
                hash["url"] = fileUrl;
                HttpContext.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                return Content(JsonMapper.ToJson(hash));    
            }
            else
            {
                image.Save(filePath);
               // imgFile.SaveAs(filePath); 
                var fileUrl = saveUrl + newFileName; 
                var hash = new Hashtable();
                hash["error"] = 0;
                hash["url"] = fileUrl;
                HttpContext.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                return Content(JsonMapper.ToJson(hash));    
            } 
        }

        private ActionResult ShowError(string message)
        {
            var hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            HttpContext.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            return Content(JsonMapper.ToJson(hash));
        }


        private class NameSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.FullName.CompareTo(yInfo.FullName);
            }
        }

        private class SizeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Length.CompareTo(yInfo.Length);
            }
        }

        private class TypeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Extension.CompareTo(yInfo.Extension);
            }
        }
    }
}
