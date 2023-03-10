using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace UC.Common.Authorization
{
    public static class AuthenticationHttpContext 
    {
        public static string GetUserToken(HttpContext httpcontext)
        {
            string _Token = string.Empty;
            if (httpcontext.Request.Headers.ContainsKey("Authorization"))
            {
                _Token = httpcontext.Request.Headers.First(x => x.Key == "Authorization").Value;
                if (_Token != "")
                {
                    string[] _TokenMain = _Token.Split(" ");
                    return _TokenMain[1];
                }
            }
            return "";
        }

        public static string GetUserTokenHash(HttpContext httpcontext)
        {
            string _Token = string.Empty;
            if (httpcontext.Request.Headers.ContainsKey("Authorization"))
            {
                _Token = httpcontext.Request.Headers.First(x => x.Key == "Authorization").Value;
                if (_Token != "")
                {
                    string[] _TokenMain = _Token.Split(" ");
                    return CreateHashing(_TokenMain[1]);
                }
            }
            return "";
        }
        public static string CreateHashing(string Value)
        {
#pragma warning disable
            var hashAlgorithm = new SHA512CryptoServiceProvider();
            var byteValue = Encoding.UTF8.GetBytes(Value);
            var byteHash = hashAlgorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }
    }
}
