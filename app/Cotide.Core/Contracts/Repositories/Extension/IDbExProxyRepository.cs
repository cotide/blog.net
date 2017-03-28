using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate.Contracts.Repositories;

namespace Cotide.Domain.Contracts.Repositories.Extension
{
    public interface IDbExProxyRepository<T> : IDbExProxyRepositoryWithTypedId<T, int>, INHibernateRepository<T> where T : class 
    {
    }
}
