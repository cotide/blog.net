//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：AdminTest.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/6 11:38:08 
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
using Cotide.Framework.Commands;
using Cotide.Framework.Config;
using Cotide.Framework.Utility;
using Cotide.QueryServices; 
using Cotide.Tasks.Commands.UserCommands;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using Tests.Base;
using Cotide.Domain.Enum;
using Cotide.Domain.Contracts.QueryServices;

namespace Tests.Cotide.Tasks
{
    [TestFixture]
    public class AdminTests : DatabaseRepositoryTestsBase
    {
        private ICommandProcessor _processor;
        

        public AdminTests()
            : base(true)
        {
        }

        [SetUp]
        public void Init()
        {
            _processor = base.Get<ICommandProcessor>(); 
        }
 

        [Test]
        public void TT()
        {
            var UserRole = UserLoginRole.Admin;
            Console.WriteLine(((UserRole & UserLoginRole.User) != 0));
        }

    }
}
