using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cotide.Domain.Contracts.Repositories.Extension;

namespace Cotide.Infrastructure.Repositories.Extension
{
    public class DbExProxyRepository<T> :DbExProxyRepositoryWithTypedId<T,int>, IDbExProxyRepository<T> where T:class 
    {
    }
}
