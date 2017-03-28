//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：SmallImgFileSetting.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/25 13:35:33 
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
using Cotide.Framework.Enumerable;

namespace Cotide.Framework.Setting
{
    /// <summary>
    /// 小图片文件配置
    /// </summary>
    public class SmallImgFileSetting
    {
        public SmallImgFileSetting()
        {
            Width = 150;
            Height = 150;
            IsUser = true;
        }

        /// <summary>
        /// 宽度 默认:150px
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高度 默认:150px
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 是否使用 默认为是
        /// </summary>
        public bool IsUser { get; set; }


        /// <summary>
        /// 图片压缩量方式
        /// </summary>
        public ImgModel ImgModel { get; set; }
    }
}
