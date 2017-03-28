//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：AdminPowerTests.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/12/24 20:29:50 
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
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Framework.Commands;
using NUnit.Framework;
using Tests.Base; 

namespace Tests.Cotide.Tasks
{
    [TestFixture]
    public class AdminPowerTests : DatabaseRepositoryTestsBase
    {
        private ICommandProcessor _processor; 

        public AdminPowerTests()
            : base(true)
        {
        }

        [SetUp]
        public void Init()
        {
            _processor = base.Get<ICommandProcessor>(); 
        }

        [Test]
        public void CreateUpdateDeleteAdminPowerTest()
        {
            const string powerName = "新增文章";
            const string actionName = "AddArticle";
            const string controllerName = "Article";
 
        }

    }
}
