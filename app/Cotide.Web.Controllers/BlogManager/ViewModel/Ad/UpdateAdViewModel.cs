//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：UpdateAdViewModel.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2013/3/25 15:30:29 
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
using Cotide.Domain.Enum;

namespace Cotide.Web.Controllers.BlogManager.ViewModel.Ad
{
    public class UpdateAdViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 广告名称
        /// </summary> 
        public string AdName { get; set; }

        /// <summary>
        /// 广告类型
        /// </summary> 
        public AdType AdType { get; set; }

        /// <summary>
        /// 广告描述
        /// </summary> 
        public string AdDesc { get; set; }

        /// <summary>
        /// 广告图片
        /// </summary> 
        public string AdImg { get; set; }

        /// <summary>
        /// 广告图片 标准图
        /// </summary>
        public string StandardAdImg { get; set; }

        /// <summary>
        /// 广告图片 小图
        /// </summary>
        public string SmallAdImg { get; set; }
 

        /// <summary>
        /// 是否显示
        /// </summary> 
        public bool IsShow { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }
    }
}
