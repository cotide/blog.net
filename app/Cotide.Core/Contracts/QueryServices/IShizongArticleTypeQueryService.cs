//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：IShizongArticleTypeQueryService.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/22 11:57:29 
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
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;

namespace Cotide.Domain.Contracts.QueryServices
{
    /// <summary>
    /// 失踪周末文章类型查询服务
    /// </summary>
    public interface IShizongArticleTypeQueryService
    {
        /// <summary>
        /// 获取文章类别分页列表
        /// </summary> 
        /// <param name="pageIndex">开始索引</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        PagedList<ShizongArticleTypeDto> FindAllPager( 
            int pageIndex,
            int pageSize);

        /// <summary>
        /// 获取文章分类列表
        /// </summary>
        /// <returns></returns>
        IList<ShizongArticleTypeDto> FindAll(bool? isShow=null);


        /// <summary>
        /// 获取文章类别
        /// </summary>
        /// <param name="shizongArticleTypeId">文章类别ID</param>
        /// <returns></returns>
        ShizongArticleTypeDto FindOne(int shizongArticleTypeId);

        /// <summary>
        /// 获取文章 默认第一条
        /// </summary>
        /// <returns></returns>
        ShizongArticleTypeDto FindOneForTop1();
    }
}
