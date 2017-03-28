using System;
using System.Collections.Generic;
using NHibernate;
using SharpArch.NHibernate;

namespace Cotide.Framework.UnitOfWork
{
    /// <summary>
    /// Win服务 Session 存储类
    /// </summary>
    public class WinServiceSessionStorage : ISessionStorage
    { 
        /// <summary>
        /// Session存储列表
        /// </summary>
        [ThreadStatic]
        private static   Dictionary<string, ISession> _storage;

        /// <summary>
        ///     Returns all the values of the internal dictionary of sessions.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ISession> GetAllSessions()
        {
            if (null != _storage)
            {
                return _storage.Values;
            }
            return new List<ISession>();
        }

        /// <summary>
        ///     Returns the session associated with the specified factoryKey or
        ///     null if the specified factoryKey is not found.
        /// </summary>
        /// <param name = "factoryKey"></param>
        /// <returns></returns>
        public ISession GetSessionForKey(string factoryKey)
        {
            ISession session = null;
            if (null != _storage)
            {
                if (!_storage.TryGetValue(factoryKey, out session))
                {
                    return null;
                }
            } 
            return session;
        } 

        /// <summary>
        ///     Stores the session into a dictionary using the specified factoryKey.
        ///     If a session already exists by the specified factoryKey, 
        ///     it gets overwritten by the new session passed in.
        /// </summary>
        /// <param name = "factoryKey"></param>
        /// <param name = "session"></param>
        public void SetSessionForKey(string factoryKey, ISession session)
        {
            if (null == _storage)
            {
                _storage = new Dictionary<string, ISession>();
            }
            _storage[factoryKey] = session; 
        }
    }
}