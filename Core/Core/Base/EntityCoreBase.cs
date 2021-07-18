using Core.Server;
using Dapper;
using Data.Context;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinkGamer.Domain.Entities;

namespace Core.Core.Base
{
    public class EntityCoreBase<T> : IEntityCoreBase<T> where T : BaseEntitie
    {
        internal ServerContainer _Server { get; private set; }
        internal virtual DBRepository<T> _Repository { get; set; }

        public EntityCoreBase(){ }

        internal EntityCoreBase(ServerContainer serverContainer)
        {
            InitiateCoreProperties(serverContainer);
        }

        public void CreateConnection(ServerContainer serverContainer)
        {
            InitiateCoreProperties(serverContainer);
        }

        protected virtual void StartDependenciesConnections() { /* para Sobrecarga dos Core's */ }


        internal void Setauthorization(WebAuthorization authorization)
        {
            _Server.Authorization = authorization;
        }

        internal void SetConnectionString(string connectionString)
        {
            _Server.ConnectionString = connectionString;
        }

        private void InitiateCoreProperties(ServerContainer serverContainer)
        {
            _Server = serverContainer;

            _Repository = new DBRepository<T>(_Server.ConnectionString, _Server?.Authorization?.Usuario.Id);

            StartDependenciesConnections();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _Repository.GetAll();
        }

        public virtual IEnumerable<T> GetAll(IMySQLContext dbContext)
        {
            return _Repository.GetAll(dbContext);
        }

        public virtual T GetById(int id)
        {
            return _Repository.GetById(id);
        }

        public virtual T GetById(int id, IMySQLContext dbContext)
        {
            return _Repository.GetById(id, dbContext);
        }

        public virtual IEnumerable<T> Select(Expression<Func<T, bool>> linqExpression)
        {
            return _Repository.Select(linqExpression);
        }

        public virtual IEnumerable<T> Select(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            return _Repository.Select(linqExpression, dbContext);
        }

        public virtual T SelectFirst(Expression<Func<T, bool>> linqExpression)
        {
            return _Repository.SelectFirst(linqExpression);
        }

        public virtual T SelectFirst(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            return _Repository.SelectFirst(linqExpression, dbContext);
        }

        public virtual T SelectFirst<TKey>(Expression<Func<T, bool>> linqExpression, bool ascOrder, Func<T, TKey> orderExpression)
        {
            return _Repository.SelectFirst(linqExpression, ascOrder, orderExpression);
        }

        public virtual T SelectFirst<TKey>(Expression<Func<T, bool>> linqExpression, bool ascOrder, Func<T, TKey> orderExpression, IMySQLContext dbContext)
        {
            return _Repository.SelectFirst(linqExpression, ascOrder, orderExpression, dbContext);
        }

        public virtual long Count(Expression<Func<T, bool>> linqExpression)
        {
            return _Repository.Count(linqExpression);
        }

