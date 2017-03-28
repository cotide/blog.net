using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using log4net;
using log4net.Core;
using Microsoft.Practices.ServiceLocation; 

namespace Cotide.Framework.Logging
{

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LoggerTyp
    {
        [Description("调试日志")]
        Debug,
        [Description("错误日志")]
        Error,
        [Description("信息日志")]
        Info
    }


    /// <summary>
    /// 日志记录基类
    /// </summary>
    public class Logger  
    {

        /// <summary>
        /// 日志类型实例前缀
        /// </summary>
        private static readonly string LogLoggerPrefix;
        private static readonly ILog LogLogdebug;
        private static readonly ILog LogLogerror;
        private static readonly ILog LogLoginfo;

        /// <summary>
        /// 初始化
        /// </summary>
        static Logger()
        {
            LogLoggerPrefix = ConfigurationManager.AppSettings["LoggerPrefix"];
            LogLogdebug = LogManager.GetLogger(LogLoggerPrefix + ".Debug");
            LogLogerror = LogManager.GetLogger(LogLoggerPrefix + ".Error");
            LogLoginfo = LogManager.GetLogger(LogLoggerPrefix + ".Info");
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Logger()
        {

        }

        /// <summary>
        /// 获取ILog实例
        /// </summary>
        /// <param name="logName"></param>
        /// <returns></returns>
        public static ILog GetLog(string logName)
        {
            return LogManager.GetLogger(logName);
        }

        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <param name="contextInfo">错误信息</param>
        /// <param name="errorPath">错误地址</param>
        public static void Debug(string msg, string contextInfo, string errorPath = null)
        {
            Log(msg, contextInfo, LoggerTyp.Debug, errorPath);
        }

        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <param name="ex">错误异常</param>
        /// <param name="contextInfo">错误信息</param>
        /// <param name="errorPath">错误地址</param>
        public static void Debug(string msg, Exception ex, string contextInfo, string errorPath = null)
        {
            Log(msg, contextInfo, ex, LoggerTyp.Debug, errorPath);
        }


        ///<summary>
        /// 记录错误日志
        ///</summary>
        ///<param name="msg">错误提示</param>
        ///<param name="contextInfo">错误信息</param>
        ///<param name="errorPath">错误地址</param>
        public static void Error(string msg, string contextInfo, string errorPath = null)
        {
            Log(msg, contextInfo, LoggerTyp.Error, errorPath);
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <param name="ex">错误异常</param>
        /// <param name="contextInfo">错误信息</param>
        /// <param name="errorPath">错误地址</param>
        public static void Error(Exception ex, string msg, string contextInfo, string errorPath = null)
        {
            Log(msg, contextInfo, ex, LoggerTyp.Error,errorPath);
        }


        ///<summary>
        /// 记录信息日志
        ///</summary>
        ///<param name="msg">错误提示</param>
        ///<param name="contextInfo">错误信息</param>
        ///<param name="errorPath">错误地址</param>
        public static void Info(string msg, string contextInfo, string errorPath = null)
        {
            Log(msg, contextInfo, LoggerTyp.Info,errorPath);
        }

        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <param name="ex">错误异常</param>
        /// <param name="contextInfo">错误信息</param>
        /// <param name="errorPath">错误地址</param>
        public static void Info(Exception ex, string msg, string contextInfo, string errorPath = null)
        {
            Log(msg, contextInfo, ex, LoggerTyp.Info, errorPath);
        }


        ///<summary>
        /// 记录信息
        ///</summary>
        ///<param name="msg">错误提示</param>
        ///<param name="contextInfo">消息详细内容</param>
        ///<param name="logType">日志类型</param>
        ///<param name="errorPath">错误地址</param>
        public static void Log(string msg, string contextInfo, LoggerTyp logType,string errorPath)
        {

            var builder = new StringBuilder();
            builder.AppendFormat("Time={0}; ", DateTime.Now);
            if (!string.IsNullOrEmpty(msg))
            {
                builder.AppendLine();
                builder.AppendFormat("[ErrorTitle]={0}", msg);
            }
            if (!string.IsNullOrEmpty(contextInfo))
            {
                builder.AppendLine();
                builder.AppendFormat("[ContextInfo]={0}", contextInfo);
            }
          
            if (!string.IsNullOrEmpty(errorPath))
            {
                builder.AppendLine();
                builder.AppendFormat("[Request Path]={0}", errorPath);
            }
            builder.AppendLine();

            switch (logType)
            {
                case LoggerTyp.Info: if (LogLoginfo.IsDebugEnabled) LogLoginfo.Info(builder.ToString()); break;
                case LoggerTyp.Error: if (LogLogerror.IsDebugEnabled) LogLogerror.Info(builder.ToString()); break;
                case LoggerTyp.Debug: if (LogLogdebug.IsDebugEnabled) LogLogdebug.Info(builder.ToString()); break;
            }

        }

        ///<summary>
        /// 记录信息
        ///</summary>
        ///<param name="msg">错误提示</param>
        ///<param name="contextInfo">消息详细内容</param>
        ///<param name="ex">异常</param>
        ///<param name="logType">日志类型</param>
        public static void Log(string msg, string contextInfo, Exception ex, LoggerTyp logType, string errorPath)
        {

            var builder = new StringBuilder();
            builder.AppendFormat("Time={0}; ", DateTime.Now);
            if (!string.IsNullOrEmpty(msg))
            {
                builder.AppendLine();
                builder.AppendFormat("[ErrorTitle]={0}", msg);
            }
            if (!string.IsNullOrEmpty(contextInfo))
            {
                builder.AppendLine();
                builder.AppendFormat("[ContextInfo]={0}", contextInfo);
            } 
            if (!string.IsNullOrEmpty(errorPath))
            {
                builder.AppendLine();
                builder.AppendFormat("[Request Path]={0}", errorPath);
            }
            builder.AppendLine();

            switch (logType)
            {
                case LoggerTyp.Info: if (LogLoginfo.IsDebugEnabled) LogLoginfo.Info(builder.ToString(), ex); break;
                case LoggerTyp.Error: if (LogLogerror.IsDebugEnabled) LogLogerror.Info(builder.ToString(), ex); break;
                case LoggerTyp.Debug: if (LogLogdebug.IsDebugEnabled) LogLogdebug.Info(builder.ToString(), ex); break;
            }

        }
    }
}
