//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：SysMenuTests.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/7 13:46:15 
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
using Cotide.Framework.Commands; 
using NUnit.Framework;
using Tests.Base;
using Cotide.Domain.Enum;

namespace Tests.Cotide.Tasks
{
    [TestFixture]
    public class SysMenuTests : DatabaseRepositoryTestsBase
    {
        private ICommandProcessor _processor;
        public SysMenuTests()
            : base(true)
        {
        }

        [SetUp]
        public void Init()
        {
            _processor = base.Get<ICommandProcessor>();
        }

        [Test]
        public void Save()
        { 
        }
    }
}
