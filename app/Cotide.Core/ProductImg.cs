//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ProductImg.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/11/14 15:01:38 
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
using Cotide.Framework.Attr.Desc;
using SharpArch.Domain.DomainModel;

namespace Cotide.Domain
{
    /// <summary>
    /// 产品图片
    /// </summary>
    [EntityDesc("产品图片")]
    public class ProductImg : Entity
    {
        /// <summary>
        /// 图片名称
        /// </summary>
        [EntityPropertyDesc("图片名称")]
        public virtual string Name { get; set; }

        /// <summary>
        /// 产品图片提示
        /// </summary>
        [EntityPropertyDesc("产品图片提示")]
        public virtual string ImgTip { get; set; }

        /// <summary>
        /// 图片文件名
        /// </summary>
        [EntityPropertyDesc("图片文件名")]
        public virtual string FileName { get; set; }

        /// <summary>
        /// 图片宽度
        /// </summary>
        [EntityPropertyDesc("图片宽度")]
        public virtual int Width { get; set; }

        /// <summary>
        /// 图片高度
        /// </summary>
        [EntityPropertyDesc("图片高度")]
        public virtual int Height { get; set; }

        /// <summary>
        /// 图片保存位置
        /// </summary>
        [EntityPropertyDesc("图片保存位置")]
        public virtual string AccessLocation { get; set; }

    }
}
