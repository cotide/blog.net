//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ProductAttrXmlTest.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/11/14 17:16:28 
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
using Cotide.Domain.Contracts.Task;
using Cotide.Domain.Enum; 
using Cotide.Framework.Utility;
using NUnit.Framework;

namespace Tests.Cotide.Utility
{
    public class ProductAttrXmlTest : ServiceLocatorInitiBase
    {
        /*
        [Test]
        public void ProductAttrXmlSerializableTest()
        {
            var obj = new ProductAttrXml();
            obj.ProductAttrItems = new List<ProductAttrItmXml>();
            var itemList = new ProductAttrItmXml()
                               {
                                   AttrType = ProductAttrType.Image,
                                   DisplayName = "颜色",
                                   FieldName = "Color",
                                   Sort = 0
                               };

            itemList.ProductAttrItmValue = new List<ProductAttrItmValueXml>();
            itemList.ProductAttrItmValue.Add(new ProductAttrItmValueXml()
                                                 {
                                                     Key = Guid.NewGuid().ToString(),
                                                     Img = "",
                                                     Sort = 0,
                                                     Value = "蓝色"
                                                 });
            itemList.ProductAttrItmValue.Add(new ProductAttrItmValueXml()
                                                {
                                                    Key = Guid.NewGuid().ToString(),
                                                    Img = "",
                                                    Sort = 0,
                                                    Value = "红色"
                                                });

            itemList.ProductAttrItmValue.Add(new ProductAttrItmValueXml()
                                                {
                                                    Key = Guid.NewGuid().ToString(),
                                                    Img = "",
                                                    Sort = 0,
                                                    Value = "绿色"
                                                });

            obj.ProductAttrItems.Add(itemList);
            var result = Serializer.ObjectToXML(obj);
            Console.WriteLine(result);
        }
        */
        [Test]
        public void CreateTicketTest()
        {
            var adminTicket = CreateTicket("1", UserLoginRole.Admin);
            var userTicket = CreateTicket("1", UserLoginRole.User);
            Console.WriteLine(adminTicket);
            Console.WriteLine(userTicket);
        }


        /// <summary>
        /// 生成包含用户角色的验证票据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        private string CreateTicket(
            string key,
            UserLoginRole userRole)
        {
            var ticket = key + "|" + userRole;
            return ticket;
        }

    }
}
