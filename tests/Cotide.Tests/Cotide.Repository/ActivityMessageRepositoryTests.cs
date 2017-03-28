//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ActivityMessageRepositoryTests.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/16 21:49:49 
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
using Cotide.Domain.Contracts.Repositories;
using NUnit.Framework;
using Tests.Base;

namespace Tests.Cotide.Repository
{
    public class ActivityMessageRepositoryTests : DatabaseRepositoryTestsBase
    {
        private IArticleMessageRepository _articleMessageRepository;
        private IArticleMessageQueryService _articleMessageQueryService;


        public ActivityMessageRepositoryTests()
            : base(true)
        {

        }

        [SetUp]
        public void Init()
        {

            _articleMessageRepository = base.Get<IArticleMessageRepository>();
            _articleMessageQueryService = base.Get<IArticleMessageQueryService>();
        }


        [Test]
        public void GetTest()
        {
            var domain = _articleMessageRepository.Get(1048);
            //var list = domain.ArticleReplyMessages;
            //Console.WriteLine(list.Count);
        }

        [Test]
        public void FindAllByArticleId()
        {
            var list = _articleMessageQueryService.FindAllByArticleId(1040);
            Console.WriteLine(list.Count);
        }
    }
}
