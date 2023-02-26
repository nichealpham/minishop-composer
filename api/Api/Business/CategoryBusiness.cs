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
    public static class CategoryHelper
    {
        // Always include CategoryItems to Counts!
        public static IQueryable<Category> GetCategoryQueryable(this ERPContext _Context)
        {
            return _Context.Categories
                .AsNoTracking()
                .Include(a => a.CategoryItems);
        }
    }

    public class CategoryBusiness
    {
        private readonly ERPContext _Context;

        public CategoryBusiness(ERPContext context)
        {
            _Context = context;
        }

        public SearchResult<CategoryModel> Search(string clinicID, string keySearch = "", string targetItemID = "", int? targetItemType = null, bool? deleted = null, int page = 1, int limit = 10)
        {
            var cid = Guid.Parse(clinicID);
            var query = _Context.GetCategoryQueryable()
                .Where(c => c.ClinicID == cid)
                .Where(c => c.CategoryItems.Count > 0);

            if (deleted == true)
            {
                query = query.Where(a => a.StatusID == (byte)EpisodeStatusType.Deleted);
            }
            else
            {
                query = query.Where(a => a.StatusID != (byte)EpisodeStatusType.Deleted);
            }

            if (!string.IsNullOrEmpty(keySearch))
            {
                keySearch = keySearch.Standardizing();
                query = query.Where(a => ApplicationDBContext.Standardizing(a.CategoryName).Contains(keySearch));
            }

            if (!string.IsNullOrEmpty(targetItemID) && targetItemType.HasValue)
            {
                var iid = Guid.Parse(targetItemID);
                query = query.Where(c => c.CategoryItems != null && c.CategoryItems.Any(i => i.ItemID == iid && i.ItemType == targetItemType.Value));
            }

            var totals = query.Count();
            var items = query
                .OrderBy(c => c.CategoryName)
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(CategoryModel.Convert)
                .ToList();

            return new SearchResult<CategoryModel>()
            {
                Totals = totals,
                Items = items
            };
        }

        public SearchResult<CategoryModel> SearchByCname(string companyName, string keySearch = "", string targetItemID = "", int? targetItemType = null, bool? deleted = null, int page = 1, int limit = 10)
        {
            var cname = companyName.Standardizing().Replace(" ", "");

            var companyID = _Context.Clinics.AsEnumerable()
                .Where(c => c.ClinicName.Standardizing().Replace(" ", "") == cname)
                .Select(c => c.ClinicID.ToString())
                .FirstOrDefault();

            return Search(companyID, keySearch, targetItemID, targetItemType, deleted, page, limit);
        }

        public CategoryModel Get(string categoryID)
        {
            var id = Guid.Parse(categoryID);

            return _Context.GetCategoryQueryable()
                .Where(c => c.CategoryID == id)
                .Select(CategoryModel.Convert)
                .FirstOrDefault();
        }

        public CategoryModel Create(string clinicID, CategoryModelCreate body)
        {
            var cid = Guid.Parse(clinicID);
            var data = new Category()
            {
                CategoryName = body.CategoryName,
                ClinicID = cid,
            };

            _Context.Categories.Add(data);
            _Context.SaveChanges();

            return CategoryModel.Convert(data);
        }

        public bool ModifyMultiple(string clinicID, string targetItemID, int targetItemType, List<string> categoryIDs)
        {
            var cid = Guid.Parse(clinicID);
            var tid = Guid.Parse(targetItemID);

            var items = _Context.CategoryItems
                .Include(c => c.Category)
                .Where(c =>
                    c.Category.ClinicID == cid &&
                    c.ItemID == tid &&
                    c.ItemType == (byte)targetItemType)
                .ToList();

            _Context.CategoryItems.RemoveRange(items);
            var newItems = categoryIDs.Select(id => new CategoryItem()
            {
                CategoryID = Guid.Parse(id),
                ItemID = tid,
                ItemType = (byte)targetItemType,
            }).ToList();
            _Context.CategoryItems.AddRange(newItems);
            _Context.SaveChanges();

            return true;
        }

        public bool AttachItem(string clinicID, string categoryID, string targetItemID, int targetItemType)
        {
            var cid = Guid.Parse(clinicID);
            var tid = Guid.Parse(targetItemID);
            var id = Guid.Parse(categoryID);

            var category = _Context.Categories
                .Include(c => c.CategoryItems)
                .FirstOrDefault(c => c.CategoryID == id && c.ClinicID == cid);

            if (category == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            if (category.CategoryItems.Any(i => i.ItemID == tid && i.ItemType == targetItemType))
            {
                return true;
            }

            category.CategoryItems.Add(new CategoryItem()
            {
                ItemID = tid,
                ItemType = (byte)targetItemType,
            });
            _Context.SaveChanges();
            return true;
        }

        public bool DetachItem(string clinicID, string categoryID, string targetItemID, int targetItemType)
        {
            var cid = Guid.Parse(clinicID);
            var tid = Guid.Parse(targetItemID);
            var id = Guid.Parse(categoryID);

            var item = _Context.CategoryItems
                .Include(c => c.Category)
                .FirstOrDefault(c =>
                    c.Category.CategoryID == id &&
                    c.Category.ClinicID == cid &&
                    c.ItemID == tid &&
                    c.ItemType == (byte)targetItemType);

            if (item != null)
            {
                _Context.CategoryItems.Remove(item);
                _Context.SaveChanges();
            }

            return true;
        }

        public bool Update(string clinicID, string categoryID, CategoryModelUpdate body)
        {
            var cid = Guid.Parse(clinicID);
            var id = Guid.Parse(categoryID);

            var category = _Context.Categories
                .FirstOrDefault(c => c.CategoryID == id && c.ClinicID == cid);

            if (category == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            category.CategoryName = body.CategoryName;
            _Context.SaveChanges();

            return true;
        }

        public bool Delete(string clinicID, string categoryID)
        {
            var cid = Guid.Parse(clinicID);
            var id = Guid.Parse(categoryID);

            var category = _Context.Categories
                .FirstOrDefault(c => c.CategoryID == id && c.ClinicID == cid);

            if (category == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            _Context.SoftDelete(category);
            return true;
        }
    }
}
