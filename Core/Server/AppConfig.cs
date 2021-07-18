using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Server
{
    public class AppConfig
    {
        public InfobipConfig Infobip { get; set; }
        public AmazonCredentials AWSCredentials { get; set; }
        public FirebaseConfig Firebase { get; set; }
    }

    public class InfobipConfig
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AmazonCredentials
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }

    public class FirebaseConfig
    {
        public string Environment { get; set; }
    }
}