        public virtual long Count(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            return _Repository.Count(linqExpression, dbContext);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _Repository.GetAllAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(IMySQLContext dbContext)
        {
            return await _Repository.GetAllAsync(dbContext);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _Repository.GetByIdAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(int id, IMySQLContext dbContext)
        {
            return await _Repository.GetByIdAsync(id, dbContext);
        }

        public virtual async Task<IEnumerable<T>> SelectAsync(Expression<Func<T, bool>> linqExpression)
        {
            return await _Repository.SelectAsync(linqExpression);
        }

        public virtual async Task<IEnumerable<T>> SelectAsync(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            return await _Repository.SelectAsync(linqExpression, dbContext);
        }

        public virtual async Task<T> SelectFirstAsync(Expression<Func<T, bool>> linqExpression)
        {
            return await _Repository.SelectFirstAsync(linqExpression);
        }

        public virtual async Task<T> SelectFirstAsync(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            return await _Repository.SelectFirstAsync(linqExpression, dbContext);
        }

        public virtual async Task<T> SelectFirstAsync<TKey>(Expression<Func<T, bool>> linqExpression, bool ascOrder, Func<T, TKey> orderExpression)
        {
            return await _Repository.SelectFirstAsync(linqExpression, ascOrder, orderExpression);
        }

        public virtual async Task<T> SelectFirstAsync<TKey>(Expression<Func<T, bool>> linqExpression, bool ascOrder, Func<T, TKey> orderExpression, IMySQLContext dbContext)
        {
            return await _Repository.SelectFirstAsync(linqExpression, ascOrder, orderExpression, dbContext);
        }


        public virtual T Insert(T entity)
        {
            return _Repository.Insert(entity);
        }

        public virtual T Insert(T entity, IMySQLContext dbContext)
        {
            return _Repository.Insert(entity, dbContext);
        }

        public virtual IEnumerable<T> Insert(IEnumerable<T> entities)
        {
            return _Repository.Insert(entities);
        }

        public virtual IEnumerable<T> Insert(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            return _Repository.Insert(entities, dbContext);
        }

        public virtual void ParallelInsert(IEnumerable<T> entities)
        {
            _Repository.ParallelInsert(entities);
        }

        public virtual void ParallelInsert(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            _Repository.ParallelInsert(entities, dbContext);
        }

        public virtual async Task<object> InsertAsync(T entity)
        {
            return await _Repository.InsertAsync(entity);
        }

        public virtual async Task<object> InsertAsync(T entity, IMySQLContext dbContext)
        {
            return await _Repository.InsertAsync(entity, dbContext);
        }

        public virtual async Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities)
        {
            return await _Repository.InsertAsync(entities);
        }

        public virtual async Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            return await _Repository.InsertAsync(entities, dbContext);
        }



        public virtual bool Update(T entity)
        {
            return _Repository.Update(entity);
        }

        public virtual bool Update(T entity, IMySQLContext dbContext)
        {
            return _Repository.Update(entity, dbContext);
        }

        public virtual bool Update(IEnumerable<T> entities)
        {
            return _Repository.Update(entities);
        }

        public virtual bool Update(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            return _Repository.Update(entities, dbContext);
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            return await _Repository.UpdateAsync(entity);
        }

        public virtual async Task<bool> UpdateAsync(T entity, IMySQLContext dbContext)
        {
            return await _Repository.UpdateAsync(entity, dbContext);
        }

        public virtual async Task<bool> UpdateAsync(IEnumerable<T> entities)
        {
            return await _Repository.UpdateAsync(entities);
        }

        public virtual async Task<bool> UpdateAsync(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            return await _Repository.UpdateAsync(entities, dbContext);
        }


        public virtual bool Delete(int id)
        {
            return _Repository.Delete(id);
        }

        public virtual bool Delete(int id, IMySQLContext dbContext)
        {
            return _Repository.Delete(id, dbContext);
        }

        public virtual bool Delete(T entity)
        {
            return _Repository.Delete(entity);
        }

        public virtual bool Delete(T entity, IMySQLContext dbContext)
        {
            return _Repository.Delete(entity, dbContext);
        }

        public virtual bool Delete(IEnumerable<T> entities)
        {
            return _Repository.Delete(entities);
        }

        public virtual bool Delete(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            return _Repository.Delete(entities, dbContext);
        }

        public virtual bool Delete(Expression<Func<T, bool>> linqExpression)
        {
            return _Repository.Delete(linqExpression);
        }

        public virtual bool Delete(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            return _Repository.Delete(linqExpression, dbContext);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            return await _Repository.DeleteAsync(id);
        }

        public virtual async Task<bool> DeleteAsync(int id, IMySQLContext dbContext)
        {
            return await _Repository.DeleteAsync(id, dbContext);
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            return await _Repository.DeleteAsync(entity);
        }

        public virtual async Task<bool> DeleteAsync(T entity, IMySQLContext dbContext)
        {
            return await _Repository.DeleteAsync(entity, dbContext);
        }

        public virtual async Task<bool> DeleteAsync(IEnumerable<T> entities)
        {
            return await _Repository.DeleteAsync(entities);
        }

