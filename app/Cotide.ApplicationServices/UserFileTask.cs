using System.Configuration;
using System.Web;
using Cotide.Domain.Contracts.Commands;
using Cotide.Domain.Contracts.Task;
using Cotide.Domain.Enum;
using Cotide.Framework.File;
using Cotide.Framework.File.Config;
using Cotide.Framework.Setting;
using System.IO;

namespace Cotide.Tasks
{
    public class UserFileTask : IUserFileTask
    {
        private readonly IFileAttachmentUtility _fileAttachmentUtility;

        public UserFileTask(IFileAttachmentUtility fileAttachmentUtility)
        {
            this._fileAttachmentUtility = fileAttachmentUtility;
        }

        /// <summary>
        /// 文件保存根目录
        /// </summary>
        protected string  BaseUploadPath
        {
            get { return ConfigurationManager.AppSettings["UploadFilePath"]; }
        }

        /// <summary>
        /// 系统文件保存路径
        /// </summary>
        protected string SystemUploadPath
        {
            get { return BaseUploadPath + "System"; }
        }

        /// <summary>
        /// 用户文件保存
        /// </summary>
        /// <param name="command"></param>
        /// <param name="smallImgHead"></param>
        /// <param name="standardImgHead"></param>
        /// <returns></returns>
        public string SaveUserImg(UserFileCommand command,
            out string smallImgHead,
            out string standardImgHead)
        {
            smallImgHead = string.Empty;
            standardImgHead = string.Empty;
            var request = command.Data;
            if (request.ContentLength > 0)
            {
                // 上传用户图片 
                var targetCategory = BaseUploadPath + command.UserId + @"/";
                targetCategory += GetSaveFile(UserFileType.Image) + @"/";
                // 允许上传文件后缀 
                var fullFilePath = _fileAttachmentUtility.SaveLocalhostImgAttach(request, targetCategory, new FileAttachOption()
                {
                    BuildOriginalImg = true,
                    BuildStandardImg = true
                });
                smallImgHead = targetCategory + "s_" + fullFilePath;
                standardImgHead = targetCategory + "b_" + fullFilePath;
                return targetCategory + fullFilePath;
            }
            return string.Empty;
        }

        /// <summary>
        /// 系统文件保存
        /// </summary>
        /// <param name="command"></param>  
        /// <returns></returns>
        public UploadImgResult SaveSystemImg(SystemImgFileCommand command)
        {
            var result = new UploadImgResult();
            var request = command.Data;
            if (request.ContentLength > 0)
            {
                // 上传用户图片 
                var targetCategory = SystemUploadPath + @"/";
                targetCategory += GetSaveFile(UserFileType.Image) + @"/";
                var option = new FileAttachOption();

                if(command.SmallImgFileSetting.IsUser)
                {
                    option.OriginalImgWidth = command.SmallImgFileSetting.Width;
                    option.OriginalImgHeight = command.SmallImgFileSetting.Height;
                    option.BuildOriginalImg = true;
                    option.OriginalImgModel = command.SmallImgFileSetting.ImgModel;
                }
                else
                {
                    option.BuildOriginalImg = false;
                }
               
                if(command.StandardImgFileSetting.IsUser)
                {
                    option.StandardImgWidth = command.StandardImgFileSetting.Width;
                    option.StandardImgHeight = command.StandardImgFileSetting.Height;
                    option.BuildStandardImg = true;
                    option.StandardImgModel = command.StandardImgFileSetting.ImgModel;
                }
                else
                {
                    option.BuildStandardImg = false;
                }

                // 允许上传文件后缀 
                var fullFilePath = _fileAttachmentUtility.SaveLocalhostImgAttach(request, targetCategory, option);
                if(command.SmallImgFileSetting.IsUser)
                {
                    result.SmallImg = targetCategory + "s_" + fullFilePath;
                }
                if(command.StandardImgFileSetting.IsUser)
                {
                    result.StandardImg = targetCategory + "b_" + fullFilePath;
                }
                 
                result.Img =  targetCategory + fullFilePath;
            }
            return result;
        }

      

        #region Helper
        private string GetSaveFile(UserFileType userFileType)
        {
            switch (userFileType)
            {
                case UserFileType.Image:
                    return "Images";
                case UserFileType.Document:
                    return "Document";
                case UserFileType.Media:
                    return "Media";
                default:
                    return "Profile";
            }
        }

        #endregion
    }
}
