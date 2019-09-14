using System;
using System.Collections.Generic;
using System.Text;

namespace TDSTecnologia.Site.Infrastructure.Util
{
    public class ConfiguracaoAmbiente
    {
        public static class GoogleReCaptcha
        {
            private static readonly string ASPNETCORE_GOOGLE_RECAPTCHA_SITE_KEY = "ASPNETCORE_GOOGLE_RECAPTCHA_SITE_KEY";
            private static readonly string ASPNETCORE_GOOGLE_RECAPTCHA_SECRET_KEY = "ASPNETCORE_GOOGLE_RECAPTCHA_SECRET_KEY";
            private static readonly string ASPNETCORE_GOOGLE_RECAPTCHA_URL_VERIFY_TOKEN = "ASPNETCORE_GOOGLE_RECAPTCHA_URL_VERIFY_TOKEN";

            public static string SiteKey
            {
                get
                {
                    return Environment.GetEnvironmentVariable(ASPNETCORE_GOOGLE_RECAPTCHA_SITE_KEY);
                }
            }

            public static string SecretKey
            {
                get
                {
                    return Environment.GetEnvironmentVariable(ASPNETCORE_GOOGLE_RECAPTCHA_SECRET_KEY);
                }
            }

            public static string URLVerifyToken
            {
                get
                {
                    return Environment.GetEnvironmentVariable(ASPNETCORE_GOOGLE_RECAPTCHA_URL_VERIFY_TOKEN);
                }
            }
        }

        private ConfiguracaoAmbiente() { }
    }
}
