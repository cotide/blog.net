using System.Collections.Generic;
using System.Linq;
using NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace Cotide.Domain.Contracts.Repositories.Extension
{
    /// <summary>
    /// 数据访问模块接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IDbProxyRepositoryWithTypedId<T, TId> : INHibernateRepositoryWithTypedId<T, TId> where T : class
    {
        IQueryable<T> FindAll();
        IQueryOver<T, T> FindAllOver();

        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <param name="spname">存储过程名</param>
        /// <param name="paras">存储过程参数</param>
        /// <returns>返回结果集</returns>
        bool ExecuteProcedure(string spname, IDictionary<string, object> paras);

        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <param name="spname">存储过程名</param>
        /// <param name="paras">存储过程参数</param>
        /// <param name="dicOutPar">带输出参数的值</param>
        /// <returns>返回结果集</returns>
        bool ExecuteProcedure(
            string spname, IDictionary<string, object> paras,
            ref Dictionary<string, object> dicOutPar);
    }
}
