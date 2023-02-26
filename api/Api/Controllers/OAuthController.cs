using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Business;
using AppGlobal.Constants;
using AppGlobal.Filters;
using AppGlobal.Models;
using AppGlobal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OAuthController : Controller
    {
        private GoogleApiService _GoogleApiService { get; set; }
        public OAuthController(GoogleApiService GoogleApiService)
        {
            _GoogleApiService = GoogleApiService;
        }

        [HttpGet("HostName")]
        public string GetHostName()
        {
            return _GoogleApiService.GetHostName();
        }
    }
}
