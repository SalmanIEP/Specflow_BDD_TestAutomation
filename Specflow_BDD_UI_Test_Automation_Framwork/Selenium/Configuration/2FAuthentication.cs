﻿using OtpNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selenium.Configuration
{
    public static class _2FAuthentication
    {
        public static string GetGoogleAuthenticationPassCode()
        {
            var Config = ConfigurationManager.Configuration();
            var otpKeyStr = Config["IsRemoteDriver"];
            var otpKeyBytes = Base32Encoding.ToBytes(otpKeyStr);
            var totp = new Totp(otpKeyBytes);
            return totp.ComputeTotp();
        }
    }
}