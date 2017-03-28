//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ArticleMessageTests.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/17 15:12:43 
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
using Cotide.Domain;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories.Extension;
using Cotide.Framework.Commands;
using Cotide.Framework.UnitOfWork;
using Cotide.Tasks.Commands.ArticleCommands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Tests.Base;
using System.Web;

namespace Tests.Cotide.Tasks
{
    [TestClass()]
    public class ArticleMessageTests : DatabaseRepositoryTestsBase
    {
        private ICommandProcessor _processor;
         

        IDbProxyRepository<ArticleMessage> _articleMessageDbProxyRepository;
        IArticleQueryService _articleDbProxyRepository;
        private IArticleMessageQueryService _articleMessageQueryService;
        

        public ArticleMessageTests()
            : base(true )
        {
        }

         [TestInitialize()]
        public void Init()
        {
            _processor = base.Get<ICommandProcessor>();
            _articleMessageQueryService = base.Get<IArticleMessageQueryService>();
            _articleMessageDbProxyRepository = base.Get<IDbProxyRepository<ArticleMessage>>();
            _articleDbProxyRepository = base.Get<IArticleQueryService>();
        }

        [TestMethod()]
         public void FindAllPagerTest()
         {
               var list =
                   _articleDbProxyRepository.FindAllPager(null, null, null, null, null, null, 1, int.MaxValue);
               Console.WriteLine(list.TotalCount); 
        }

        [TestMethod()]
        public void DeleteArticleMessageTest()
        {
            _processor.Process<DeleteArticleMessageCommand>(new DeleteArticleMessageCommand(1000, 666));

        }



       [TestMethod()]
        public void CreateArticle()
        {
            const int userId = 1005;
            using (var transaction = UnitOfWork.Begin())
            {
                var articleID = _processor.Process<CreateArticleCommand, int>(new CreateArticleCommand(
                                                                                  "ArticleTitle Test!",
                                                                                  userId,
                                                                                  "ArticleContent Test!"));

                 _processor.Process<CreateArticleMessageCommand>(
                    new CreateArticleMessageCommand(
                        articleID,
                        userId,
                        "Good!"));

              
                transaction.Commit();
            } 
        }

        [TestMethod()]
        public void CreateArticleMessage()
        {
            using (var transaction = UnitOfWork.Begin())
            {
                _processor.Process<CreateArticleMessageReplyCommand>(
                    new CreateArticleMessageReplyCommand(
                        1002, 
                        1005,
                        "Very Good!"));
                transaction.Commit();
            }

        }

        [TestMethod()]
        public void FindAllByArticleId()
         {

             var query = _articleMessageQueryService.FindAllByArticleId(1001);

             Console.WriteLine(query.Count);

            //var quer = _articleMessageDbProxyRepository.FindAll().Where(x => x.Article.Id == 1001).ToList();
            //foreach (var articleMessage in quer)
            //{
            //    Console.WriteLine(articleMessage.Content);
            //    foreach (var message in articleMessage.ArticleMessages)
            //    {
            //        Console.WriteLine(message.Content);
                   
            //    }
            //}
        }


    }
}
