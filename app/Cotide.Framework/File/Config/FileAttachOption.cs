using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Framework.Enumerable;

namespace Cotide.Framework.File.Config
{
    /// <summary>
    /// 附件配置
    /// </summary>
    public class FileAttachOption
    {
        /// <summary>
        /// 是否生成缩略图(默认:是)
        /// </summary>
        private bool _buildOriginalImg = true;

        /// <summary>
        /// 缩略图宽度(默认：50px)
        /// </summary>
        private int _originalImgWidth = 50;

        /// <summary>
        /// 缩略图高度(默认：50px)
        /// </summary>
        private int _originalImgHeight = 50;



        /// <summary>
        /// 缩略图图片压缩量方式
        /// </summary>
        public ImgModel OriginalImgModel { get; set; }

        /// <summary>
        /// 是否生成标准图
        /// </summary>
        private bool _buildStandardImg = true;

        /// <summary>
        /// 标准图宽度(默认：150px)
        /// </summary>
        private int _standardImgWidth = 190;

        /// <summary>
        /// 标准图高度(默认：150px)
        /// </summary>
        private int _standardImgHeight = 190;

        /// <summary>
        /// 允许上传的文件格式
        /// </summary>
        public string[] ImgSuffix { get; set; }


        /// <summary>
        /// 标准图图片缩量方式
        /// </summary>
        public ImgModel StandardImgModel { get; set; }

        #region Get/Set
        /// <summary>
        /// 是否生成缩略图(默认:是)
        /// </summary>
        public bool BuildOriginalImg
        {
            get { return _buildOriginalImg; }
            set { _buildOriginalImg = value; }
        } 

        /// <summary>
        /// 缩略图宽度(默认：50px)
        /// </summary>
        public int OriginalImgWidth
        {
            get { return _originalImgWidth; }
            set { _originalImgWidth = value; }
        }
        
        /// <summary>
        /// 缩略图高度(默认：50px)
        /// </summary>
        public int OriginalImgHeight
        {
            get { return _originalImgHeight; }
            set { _originalImgHeight = value; }
        } 
    
        /// <summary>
        /// 是否生成标准图(默认:是)
        /// </summary>
        public bool BuildStandardImg
        {
            get { return _buildStandardImg; }
            set { _buildStandardImg = value; }
        }

        /// <summary>
        /// 标准图宽度(默认：190px)
        /// </summary>
        public int StandardImgWidth
        {
            get { return _standardImgWidth; }
            set { _standardImgWidth = value; }
        }
         
        /// <summary>
        /// 标准图高度(默认：190px)
        /// </summary>
        public int StandardImgHeight
        {
            get { return _standardImgHeight; }
            set { _standardImgHeight = value; }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public  FileAttachOption()
        { 

        }

       
    }
}
