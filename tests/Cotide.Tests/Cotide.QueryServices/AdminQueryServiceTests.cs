//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：AdminQueryServiceTests.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/10/6 13:51:58 
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
using NUnit.Framework;
using Tests.Base;

namespace Tests.Cotide.QueryServices
{ 
    public class AdminQueryServiceTests : DatabaseRepositoryTestsBase
    {
        [Test]
        public void TTT()
        {
            var query = this.Get<IArticleMessageQueryService>();

           var list =   query.FindTop(1000, 10);
            foreach (var articleMessageDto in list)
            {
                Console.WriteLine(articleMessageDto.UserArticleMessageDto.NickName);
            }
        }
      
    }
}
