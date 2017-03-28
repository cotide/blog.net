//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：IShizongArticleQueryService.cs
//模块名称：
//模块编号：
//作　　者：lhc
//创建时间：2013/3/23 17:30:01 
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
    public interface IShizongArticleQueryService
    {
        /// <summary>
        /// 获取文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns></returns>
        ShizongArticleDto FindOne(int articleId);

        /// <summary>
        /// 获取用户文章列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="articleTypeId">文章类别ID</param> 
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="isShow">前端是否显示</param>
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PagedList<ShizongArticleDto> FindAllPager(
            int? userId,
            int? articleTypeId, 
            int? year,
            int? month,
            bool? isShow,
            int pageIndex,
            int pageSize);

    
        /// <summary>
        /// 获取指定条数文章列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="topIndex">显示条数</param>
        /// <returns></returns>
        IList<ShizongArticleDto> GetTopList(
            int userId,
            int topIndex);
    }
}
