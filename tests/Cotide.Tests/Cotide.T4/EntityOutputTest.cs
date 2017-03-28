//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：EntityOutputTest.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/12/16 18:32:26 
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
using Cotide.Framework.Utility;
using NUnit.Framework;
using System;
using System.IO;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text; 

namespace Tests.Cotide.T4
{
    [Serializable]
    [TestFixture]
    public class EntityOutputTest : ServiceLocatorInitiBase
    {
 
        [Test]
        public void OutputEntity()
        {
          
            // var dll = System.Reflection.Assembly.LoadFrom("Cotide.Domain.dll");

           // var dll = new DomainMappingHelper(@"C:\资料\Person\项目\Codeplex\Person-Blog\版本二\tests\Cotide.Tests\Hibernate.cfg.xml", @"C:\资料\Person\项目\Codeplex\Person-Blog\版本二\tests\Cotide.Tests\bin\Debug\Cotide.Infrastructure.dll");
            /*
            try
            {
                T4Program.ProcessTemplate(new string[] { @"C:\资料\Person\项目\Codeplex\Person-Blog\版本二\tests\Cotide.Tests\Cotide.T4\Templates\default.tt" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }*/
        }


         
        //This will accept the path of a text template as an argument.
        //It will create an instance of the custom host and an instance of the
        //text templating transformation engine, and will transform the
        //template to create the generated text output file.
        //-------------------------------------------------------------------------
       public class T4Program
        {
           
            public static void ProcessTemplate(string[] args)
            {
                string templateFileName = null;
                if (args.Length == 0)
                {
                    throw new System.Exception("you must provide a text template file path");
                }
                templateFileName = args[0];
                if (templateFileName == null)
                {
                    throw new ArgumentNullException("the file name cannot be null");
                }
                if (!File.Exists(templateFileName))
                {
                    throw new FileNotFoundException("the file cannot be found");
                }
             
            }
        }
    } 

}
