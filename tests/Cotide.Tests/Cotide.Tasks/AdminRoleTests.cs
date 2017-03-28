//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：AdminRoleTests.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/12/26 9:12:38 
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
using Cotide.Domain.Dtos;
using Cotide.Framework.Commands;
using NUnit.Framework;
using Tests.Base; 

namespace Tests.Cotide.Tasks
{
    public class AdminRoleTests : DatabaseRepositoryTestsBase
    {
        private ICommandProcessor _processor; 

        public AdminRoleTests()
            : base(true)
        {
        }

        [SetUp]
        public void Init()
        {
            _processor = base.Get<ICommandProcessor>(); 
        }


     

      
    }
}
