//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：JsonResultObjec.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/16 10:52:12 
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
using System.Web.Mvc;

namespace Cotide.Framework.ActionResult
{
    /// <summary>
    /// 操作结果JsonResult对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonResultObjec<T> :JsonResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public readonly bool Success;
        /// <summary>
        /// 数据
        /// </summary>
        public new readonly T Data;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="success"></param>
        /// <param name="data"></param>
        public JsonResultObjec(bool success, T data)
        { 
            Success = success;
            Data = data;
            base.Data = new
                            {
                                @Success = this.Success,
                                @Data = this.Data
                            };
        }
    }
}
