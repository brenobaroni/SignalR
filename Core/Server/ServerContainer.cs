using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Server
{
    public class ServerContainer
    {
        public string ConnectionString { get; set; }
        public WebAuthorization Authorization { get; set; }
        public AppConfig Configuration { get; set; }

        public ServerContainer()
        {
            Authorization = new WebAuthorization();
            Configuration = new AppConfig();
        }
    }
}
