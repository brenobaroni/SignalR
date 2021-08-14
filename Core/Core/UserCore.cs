using Core.Contracts;
using Core.Core;
using Core.Core.Base;
using Core.Server;
using System.Net;
using LinkGamer.Domain.Entities;
using LinkGamer.Models;
using BC = BCrypt.Net.BCrypt;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Core
{
    public class UserCore : EntityCoreBase<User>, IUserCore
    {
        #region [ Propriedades / Construtor ]

        public ITokenServiceCore _tokenServiceCore;

        public UserCore(ITokenServiceCore tokenServiceCore) {
            _tokenServiceCore = tokenServiceCore;
        }

        internal UserCore(ServerContainer serverContainer)
            : base(serverContainer) {
        }

        #endregion [ Propriedades / Construtor ]

        #region [ Configurações de Conexão ]

        protected override void StartDependenciesConnections()
        {
        }

        #endregion [ Configurações de Conexão ]

        public User Get(string email)
        {
            var user = _Repository.SelectFirst(s => s.Email == email && s.Ativo);
            return user;
        }

        //public User Get(string email)
        //{
        //    throw new NotImplementedException();
        //}

        public LinkGamerResult Login(UserModel user)
        {
            try
            {
                User userServer = _Repository.SelectFirst(s => s.Email == user.Email);

                if (user == null || userServer == null || string.IsNullOrEmpty(userServer.Email) || string.IsNullOrEmpty(user.Pass) || !BC.Verify(user.Pass, userServer.Password))
                    return new LinkGamerResult(HttpStatusCode.Unauthorized, false, "Usuário ou senha inválidos");

                var userToken = _tokenServiceCore.GenerateToken(userServer);

                // Oculta a senha
                userServer.Password = "";

                return new LinkGamerResult(HttpStatusCode.OK, true, "Login efetuado com sucesso.",
                    new
                    {
                        user = userServer,
                        token = userToken
                    });
            }
            catch (System.Exception ex)
            {
                return new LinkGamerResult(HttpStatusCode.InternalServerError, true, "Não conseguimos recuperar seu login, por favor tente mais tarde!");
            }
            

        }

        public async Task<LinkGamerResult> GetLogin(int id)
        {
            try
            {

                User user = await _Repository.SelectFirstAsync(s => s.Id == id);

                return new LinkGamerResult(HttpStatusCode.OK, true, user);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
