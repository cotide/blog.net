using System.Collections.Generic;
using System.Linq;
using Cotide.Domain;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories;
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;

namespace Cotide.QueryServices
{
    public class ProjectTypeQueryService : IProjectTypeQueryService
    {

        private readonly IProjectTypeRepository _projectTypeRepository;

        public ProjectTypeQueryService(IProjectTypeRepository projectTypeRepository)
        {
            _projectTypeRepository = projectTypeRepository;
        }

        #region Implementation of IProjectTypeQueryService

        /// <summary>
        /// 获取项目类型
        /// </summary>
        /// <param name="productTypeId">项目类型ID</param>
        /// <returns></returns>
        public ProjectTypeDto FindOne(int productTypeId)
        {
            var query = (from pt in _projectTypeRepository.FindAll()
                         where pt.Id == productTypeId
                         select CreateProjectTypeDto(pt));
            return query.SingleOrDefault();
        }

        /// <summary>
        /// 获取项目类型列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="isShow">前端是否显示</param>  
        /// <returns></returns>
        public IList<ProjectTypeDto> FindAll(int userId, bool? isShow)
        {
            if (isShow == null)
            {
                return (from pt in _projectTypeRepository.FindAll()
                        let u = pt.User
                        where u.Id == userId
                        orderby pt.Id descending
                        select CreateProjectTypeDto(pt))
                    .ToList();
            }
            if (isShow == true)
            {
                return (from pt in _projectTypeRepository.FindAll()
                        let u = pt.User
                        where pt.IsShow && u.Id == userId
                        select CreateProjectTypeDto(pt))
                    .ToList();
            }
            return (from pt in _projectTypeRepository.FindAll()
                    let u = pt.User
                    where !pt.IsShow && u.Id == userId
                    select CreateProjectTypeDto(pt))
                    .ToList();
        }

        /// <summary>
        /// 获取项目类型列表
        /// </summary>a
        /// <param name="userId">用户ID</param>
        /// <param name="isShow">前端是否显示</param> 
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public PagedList<ProjectTypeDto> FindAllPager(int userId, bool? isShow, int pageIndex, int pageSize)
        {
            if (isShow == null)
            {
                return (from pt in _projectTypeRepository.FindAll()
                        let u = pt.User
                        where u.Id == userId
                        orderby pt.Id descending
                        select CreateProjectTypeDto(pt))
                        .ToPagedList(pageIndex, pageSize);
            }
            if (isShow == true)
            {
                return (from pt in _projectTypeRepository.FindAll()
                        let u = pt.User
                        where pt.IsShow && u.Id == userId
                        select CreateProjectTypeDto(pt))
                        .ToPagedList(pageIndex, pageSize);
            }
            return (from pt in _projectTypeRepository.FindAll()
                        let u = pt.User
                        where !pt.IsShow && u.Id == userId
                        select CreateProjectTypeDto(pt))
                        .ToPagedList(pageIndex, pageSize);
        }

        #endregion

        #region Helper
        private ProjectTypeDto CreateProjectTypeDto(ProjectType projectType)
        {
            return new ProjectTypeDto()
                       {
                           Id = projectType.Id,
                           IsShow = projectType.IsShow,
                           TypeName = projectType.TypeName
                       };
        }

        #endregion
    }
}
