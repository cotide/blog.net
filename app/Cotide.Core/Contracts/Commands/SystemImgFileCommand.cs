//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：SystemFileCommand.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/25 11:15:46 
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
using System.Web;
using Cotide.Domain.Enum;
using Cotide.Framework.Setting;
using Cotide.Framework.Utility;

namespace Cotide.Domain.Contracts.Commands
{
    /// <summary>
    /// 保存系统文件命令
    /// </summary>
    public class SystemImgFileCommand
    {

        /// <summary>
        /// 文件
        /// </summary>
        public readonly HttpPostedFile Data;
         
        /// <summary>
        /// 标准图片配置
        /// </summary>
        public StandardImgFileSetting StandardImgFileSetting { get; set; }

        /// <summary>
        /// 小图片配置
        /// </summary>
        public SmallImgFileSetting SmallImgFileSetting { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        public SystemImgFileCommand(
            HttpPostedFile data)
        { 
            Guard.IsNotNull(data, "data"); 
            StandardImgFileSetting = new StandardImgFileSetting();
            SmallImgFileSetting = new SmallImgFileSetting(); 
            Data = data; 
        }
    } 
   
}
