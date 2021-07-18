using Dapper.Contrib.Extensions;
using Data.Context;
using LinkGamer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Data.Repository
{
    public partial class DBRepository<T> where T : BaseEntitie
    {
        //--  Insert (T entity)
        public virtual T Insert(T entity)
        {
            using (var context = DBRepository.ReturnMySqlContext(_connectionString))
            {
                entity = SetIncludeData(entity);
                entity.Id = Convert.ToInt32(context.Connection.Insert(entity));
                return entity;
            }
        }

        public virtual T Insert(T entity, IMySQLContext dbContext)
        {
            entity = SetIncludeData(entity);
            entity.Id = Convert.ToInt32(dbContext.Connection.Insert(entity, dbContext.Transaction));
            return entity;
        }



        //--  Insert (IEnumerable<T> entities)
        public virtual IEnumerable<T> Insert(IEnumerable<T> entities)
        {
            entities = SetIncludeData(entities);
            using (var dbContext = NewConnection())
            {
                dbContext.BeginTransaction();

                try
                {
                    T[] lstEntity = entities.ToArray();

                    for (int i = 0; i < lstEntity.Count(); i++)
                        lstEntity[i] = Insert(lstEntity[i], dbContext);

                    dbContext.Commit();

                    return lstEntity;
                }
                catch
                {
                    dbContext.Rollback();
                    throw;
                }
            }
        }

        public virtual IEnumerable<T> Insert(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            entities = SetIncludeData(entities);
            T[] lstEntity = entities.ToArray();

            for (int i = 0; i < lstEntity.Count(); i++)
                lstEntity[i] = Insert(lstEntity[i], dbContext);

            return lstEntity;
        }



        //--  ParallelInsert (IEnumerable<T> entities)
        public virtual void ParallelInsert(IEnumerable<T> entities)
        {
            entities = SetIncludeData(entities);
            using (var dbContext = NewConnection())
            {
                dbContext.BeginTransaction();

                try
                {
                    List<Task<object>> taks = new List<Task<object>>();

                    foreach (T entity in entities)
                    {
                        var t = InsertAsync(entity, dbContext);
                        taks.Add(t);
                    }

                    Task.WaitAll(taks.ToArray());
                    dbContext.Commit();
                }
                catch
                {
                    dbContext.Rollback();
                    throw;
                }
            }
        }

        public virtual void ParallelInsert(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            entities = SetIncludeData(entities);
            List<Task<object>> taks = new List<Task<object>>();

            foreach (T entity in entities)
            {
                var t = InsertAsync(entity, dbContext);
                taks.Add(t);
            }

            Task.WaitAll(taks.ToArray());
        }



        //--  InsertAsync (T entity)
        public virtual async Task<object> InsertAsync(T entity)
        {
            entity = SetIncludeData(entity);
            using (var context = DBRepository.ReturnMySqlContext(_connectionString))
            {
                return await context.Connection.InsertAsync(entity);
            }
        }

        public virtual async Task<object> InsertAsync(T entity, IMySQLContext dbContext)
        {
            entity = SetIncludeData(entity);
            return await dbContext.Connection.InsertAsync(entity, dbContext.Transaction);
        }



        //--  InsertAsync (IEnumerable<T> entities)
        public virtual async Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities)
        {
            return await Task.Run(() => Insert(entities));
        }

        public virtual async Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            return await Task.Run(() => Insert(entities, dbContext));
        }


        #region [ Internal Methods ]

        private IEnumerable<T> SetIncludeData(IEnumerable<T> entities)
        {
            List<T> lstEntidades = new List<T>();
            foreach (var entity in entities)
                lstEntidades.Add(SetIncludeData(entity));

            return lstEntidades;
        }

        private T SetIncludeData(T entity)
        {
            var propertiesEntity = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo pEntity in propertiesEntity)
            {
                switch (pEntity.Name)
                {
                    case "Ativo":
                        pEntity.SetValue(entity, true);
                        break;
                    case "IdUsuarioCadastro":
                        pEntity.SetValue(entity, _userId);
                        break;
                    case "DataCadastro":
                        pEntity.SetValue(entity, DateTime.Now);
                        break;
                    default:
                        break;
                }
            }

            return entity;
        }


        #endregion [ Internal Methods ]
    }
}
