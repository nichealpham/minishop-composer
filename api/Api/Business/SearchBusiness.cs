using AppGlobal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppGlobal.Entities;
using AppGlobal.Commons;
using AppGlobal.Models;
using AppGlobal.DBContext;
using Microsoft.EntityFrameworkCore;
using AppGlobal.Extensions;
using FirebaseAdmin.Auth;
using AppGlobal.Constants;

namespace Api.Business
{
    public static class SearchHelper
    {
        public static IQueryable<Provider> GetSearchQueryable(this ERPContext _Context)
        {
            return _Context.Providers
                .Include(p => p.User)
                .Include(p => p.Service)
                .Include(p => p.Clinic)
                .AsNoTracking();
        }

        public static IQueryable<Provider> SearchPriceRange(this IQueryable<Provider> query, decimal? priceStart = null, decimal? priceEnd = null)
        {
            if (priceStart.HasValue)
            {
                query = query.Where(p =>
                    (p.Service.Price >= priceStart.Value) ||
                    (p.SpecialPrice.HasValue && p.SpecialPrice.Value >= priceStart.Value));
            }

            if (priceEnd.HasValue)
            {
                query = query.Where(p =>
                    (p.Service.Price <= priceEnd.Value) ||
                    (p.SpecialPrice.HasValue && p.SpecialPrice.Value <= priceEnd.Value));
            }

            return query;
        }

        public static SearchResult<ProviderModel> GetSearchResults(this IQueryable<Provider> query, int page = 1, int limit = 10)
        {
            var totals = query.Count();
            var items = query
                .OrderByDescending(p => p.DateCreated)
                .Select(ProviderModel.Convert)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToList();

            return new SearchResult<ProviderModel>()
            {
                Totals = totals,
                Items = items
            };
        }
    }
    public class SearchBusiness
    {
        private readonly ERPContext _Context;
        public SearchBusiness(ERPContext context)
        {
            _Context = context;
        }

        public SearchResult<ProviderModel> SearchServices(string keySearch, decimal? priceStart = null, decimal? priceEnd = null, int page = 1, int limit = 10)
        {
            var query = _Context.GetSearchQueryable();

            if (!string.IsNullOrEmpty(keySearch))
            {
                keySearch = keySearch.Standardizing();
                query = query.Where(p =>
                    ApplicationDBContext.Standardizing(p.Service.ServiceName).Contains(keySearch) ||
                    ApplicationDBContext.Standardizing(p.Service.ShortDescription).Contains(keySearch));
            }

            query = query.SearchPriceRange(priceStart, priceEnd);
            return query.GetSearchResults(page, limit);
        }

        public SearchResult<ProviderModel> SearchDoctors(string keySearch, decimal? priceStart = null, decimal? priceEnd = null, int page = 1, int limit = 10)
        {
            var query = _Context.GetSearchQueryable();

            if (!string.IsNullOrEmpty(keySearch))
            {
                query = query.Include(p => p.User).ThenInclude(u => u.Roles);
                var nameSearch = keySearch.Standardizing();
                var phoneSearch = keySearch.PreparePhone();
                query = query.Where(p =>
                    (ApplicationDBContext.Standardizing(p.User.FullName).Contains(nameSearch)) ||
                    (!string.IsNullOrEmpty(p.User.Phone) && p.User.Phone.Contains(phoneSearch)) ||
                    (p.User.Roles.Any(r => r.RoleType == (int)RoleType.Doctor)));
            }

            query = query.SearchPriceRange(priceStart, priceEnd);
            return query.GetSearchResults(page, limit);
        }

        public SearchResult<ProviderModel> SearchClinics(string keySearch, decimal? priceStart = null, decimal? priceEnd = null, int page = 1, int limit = 10)
        {
            var query = _Context.GetSearchQueryable();

            if (!string.IsNullOrEmpty(keySearch))
            {
                var nameSearch = keySearch.Standardizing();
                var phoneSearch = keySearch.PreparePhone();
                query = query
                    .Where(p => p.Clinic != null)
                    .Where(p =>
                        (ApplicationDBContext.Standardizing(p.Clinic.ClinicName).Contains(nameSearch)) ||
                        (!string.IsNullOrEmpty(p.Clinic.Phone) && p.Clinic.Phone.Contains(phoneSearch)));
            }

            query = query.SearchPriceRange(priceStart, priceEnd);
            return query.GetSearchResults(page, limit);
        }
    }
}
