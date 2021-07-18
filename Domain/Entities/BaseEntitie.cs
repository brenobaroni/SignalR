using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinkGamer.Domain.Entities
{
    public abstract class BaseEntitie
    {

        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataExclusao { get; set; }

        private List<string> _messageValidation { get; set; }

        private List<string> MessageValidation
        {
            get { return _messageValidation ?? (_messageValidation = new List<string>()); }
        }

        protected void ClearMessageValidation()
        {
            MessageValidation.Clear();
        }

        protected void AddWarning(string message)
        {
            MessageValidation.Add(message);
        }

        public string GetMessageValidation()
        {
            return string.Concat(_messageValidation);
        }

        public abstract void Validate();

        public bool IsValid
        {
            get { return !MessageValidation.Any(); }
        }

        public TEntity CopyToVM<TEntity>() where TEntity : class
        {
            try
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(this);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<TEntity>(json);

                //var flags = BindingFlags.Public | BindingFlags.Instance;
                //var tEntity = System.Activator.CreateInstance<TEntity>();

                //foreach (var prop in GetType().GetProperties(flags))
                //{
                //    foreach (var propVM in tEntity.GetType().GetProperties(flags))
                //    {
                //        if (prop.Name == propVM.Name && prop.PropertyType.FullName == propVM.PropertyType.FullName)
                //        {
                //            propVM.SetValue(tEntity, prop.GetValue(this));
                //        }
                //    }
                //}

                //return tEntity;
            }
            catch
            {
                return null;
            }
        }
    }
}
