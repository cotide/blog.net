using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;

namespace Cotide.Domain.Contracts.QueryServices
{
    public interface IProjectQueryService
    {
        /// <summary>
        /// 获取项目
        /// </summary>
        /// <param name="productId">项目ID</param>
        /// <returns></returns>
        ProjectDto FindOne(int productId);

        /// <summary>
        /// 获取用户项目列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="isShow">前端是否显示</param>
        /// <param name="productTypeId">项目类型Id</param>
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PagedList<ProjectDto> FindAllPager(
            int? userId,
            bool? isShow, 
            int? productTypeId,
            int pageIndex, 
            int pageSize);
    }
}
