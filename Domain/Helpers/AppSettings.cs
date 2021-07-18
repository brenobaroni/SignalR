using Microsoft.Extensions.Configuration;

namespace LinkGamer.Domain.Helpers
{
    public class AppSettings
    {
        public AppSettings()
        {
            Secret = "fgg45f1d55FHhsd5ffrhuKA5sdgd5ggDFS5gG5775sdf";
        }
        public string Secret { get; set; }


    }
}
