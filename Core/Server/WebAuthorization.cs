using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Server
{
    public class WebAuthorization
    {
        public struct WebAuthorizationUsuario
        {
            //Senha: Não adicionar a propriedade de senha (por questão de segurança de dados)
            public int? Id;
            public string Email;
        }

        public struct WebAuthorizationEmpresa
        {
            public int? Id;
            public string SubDominio;
        }


        public WebAuthorizationUsuario Usuario;
        public WebAuthorizationEmpresa Empresa;

        public WebAuthorization()
        {
            Usuario = new WebAuthorizationUsuario();
            Empresa = new WebAuthorizationEmpresa();
        }
    }
}
