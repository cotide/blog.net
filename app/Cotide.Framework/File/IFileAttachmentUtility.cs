using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using Cotide.Framework.File.Config;

namespace Cotide.Framework.File
{
    /// <summary>
    /// 文件上传服务接口
    /// </summary>
    public interface IFileAttachmentUtility
    {

        #region 站内文件上传

        /// <summary>
        /// 保存附件
        /// </summary>
        /// <param name="file">要保存的附件</param>
        /// <param name="targetCategory">附件存放的文件夹</param>
        /// <param name="fileAttachOption">附件配置</param>
        /// <returns>返回文件名</returns>
        string SaveLocalhostImgAttach(HttpPostedFile file, string targetCategory, FileAttachOption fileAttachOption);

        

        #endregion

        #region FTP文件上传

        /// <summary>
        ///   附件服务器的URL
        /// </summary>
        string AttachmentServerUrl { get; }

        /// <summary>
        ///   获取附件的完整路径
        /// </summary>
        /// <param name = "categoryPath">附件真实的相对路径</param>
        /// <returns></returns>
        string GetAttachmentFullUrl(string categoryPath);

        /// <summary>
        ///   获取附件文件流
        /// </summary>
        /// <param name = "categoryPath">附件路径</param>
        /// <returns></returns>
        FileStream GetAttachmentFile(string categoryPath);

        /// <summary>
        ///   保存附件
        /// </summary>
        /// <param name = "sourceFilePath">原始文件路径</param>
        /// <param name = "targetCategory">附件的分类文件夹</param>
        /// <param name = "fileName">附件的新的文件名称</param>
        /// <param name = "delSource">复制成功后是否删除原文件</param>
        /// <returns></returns>
        string SaveAttach(string sourceFilePath, string targetCategory, string fileName, bool delSource);

        /// <summary>
        ///   保存附件
        /// </summary>
        /// <param name = "sourceFile">要上传的附件的流</param>
        /// <param name = "targetCategory">附件的分类文件夹</param>
        /// <param name = "fileName">附件新名称</param>
        /// <returns></returns>
        string SaveAttach(Stream sourceFile, string targetCategory, string fileName);

        /// <summary>
        ///   保存附件
        /// </summary>
        /// <param name = "sourceFile">要上传的附件的字节数组</param>
        /// <param name = "targetCategory"></param>
        /// <param name = "fileName"></param>
        /// <returns></returns>
        string SaveAttach(byte[] sourceFile, string targetCategory, string fileName);

        /// <summary>
        ///   保存附件,并返回附件服务器上的真实的分类目录
        /// </summary>
        /// <param name = "postedFile">要保存的附件</param>
        /// <param name = "targetCategory">附件的分类文件夹</param>
        /// <param name = "fileName">附件的新名称</param>
        /// <returns></returns>
        string SaveAttach(HttpPostedFile postedFile, string targetCategory, string fileName);

        /// <summary>
        ///   保存附件(推荐使用),并返回附件服务器上的真实的分类目录
        /// </summary>
        /// <param name = "postedFile">要保存的附件</param>
        /// <param name = "targetCategory">附件的分类文件夹</param>
        /// <returns></returns>
        string SaveAttach(HttpPostedFile postedFile, string targetCategory);

        /// <summary>
        /// 保存附件,并返回附件服务器上的真实的分类目录
        /// </summary>
        /// <param name="postedFile">要保存的附件</param>
        /// <param name="targetCategory">附件的分类文件夹</param>
        /// <param name="fileName">附件的新名称</param>
        /// <param name="genUniqueFileName">是否改名</param>
        /// <returns></returns>
        string SaveAttach(HttpPostedFile postedFile, string targetCategory, string fileName, bool genUniqueFileName);

        /// <summary>
        /// 保存附件,并返回附件服务器上的真实的分类目录
        /// </summary>
        /// <param name="sourceFile">要保存的附件</param>
        /// <param name="targetCategory">附件的分类文件夹</param>
        /// <param name="fileName">附件的新名称</param>
        /// <param name="genUniqueFileName">是否改名</param>
        /// <returns></returns>
        string SaveAttach(Stream sourceFile, string targetCategory, string fileName, bool genUniqueFileName);


        /// <summary>
        /// 保存邮件的附件(邮件支撑这使用)
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="targetCategory"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string SaveEmailAttach(byte[] sourceFile, string targetCategory, string fileName);

        /// <summary>
        ///   附件是否存在
        /// </summary>
        /// <param name = "targetCategoryPath"></param>
        /// <returns></returns>
        bool HasAttachment(string targetCategoryPath);

        /// <summary>
        ///   删除文件
        /// </summary>
        /// <param name = "targetCategoryAttachPath">附件的分类路径</param>
        /// <returns></returns>
        bool DeleteFile(string targetCategoryAttachPath);

        /// <summary>
        ///   下载文件
        /// </summary>
        /// <param name = "targetCategoryAttachPath">需要下载的文件路径</param>
        void DownLoadFile(string targetCategoryAttachPath);

        /// <summary>
        ///   下载文件
        /// </summary>
        /// <param name = "targetCategoryAttachPath">需要下载的文件路径</param>
        /// <param name = "newFileName">保存文件的文件名</param>
        void DownLoadFile(string targetCategoryAttachPath, string newFileName);

        /// <summary>
        ///   下载文件到网站本地的Path目录
        /// </summary>
        /// <param name = "targetCategoryAttachPath">需要下载的文件路径</param>
        /// <param name = "path"></param>
        /// <param name = "newFileName"></param>
        bool DownLoadFileToPath(string targetCategoryAttachPath, string path, string newFileName);

        /// <summary>
        ///   下载文件到网站本地的Temp目录
        /// </summary>
        /// <param name = "targetCategoryAttachPath">需要下载的文件路径</param>
        bool DownLoadFileToTemp(string targetCategoryAttachPath);

        /// <summary>
        ///   下载文件
        /// </summary>
        /// <param name = "fullpath">文件的全路径</param>
        /// <param name = "newFileName">文件新名称</param>
        void DownLoadFileByFullPath(string fullpath, string newFileName);

        /// <summary>
        ///   下载文件到网站本地的Temp目录
        /// </summary>
        /// <param name = "targetCategoryAttachPath">需要下载的文件路径</param>
        /// <param name = "newFileName">新文件名</param>
        /// <returns></returns>
        bool DownLoadFileToTemp(string targetCategoryAttachPath, string newFileName);

        /// <summary>
        ///   获取附件的绝对物理路径
        /// </summary>
        /// <param name = "categoryPath"></param>
        /// <returns></returns>
        string GetAttachmentFullPath(string categoryPath);

        /// <summary>
        ///   获取附件的完整物理路径
        /// </summary>
        /// <returns></returns>
        string GetAttachmentFullFileAddress(); 

        #endregion
    }
}
