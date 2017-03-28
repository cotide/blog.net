using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Cotide.Domain.Contracts.Repositories.Extension;
using NHibernate;
using NHibernate.Linq;
using SharpArch.NHibernate;
using Cotide.Framework.Logging;

namespace Cotide.Infrastructure.Repositories.Extension
{
    public class DbProxyRepositoryWithTypedId<T, TId> :
        NHibernateRepositoryWithTypedId<T, TId>,
        IDbProxyRepositoryWithTypedId<T, TId> where T : class
    {
        public IQueryable<T> FindAll()
        {
            return Session.Query<T>();
        }

        public IQueryOver<T, T> FindAllOver()
        {
            return Session.QueryOver<T>();
        }


        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <param name="spname">存储过程名</param>
        /// <param name="paras">存储过程参数</param>
        /// <returns>返回结果集</returns>
        public   bool ExecuteProcedure(string spname, IDictionary<string, object> paras)
        {
            var dicOutPar = new Dictionary<string, object>();
            return ExecuteProcedure(spname, paras, ref dicOutPar);
        }


        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <param name="spname">存储过程名</param>
        /// <param name="paras">存储过程参数</param>
        /// <param name="dicOutPar">带输出参数的值</param>
        /// <returns>返回结果集</returns>
        public   bool ExecuteProcedure(
            string spname, IDictionary<string, object> paras, 
            ref Dictionary<string, object> dicOutPar)
        {

            IDbCommand cmd = Session.Connection.CreateCommand(); 
            cmd.CommandText = spname;
            cmd.CommandType = CommandType.StoredProcedure;
           // IDbTransaction myTrans = Session.Connection.BeginTransaction();

           // cmd.Transaction = myTrans;
            if (paras != null)
            {
                foreach (string key in paras.Keys.Distinct())
                {
                    #region 添加参数 
                    IDbDataParameter pra = cmd.CreateParameter();
                    pra.ParameterName = key;
                    pra.Value = paras[key];  
                    #endregion

                    cmd.Parameters.Add(pra);
                }
            }
            try
            {
                if (Session.Connection.State == ConnectionState.Closed)
                {
                    Session.Connection.Open();
                }
                //执行存储过程 
                cmd.ExecuteNonQuery();
              //  myTrans.Commit();

                #region 获取参数返回的值

                foreach (IDbDataParameter dp in cmd.Parameters)
                {
                    if (dp.Direction == ParameterDirection.Output || dp.Direction == ParameterDirection.InputOutput ||
                        dp.Direction == ParameterDirection.ReturnValue)
                    {
                        dicOutPar.Add(dp.ParameterName, dp.Value);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                Logger.Error("调用存储过程错误", ex.Message);
               // myTrans.Rollback();
            }
            finally
            {
                Session.Connection.Close();
            }
            return true;
        }
    }
}
