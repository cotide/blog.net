using NHibernate;
using SharpArch.NHibernate;

namespace Cotide.Framework.UnitOfWork
{
    public class EnsNhUnitOfWork : DisposableResource, IUnitOfWork
    {
        private static object LockSessionFactoryKeyManger = new object();

        private bool _isDisposed;
        private bool _isCommited = false;
         

        #region IUnitOfWork Members

        public void Commit()
        {
            _isCommited = true;
            ITransaction currentTransaction = NHibernateSession.Current.Transaction;
            if (currentTransaction.IsActive)
            {
                try
                {
                    //forces a flush of the current unit of work
                    currentTransaction.Commit();
                }
                catch
                {
                    currentTransaction.Rollback();
                    throw;
                }
                finally
                {
                    currentTransaction.Dispose();
                }
            }
        }

        public void Init()
        { 
            NHibernateSession.Current.BeginTransaction();
             
               
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
            }

            if(!_isCommited)
            {
                var currentTransaction = NHibernateSession.Current.Transaction;
                currentTransaction.Rollback();
            }
            base.Dispose(disposing);
        }
    }
}