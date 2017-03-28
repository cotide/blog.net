//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ImgResult.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/25 13:36:44 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cotide.Framework.Setting
{
    /// <summary>
    /// 上传图片结果
    /// </summary>
    public class UploadImgResult
    {
        /// <summary>
        /// 原图片文件
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        /// 标准图片文件
        /// </summary>
        public string StandardImg { get; set; }
         
        /// <summary>
        /// 小图片文件
        /// </summary>
        public string SmallImg { get; set; }
    }
}
