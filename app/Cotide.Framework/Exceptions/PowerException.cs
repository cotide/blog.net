//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：PowerException.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/1/25 21:07:08 
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

namespace Cotide.Framework.Exceptions
{
    /// <summary>
    /// 权限异常
    /// </summary>
    public class PowerException: Exception
    {
        private readonly string _errorMsg;

        public PowerException(string errorMsg)
            : base(errorMsg)
        {
            _errorMsg = errorMsg;
        }

        public string BusinessExceptionMsg
        {
            get { return _errorMsg; }
        }

    }
}
