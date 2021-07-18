using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Data.Context
{
    public interface IMySQLContext : IDisposable
    {
        MySqlConnection Connection { get; }
        MySqlTransaction Transaction { get; }
        void BeginTransaction(IsolationLevel? isolationLevel = null);
        void Commit(bool newTransaction = false);
        void Rollback(bool newTransaction = false);
    }
}
