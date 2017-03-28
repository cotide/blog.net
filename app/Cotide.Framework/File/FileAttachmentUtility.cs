using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using Cotide.Framework.Exceptions;
using Cotide.Framework.File.Config;
using Cotide.Framework.Utility;
using com.gmccadc.Utility;
using com.gmccadc.Utility.Attachment;

namespace Cotide.Framework.File
{
    /// <summary>
    /// 文件上传服务类
    /// </summary>
    public class FileAttachmentUtility : IFileAttachmentUtility
    {
        private static bool _mIsInitialized;
        private static AttachmentProvider _mProvider;
        private static AttachmentProviderCollection _mProviders;
        private readonly string _mUser;

        /// <summary>
        ///   上传附件组件构造函数
        /// </summary>
        public FileAttachmentUtility()
        {
            _mUser = "admin";
            Initialize();
        }

        //附件服务器
        private string AttachmentServerRemotePath
        {
            get
            {
                string url = ConfigurationManager.AppSettings["AttachmentServerPath"];
                if (string.IsNullOrEmpty(url))
                {
                    throw new ConfigurationErrorsException(
                        "缺少AttachmentServerPath配置，请在web.config或app.config中的<AppSettings>中配置附件服务器的物理路径AttachmentServerPath");
                }
                return url;
            }
        }

        /// <summary>
        ///   附件服务器登录用户名
        /// </summary>
        private string AttachmentServerUserName
        {
            get
            {
                string userName = ConfigurationManager.AppSettings["AttachmentServerUserName"];
                if (string.IsNullOrEmpty(userName))
                {
                    throw new ConfigurationErrorsException(
                        "缺少AttachmentServerUserName配置，请在web.config或app.config中的<AppSettings>中配置附件服务器的物理路径AttachmentServerUserName");
                }
                return userName;
            }
        }

        /// <summary>
        ///   附件服务器登录用户密码
        /// </summary>
        private string AttachmentServerUserPwd
        {
            get
            {
                string userPwd = ConfigurationManager.AppSettings["AttachmentServerUserPwd"];
                if (string.IsNullOrEmpty(userPwd))
                {
                    throw new ConfigurationErrorsException(
                        "缺少AttachmentServerUserPwd配置，请在web.config或app.config中的<AppSettings>中配置附件服务器的物理路径AttachmentServerUserPwd");
                }
                return userPwd;
            }
        }


        private string ImgSuffixConfig
        {
            get { return ConfigurationManager.AppSettings["ImgSuffix"]; }
        }

        #region IFileAttachmentUtility Members

        /// <summary>
        /// 保存附件
        /// </summary>
        /// <param name="file">要保存的附件</param>
        /// <param name="targetCategory">附件存放的文件夹</param>
        /// <param name="fileAttachOption">附件配置</param>
        /// <returns>返回文件名</returns>
        public string SaveLocalhostImgAttach(HttpPostedFile file, string targetCategory, FileAttachOption fileAttachOption)
        { 
            var fileSuffix = Utils.GetUrlSuffix(file.FileName);
            if (fileAttachOption.ImgSuffix==null||fileAttachOption.ImgSuffix.Count() <= 0)
            {
                // 加载配置数据
                if(!string.IsNullOrEmpty(ImgSuffixConfig))
                {
                    fileAttachOption.ImgSuffix = ImgSuffixConfig.Split('|');
                }
            }

            CheckFileSuffix(fileAttachOption, fileSuffix); 
            //var fileInfo = new FileInfo(file.FileName);
            // 随机生成图片名并为原图
            string saveFileName = Guid.NewGuid() + "." + fileSuffix;
            string sitePath = targetCategory;
            string basePath = HttpContext.Current.Server.MapPath("/") + sitePath;
            // 文件名称 
            string fileNameS = "s_" + saveFileName;                           // 缩略图文件名称 
            string fileNameB = "b_" + saveFileName;                           // 标准图文件名称
            string webFilePath = basePath + saveFileName;                     // 文件原图
            // 保存原始文件
            if (!System.IO.File.Exists(webFilePath))
            {
                // 检查是否有该文件夹，没有则创建 
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                } 

                file.SaveAs(webFilePath);
                // 生成缩略图方法
                if(fileAttachOption.BuildOriginalImg)
                { 
                    ImgHelper.MakeThumbnail(webFilePath,
                                           basePath + fileNameS, 
                                           fileAttachOption.OriginalImgWidth,
                                           fileAttachOption.OriginalImgHeight,
                                           fileAttachOption.OriginalImgModel.ToString());
                }
                // 生成标准图方法
                if (fileAttachOption.BuildStandardImg)
                {
                    ImgHelper.MakeThumbnail(webFilePath, 
                                            basePath + fileNameB, 
                                            fileAttachOption.StandardImgWidth,
                                            fileAttachOption.StandardImgHeight,
                                            fileAttachOption.StandardImgModel.ToString());
                }
               //return  sitePath + saveFileName;
                return saveFileName;
            }
            return "";
        }

