//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：IUserFileTask.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/1/16 21:36:25 
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
using Cotide.Domain.Contracts.Commands;
using Cotide.Framework.Setting;

namespace Cotide.Domain.Contracts.Task
{
    /// <summary>
    /// 文件事务接口
    /// </summary>
    public interface IUserFileTask
    {
        /// <summary>
        /// 用户文件保存
        /// </summary>
        /// <param name="command"></param>
        /// <param name="smallImgHead"></param>
        /// <param name="standardImgHead"></param>
        /// <returns></returns>
        string SaveUserImg(UserFileCommand command,
            out string smallImgHead,
            out string standardImgHead);

        /// <summary>
        /// 系统文件保存
        /// </summary>
        /// <param name="command"></param>  
        /// <returns></returns>
        UploadImgResult SaveSystemImg(SystemImgFileCommand command);
         
    }
}
