//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ServiceLocatorInitiBase.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/6 11:39:53 
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
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using log4net.Config;

namespace Tests
{
    public class ServiceLocatorInitiBase
    {

         
        public static    void SetUp()
        {
            XmlConfigurator.Configure();
            ServiceLocatorInitializer.Init(); 
        }

        /// <summary>
        /// 获取接口实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T Get<T>()
        {
            return ServiceLocator.Current.GetInstance<T>();
        }
    }
}