        /// <summary>
        ///   附件服务器的URL
        /// </summary>
        public string AttachmentServerUrl
        {
            get
            {
                string url = ConfigurationManager.AppSettings["AttachmentServerUrl"];
                if (string.IsNullOrEmpty(url))
                {
                    throw new ConfigurationErrorsException(
                        "缺少AttachmentServerUrl配置，请在web.config或app.config中的<AppSettings>中配置附件服务器的URL");
                }
                return url;
            }
        }

        /// <summary>
        ///   获取附件的完整路径
        /// </summary>
        /// <param name = "categoryPath">附件真实的相对路径</param>
        /// <returns></returns>
        public string GetAttachmentFullUrl(string categoryPath)
        {
            return _mProvider.GetAttachmentFullUrl(categoryPath);
        }

        /// <summary>
        ///   获取附件文件流
        /// </summary>
        /// <param name = "categoryPath">附件路径</param>
        /// <returns></returns>
        public FileStream GetAttachmentFile(string categoryPath)
        {
            return _mProvider.GetAttachmentFile(categoryPath);
        }

        /// <summary>
        ///   保存附件
        /// </summary>
        /// <param name = "sourceFilePath">原始文件路径</param>
        /// <param name = "targetCategory">附件的分类文件夹</param>
        /// <param name = "fileName">附件的新的文件名称</param>
        /// <param name = "delSource">复制成功后是否删除原文件</param>
        /// <returns></returns>
        public string SaveAttach(string sourceFilePath, string targetCategory, string fileName, bool delSource)
        {
            ValidateExt(fileName);

            return _mProvider.SaveAttach(sourceFilePath, targetCategory, GenerateFileName(fileName), delSource);
        }

        /// <summary>
        ///   保存附件
        /// </summary>
        /// <param name = "sourceFile">要上传的附件的流</param>
        /// <param name = "targetCategory">附件的分类文件夹</param>
        /// <param name = "fileName">附件新名称</param>
        /// <returns></returns>
        public string SaveAttach(Stream sourceFile, string targetCategory, string fileName)
        {
            ValidateExt(fileName);

            return _mProvider.SaveAttach(sourceFile, targetCategory, GenerateFileName(fileName));
        }

        /// <summary>
        ///   保存附件
        /// </summary>
        /// <param name = "sourceFile">要上传的附件的字节数组</param>
        /// <param name = "targetCategory"></param>
        /// <param name = "fileName"></param>
        /// <returns></returns>
        public string SaveAttach(byte[] sourceFile, string targetCategory, string fileName)
        {
            ValidateExt(fileName);

            return _mProvider.SaveAttach(sourceFile, targetCategory, GenerateFileName(fileName));
        }



        /// <summary>
        ///   保存附件,并返回附件服务器上的真实的分类目录
        /// </summary>
        /// <param name = "postedFile">要保存的附件</param>
        /// <param name = "targetCategory">附件的分类文件夹</param>
        /// <param name = "fileName">附件的新名称</param>
        /// <returns></returns>
        public string SaveAttach(HttpPostedFile postedFile, string targetCategory, string fileName)
        {
            ValidateExt(fileName);

            return _mProvider.SaveAttach(postedFile, targetCategory, GenerateFileName(fileName));
        }

        /// <summary>
        ///   保存附件(推荐使用),并返回附件服务器上的真实的分类目录
        /// </summary>
        /// <param name = "postedFile">要保存的附件</param>
        /// <param name = "targetCategory">附件的分类文件夹</param>
        /// <returns></returns>
        public string SaveAttach(HttpPostedFile postedFile, string targetCategory)
        {
            return SaveAttach(postedFile, targetCategory, postedFile.FileName);
        }

