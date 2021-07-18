using Core;
using Core.Contracts;
using Core.Core.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkGamer.Core;

namespace LinkGamer.Configuration
{
    public class DependencyInjection
    {
        #region [ Método Externo ]

        public static void AddDependencies(ref IServiceCollection services)
        {
            AddGenericCore(ref services);
            AddEspecificCore(ref services);
        }

        #endregion [ Método Externo ]

        #region [ Repositório Genérico ]

        private static void AddGenericCore(ref IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityCoreBase<>), typeof(EntityCoreBase<>));
        }

        #endregion [ Repositório Genérico ]

        #region [ Repositório Específicos - Core]

        private static void AddEspecificCore(ref IServiceCollection services)
        {
            services.AddScoped<ITokenServiceCore, TokenServiceCore>();
            services.AddScoped(typeof(IUserCore), typeof(UserCore));
        }

        #endregion [ Repositório Específicos ]
    }
}
