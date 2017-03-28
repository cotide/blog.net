//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：ProductTypeQueryService.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2012/11/14 14:20:02 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Cotide.Domain;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories.Extension;
using Cotide.Domain.Dtos;
using Cotide.Framework.Enumerable;

namespace Cotide.QueryServices
{
    public class ProductTypeQueryService : IProductTypeQueryService
    {
        protected readonly IDbProxyRepository<ProductType> ProductTypeDbProxyRepository;

        public ProductTypeQueryService(IDbProxyRepository<ProductType> productTypeDbProxyRepository)
        {
            ProductTypeDbProxyRepository = productTypeDbProxyRepository;
        }

        /// <summary>
        /// 获取商品类型
        /// </summary> 
        /// <returns></returns>
        public DataTable GetAllToDataTable()
        {
            var query = ProductTypeDbProxyRepository.FindAll();
            return query.Select(CreateProductTypeDto).ToDataTable();
        }


        #region Helepr
        private ProductTypeDto CreateProductTypeDto(ProductType productType)
        {
            var dto = new ProductTypeDto()
                          {

                              Id = productType.Id,
                              IsShow = productType.IsShow,
                              Level = productType.Level,
                              Logo = productType.Logo,
                              MetaDescription = productType.MetaDescription,
                              MetaKeywords = productType.MetaKeywords,
                              Sort = productType.Sort,
                              TypeName = productType.TypeName,
                              Title = productType.Title,
                              UrlRouting = productType.UrlRouting
                          };
            if (productType.BaseProductType != null)
            {
                dto.BaseProductType = productType.BaseProductType.Id;
            }
            return dto;
        }

        #endregion
    }
}
