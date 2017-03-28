using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.Repositories.Extension;
using NHibernate.Linq; 

namespace Cotide.Infrastructure.Repositories.Extension
{

    public class DbExProxyRepositoryWithTypedId<T, TId> : DbProxyRepositoryWithTypedId<T, TId>, IDbExProxyRepositoryWithTypedId<T, TId> where T : class
    {
        #region Implementation of IDbExProxyRepositoryWithTypedId<T,TId>

        /// <summary>
        /// 获取子类 实体对象
        /// </summary>
        /// <typeparam name="TSub"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public TSub Get<TSub>(TId id) where TSub : T
        {
            return Session.Get<TSub>(id);
        }

        /// <summary>
        /// 获取所有子类实体列表查询列表
        /// </summary>
        /// <typeparam name="TSub">子类 实体类型</typeparam>
        /// <returns></returns>
        public IQueryable<TSub> FindAll<TSub>()
        {
            return Session.Query<TSub>();
        }

        /// <summary>
        /// 获取子类 实体对象(非立即加载)
        /// </summary>
        /// <typeparam name="TSub"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public TSub Load<TSub>(TId id) where TSub : T
        {
            return Session.Load<TSub>(id);
        }

        #endregion
    }
}
