using Dapper;
using Data.Context;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public partial class DBRepository
    {
        internal static IMySQLContext ReturnMySqlContext(string connectionString)
        {
            return new MySqlContext(connectionString);
        }

        public static IMySQLContext NewConnection(string connectionString)
        {
            return ReturnMySqlContext(connectionString);
        }



        public static IEnumerable<dynamic> ExecuteProcedure(string connectionString, string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null)
        {
            if (dbContext == null)
            {
                using (var context = ReturnMySqlContext(connectionString))
                {
                    return context.Connection.Query(storedProcedure, objParameters, null, true, timeout, CommandType.StoredProcedure);
                }
            }
            else
                return dbContext.Connection.Query(storedProcedure, objParameters, dbContext.Transaction, true, timeout, CommandType.StoredProcedure);
        }

        public static IEnumerable<TEntity> ExecuteProcedure<TEntity>(string connectionString, string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null)
        {
            if (dbContext == null)
            {
                using (var context = ReturnMySqlContext(connectionString))
                {
                    return context.Connection.Query<TEntity>(storedProcedure, objParameters, null, true, timeout, CommandType.StoredProcedure);
                }
            }
            else
                return dbContext.Connection.Query<TEntity>(storedProcedure, objParameters, dbContext.Transaction, true, timeout, CommandType.StoredProcedure);
        }

        public static async Task<IEnumerable<dynamic>> ExecuteProcedureAsync(string connectionString, string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null)
        {
            if (dbContext == null)
            {
                using (var context = ReturnMySqlContext(connectionString))
                {
                    return await context.Connection.QueryAsync(storedProcedure, objParameters, null, timeout, CommandType.StoredProcedure);
                }
            }
            else
                return await dbContext.Connection.QueryAsync(storedProcedure, objParameters, dbContext.Transaction, timeout, CommandType.StoredProcedure);
        }

        public static async Task<IEnumerable<TEntity>> ExecuteProcedureAsync<TEntity>(string connectionString, string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null)
        {
            if (dbContext == null)
            {
                using (var context = ReturnMySqlContext(connectionString))
                {
                    return await context.Connection.QueryAsync<TEntity>(storedProcedure, objParameters, null, timeout, CommandType.StoredProcedure);
                }
            }
            else
                return await dbContext.Connection.QueryAsync<TEntity>(storedProcedure, objParameters, dbContext.Transaction, timeout, CommandType.StoredProcedure);
        }



        public static dynamic ExecuteProcedureFirst(string connectionString, string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true)
        {
            if (dbContext == null)
            {
                using (var context = ReturnMySqlContext(connectionString))
                {
                    return context.Connection.QueryFirstOrDefault(storedProcedure, objParameters, null, timeout, CommandType.StoredProcedure);
                }
            }
            else
                return dbContext.Connection.QueryFirstOrDefault(storedProcedure, objParameters, dbContext.Transaction, timeout, CommandType.StoredProcedure);
        }

        public static TEntity ExecuteProcedureFirst<TEntity>(string connectionString, string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true)
        {
            if (dbContext == null)
            {
                using (var context = ReturnMySqlContext(connectionString))
                {
                    return context.Connection.QueryFirstOrDefault<TEntity>(storedProcedure, objParameters, null, timeout, CommandType.StoredProcedure);
                }
            }
            else
                return dbContext.Connection.QueryFirstOrDefault<TEntity>(storedProcedure, objParameters, dbContext.Transaction, timeout, CommandType.StoredProcedure);
        }

        public static async Task<dynamic> ExecuteProcedureFirstAsync(string connectionString, string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true)
        {
            if (dbContext == null)
            {
                using (var context = ReturnMySqlContext(connectionString))
                {
                    return await context.Connection.QueryFirstOrDefaultAsync(storedProcedure, objParameters, null, timeout, CommandType.StoredProcedure);
                }
            }
            else
                return await dbContext.Connection.QueryFirstOrDefaultAsync(storedProcedure, objParameters, dbContext.Transaction, timeout, CommandType.StoredProcedure);
        }

        public static async Task<TEntity> ExecuteProcedureFirstAsync<TEntity>(string connectionString, string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true)
        {
            if (dbContext == null)
            {
                using (var context = ReturnMySqlContext(connectionString))
                {
                    return await context.Connection.QueryFirstOrDefaultAsync<TEntity>(storedProcedure, objParameters, null, timeout, CommandType.StoredProcedure);
                }
            }
            else
                return await dbContext.Connection.QueryFirstOrDefaultAsync<TEntity>(storedProcedure, objParameters, dbContext.Transaction, timeout, CommandType.StoredProcedure);
        }



        public static IEnumerable<dynamic> ExecuteProcedureMultiple(string connectionString, string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true)
        {
            List<dynamic> selects = new List<dynamic>();
            if (dbContext == null)
            {
                using (var context = ReturnMySqlContext(connectionString))
                {
                    using (var ret = context.Connection.QueryMultiple(storedProcedure, objParameters, null, timeout, CommandType.StoredProcedure))
                    {
                        while (!ret.IsConsumed)
                        {
                            selects.Add(ret.Read());
                        }

                        return selects;
                    }
                }
            }
            else
            {
                using (var ret = dbContext.Connection.QueryMultiple(storedProcedure, objParameters, dbContext.Transaction, timeout, CommandType.StoredProcedure))
                {
                    while (!ret.IsConsumed)
                    {
                        selects.Add(ret.Read());
                    }

                    return selects;
                }
            }
        }


    }
}
