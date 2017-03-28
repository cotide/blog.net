using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cotide.Framework.Exceptions
{
    /// <summary>
    /// 业务异常
    /// </summary>
    public class BusinessException : Exception
    {
        private readonly string _errorMsg;

        public BusinessException(string errorMsg)
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