        /// <summary>
        /// 保存附件,并返回附件服务器上的真实的分类目录
        /// </summary>
        /// <param name="postedFile">要保存的附件</param>
        /// <param name="targetCategory">附件的分类文件夹</param>
        /// <param name="fileName">附件的新名称</param>
        /// <param name="genUniqueFileName">是否改名</param>
        /// <returns></returns>
        public string SaveAttach(HttpPostedFile postedFile, string targetCategory, string fileName, bool genUniqueFileName)
        {
            ValidateExt(fileName);

            if (genUniqueFileName)
                fileName = GenerateFileName(fileName);

            return _mProvider.SaveAttach(postedFile, targetCategory, fileName);
        }

        /// <summary>
        /// 保存附件,并返回附件服务器上的真实的分类目录
        /// </summary>
        /// <param name="sourceFile">要保存的附件</param>
        /// <param name="targetCategory">附件的分类文件夹</param>
        /// <param name="fileName">附件的新名称</param>
        /// <param name="genUniqueFileName">是否改名</param>
        /// <returns></returns>
        public string SaveAttach(Stream sourceFile, string targetCategory, string fileName, bool genUniqueFileName)
        {
            ValidateExt(fileName);

            if (genUniqueFileName)
                fileName = GenerateFileName(fileName);

            return _mProvider.SaveAttach(sourceFile, targetCategory, fileName);
        }

        /// <summary>
        /// 保存邮件的附件(邮件支撑这使用)
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="targetCategory"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string SaveEmailAttach(byte[] sourceFile, string targetCategory, string fileName)
        {
            fileName = GenerateFileName(fileName);
            return _mProvider.SaveAttach(sourceFile, targetCategory, fileName);
        }


        /// <summary>
        ///   附件是否存在
        /// </summary>
        /// <param name = "targetCategoryPath"></param>
        /// <returns></returns>
        public bool HasAttachment(string targetCategoryPath)
        {
            return _mProvider.HasAttachment(targetCategoryPath);
        }

        /// <summary>
        ///   删除文件
        /// </summary>
        /// <param name = "targetCategoryAttachPath">附件的分类路径</param>
        /// <returns></returns>
        public bool DeleteFile(string targetCategoryAttachPath)
        {
            return _mProvider.DeleteFile(targetCategoryAttachPath);
        }

        /// <summary>
        ///   下载文件
        /// </summary>
        /// <param name = "targetCategoryAttachPath">需要下载的文件路径</param>
        public void DownLoadFile(string targetCategoryAttachPath)
        {
            DownLoadFile(targetCategoryAttachPath, string.Empty);
        }

        /// <summary>
        ///   下载文件
        /// </summary>
        /// <param name = "targetCategoryAttachPath">需要下载的文件路径</param>
        /// <param name = "newFileName">保存文件的文件名</param>
        public void DownLoadFile(string targetCategoryAttachPath, string newFileName)
        {
            _mProvider.DownLoadFile(targetCategoryAttachPath, newFileName);
        }

        /// <summary>
        ///   下载文件到网站本地的Path目录
        /// </summary>
        /// <param name = "targetCategoryAttachPath">需要下载的文件路径</param>
        public bool DownLoadFileToPath(string targetCategoryAttachPath, string path, string newFileName)
        {
            return _mProvider.DownLoadFileToPath(targetCategoryAttachPath, path, newFileName);
        }

        /// <summary>
        ///   下载文件到网站本地的Temp目录
        /// </summary>
        /// <param name = "targetCategoryAttachPath">需要下载的文件路径</param>
        public bool DownLoadFileToTemp(string targetCategoryAttachPath)
        {
            return DownLoadFileToTemp(targetCategoryAttachPath, string.Empty);
        }

