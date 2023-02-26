using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Business;
using AppGlobal.Entities;
using AppGlobal.Filters;
using AppGlobal.Models;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController : Controller
    {
        private readonly SearchBusiness _SearchBusiness;

        public SearchController(SearchBusiness SearchBusiness)
        {
            _SearchBusiness = SearchBusiness;
        }

        [Authorize]
        [HttpGet("Doctors")]
        public SearchResult<ProviderModel> SearchDoctors(string keySearch, decimal? priceStart = null, decimal? priceEnd = null, [FromHeader]int page = 1, [FromHeader] int limit = 10)
        {
            return _SearchBusiness.SearchDoctors(keySearch, priceStart, priceEnd, page, limit);
        }

        [Authorize]
        [HttpGet("Services")]
        public SearchResult<ProviderModel> Services(string keySearch, decimal? priceStart = null, decimal? priceEnd = null, [FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            return _SearchBusiness.SearchServices(keySearch, priceStart, priceEnd, page, limit);
        }

        [Authorize]
        [HttpGet("Clinics")]
        public SearchResult<ProviderModel> SearchClinics(string keySearch, decimal? priceStart = null, decimal? priceEnd = null, [FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            return _SearchBusiness.SearchClinics(keySearch, priceStart, priceEnd, page, limit);
        }
    }
}
