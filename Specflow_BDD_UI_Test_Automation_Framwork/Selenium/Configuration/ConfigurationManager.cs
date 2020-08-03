﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Selenium.Support.Extensions;
using System.Diagnostics;

namespace Selenium.Configuration
{
    public class ConfigurationManager
    {
        public static IConfiguration Configuration()
        {
            string Path = "appsettings.json";
            Path = Path.AbsolutePath();
            var config = new ConfigurationBuilder()
                .AddJsonFile(Path.AbsolutePath().ToString())
                .Build();
            return config;
        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public static string DecodePassword(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public static void ExecuteCommandFile(string filename)
        {
            //Process proc = null;
            try
            {
                string Path = "SeleniumHub";
                Path = Path.AbsolutePath();
                System.Diagnostics.Process.Start("CMD.exe", "java -jar selenium-server-standalone-3.141.59.jar -role hub -hubConfig HubConfiguartions.json");
                ProcessStartInfo commandInfo = new ProcessStartInfo();
                commandInfo.WorkingDirectory = Path;
                commandInfo.CreateNoWindow = true;
                commandInfo.UseShellExecute = false;
                commandInfo.RedirectStandardInput = false;
                commandInfo.RedirectStandardOutput = false;
                commandInfo.FileName = "cmd.exe";
                commandInfo.Arguments = "java -jar selenium-server-standalone-3.141.59.jar -role hub -hubConfig HubConfiguartions.json";
                Process process = Process.Start(commandInfo);
                process.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }

    }
}
