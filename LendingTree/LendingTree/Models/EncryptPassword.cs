using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LendingTree.Models
{
    public class EncryptPassword
    {
        public static string Key = "adef@@kfxcbv@";

        public string Encode(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";

            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            return Convert.ToBase64String(passwordBytes);
        }

        public string Decode(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData)) return "";

            var passwordBytes = Convert.FromBase64String(base64EncodeData);
            var result = Encoding.UTF8.GetString(passwordBytes);
            result = result.Substring(0, result.Length - Key.Length);

            return result;
        }
    }
}