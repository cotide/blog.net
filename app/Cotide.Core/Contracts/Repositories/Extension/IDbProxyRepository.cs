 
using SharpArch.NHibernate.Contracts.Repositories;

namespace Cotide.Domain.Contracts.Repositories.Extension
{
    public interface IDbProxyRepository<T> : IDbProxyRepositoryWithTypedId<T, int>, INHibernateRepository<T> where T : class 
    {
    }
}
