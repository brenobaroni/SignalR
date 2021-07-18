using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Data.Context
{
    public class MySqlContext : IMySQLContext
    {

        private MySqlConnection _connection { get; set; }
        public MySqlConnection Connection => _connection;
        private MySqlTransaction _transaction { get; set; }
        public MySqlTransaction Transaction => _transaction;

        public MySqlContext(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);

            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }

        ~MySqlContext()
        {
            try
            {
                Dispose();
            }
            catch
            {}
        }


        public void BeginTransaction(IsolationLevel? isolationLevel = null)
        {
            if(_connection != null)
            {
                Rollback();
                if (_connection != null)
                    _transaction = _connection.BeginTransaction(isolationLevel ?? IsolationLevel.ReadCommitted);
            }
            else
            {
                throw new Exception("Conexão nula ao realizar o BeginTransaction");
            }
        }

        public void Commit(bool newTransaction = false)
        {
            if (ExisteTransacaoEmAberto())
            {
                _transaction.Commit();
                _transaction.Dispose();

                if (newTransaction)
                    BeginTransaction();
            }
        }

        public void Dispose()
        {
            Rollback();
            _connection.Dispose();
            _connection.Close();
        }

        public void Rollback(bool newTransaction = false)
        {
            if (ExisteTransacaoEmAberto())
            {
                _transaction.Rollback();
                _transaction.Dispose();

                if (newTransaction)
                    BeginTransaction();
            }
        }

        private bool ExisteTransacaoEmAberto()
        {
            if (_transaction != null && _transaction.Connection != null)
                return true;

            return false;
        }

    }
}
