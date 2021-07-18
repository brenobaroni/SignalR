using Data.Context;
using Dommel;
using LinkGamer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repository
{
    public partial class DBRepository<T> where T : BaseEntitie
    {
        public virtual IEnumerable<T> GetAll()
        {
            using var dbContext = DBRepository.ReturnMySqlContext(_connectionString);
            return GetAll(dbContext);
        }

        public virtual IEnumerable<T> GetAll(IMySQLContext dbContext)
        {
            if (_repositorySpecific != null)
                return _repositorySpecific.ConfigurarBuscaObjetoCompleto(o => o.Id > 0, dbContext);

            return dbContext.Connection.GetAll<T>(dbContext.Transaction);
        }

        public virtual T GetById(int id)
        {
            using var dbContext = DBRepository.ReturnMySqlContext(_connectionString);
            return GetById(id, dbContext);
        }

        public virtual T GetById(int id, IMySQLContext dbContext)
        {
            if (_repositorySpecific != null)
                return _repositorySpecific.ConfigurarBuscaObjetoCompleto(o => o.Id == id, dbContext).FirstOrDefault();

            return dbContext.Connection.Get<T>(id, dbContext.Transaction);
        }

        public virtual long Count(Expression<Func<T, bool>> linqExpression)
        {
            using var dbContext = DBRepository.ReturnMySqlContext(_connectionString);
            return Count(linqExpression, dbContext);
        }

        public virtual long Count(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            if (_repositorySpecific != null)
                return _repositorySpecific.ConfigurarBuscaObjetoCompleto(linqExpression, dbContext).Count();

            return dbContext.Connection.Count(linqExpression, dbContext.Transaction);
        }

        public virtual IEnumerable<T> Select(Expression<Func<T, bool>> linqExpression)
        {
            using var dbContext = DBRepository.ReturnMySqlContext(_connectionString);
            return Select(linqExpression, dbContext);
        }

        public virtual IEnumerable<T> Select(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            if (_repositorySpecific != null)
                return _repositorySpecific.ConfigurarBuscaObjetoCompleto(linqExpression, dbContext);

            return dbContext.Connection.Select(linqExpression, dbContext.Transaction);
        }

        public virtual T SelectFirst(Expression<Func<T, bool>> linqExpression)
        {
            using var dbContext = DBRepository.ReturnMySqlContext(_connectionString);
            return SelectFirst(linqExpression, dbContext);
        }

        public virtual T SelectFirst(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            if (_repositorySpecific != null)
                return _repositorySpecific.ConfigurarBuscaObjetoCompleto(linqExpression, dbContext).FirstOrDefault();

            return dbContext.Connection.FirstOrDefault(linqExpression, dbContext.Transaction);
        }

        public virtual T SelectFirst<TKey>(Expression<Func<T, bool>> linqExpression, bool ascOrder, Func<T, TKey> orderExpression)
        {
            using var dbContext = DBRepository.ReturnMySqlContext(_connectionString);
            return SelectFirst(linqExpression, ascOrder, orderExpression, dbContext);
        }

        public virtual T SelectFirst<TKey>(Expression<Func<T, bool>> linqExpression, bool ascOrder, Func<T, TKey> orderExpression, IMySQLContext dbContext)
        {
            if (_repositorySpecific != null)
            {
                if (ascOrder)
                    return _repositorySpecific.ConfigurarBuscaObjetoCompleto(linqExpression, dbContext).OrderBy(orderExpression).FirstOrDefault();
                else
                    return _repositorySpecific.ConfigurarBuscaObjetoCompleto(linqExpression, dbContext).OrderByDescending(orderExpression).FirstOrDefault();
            }

            if (ascOrder)
                return dbContext.Connection.Select(linqExpression, dbContext.Transaction).OrderBy(orderExpression).FirstOrDefault();
            else
                return dbContext.Connection.Select(linqExpression, dbContext.Transaction).OrderByDescending(orderExpression).FirstOrDefault();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using var dbContext = DBRepository.ReturnMySqlContext(_connectionString);
            return await GetAllAsync(dbContext);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(IMySQLContext dbContext)
        {
            if (_repositorySpecific != null)
                return await Task.Run(() => _repositorySpecific.ConfigurarBuscaObjetoCompleto(o => o.Id > 0, dbContext));

            return await dbContext.Connection.GetAllAsync<T>(dbContext.Transaction);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            using var dbContext = DBRepository.ReturnMySqlContext(_connectionString);
            return await GetByIdAsync(id, dbContext);
        }

        public virtual async Task<T> GetByIdAsync(int id, IMySQLContext dbContext)
        {
            if (_repositorySpecific != null)
                return await Task.Run(() => _repositorySpecific.ConfigurarBuscaObjetoCompleto(o => o.Id == id, dbContext).FirstOrDefault());

            return await dbContext.Connection.GetAsync<T>(id, dbContext.Transaction);
        }

        public virtual async Task<IEnumerable<T>> SelectAsync(Expression<Func<T, bool>> linqExpression)
        {
            using var dbContext = DBRepository.ReturnMySqlContext(_connectionString);
            return await SelectAsync(linqExpression, dbContext);
        }

        public virtual async Task<IEnumerable<T>> SelectAsync(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            if (_repositorySpecific != null)
                return await Task.Run(() => _repositorySpecific.ConfigurarBuscaObjetoCompleto(linqExpression, dbContext));

            return await dbContext.Connection.SelectAsync(linqExpression, dbContext.Transaction);
        }

        public virtual async Task<T> SelectFirstAsync(Expression<Func<T, bool>> linqExpression)
        {
            using var dbContext = DBRepository.ReturnMySqlContext(_connectionString);
            return await SelectFirstAsync(linqExpression, dbContext);
        }

        public virtual async Task<T> SelectFirstAsync(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            if (_repositorySpecific != null)
                return await Task.Run(() => _repositorySpecific.ConfigurarBuscaObjetoCompleto(linqExpression, dbContext).FirstOrDefault());

            return await dbContext.Connection.FirstOrDefaultAsync(linqExpression, dbContext.Transaction);
        }

        public virtual async Task<T> SelectFirstAsync<TKey>(Expression<Func<T, bool>> linqExpression, bool ascOrder, Func<T, TKey> orderExpression)
        {
            using var dbContext = DBRepository.ReturnMySqlContext(_connectionString);
            return await SelectFirstAsync(linqExpression, ascOrder, orderExpression, dbContext);
        }

        public virtual async Task<T> SelectFirstAsync<TKey>(Expression<Func<T, bool>> linqExpression, bool ascOrder, Func<T, TKey> orderExpression, IMySQLContext dbContext)
        {
            return await Task.Run(() => SelectFirst(linqExpression, ascOrder, orderExpression, dbContext));
            //if (_repositorySpecific != null)
            //{
            //    if (ascOrder)
            //        return await Task.Run(() => _repositorySpecific.ConfigurarBuscaObjetoCompleto(linqExpression, dbContext).OrderBy(orderExpression).FirstOrDefault());
            //    else
            //        return await Task.Run(() => _repositorySpecific.ConfigurarBuscaObjetoCompleto(linqExpression, dbContext).OrderByDescending(orderExpression).FirstOrDefault());
            //}

            //if (ascOrder)
            //    return await dbContext.Connection.FirstOrDefaultAsync(linqExpression, dbContext.Transaction);//.OrderBy(orderExpression).FirstOrDefault();
            //else
            //    return await dbContext.Connection.FirstOrDefaultAsync(linqExpression, dbContext.Transaction);//.OrderByDescending(orderExpression).FirstOrDefault();
        }
    }
}
