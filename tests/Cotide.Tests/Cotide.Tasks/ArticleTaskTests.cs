//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ArticleTaskTests.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/12/26 14:18:37 
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
using Cotide.Tasks.Commands.ArticleCommands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Tests.Base;

namespace Tests.Cotide.Tasks
{
    [TestClass()]
    public class ArticleTaskTests : DatabaseRepositoryTestsBase
    {
        private ICommandProcessor _processor;

        public ArticleTaskTests()
            : base(true)
        {
        }

         [TestInitialize()]
        public void Init()
        {
            _processor = base.Get<ICommandProcessor>();

        }

        [TestMethod()]
        public void DeleteArticle()
        {
            _processor.Process<DeleteArticleCommand>(new DeleteArticleCommand(70, 1000));
        }



    }
}
