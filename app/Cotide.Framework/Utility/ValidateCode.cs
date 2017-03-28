using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Cotide.Framework.Utility
{
    /// <summary>
    /// 验证码生成辅助类
    /// </summary>
    public class ValidateCode
    {
        #region///生产验证码类
        /// <summary>
        /// 模糊度
        /// </summary>
        private int _fineness;
        /// <summary>
        /// 字体
        /// </summary>
        private Font _font;
        /// <summary>
        /// 笔刷
        /// </summary>
        private Brush _brush;
        /// <summary>
        /// 背景笔刷
        /// </summary>
        private Brush _backGround = new SolidBrush(Color.White); //默认白色

        /// <summary>
        /// 随机字体
        /// </summary>
        private static string[] FontItems = new string[] { "Arial", "Helvetica", "Geneva", "sans-serif", "Verdana", "新宋体", "黑体" };

        /// <summary>
        /// 是否自动随机生成字体
        /// </summary>
        private bool _isRandomFont = false;

        /// <summary>
        /// 是否自动随机生成字体GET/SET
        /// </summary>
        public bool IsRandomFont
        {
            get { return _isRandomFont; }
            set { _isRandomFont = value; }
        }

        /// <summary>
        ///  构造函数
        /// </summary>
        public ValidateCode()
        {
            _fineness = 13;
            _font = new Font("Verdana", 12, FontStyle.Regular);
            _brush = new SolidBrush(Color.Red);
            //_backGround = new SolidBrush(Color.FromArgb(0,0, 0));
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fineness">模糊度</param>
        /// <param name="font">字体</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="backGround">背景颜色</param>
        public ValidateCode(int fineness, Font font, Color fontColor, Color backGround)
        {
            _fineness = fineness;
            _font = font;
            _brush = new SolidBrush(fontColor);
            _backGround = new SolidBrush(backGround);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fineness">模糊度</param> 
        /// <param name="fontColor">字体颜色</param>
        /// <param name="backGround">背景颜色</param>
        public ValidateCode(int fineness, Color fontColor, Color backGround)
        {
            _fineness = fineness;
            _font = new Font("Verdana", 14, FontStyle.Regular);
            _brush = new SolidBrush(fontColor);
            _backGround = new SolidBrush(backGround);
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fineness">模糊度</param>
        /// <param name="font">字体</param>
        /// <param name="fontColor">字体颜色</param>
        /// <param name="backGround">背景颜色</param>
        /// <param name="isRandomFont">是否随机生成字体</param>
        public ValidateCode(int fineness, Font font, Color fontColor, Color backGround, bool isRandomFont)
        {
            _fineness = fineness;
            _font = font;
            _brush = new SolidBrush(fontColor);
            _backGround = new SolidBrush(backGround);
            _isRandomFont = isRandomFont;
        }

        /// <summary></summary>  
        /// 随机取一个字体    
        /// <returns>返回该字体</returns>  
        private Font GetFont()
        {
            int fontIndex = new Random().Next(0, FontItems.Length);
            return new Font(FontItems[fontIndex], _font.Size, GetFontStyle());
        }

        /// <summary></summary>  
        /// 取一个字体的样式  
        /// <returns>返回该样式</returns>  
        private static FontStyle GetFontStyle()
        {
            switch (DateTime.Now.Second % 2)
            {
                case 0:
                    return FontStyle.Regular | FontStyle.Bold;
                case 1:
                    return FontStyle.Italic | FontStyle.Bold;
                default:
                    return FontStyle.Regular | FontStyle.Bold;
            }
        }

        /// <summary>
        /// 创建验证码图片
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="code">验证码</param>
        /// <returns>返回生成的验证码图片</returns>
        public Image CreateImage(int width, int height, out string code)
        {
            code = this.RandValidateCode();
            return this.CreateImage(code, width, height);
        }


        /// <summary>
        /// 创建验证码图片
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>返回生成的验证码图片</returns>
        public Image CreateImage(string code, int width, int height)
        {
            Bitmap img = new Bitmap(width, height);
            Graphics grfx = Graphics.FromImage(img);
            //grfx.FillRectangle(_backGround, 30, 30, 10,10);
            this.DisturbBitmap(img);
            this.DrawValidateCode(img, code);

            return img;
        }

        /// <summary>
        /// 生成随机验证码图片()
        /// </summary>
        /// <returns></returns>
        private string RandValidateCode()
        {
            Random rnd = new Random();
            StringBuilder code = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                // 生成数字 & 英文
                //switch (rnd.Next(3))
                //{
                //    case 0: code.Append((char)rnd.Next((int)'a', ((int)'z') + 1)); break;
                //    case 1: code.Append(rnd.Next(10)); break;
                //    default: code.Append((char)rnd.Next((int)'A', ((int)'Z') + 1)); break;
                //}

                // 生成数字
                code.Append(rnd.Next(10));

            }
            return code.ToString();
        }


        /// <summary>
        /// 制作模糊度
        /// </summary>
        /// <param name="img">图片</param>
        private void DisturbBitmap(Bitmap img)
        {
            // 生成模糊度方式一

            //Random random = new Random();

            //for (int i = 0; i < img.Width; i++)
            //{
            //    for (int j = 0; j < img.Height; j++)
            //    {
            //        if (random.Next(100) <= _fineness)
            //            img.SetPixel(i, j, Color.White);
            //    }
            //}

            // 生成模糊度方式二
            Pen linePen = new Pen(new SolidBrush(Color.Red), 1);
            Random r = new Random();
            Graphics graph = Graphics.FromImage(img);
            graph.FillRectangle(_backGround, 0, 0, img.Width, img.Height);
            for (int x = 1; x <= _fineness; x++)
            {
                graph.DrawLine(linePen, new Point(r.Next(0, 199), r.Next(0, 59)), new Point(r.Next(0, 199), r.Next(0, 59)));
            }
        }

        /// <summary>
        /// 在图片上制作验证码
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="validateCode">验证码</param>
        private void DrawValidateCode(Bitmap img, string validateCode)
        {
            Graphics grfx = Graphics.FromImage(img);
            SizeF stringSize = grfx.MeasureString(validateCode, _font);
            float perWidth = stringSize.Width / validateCode.Length;
            float avgWidth = img.Width / (float)validateCode.Length;

            float x = (avgWidth - perWidth) / 2;
            float y = (img.Height - stringSize.Height) / 2;

            //动态生成字体
            if (_isRandomFont)
            {
                for (int i = 0; i < validateCode.Length; i++)
                {
                    string c = validateCode[i].ToString();
                    grfx.DrawString(c, GetFont(), _brush, (i * avgWidth + x), y);
                }
            }
            else //使用默认字体
            {
                for (int i = 0; i < validateCode.Length; i++)
                {
                    string c = validateCode[i].ToString();
                    grfx.DrawString(c, _font, _brush, (i * avgWidth + x), y);
                }

            }
        }

        #endregion
    }
}
