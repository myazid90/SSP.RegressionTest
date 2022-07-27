using Microsoft.Extensions.Configuration;

namespace SSP.RegressionTest.Helper
{
    public class AppsettingsHelper
    {
        public IConfiguration Appsettings { get; set; }

        public AppsettingsHelper()
        {
            Appsettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

    }
}