        public virtual async Task<bool> DeleteAsync(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            return await _Repository.DeleteAsync(entities, dbContext);
        }

        public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> linqExpression)
        {
            return await _Repository.DeleteAsync(linqExpression);
        }

        public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            return await _Repository.DeleteAsync(linqExpression, dbContext);
        }



        public virtual bool DeletePermanent(int id)
        {
            return _Repository.DeletePermanent(id);
        }

        public virtual bool DeletePermanent(int id, IMySQLContext dbContext)
        {
            return _Repository.DeletePermanent(id, dbContext);
        }

        public virtual bool DeletePermanent(T entity)
        {
            return _Repository.DeletePermanent(entity);
        }

        public virtual bool DeletePermanent(T entity, IMySQLContext dbContext)
        {
            return _Repository.DeletePermanent(entity, dbContext);
        }

        public virtual bool DeletePermanent(IEnumerable<T> entities)
        {
            return _Repository.DeletePermanent(entities);
        }

        public virtual bool DeletePermanent(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            return _Repository.DeletePermanent(entities, dbContext);
        }

        public virtual bool DeletePermanent(Expression<Func<T, bool>> linqExpression)
        {
            return _Repository.DeletePermanent(linqExpression);
        }

        public virtual bool DeletePermanent(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            return _Repository.DeletePermanent(linqExpression, dbContext);
        }

        public virtual async Task<bool> DeletePermanentAsync(int id)
        {
            return await _Repository.DeletePermanentAsync(id);
        }

        public virtual async Task<bool> DeletePermanentAsync(int id, IMySQLContext dbContext)
        {
            return await _Repository.DeletePermanentAsync(id, dbContext);
        }

        public virtual async Task<bool> DeletePermanentAsync(T entity)
        {
            return await _Repository.DeletePermanentAsync(entity);
        }

        public virtual async Task<bool> DeletePermanentAsync(T entity, IMySQLContext dbContext)
        {
            return await _Repository.DeletePermanentAsync(entity, dbContext);
        }

        public virtual async Task<bool> DeletePermanentAsync(IEnumerable<T> entities)
        {
            return await _Repository.DeletePermanentAsync(entities);
        }

        public virtual async Task<bool> DeletePermanentAsync(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            return await _Repository.DeletePermanentAsync(entities, dbContext);
        }

        public virtual async Task<bool> DeletePermanentAsync(Expression<Func<T, bool>> linqExpression)
        {
            return await _Repository.DeletePermanentAsync(linqExpression);
        }

        public virtual async Task<bool> DeletePermanentAsync(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            return await _Repository.DeletePermanentAsync(linqExpression, dbContext);
        }


        public IEnumerable<T> ExecuteProcedure(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null)
        {
            return _Repository.ExecuteProcedure(storedProcedure, objParameters, timeout, dbContext);
        }

        public async Task<IEnumerable<T>> ExecuteProcedureAsync(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null)
        {
            return await _Repository.ExecuteProcedureAsync(storedProcedure, objParameters, timeout, dbContext);
        }

        public T ExecuteProcedureFirst(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true)
        {
            return _Repository.ExecuteProcedureFirst(storedProcedure, objParameters, timeout, dbContext, buffered);
        }

        public async Task<T> ExecuteProcedureFirstAsync(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true)
        {
            return await _Repository.ExecuteProcedureFirstAsync(storedProcedure, objParameters, timeout, dbContext, buffered);
        }

        public SqlMapper.GridReader ExecuteProcedureMultiple(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true)
        {
            return _Repository.ExecuteProcedureMultiple(storedProcedure, objParameters, timeout, dbContext, buffered);
        }

        public async Task<SqlMapper.GridReader> ExecuteProcedureMultipleAsync(string storedProcedure, object objParameters = null, int? timeout = 120, IMySQLContext dbContext = null, bool buffered = true)
        {
            return await _Repository.ExecuteProcedureMultipleAsync(storedProcedure, objParameters, timeout, dbContext, buffered);
        }

    }
}
