using System;
using System.Data;
using NHibernate;
using com.devevil.DAL.Core.Transaction;

namespace com.devevil.DAL.Nhibernate.Base
{
    /// <summary>
    /// A simple Unit of Work wrapper. It is IDisposable. Call <see cref="Commit"/> or call <see cref="Rollback"/> to abort the transaction.
    /// </summary>
    public sealed class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ISession session;
        private ITransaction transaction;

        public UnitOfWork()
        {
            session = SessionManager.Instance.Session; //this may be an already open session...
            session.FlushMode = FlushMode.Auto; //default
            transaction = session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public UnitOfWork(IsolationLevel isolationLevel)
        {
            session = SessionManager.Instance.Session; //this may be an already open session...
            session.FlushMode = FlushMode.Auto; //default
            transaction = session.BeginTransaction(isolationLevel);
        }

        public ISession Current
        {
            get { return session; }
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit()
        {
            //becuase flushMode is auto, this will automatically commit when disposed
            if (!transaction.IsActive)
                throw new InvalidOperationException("No active transaction");
            transaction.Commit();
            //start a new transaction
            transaction = session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Rolls back this instance. You should probably close session.
        /// </summary>
        public void Rollback()
        {
            if (transaction.IsActive) transaction.Rollback();
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (session != null) session.Close();
        }

        #endregion
    }
}
