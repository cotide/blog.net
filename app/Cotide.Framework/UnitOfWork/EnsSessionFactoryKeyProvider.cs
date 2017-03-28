using System.Threading;
using SharpArch.NHibernate;

namespace Cotide.Framework.UnitOfWork
{
    /// <summary>
    /// NHibernateSession≥ı ºªØ
    /// </summary>
    public class EnsSessionFactoryKeyProvider : ISessionFactoryKeyProvider
    {
        public string GetKey()
        { 
            if (SessionFactoryKeyManger.Instance().ContainsKey(Thread.CurrentThread.ManagedThreadId))
            {
                return SessionFactoryKeyManger.Instance()[Thread.CurrentThread.ManagedThreadId];
            }
            return NHibernateSession.DefaultFactoryKey;
        }

        public string GetKeyFrom(object anObject)
        { 
            if (SessionFactoryKeyManger.Instance().ContainsKey(Thread.CurrentThread.ManagedThreadId))
            {
                return SessionFactoryKeyManger.Instance()[Thread.CurrentThread.ManagedThreadId];
            }
            return SessionFactoryAttribute.GetKeyFrom(anObject);
        }
    }
}