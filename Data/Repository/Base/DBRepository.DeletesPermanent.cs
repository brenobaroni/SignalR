using Data.Context;
using Dommel;
using LinkGamer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repository
{
    /*************************************************
   *   DELETE PERMANENT
   *************************************************/
    public partial class DBRepository<T> where T : BaseEntitie
    {
        //--  DeletePermanent (int id)
        public virtual bool DeletePermanent(int id)
        {
            using (var context = DBRepository.ReturnMySqlContext(_connectionString))
            {
                var obj = Activator.CreateInstance<T>();
                obj.Id = id;
                return context.Connection.Delete(obj);
            }
        }

        public virtual bool DeletePermanent(int id, IMySQLContext dbContext)
        {
            var obj = Activator.CreateInstance<T>();
            obj.Id = id;
            return dbContext.Connection.Delete(obj, dbContext.Transaction);
        }



        //--  DeletePermanent (T entity)
        public virtual bool DeletePermanent(T entity)
        {
            using (var context = DBRepository.ReturnMySqlContext(_connectionString))
            {
                return context.Connection.Delete(entity);
            }
        }

        public virtual bool DeletePermanent(T entity, IMySQLContext dbContext)
        {
            return dbContext.Connection.Delete(entity, dbContext.Transaction);
        }



        //--  DeletePermanent (IEnumerable<T> entities)
        public virtual bool DeletePermanent(IEnumerable<T> entities)
        {
            using (var dbContext = NewConnection())
            {
                dbContext.BeginTransaction();

                try
                {
                    bool sucesso = false;

                    foreach (T entity in entities)
                    {
                        sucesso = DeletePermanent(entity, dbContext);
                        if (!sucesso)
                            break;
                    }

                    if (sucesso)
                        dbContext.Commit();
                    else
                        dbContext.Rollback();

                    return sucesso;
                }
                catch
                {
                    dbContext.Rollback();
                    throw;
                }
            }
        }

        public virtual bool DeletePermanent(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            bool sucesso = false;

            foreach (T entity in entities)
            {
                sucesso = DeletePermanent(entity, dbContext);
                if (!sucesso)
                    break;
            }

            return sucesso;
        }



        // DeletePermanent (Expression<Func<T, bool>> linqExpression)
        public virtual bool DeletePermanent(Expression<Func<T, bool>> linqExpression)
        {
            var entities = Select(linqExpression);
            return DeletePermanent(entities);
        }

        public virtual bool DeletePermanent(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            var entities = Select(linqExpression);
            return DeletePermanent(entities, dbContext);
        }



        //--  DeletePermanentAsync (int id)
        public virtual async Task<bool> DeletePermanentAsync(int id)
        {
            using (var context = DBRepository.ReturnMySqlContext(_connectionString))
            {
                var obj = Activator.CreateInstance<T>();
                obj.Id = id;
                return await context.Connection.DeleteAsync(obj);
            }
        }

        public virtual async Task<bool> DeletePermanentAsync(int id, IMySQLContext dbContext)
        {
            var obj = Activator.CreateInstance<T>();
            obj.Id = id;
            return await dbContext.Connection.DeleteAsync(obj, dbContext.Transaction);
        }



        //--  DeletePermanentAsync (T entity)
        public virtual async Task<bool> DeletePermanentAsync(T entity)
        {
            using (var context = DBRepository.ReturnMySqlContext(_connectionString))
            {
                return await context.Connection.DeleteAsync(entity);
            }
        }

        public virtual async Task<bool> DeletePermanentAsync(T entity, IMySQLContext dbContext)
        {
            return await dbContext.Connection.DeleteAsync(entity, dbContext.Transaction);
        }



        //--  DeletePermanentAsync (IEnumerable<T> entities)
        public virtual async Task<bool> DeletePermanentAsync(IEnumerable<T> entities)
        {
            return await Task.Run(() => DeletePermanent(entities));
        }

        public virtual async Task<bool> DeletePermanentAsync(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            return await Task.Run(() => DeletePermanent(entities, dbContext));
        }



        //--  DeletePermanentAsync (Expression<Func<T, bool>> linqExpression)
        public virtual async Task<bool> DeletePermanentAsync(Expression<Func<T, bool>> linqExpression)
        {
            return await Task.Run(() => DeletePermanent(linqExpression));
        }

        public virtual async Task<bool> DeletePermanentAsync(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            return await Task.Run(() => DeletePermanent(linqExpression, dbContext));
        }

    }
}
