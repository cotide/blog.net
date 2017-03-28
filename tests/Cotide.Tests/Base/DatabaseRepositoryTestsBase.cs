//-------------------------------------------------------------------
//版权所有：版权所有(C) 2010，Microsoft(China) Co.,LTD
//系统名称： 
//文件名称：DatabaseRepositoryTestsBase.cs
//模块名称：
//模块编号：
//作　　者：cotide
//创建时间：2012/10/6 11:43:05 
//功能说明：
//-----------------------------------------------------------------
//修改记录： 
//修改人：   
//修改时间： 
//修改内容： 
//-----------------------------------------------------------------  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SharpArch.Domain.Commands;
using SharpArch.NHibernate;
using Tests.SharpArch.NHibernate;

namespace Tests.Base
{
    public class DatabaseRepositoryTestsBase : ServiceLocatorInitiBase
    {
        private readonly bool _commit;

        public DatabaseRepositoryTestsBase()
            : this(false)
        {
           
        }

        public DatabaseRepositoryTestsBase(bool commit)
        {
            _commit = commit;
        }

        [TestInitialize()]
        public virtual void SetUp()
        {
            ServiceLocatorInitiBase.SetUp(); 
            RepositoryTestsHelper.InitializeNHibernateSession();
            if (_commit)
                NHibernateSession.Current.BeginTransaction();
        }

        [TestCleanup]
        public virtual void TearDown()
        {
            if (_commit)
                NHibernateSession.Current.Transaction.Commit();
            else if (NHibernateSession.Current.Transaction.IsActive)
                NHibernateSession.Current.Transaction.Rollback();
            RepositoryTestsHelper.Shutdown();
        }

    }
}
