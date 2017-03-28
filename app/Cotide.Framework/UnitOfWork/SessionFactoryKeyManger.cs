using System.Collections.Generic;

namespace Cotide.Framework.UnitOfWork
{
    public class SessionFactoryKeyManger:Dictionary<int,string>
    {
        private static SessionFactoryKeyManger instance;

        protected SessionFactoryKeyManger() { }
        private static readonly object syncLock = new object();

        public static SessionFactoryKeyManger Instance()
        {
            if (instance == null)
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = new SessionFactoryKeyManger();
                    }
                }
            }

            return instance;
        }



    }
}