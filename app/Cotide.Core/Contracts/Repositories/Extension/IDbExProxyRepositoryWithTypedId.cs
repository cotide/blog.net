using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cotide.Domain.Contracts.Repositories.Extension
{
    /// <summary>
    /// 数据访问模块接口(针对基类领域模型扩展查询子类的数据接口定义)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IDbExProxyRepositoryWithTypedId<T, TId> : IDbProxyRepositoryWithTypedId<T, TId> where T : class
    {

        /// <summary>
        /// 获取子类 实体对象
        /// </summary>
        /// <typeparam name="TSub"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        TSub Get<TSub>(TId id) where TSub : T;

        /// <summary>
        /// 获取所有子类实体列表查询列表
        /// </summary>
        /// <typeparam name="TSub">子类 实体类型</typeparam>
        /// <returns></returns>
        IQueryable<TSub> FindAll<TSub>();

        /// <summary>
        /// 获取子类 实体对象(非立即加载)
        /// </summary>
        /// <typeparam name="TSub"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        TSub Load<TSub>(TId id) where TSub : T;

    }
}
