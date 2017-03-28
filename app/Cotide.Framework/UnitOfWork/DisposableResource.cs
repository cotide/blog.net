#region Using

using System;

#endregion

namespace Cotide.Framework.UnitOfWork
{
    ///<summary>
    ///</summary>
    public abstract class DisposableResource : IDisposable
    {
        #region IDisposable Members

        ///<summary>
        ///</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        ~DisposableResource()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}