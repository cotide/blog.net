using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using Cotide.Framework.Enumerable;

namespace Cotide.Framework.Utility
{
  
    /// <summary>
    /// 图片帮助类
    /// </summary>
    public class ImgHelper
    {

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    else
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.White);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            var encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;

            var encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            var lenEnd = originalImagePath.LastIndexOf('.');
            var fileType = originalImagePath.Substring(lenEnd, originalImagePath.Length - lenEnd);

            ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegICI = null;
            ImageCodecInfo GIFICI = null;
            ImageCodecInfo jpgICI = null;
            ImageCodecInfo pngICI = null;
            ImageCodecInfo bmpICI = null;

            ImageCodecInfo allICI = null;
            foreach (var t in arrayICI)
            {
                if (t.FormatDescription.Equals("JPEG"))
                    jpegICI = t;//设置JPEG编码

                if (t.FormatDescription.Equals("GIF"))
                    GIFICI = t;//设置GIF编码

                if (t.FormatDescription.Equals("JPG"))
                    jpgICI = t;//设置GIF编码

                if (t.FormatDescription.Equals("PNG"))
                    pngICI = t;//设置GIF编码

                if (t.FormatDescription.Equals("BMP"))
                    bmpICI = t;//设置GIF编码
            }

            switch (fileType.ToLower())
            {
                case ".jpg":
                    allICI = jpegICI;
                    break;
                case ".gif":
                    allICI = GIFICI;
                    break;
                case ".jpeg":
                    allICI = jpegICI;
                    break;
                case ".bmp":
                    allICI = bmpICI;
                    break;
                case ".png":
                    allICI = pngICI;
                    break;
            }

            try
            {
                bitmap.Save(thumbnailPath, allICI, encoderParams);
                ////以jpg格式保存缩略图
                //bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        ///// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImageStream">源图字节</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="model">图片缩量方式</param>
        /// <param name="imgFomat">图片类型</param>    
        public static byte[] MakeThumbnail(Stream originalImageStream, int width, int height, ImgModel model, ImageFormat imgFomat)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(originalImageStream);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (model)
            {
                case ImgModel.Hw://指定高宽缩放（可能变形）                
                    break;
                case ImgModel.W://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case ImgModel.H://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case ImgModel.Cut://指定高宽裁减（不变形）       
                    oh = originalImage.Height;
                    ow = originalImage.Width;
                    x = 0;
                    y = 0;
                    //if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    //{
                    //    oh = originalImage.Height;
                    //    ow = originalImage.Height * towidth / toheight;
                    //    y = 0;
                    //    x = (originalImage.Width - ow) / 2;
                    //}
                    //else
                    //{
                    //    ow = originalImage.Width;
                    //    oh = originalImage.Width * height / towidth;
                    //    x = 0;
                    //    y = (originalImage.Height - oh);
                    //}
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以指定格式保存缩略图 
                return ImageToByteArray(bitmap, imgFomat);
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }


        /// <summary>
        /// 图片转换成字节
        /// </summary>
        /// <param name="imageIn">图片Images对象</param>
        /// <param name="model">图片转换类型</param>
        /// <returns>返回转换后字节</returns>
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn, ImageFormat model)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, model);
            return ms.ToArray();
        }

        /// <summary>
        /// 替换文件后缀
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="replace">替换的后缀名</param>
        /// <returns>返回替换后的文件名</returns>
        public static string ReplaceSuffix(string filename, string replace)
        {
            var oldValue = filename.Substring(filename.IndexOf('.') + 1);

            return filename.Replace(oldValue, replace);
        }

        /// <summary>
        /// 字节转换成图片
        /// </summary>
        /// <param name="byteArrayIn">图片字节</param>
        /// <returns>返回图片Images对象</returns>
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = Image.FromStream(ms);
            return returnImage;

        }


    }
}
