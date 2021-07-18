using Data.Context;
using Dommel;
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
        //--  Update (T entity)
        public virtual bool Update(T entity)
        {
            entity = SetAlterData(entity);
            using (var context = DBRepository.ReturnMySqlContext(_connectionString))
            {
                return context.Connection.Update(entity);
            }
        }

        public virtual bool Update(T entity, IMySQLContext dbContext)
        {
            entity = SetAlterData(entity);
            return dbContext.Connection.Update(entity, dbContext.Transaction);
        }

        //--  Update (IEnumerable<T> entities)
        public virtual bool Update(IEnumerable<T> entities)
        {
            entities = SetAlterData(entities);
            using (var dbContext = NewConnection())
            {
                dbContext.BeginTransaction();
                int countSuccess = 0;

                try
                {
                    T[] lstEntity = entities.ToArray();
                    for (int i = 0; i < lstEntity.Count(); i++)
                    {
                        countSuccess = Update(lstEntity[i], dbContext) ? (countSuccess + 1) : (countSuccess + 0);
                    }

                    if (countSuccess == lstEntity.Count())
                    {
                        dbContext.Commit();
                        return true;
                    }
                    else
                    {
                        dbContext.Rollback();
                        return false;
                    }
                }
                catch
                {
                    dbContext.Rollback();
                    throw;
                }
            }
        }

        public virtual bool Update(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            entities = SetAlterData(entities);
            int countSuccess = 0;

            T[] lstEntity = entities.ToArray();
            for (int i = 0; i < lstEntity.Count(); i++)
                countSuccess = Update(lstEntity[i], dbContext) ? (countSuccess + 1) : (countSuccess + 0);

            return countSuccess == lstEntity.Count();
        }



        //--  UpdateAsync (T entity)
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            entity = SetAlterData(entity);
            using (var context = DBRepository.ReturnMySqlContext(_connectionString))
            {
                return await context.Connection.UpdateAsync(entity);
            }
        }

        public virtual async Task<bool> UpdateAsync(T entity, IMySQLContext dbContext)
        {
            entity = SetAlterData(entity);
            return await dbContext.Connection.UpdateAsync(entity, dbContext.Transaction);
        }



        //--  UpdateAsync (IEnumerable<T> entities)
        public virtual async Task<bool> UpdateAsync(IEnumerable<T> entities)
        {
            return await Task.Run(() => Update(entities));
        }

        public virtual async Task<bool> UpdateAsync(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            return await Task.Run(() => Update(entities, dbContext));
        }

        #region [ Internal Methods ]

        private IEnumerable<T> SetAlterData(IEnumerable<T> entities)
        {
            List<T> lstEntidades = new List<T>();
            foreach (var entity in entities)
                lstEntidades.Add(SetAlterData(entity));

            return lstEntidades;
        }

        private T SetAlterData(T entity)
        {
            var propertiesEntity = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo pEntity in propertiesEntity)
            {
                switch (pEntity.Name)
                {
                    case "IdUsuarioAlteracao":
                        pEntity.SetValue(entity, _userId);
                        break;
                    case "DataAtualizacao":
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
