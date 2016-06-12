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
        private readonly ISession _session;
        private ITransaction _transaction;
        private IsolationLevel _isolationLevel;

        public UnitOfWork()
        {
            _session = SessionManager.Instance.Session; //this may be an already open session...
            _session.FlushMode = FlushMode.Auto; //default
            _isolationLevel = IsolationLevel.ReadCommitted;
            _transaction = _session.BeginTransaction(_isolationLevel);
        }

        public UnitOfWork(IsolationLevel isolationLevel)
        {
            _session = SessionManager.Instance.Session; //this may be an already open session...
            _session.FlushMode = FlushMode.Auto; //default
            _isolationLevel = isolationLevel;
            _transaction = _session.BeginTransaction(_isolationLevel);
        }

        public ISession Current
        {
            get { return _session; }
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit()
        {
            //becuase flushMode is auto, this will automatically commit when disposed
            if (!_transaction.IsActive)
                throw new InvalidOperationException("No active transaction");
            _transaction.Commit();
            //start a new transaction
            _transaction = _session.BeginTransaction(_isolationLevel);
        }

        /// <summary>
        /// Rolls back this instance. You should probably close session.
        /// </summary>
        public void Rollback()
        {
            if (_transaction.IsActive) _transaction.Rollback();
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_session != null) _session.Close();
        }

        #endregion
    }
}
