using System.Collections.Generic;
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;

namespace Cotide.Domain.Contracts.QueryServices
{
    public interface IProjectTypeQueryService
    {
        /// <summary>
        /// 获取项目类型
        /// </summary>
        /// <param name="productTypeId">项目类型ID</param>
        /// <returns></returns>
        ProjectTypeDto FindOne(int productTypeId);
         
        /// <summary>
        /// 获取项目类型列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="isShow">前端是否显示</param>  
        /// <returns></returns>
        IList<ProjectTypeDto> FindAll(
            int userId,
            bool? isShow);

         /// <summary>
        /// 获取项目类型列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="isShow">前端是否显示</param> 
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PagedList<ProjectTypeDto> FindAllPager(
            int userId,
            bool? isShow, 
            int pageIndex,
            int pageSize);
    }
   
}
