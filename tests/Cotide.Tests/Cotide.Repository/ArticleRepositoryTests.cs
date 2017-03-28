using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Base;

namespace Tests.Cotide.Repository
{
    [TestClass()]
    public class ArticleRepositoryTests : DatabaseRepositoryTestsBase
    {

        private IArticleRepository _articleRepository;

          [TestInitialize()]
        public void Init()
        {

            _articleRepository = base.Get<IArticleRepository>(); 
        }


        [TestMethod()]
        public void DeleteHouseBuilding()
        {
            var domain = _articleRepository.Load(70);
            _articleRepository.Delete(domain);
        }
    }
}
