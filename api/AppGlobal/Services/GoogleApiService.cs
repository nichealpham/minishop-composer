using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppGlobal.Services
{
    public class GoogleApiService
    {
        private readonly string ServerUrl;
        private readonly string GoogleClientID;
        private readonly string GoogleClientSecret;
        //private readonly string GoogleAuthUri = "https://accounts.google.com/o/oauth2/auth";
        //private readonly string GoogleTokenUri = "https://oauth2.googleapis.com/token";
        public GoogleApiService(IConfiguration Configuration, IHttpContextAccessor httpContextAccessor)
        {
            ServerUrl = string.Format(@"{0}://{1}/oauth", httpContextAccessor.HttpContext.Request.Scheme, httpContextAccessor.HttpContext.Request.Host);
            GoogleClientID = Configuration["GoogleClientID"];
            GoogleClientSecret = Configuration["GoogleClientSecret"];
        }

        public string GetHostName()
        {
            return ServerUrl;
        }
    }
}
