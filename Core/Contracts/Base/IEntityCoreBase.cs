using Core.Server;
using Dapper;
using Data.Context;
using LinkGamer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core
{
    public interface IEntityCoreBase<T> where T : BaseEntitie
    {
        void CreateConnection(ServerContainer serverContainer);

        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(IMySQLContext dbContext);
        T GetById(int id);
        T GetById(int id, IMySQLContext dbContext);

        long Count(Expression<Func<T, bool>> linqExpression);
        long Count(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext);

        IEnumerable<T> Select(Expression<Func<T, bool>> linqExpression);
        IEnumerable<T> Select(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext);
        T SelectFirst(Expression<Func<T, bool>> linqExpression);
        T SelectFirst(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext);
        T SelectFirst<TKey>(Expression<Func<T, bool>> linqExpression, bool ascOrder, Func<T, TKey> orderExpression);
        T SelectFirst<TKey>(Expression<Func<T, bool>> linqExpression, bool ascOrder, Func<T, TKey> orderExpression, IMySQLContext dbContext);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(IMySQLContext dbContext);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, IMySQLContext dbContext);
        Task<IEnumerable<T>> SelectAsync(Expression<Func<T, bool>> linqExpression);
        Task<IEnumerable<T>> SelectAsync(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext);
        Task<T> SelectFirstAsync(Expression<Func<T, bool>> linqExpression);
        Task<T> SelectFirstAsync(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext);
        Task<T> SelectFirstAsync<TKey>(Expression<Func<T, bool>> linqExpression, bool ascOrder, Func<T, TKey> orderExpression);
        Task<T> SelectFirstAsync<TKey>(Expression<Func<T, bool>> linqExpression, bool ascOrder, Func<T, TKey> orderExpression, IMySQLContext dbContext);


        T Insert(T entity);
        T Insert(T entity, IMySQLContext dbContext);
        IEnumerable<T> Insert(IEnumerable<T> entities);
        IEnumerable<T> Insert(IEnumerable<T> entities, IMySQLContext dbContext);
        void ParallelInsert(IEnumerable<T> entities);
        void ParallelInsert(IEnumerable<T> entities, IMySQLContext dbContext);
        Task<object> InsertAsync(T entity);
        Task<object> InsertAsync(T entity, IMySQLContext dbContext);


        bool Update(T entity);
        bool Update(T entity, IMySQLContext dbContext);
        bool Update(IEnumerable<T> entities);
        bool Update(IEnumerable<T> entities, IMySQLContext dbContext);
        Task<bool> UpdateAsync(T entity);
        Task<bool> UpdateAsync(T entity, IMySQLContext dbContext);
        Task<bool> UpdateAsync(IEnumerable<T> entities);
        Task<bool> UpdateAsync(IEnumerable<T> entities, IMySQLContext dbContext);
        Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities, IMySQLContext dbContext);


        bool Delete(int id);
        bool Delete(int id, IMySQLContext dbContext);
        bool Delete(T entity);
        bool Delete(T entity, IMySQLContext dbContext);
        bool Delete(IEnumerable<T> entities);
        bool Delete(IEnumerable<T> entities, IMySQLContext dbContext);
        bool Delete(Expression<Func<T, bool>> linqExpression);
        bool Delete(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(int id, IMySQLContext dbContext);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteAsync(T entity, IMySQLContext dbContext);
        Task<bool> DeleteAsync(IEnumerable<T> entities);
        Task<bool> DeleteAsync(IEnumerable<T> entities, IMySQLContext dbContext);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> linqExpression);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext);


        bool DeletePermanent(int id);
        bool DeletePermanent(int id, IMySQLContext dbContext);
        bool DeletePermanent(T entity);
        bool DeletePermanent(T entity, IMySQLContext dbContext);
        bool DeletePermanent(IEnumerable<T> entities);
        bool DeletePermanent(IEnumerable<T> entities, IMySQLContext dbContext);
        bool DeletePermanent(Expression<Func<T, bool>> linqExpression);
        bool DeletePermanent(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext);
        Task<bool> DeletePermanentAsync(int id);
        Task<bool> DeletePermanentAsync(int id, IMySQLContext dbContext);
        Task<bool> DeletePermanentAsync(T entity);
        Task<bool> DeletePermanentAsync(T entity, IMySQLContext dbContext);
        Task<bool> DeletePermanentAsync(IEnumerable<T> entities);
        Task<bool> DeletePermanentAsync(IEnumerable<T> entities, IMySQLContext dbContext);
        Task<bool> DeletePermanentAsync(Expression<Func<T, bool>> linqExpression);
        Task<bool> DeletePermanentAsync(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext);


        IEnumerable<T> ExecuteProcedure(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null);
        Task<IEnumerable<T>> ExecuteProcedureAsync(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null);
        T ExecuteProcedureFirst(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true);
        Task<T> ExecuteProcedureFirstAsync(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true);
        SqlMapper.GridReader ExecuteProcedureMultiple(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true);
        Task<SqlMapper.GridReader> ExecuteProcedureMultipleAsync(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true);
    }
}
