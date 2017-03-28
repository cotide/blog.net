#region using

using Microsoft.Practices.ServiceLocation;

#endregion

namespace Cotide.Framework.UnitOfWork
{
    ///<summary>
    /// 事务类(主要用于提交action里的事务,如果action没有[Transaction]标记)<br/>
    /// 使用例如:  using (var unitOfWork = UnitOfWork.Begin()){ 操作}
    ///</summary>
    public static class UnitOfWork
    {
        

        ///<summary>
        /// 开始事务
        ///</summary> 
        ///<returns>事务接口</returns>
        public static IUnitOfWork Begin()
        {
            var uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();
            uow.Init();
            return uow;
        }
    }
}