        /// <summary>
        ///   下载文件
        /// </summary>
        /// <param name = "fullpath">文件的全路径</param>
        /// <param name = "newFileName">文件新名称</param>
        public void DownLoadFileByFullPath(string fullpath, string newFileName)
        {
            if (System.IO.File.Exists(fullpath))
            {
                HttpContext context = HttpContext.Current;
                if (context != null)
                {
                    string fileName = string.IsNullOrEmpty(newFileName)
                                          ? Path.GetFileName(fullpath)
                                          : HttpUtility.UrlEncode(newFileName, Encoding.UTF8);

                    HttpResponse Response = context.Response;
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Connection", "keep-alive");
                    Response.AppendHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName));

                    byte[] b;
                    using (FileStream fs = System.IO.File.OpenRead(fullpath))
                    {
                        b = new byte[fs.Length];
                        fs.Read(b, 0, b.Length);
                        fs.Close();
                    }

                    if (b.Length > 0)
                    {
                        Response.BinaryWrite(b);
                    }

                    Response.Flush();
                    Response.End();
                }
            }
        }

        /// <summary>
        ///   下载文件到网站本地的Temp目录
        /// </summary>
        /// <param name = "targetCategoryAttachPath">需要下载的文件路径</param>
        /// <param name = "newFileName">新文件名</param>
        /// <returns></returns>
        public bool DownLoadFileToTemp(string targetCategoryAttachPath, string newFileName)
        {
            return _mProvider.DownLoadFileToTemp(targetCategoryAttachPath, newFileName);
        }

        /// <summary>
        ///   获取附件的绝对物理路径
        /// </summary>
        /// <param name = "categoryPath"></param>
        /// <returns></returns>
        public string GetAttachmentFullPath(string categoryPath)
        {
            return _mProvider.GetAttachmentFullUrl(categoryPath);
        }

        /// <summary>
        ///   获取附件的完整物理路径
        /// </summary>
        /// <returns></returns>
        public string GetAttachmentFullFileAddress()
        {
            return _mProvider.GetAttachmentFullFileAddress();
        }

        #endregion

        #region Helper

        /// <summary>
        ///   初始化
        /// </summary>
        private void Initialize()
        {
            try
            {
            if (!_mIsInitialized)
            {
                // get the configuration section for the feature
                var attachmentConfig =
                    (AttachmentProviderConfigurationSectionHandler)ConfigurationManager.GetSection("attachment");

                if (attachmentConfig == null)
                    throw new ConfigurationErrorsException("配置节错误!");

                _mProviders = new AttachmentProviderCollection();

                // use the ProvidersHelper class to call Initialize() on each provider
                ProvidersHelper.InstantiateProviders(attachmentConfig.Providers, _mProviders,
                                                     typeof(AttachmentProvider));

                // set a reference to the default provider
                _mProvider = _mProviders[attachmentConfig.DefaultProvider] as AttachmentProvider;
                _mProvider.UserName = _mUser;
                _mIsInitialized = true;
            }
            }
            catch (Exception ex)
            {
                //Log.Error( ex, "附件上传下载组件初始化出错",
                //               ex.ToString());
            }
        }

        /// <summary>
        ///   对附件类型进行验证
        /// </summary>
        /// <param name = "fileName">文件名</param>
        private static void ValidateExt(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            if (!IsValidExt(ext))
                throw new ADCBusinessException("E_00006300");
        }

        /// <summary>
        ///   生成唯一的文件名
        /// </summary>
        /// <param name = "fileName">文件名</param>
        /// <returns></returns>
        private static string GenerateFileName(string fileName)
        {
            string guid = Guid.NewGuid().ToString();
            string ext = Path.GetExtension(fileName);
            string time = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
            string newFileName = string.Format("{0}-{1}{2}", guid, time, ext);
            return newFileName;
        }

        /// <summary>
        ///   判断附件扩展名是否为允许的扩展名
        /// </summary>
        /// <param name = "ext">扩展名，如：.exe</param>
        /// <returns></returns>
        private static bool IsValidExt(string ext)
        {
            ext = ext.ToLower();
            if (ConfigurationManager.AppSettings["FileSuffix"] == null)
            {
                return true;
            }
            var validExts = ConfigurationManager.AppSettings["FileSuffix"].Split(';');
            return validExts.All(validExt => validExt.ToLower() != ext);
        }


        /// <summary>
        /// 检查文件后缀 
        /// </summary>
        /// <param name="fileAttachOption">附件</param>
        /// <param name="fileSuffix">文件后缀</param>
        private static void CheckFileSuffix(FileAttachOption fileAttachOption, string fileSuffix)
        {
            var isCheck = false;
            if (fileAttachOption.ImgSuffix != null)
            {
                foreach (var item in fileAttachOption.ImgSuffix.Where(item => item.ToLower() == fileSuffix.ToLower()))
                {
                    isCheck = true;
                }
            }
            else
            {
                isCheck = true;
            }

            if (!isCheck)
            {
                throw new BusinessException("上传的附件格式不正确");
            }
        }

        #endregion
    }
}
