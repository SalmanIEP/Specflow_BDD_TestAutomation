using OtpNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Configuration
{
    public static class _2FAuthentication
    {
        /// <summary>
        /// This method generate the Google authentication code for validation for successfull login on the basis of secret key
        /// </summary>
        /// <returns></returns>
        public static string GetGoogleAuthenticationPassCode()
        {
            var Config = ConfigurationManager.Configuration();
            var otpKeyStr = Config["2FSecretKey"];
            var otpKeyBytes = Base32Encoding.ToBytes(otpKeyStr);
            var totp = new Totp(otpKeyBytes);
            return totp.ComputeTotp();
        }
    }
}
