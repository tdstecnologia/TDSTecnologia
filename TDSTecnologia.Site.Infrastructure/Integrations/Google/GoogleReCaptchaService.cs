using Newtonsoft.Json.Linq;
using System;
using System.Net;
using TDSTecnologia.Site.Infrastructure.Util;

namespace TDSTecnologia.Site.Infrastructure.Integrations.Google
{
    public class GoogleReCaptchaService
    {
        /**
         * Valida o token ReCaptcha
         * 
         * param name="recaptcha" Token para ser validado pela API.
         * 
         * returns true se e somente o token for validado pela API Google Service ReCaptcha
         */
        public static bool IsReCaptchaValido(string recaptcha)
        {
            try
            {
                WebClient webcClient = new WebClient();
                string secretKey = ConfiguracaoAmbiente.GoogleReCaptcha.SecretKey;
                var jsonResult = webcClient.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, recaptcha));
                var obj = JObject.Parse(jsonResult);
                return (bool)obj.SelectToken("success");
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }
        }
    }
}
