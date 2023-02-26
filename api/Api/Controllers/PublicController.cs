using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    public class PublicController : Controller
    {
        private PublicBusiness _PublicBusiness { get; set; }
        private CategoryBusiness _CategoryBusiness { get; set; }

        public PublicController(PublicBusiness PublicBusiness, CategoryBusiness CategoryBusiness)
        {
            _PublicBusiness = PublicBusiness;
            _CategoryBusiness = CategoryBusiness;
        }

        [HttpPost("Orders")]
        [Authorize]
        public bool CreateSaleOrder([FromBody]PublicOrderCreate body)
        {
            var user = (AuthEntity)HttpContext.Items["Auth"];
            return _PublicBusiness.CreateSaleOrder(user.UserID, user.Email, body);
        }

        #region company APIs
        [HttpGet("Companies/{companyName}")]
        public PublicCompanyModel GetCompany(string companyName)
        {
            return _PublicBusiness.GetCompany(companyName);
        }

        [HttpGet("Companies/{companyName}/Categories")]
        public SearchResult<CategoryModel> SearchCategories(string companyName, [FromHeader] int page = 1, [FromHeader] int limit = 10,
            string keySearch = "", string targetItemID = "", int? targetItemType = null, bool? deleted = null)
        {
            return _CategoryBusiness.SearchByCname(companyName, keySearch, targetItemID, targetItemType, deleted, page, limit);
        }

        [HttpGet("Companies/{companyName}/Products")]
        public SearchResult<PublicProductModel> SearchProducts(string companyName, [FromHeader] int page = 1, [FromHeader] int limit = 10,
            string categoryIDs = null, decimal? priceLower = null, decimal? priceUpper = null, int? productType = null, string keySearch = "")
        {
            var catids = String.IsNullOrEmpty(categoryIDs) ? null : categoryIDs.Split(",").ToList();
            return _PublicBusiness.SearchPublicProducts(companyName, catids, productType, priceLower, priceUpper, keySearch, page, limit);
        }

        [HttpGet("Companies/{companyName}/Blogs")]
        public SearchResult<PublicBlogModel> SearchBlogs(string companyName, [FromHeader] int page = 1, [FromHeader] int limit = 10,
            string categoryIDs = null, string keySearch = "")
        {
            var catids = String.IsNullOrEmpty(categoryIDs) ? null : categoryIDs.Split(",").ToList();
            return _PublicBusiness.SearchPublicBlogs(companyName, catids, keySearch, page, limit);
        }

        [HttpGet("Companies/{companyName}/ImageGallery")]
        public SearchResult<AttachmentAssetForSale> SearchImageGallery(string companyName, string keySearch = "", [FromHeader] int page = 1, [FromHeader] int limit = 10)
        {
            return _PublicBusiness.SearchAttachmentAssetsForSale(companyName, keySearch, page, limit);
        }

        #endregion

        #region Legalcy APIs
        [HttpGet("Episode/{publicName}")]
        public EpisodeDetailPublic GetPublicEpisode(string publicName)
        {
            return _PublicBusiness.GetPublicEpisode(publicName);
        }

        [HttpGet("Episodes/{clinicID}")]
        public SearchResult<EpisodeModelPublic> SearchPublicEpisodes(string clinicID, string keySearch, [FromHeader]int page = 1, [FromHeader]int limit = 10)
        {
            return _PublicBusiness.SearchPublicEpisodes(clinicID, keySearch, page, limit);
        }

        [HttpGet("Blogs/{blogID}")]
        public PublicBlogModel GetDetailBlog(string blogID)
        {
            return _PublicBusiness.GetDetailBlog(blogID);
        }

        [HttpGet("Blogs/{blogID}/details")]
        public string GetDetailDescriptionBlog(string blogID)
        {
            return _PublicBusiness.GetDetailDescriptionBlog(blogID);
        }

        [HttpGet("Products/{productID}")]
        public PublicProductModel GetDetailProduct(string productID)
        {
            return _PublicBusiness.GetDetailProduct(productID);
        }

        [HttpGet("Products/{productID}/details")]
        public string GetDetailDescriptionProduct(string productID)
        {
            return _PublicBusiness.GetDetailDescriptionProduct(productID);
        }
        #endregion
    }
}
