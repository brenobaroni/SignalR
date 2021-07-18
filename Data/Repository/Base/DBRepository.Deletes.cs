using Data.Context;
using LinkGamer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Data.Repository
{
    public partial class DBRepository<T> where T : BaseEntitie
    {
        //--  Delete (int id)
        public virtual bool Delete(int id)
        {
            var entity = GetById(id);
            entity = SetInativeData(entity);
            return Update(entity);
        }

        public virtual bool Delete(int id, IMySQLContext dbContext)
        {
            var entity = GetById(id);
            entity = SetInativeData(entity);
            return Update(entity, dbContext);
        }



        //--  Delete (T entity)
        public virtual bool Delete(T entity)
        {
            entity = SetInativeData(entity);
            return Update(entity);
        }

        public virtual bool Delete(T entity, IMySQLContext dbContext)
        {
            entity = SetInativeData(entity);
            return Update(entity, dbContext);
        }



        //--  Delete (IEnumerable<T> entities)
        public virtual bool Delete(IEnumerable<T> entities)
        {
            entities = SetInativeData(entities);
            return Update(entities);
        }

        public virtual bool Delete(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            entities = SetInativeData(entities);
            return Update(entities, dbContext);
        }



        // Delete (Expression<Func<T, bool>> linqExpression)
        public virtual bool Delete(Expression<Func<T, bool>> linqExpression)
        {
            var entities = Select(linqExpression);
            entities = SetInativeData(entities);
            return Update(entities);
        }

        public virtual bool Delete(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            var entities = Select(linqExpression);
            entities = SetInativeData(entities);
            return Update(entities, dbContext);
        }



        //--  DeleteAsync (int id)
        public virtual async Task<bool> DeleteAsync(int id)
        {
            return await Task.Run(() => Delete(id));
        }

        public virtual async Task<bool> DeleteAsync(int id, IMySQLContext dbContext)
        {
            return await Task.Run(() => Delete(id, dbContext));
        }



        //--  DeleteAsync (T entity)
        public virtual async Task<bool> DeleteAsync(T entity)
        {
            return await Task.Run(() => Delete(entity));
        }

        public virtual async Task<bool> DeleteAsync(T entity, IMySQLContext dbContext)
        {
            return await Task.Run(() => Delete(entity, dbContext));
        }



        //--  DeleteAsync (IEnumerable<T> entities)
        public virtual async Task<bool> DeleteAsync(IEnumerable<T> entities)
        {
            entities = SetInativeData(entities);
            return await UpdateAsync(entities);
        }

        public virtual async Task<bool> DeleteAsync(IEnumerable<T> entities, IMySQLContext dbContext)
        {
            entities = SetInativeData(entities);
            return await UpdateAsync(entities, dbContext);
        }



        //--  DeleteAsync (Expression<Func<T, bool>> linqExpression)
        public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> linqExpression)
        {
            return await Task.Run(() =>
            {
                var entities = Select(linqExpression);
                entities = SetInativeData(entities);
                return Update(entities);
            });
        }

        public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> linqExpression, IMySQLContext dbContext)
        {
            return await Task.Run(() =>
            {
                var entities = Select(linqExpression);
                entities = SetInativeData(entities);
                return Update(entities, dbContext);
            });
        }


        #region [ Internal Methods ]

        private IEnumerable<T> SetInativeData(IEnumerable<T> entities)
        {
            List<T> lstEntidades = new List<T>();
            foreach (var entity in entities)
                lstEntidades.Add(SetInativeData(entity));

            return lstEntidades;
        }

        private T SetInativeData(T entity)
        {
            bool possuiAtivo = false;
            var propertiesEntity = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo pEntity in propertiesEntity)
            {
                switch (pEntity.Name)
                {
                    case "Ativo":
                        pEntity.SetValue(entity, false);
                        possuiAtivo = true;
                        break;
                    case "IdUsuarioExclusao":
                        pEntity.SetValue(entity, _userId);
                        break;
                    case "DataExclusao":
                        pEntity.SetValue(entity, DateTime.Now);
                        break;
                    default:
                        break;
                }
            }

            if (!possuiAtivo)
                throw new InvalidOperationException($"Não foi possível encontrar a propriedade 'Ativo' na Entidade a ser Deletada (Id = {entity.Id}).");

            return entity;
        }


        #endregion [ Internal Methods ]
    }
}
