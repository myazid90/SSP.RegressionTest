using Microsoft.Extensions.Configuration;

namespace SSP.RegressionTest.Helper
{
    public class ChromeSettings
    {
        public string Userdatadir { get; set; }
        public string Userprofile { get; set; }

        internal ChromeSettings()
        {
            AppsettingsHelper config = new();
            Userdatadir = config.Appsettings.GetValue<string>("browsersettings:chrome:userdatadir");
            Userprofile = config.Appsettings.GetValue<string>("browsersettings:chrome:userprofile");
        }
    }
}
