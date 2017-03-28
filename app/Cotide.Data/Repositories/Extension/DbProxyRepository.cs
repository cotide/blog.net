using Cotide.Domain.Contracts.Repositories.Extension; 

namespace Cotide.Infrastructure.Repositories.Extension
{
    public class DbProxyRepository<T> : DbProxyRepositoryWithTypedId<T, int>, IDbProxyRepository<T> where T : class 
    {
    }
}
