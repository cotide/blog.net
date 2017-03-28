using System.Linq;
using System.Web;
using Cotide.Domain;
using Cotide.Domain.Contracts.QueryServices;
using Cotide.Domain.Contracts.Repositories.Extension;
using Cotide.Domain.Dtos;
using Cotide.Framework.Collections;

namespace Cotide.QueryServices
{
    public class ProjectQueryService : IProjectQueryService
    {
        private readonly IDbProxyRepository<Project> _projectDbProxyRepository;
        private readonly IDbProxyRepository<User> _userDbProxyRepository;
        private readonly IDbProxyRepository<ProjectType> _projectTypeDbProxyRepository;

        #region ICO注入
        public ProjectQueryService(IDbProxyRepository<Project> projectDbProxyRepository, IDbProxyRepository<User> userDbProxyRepository, IDbProxyRepository<ProjectType> projectTypeDbProxyRepository)
        {
            _projectDbProxyRepository = projectDbProxyRepository;
            _userDbProxyRepository = userDbProxyRepository;
            _projectTypeDbProxyRepository = projectTypeDbProxyRepository;
        }
#endregion

        #region Implementation of IProjectQueryService

        /// <summary>
        /// 获取项目
        /// </summary>
        /// <param name="productId">项目ID</param>
        /// <returns></returns>
        public ProjectDto FindOne(int productId)
        {
            var query = (from p in _projectDbProxyRepository.FindAll()
                         let u = p.User
                         let pt = p.ProjectType
                         where p.Id == productId 
                         select CreateProjectDto(p, u, pt));
            return query.SingleOrDefault();
        }

        /// <summary>
        /// 获取用户项目列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="isShow">前端是否显示</param>
        /// <param name="productTypeId">项目类型ID</param>
        /// <param name="pageIndex">开始页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public PagedList<ProjectDto> FindAllPager(int? userId, bool? isShow, int? productTypeId, int pageIndex, int pageSize)
        { 
            // 构造查询关联对象
            var projects = _projectDbProxyRepository.FindAll();
            var users = _userDbProxyRepository.FindAll();
            var projectTyps = _projectTypeDbProxyRepository.FindAll(); 
            var query = projects.Join(users, x => x.User.Id, u => u.Id, (x, u) => x);
            query = query.Join(projectTyps, x => x.ProjectType.Id, t => t.Id, (x, t) => x);
            if(userId!=null)
            { 
                query = query.Where(x => x.User.Id == userId);
            }
            if(productTypeId!=null)
            {
                query = query.Where(x => x.ProjectType.Id == productTypeId);
            }
            if (isShow != null)
            {
                query = isShow == true ? query.Where(x => x.IsShow) : query.Where(x => !x.IsShow);
            }
            query = query.OrderByDescending(x => x.Sort);
            return query.Select(x => CreateProjectDto(x, x.User, x.ProjectType))
                   .ToPagedList(pageIndex, pageSize); 
        }

        #endregion

        #region Helper
        private ProjectDto CreateProjectDto(Project p, User u, ProjectType pt)
        {
            return new ProjectDto()
            {
                Content = HttpUtility.HtmlDecode(p.Content),
                CreateDate = p.CreateDate,
                Id = p.Id,
                Introduction = HttpUtility.HtmlDecode(p.Introduction),
                IsShow = p.IsShow,
                LastDateTime = p.LastDateTime,
                ProjectImg = p.ProjectImg,
                SmallProjectImg = p.SmallProjectImg,
                StandardProjectImg = p.SmallProjectImg,
                ProjectName = p.ProjectName,
                UserId = u.Id,
                ProductTypeId = pt.Id,
                ProductTypeName = pt.TypeName,
                WebSite = p.WebSite,
                Sort = p.Sort
            };
        }

        #endregion
    }
}
