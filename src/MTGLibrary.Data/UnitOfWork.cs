using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MTGLibrary.Data.Repositories;

namespace MTGLibrary.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private ISetRepository _setRepository;
        private ICardRepository _cardRepository;
        private IRarityRepository _rarityRepository;
        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public ISetRepository SetRepository
        {
            get
            {
                return _setRepository ?? (_setRepository = new SetRepository(_transaction));
            }
        }

        public ICardRepository CardRepository
        {
            get
            {
                return _cardRepository ?? (_cardRepository = new CardRepository(_transaction));
            }
        }

        public IRarityRepository RarityRepository
        {
            get
            {
                return _rarityRepository ?? (_rarityRepository = new RarityRepository(_transaction));
            }
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _setRepository = null;
            _cardRepository = null;
            _rarityRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
