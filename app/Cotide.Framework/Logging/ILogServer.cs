using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Cotide.Framework.Logging
{
    public interface ILogServer
    {
        /// <summary>
        /// 获取ILog实例
        /// </summary>
        /// <param name="logName"></param>
        /// <returns></returns>
        ILog GetLog(string logName);

        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="errorTitle">错误提示</param>
        /// <param name="contextInfo">错误信息</param>
        /// <param name="errorPath">错误地址</param>
        void Debug(string errorTitle, string contextInfo, string errorPath = null);

        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="errorTitle">错误提示</param>
        /// <param name="ex">错误异常</param>
        /// <param name="contextInfo">错误信息</param>
        /// <param name="errorPath">错误地址</param>
        void Debug(string errorTitle, Exception ex, string contextInfo, string errorPath = null);


        ///<summary>
        /// 记录错误日志
        ///</summary>
        ///<param name="errorTitle">错误提示</param>
        ///<param name="contextInfo">错误信息</param>
        ///<param name="errorPath">错误地址</param>
        void Error(string errorTitle, string contextInfo, string errorPath = null);

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="errorTitle">错误提示</param>
        /// <param name="ex">错误异常</param>
        /// <param name="contextInfo">错误信息</param>
        /// <param name="errorPath">错误地址</param>
        void Error(Exception ex, string errorTitle, string contextInfo, string errorPath = null);


        ///<summary>
        /// 记录信息日志
        ///</summary>
        ///<param name="errorTitle">错误提示</param>
        ///<param name="contextInfo">错误信息</param>
        ///<param name="errorPath"></param>
        void Info(string errorTitle, string contextInfo, string errorPath = null);

        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="errorTitle">错误提示</param>
        /// <param name="ex">错误异常</param>
        /// <param name="contextInfo">错误信息</param>
        /// <param name="errorPath">错误地址</param>
        void Info(Exception ex, string errorTitle, string contextInfo, string errorPath = null);


        ///<summary>
        /// 记录信息
        ///</summary>
        ///<param name="errorTitle">错误提示</param>
        ///<param name="contextInfo">消息详细内容</param>
        ///<param name="logType">日志类型</param>
        ///<param name="errorPath">错误地址</param>
        void Log(string errorTitle, string contextInfo, LoggerTyp logType, string errorPath = null);


        ///<summary>
        /// 记录信息
        ///</summary>
        ///<param name="errorTitle">错误提示</param>
        ///<param name="contextInfo">消息详细内容</param>
        ///<param name="ex">异常</param>
        ///<param name="logType">日志类型</param>
        ///<param name="errorPath">错误地址</param>
        void Log(string errorTitle, string contextInfo, Exception ex, LoggerTyp logType, string errorPath = null);
    }
}
