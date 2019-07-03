using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Dapper.Contrib.Extensions;

namespace EstheticsPro.Core.ADO
{
    public class UnitOfWork : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;
        private SqlTransaction _transaction;

        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection GetConnection
            => _connection ?? (_connection = new SqlConnection(_connectionString));


        public void BeginTransaction()
        {
            if (_transaction != null)
            {
                return;
            }

            _transaction = GetConnection
                .BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_transaction == null)
            {
                return;
            }

            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            if (_transaction == null)
            {
                return;
            }

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }

        public List<T> Query<T>(StringBuilder query, object paramters = null)
            => Query<T>(query.ToString(), paramters);
        public List<T> Query<T>(string query, object paramters = null)
        {
            var result = ExecuteOpenCloseConnection(connection => connection.Query<dynamic>(query, paramters, _transaction));
            return Slapper.AutoMapper.MapDynamic<T>(result).ToList();
        }
        public int Execute(StringBuilder command, object parameters = null)
            => Execute(command.ToString(), parameters);
        public int Execute(string command, object parameters = null)
        {
            return ExecuteOpenCloseConnection(connection => connection.Execute(command, parameters, _transaction));
        }

        public T Get<T, K>(K id)
        where T : class
        {
            return ExecuteOpenCloseConnection(connection => connection.Get<T>(id, _transaction));
        }
        public List<T> GetAll<T>()
        where T : class
        {
            return ExecuteOpenCloseConnection(connection => connection.GetAll<T>(_transaction).ToList());
        }
        public long Insert<T>(T entity)
            where T : class
        {
            return ExecuteOpenCloseConnection(connection => connection.Insert(entity, _transaction));
        }
        public long Insert<T>(IEnumerable<T> entities)
        where T : class
        {
            return ExecuteOpenCloseConnection(connection => connection.Insert(entities, _transaction));
        }
        public bool Update<T>(T entity)
            where T : class
        {
            return ExecuteOpenCloseConnection(connection => connection.Update(entity, _transaction));
        }
        public bool Update<T>(IEnumerable<T> entities)
            where T : class
        {
            return ExecuteOpenCloseConnection(connection => connection.Update(entities, _transaction));
        }
        public bool Delete<T>(T entity)
        where T : class
        {
            return ExecuteOpenCloseConnection(connection => connection.Delete(entity, _transaction));
        }
        public bool Delete<T>(IEnumerable<T> entities)
            where T : class
        {
            return ExecuteOpenCloseConnection(connection => connection.Delete(entities, _transaction));
        }
        public bool DeleteAll<T>()
            where T : class
        {
            return ExecuteOpenCloseConnection(connection => connection.DeleteAll<T>(_transaction));
        }

        private T ExecuteOpenCloseConnection<T>(Func<SqlConnection, T> command)
        {
            try
            {
                GetConnection.Open();
                return command(GetConnection);
            }
            finally
            {
                if (_transaction == null)
                {
                    GetConnection.Close();
                }
            }
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _transaction?.Dispose();
        }
    }
}
