using Amazon.Runtime.CredentialManagement;
using Core.Core;
using Core.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkGamer.Controllers.Base
{
    public class LinkGamerControllerBase : ControllerBase
    {
        #region [ Properties / Constructor ]

        internal const string ROUTE = "main";
        //internal const string ROUTE_MOBILE = "mobile";

        private ServerContainer _server;
        internal ServerContainer _Server { get { return GetServerData(); } }

        public LinkGamerControllerBase(IConfiguration configuration)
        {
            _server = new ServerContainer();
            _server.ConnectionString = Environment.GetEnvironmentVariable("LinkGamerDB");

            configuration.Bind(_server.Configuration);

            //try
            //{
            //    var options = configuration.GetAWSOptions();
            //    var chain = new CredentialProfileStoreChain(options.ProfilesLocation);
            //    chain.TryGetAWSCredentials(options.Profile, out var result);
            //    var credentials = result.GetCredentials();

            //    _server.Configuration.AWSCredentials = new AmazonCredentials()
            //    {
            //        AccessKey = credentials.AccessKey,
            //        SecretKey = credentials.SecretKey
            //    };
            //}
            //catch
            //{
            //    throw new ProcessAWSCredentialException("Não foi possivel carregar o arquivo de credenciais de acesso da AWS.");
            //}
        }

        internal virtual void CreateCoreConnections() { /* Para sobrecarga nos Controllers */ }

        #endregion [ Properties / Constructor ]

        protected IActionResult Result(LinkGamerResult result)
        {
            return StatusCode(200, result);
        }

        public override OkObjectResult Ok([Microsoft.AspNetCore.Mvc.Infrastructure.ActionResultObjectValue] object value)
        {
            if (value is LinkGamerResult)
                throw new PlatformNotSupportedException("Utilizar o método Result para retornar objetos do tipo LinkGamerResult");

            return base.Ok(value);
        }

        private ServerContainer GetServerData()
        {
            if (_server.Authorization == null || string.IsNullOrEmpty(_server.Authorization.Usuario.Email))
            {
                //if (!Request.Headers.ContainsKey("Authorization"))
                //    return _server;

                //try
                //{
                //    string empId = "", empSubDomain = "";
                //    string userId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Sid).Value;
                //    string userEmail = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Email).Value;

                //    try
                //    {
                //        empId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.GroupSid).Value;
                //        empSubDomain = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Surname).Value;
                //    }
                //    catch { }

                //    _server.Authorization = new WebAuthorization()
                //    {
                //        Usuario = new WebAuthorization.WebAuthorizationUsuario
                //        {
                //            Id = Convert.ToInt32(userId),
                //            Email = userEmail,
                //        }
                //    };

                //    if (!string.IsNullOrEmpty(empId))
                //    {
                //        _server.Authorization.Empresa = new WebAuthorization.WebAuthorizationEmpresa
                //        {
                //            Id = Convert.ToInt32(empId),
                //            SubDominio = empSubDomain
                //        };
                //    }
                //}
                //catch
                //{
                //    _server = new ServerContainer();
                //}
            }

            return _server;
        }

    }
}
