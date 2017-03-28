using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using log4net;

namespace Cotide.Framework.Logging
{
    public class Log4NetLogServer : ILogServer
    {

        //private readonly string _parentMethodDeclaringType;
        //private readonly string _parentMethodName;

        //public Log4NetLogServer()
        //{
        //    try
        //    {
        //        var st = new StackTrace(true);
        //        _parentMethodDeclaringType = st.GetFrame(0).GetMethod().DeclaringType.ToString();
        //        _parentMethodName = st.GetFrame(0).GetMethod().ToString();
        //    }
        //    catch (NullReferenceException)
        //    {
        //        _parentMethodDeclaringType = "找不到类型";
        //        _parentMethodName = "找不到方法";
        //    }
        //}

        #region Implementation of ILogger

        /// <summary>
        /// 获取ILog实例
        /// </summary>
        /// <param name="logName"></param>
        /// <returns></returns>
        public ILog GetLog(string logName)
        {
            return Logger.GetLog(logName);
        }

        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <param name="contextInfo">错误信息</param>
        /// <param name="errorPath">错误地址</param>
        public void Debug(string msg, string contextInfo, string errorPath = null)
        {
            Logger.Debug(msg, contextInfo);
        }

        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <param name="ex">错误异常</param>
        /// <param name="contextInfo">错误信息</param>
        /// <param name="errorPath">错误地址</param>
        public void Debug(string msg, Exception ex, string contextInfo, string errorPath = null)
        {
            Logger.Debug(msg, ex, contextInfo,errorPath);
        }

        ///<summary>
        /// 记录错误日志
        ///</summary>
        ///<param name="msg">错误提示</param>
        ///<param name="contextInfo">错误信息</param>
        ///<param name="errorPath">错误地址</param>
        public void Error(string msg, string contextInfo, string errorPath = null)
        {
            Logger.Error(msg, contextInfo, errorPath);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <param name="ex">错误异常</param>
        /// <param name="contextInfo">错误信息</param>
        /// <param name="errorPath">错误地址</param>
        public void Error(Exception ex, string msg, string contextInfo, string errorPath = null)
        {
            Logger.Error(ex, msg, contextInfo, errorPath);
        }

        ///<summary>
        /// 记录信息日志
        ///</summary>
        ///<param name="msg">错误提示</param>
        ///<param name="contextInfo">错误信息</param>
        ///<param name="errorPath">错误地址</param>
        public void Info(string msg, string contextInfo, string errorPath = null)
        {
            Logger.Info(msg, contextInfo, errorPath);
        }

        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <param name="ex">错误异常</param>
        /// <param name="contextInfo">错误信息</param>
        /// <param name="errorPath">错误地址</param>
        public void Info(Exception ex, string msg, string contextInfo, string errorPath = null)
        {
            Logger.Info(ex, msg, contextInfo, errorPath);
        }

        ///<summary>
        /// 记录信息
        ///</summary>
        ///<param name="msg">错误提示</param>
        ///<param name="contextInfo">消息详细内容</param>
        ///<param name="logType">日志类型</param>
        ///<param name="errorPath">错误地址</param>
        public void Log(string msg, string contextInfo, LoggerTyp logType, string errorPath = null)
        {
            Logger.Log(msg, contextInfo, logType,errorPath);
        }

        ///<summary>
        /// 记录信息
        ///</summary>
        ///<param name="msg">错误提示</param>
        ///<param name="contextInfo">消息详细内容</param>
        ///<param name="ex">异常</param>
        ///<param name="logType">日志类型</param>
        ///<param name="errorPath">错误地址</param>
        public void Log(string msg, string contextInfo, Exception ex, LoggerTyp logType,string errorPath=null)
        {
            Logger.Log(msg, contextInfo, ex, logType,errorPath);
        }

        #endregion
    }
}
