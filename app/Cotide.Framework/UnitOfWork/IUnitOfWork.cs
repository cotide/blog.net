#region using

using System;

#endregion

namespace Cotide.Framework.UnitOfWork
{
    ///<summary>
    /// 事务接口
    ///</summary>
    public interface IUnitOfWork : IDisposable
    {
        ///<summary>
        /// 初始化工厂key
        ///</summary>
        void Init();

        ///<summary>
        /// 提交方法
        ///</summary>
        void Commit();
    }
}