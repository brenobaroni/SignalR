using Dapper;
using Data.Context;
using Data.Repository.Base;
using LinkGamer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repository
{
    public partial class DBRepository<T> where T : BaseEntitie
    {
        private string _connectionString { get; set; }
        private int? _userId { get; set; }


        /// <summary>
        /// Determina se o repositório irá recuperar sempre o objeto conforme o método [ConfigurarBuscaObjetoCompleto]
        /// </summary>
        SpecificRepositoryBase<T> _repositorySpecific = null;
        public DBRepository(string connectionString, int? userId)
        {
            _connectionString = connectionString;
            _userId = userId;


            try
            {
                // Tenta criar o repositório específico (caso exista)
                string objectToInstantiate = $"Data.{typeof(T).Name}Repository, Data";
                var objectType = Type.GetType(objectToInstantiate);
                _repositorySpecific = Activator.CreateInstance(objectType, _connectionString, _userId) as SpecificRepositoryBase<T>;
            }
            catch { }
        }

        public IMySQLContext NewConnection()
        {
            return DBRepository.ReturnMySqlContext(_connectionString);
        }

        public IMySQLContext NewConnection(string connectionString)
        {
            _connectionString = connectionString;
            return DBRepository.ReturnMySqlContext(_connectionString);
        }

        protected virtual IEnumerable<T> ConfigurarBuscaObjetoCompleto(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            throw new PlatformNotSupportedException("Não possui o método RecuperarCompleto nesse repositório.");
        }

        public IEnumerable<T> ExecuteProcedure(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null)
        {
            if (dbContext == null)
            {
                using (var context = DBRepository.ReturnMySqlContext(_connectionString))
                {
                    return context.Connection.Query<T>(storedProcedure, objParameters, null, true, timeout, CommandType.StoredProcedure);
                }
            }
            else
                return dbContext.Connection.Query<T>(storedProcedure, objParameters, dbContext.Transaction, true, timeout, CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<T>> ExecuteProcedureAsync(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null)
        {
            if (dbContext == null)
            {
                using (var context = DBRepository.ReturnMySqlContext(_connectionString))
                {
                    return await context.Connection.QueryAsync<T>(storedProcedure, objParameters, null, timeout, CommandType.StoredProcedure);
                }
            }
            else
                return await dbContext.Connection.QueryAsync<T>(storedProcedure, objParameters, dbContext.Transaction, timeout, CommandType.StoredProcedure);
        }



        public T ExecuteProcedureFirst(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true)
        {
            if (dbContext == null)
            {
                using (var context = DBRepository.ReturnMySqlContext(_connectionString))
                {
                    return context.Connection.QueryFirstOrDefault<T>(storedProcedure, objParameters, null, timeout, CommandType.StoredProcedure);
                }
            }
            else
                return dbContext.Connection.QueryFirstOrDefault<T>(storedProcedure, objParameters, dbContext.Transaction, timeout, CommandType.StoredProcedure);
        }

        public async Task<T> ExecuteProcedureFirstAsync(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true)
        {
            if (dbContext == null)
            {
                using (var context = DBRepository.ReturnMySqlContext(_connectionString))
                {
                    return await context.Connection.QueryFirstOrDefaultAsync<T>(storedProcedure, objParameters, null, timeout, CommandType.StoredProcedure);
                }
            }
            else
                return await dbContext.Connection.QueryFirstOrDefaultAsync<T>(storedProcedure, objParameters, dbContext.Transaction, timeout, CommandType.StoredProcedure);
        }



        public SqlMapper.GridReader ExecuteProcedureMultiple(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true)
        {
            if (dbContext == null)
            {
                using (var context = DBRepository.ReturnMySqlContext(_connectionString))
                {
                    return context.Connection.QueryMultiple(storedProcedure, objParameters, null, timeout, CommandType.StoredProcedure);
                }
            }
            else
                return dbContext.Connection.QueryMultiple(storedProcedure, objParameters, dbContext.Transaction, timeout, CommandType.StoredProcedure);
        }

        public async Task<SqlMapper.GridReader> ExecuteProcedureMultipleAsync(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true)
        {
            if (dbContext == null)
            {
                using (var context = DBRepository.ReturnMySqlContext(_connectionString))
                {
                    return await context.Connection.QueryMultipleAsync(storedProcedure, objParameters, null, timeout, CommandType.StoredProcedure);
                }
            }
            else
                return await dbContext.Connection.QueryMultipleAsync(storedProcedure, objParameters, dbContext.Transaction, timeout, CommandType.StoredProcedure);
        }

    }
}
