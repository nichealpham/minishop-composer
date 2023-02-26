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
using Microsoft.Extensions.Configuration;

namespace Api.Business
{
    public class AssetBusiness
    {
        private readonly ERPContext _Context;
        private readonly string _SandrasoftUri;

        public AssetBusiness(ERPContext context, IConfiguration Configuration)
        {
            _Context = context;
            _SandrasoftUri = Configuration["SandrasoftUri"];
        }

        #region Asset API
        public SearchResult<AssetModel> GetAssets(string clinicID, string keySearch, int? statusID = (int)StatusType.Active, int page = 1, int limit = 10)
        {
            var cid = Guid.Parse(clinicID);
            var query = _Context.Assets.AsNoTracking()
                .Where(a => a.StatusID == statusID)
                .Where(a => a.ClinicID == cid);

            if (!string.IsNullOrEmpty(keySearch))
            {
                keySearch = keySearch.Standardizing();
                query = query.Where(a =>
                    (!string.IsNullOrEmpty(a.AssetName) && ApplicationDBContext.Standardizing(a.AssetName).Contains(keySearch)) ||
                    (!string.IsNullOrEmpty(a.AssetCode) && ApplicationDBContext.Standardizing(a.AssetCode).Contains(keySearch)));
            }

            var totals = query.Count();
            var items = query
                .OrderByDescending(p => p.DateCreated)
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(AssetModel.Convert)
                .ToList();

            items.ForEach(item => item.AssetQrUri = _SandrasoftUri + item.AssetQrUri);

            return new SearchResult<AssetModel>()
            {
                Totals = totals,
                Items = items
            };
        }

        public AssetModel GetAssetDetail(string assetID)
        {
            var aid = Guid.Parse(assetID);
            var result = _Context.Assets.AsNoTracking()
                .Where(a => a.AssetID == aid)
                .Select(AssetModel.Convert)
                .FirstOrDefault();
            result.AssetQrUri = _SandrasoftUri + result.AssetQrUri;
            return result;
        }

        public AssetModel CreateAsset(string clinicID, string userID, AssetModelCreate data)
        {
            var cid = Guid.Parse(clinicID);
            var uid = Guid.Parse(userID);

            var asset = AssetModel.Convert(data);
            asset.ClinicID = cid;
            asset.UserCreatedID = uid;

            var import = new AssetImported()
            {
                Amount = asset.Amount,
                Price = asset.Price,
                Description = "Asset created first time",
                UserCreatedID = uid,
            };
            asset.AssetImporteds.Add(import);

            _Context.Assets.Add(asset);
            _Context.SaveChanges();

            var result = AssetModel.Convert(asset);
            result.AssetQrUri = _SandrasoftUri + result.AssetQrUri;
            return result;
        }

        public AssetModel UpdateAsset(string assetID, string userID, AssetModelUpdate data)
        {
            var aid = Guid.Parse(assetID);
            var uid = Guid.Parse(userID);

            var asset = _Context.Assets.Where(a => a.AssetID == aid).FirstOrDefault();
            if (asset == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            asset.AssetName = data.AssetName;
            asset.AssetCode = data.AssetCode;
            asset.Description = data.Description;
            asset.Price = data.Price;
            asset.UserCreatedID = uid;
            asset.IsSale = data.IsSale;
            asset.SaleDescription = data.SaleDescription;

            _Context.SaveChanges();

            var result = AssetModel.Convert(asset);
            result.AssetQrUri = _SandrasoftUri + result.AssetQrUri;
            return result;
        }

        public bool DeleteAsset(string assetID)
        {
            var aid = Guid.Parse(assetID);
            var asset = _Context.Assets.Where(a => a.AssetID == aid).FirstOrDefault();
            if (asset == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            _Context.SoftDelete(asset);

            return true;
        }
        #endregion

        #region Asset Import APIs
        public AssetImportModel ImportAsset(string assetID, string userID, AssetImportCreate data)
        {
            var aid = Guid.Parse(assetID);
            var uid = Guid.Parse(userID);

            var asset = _Context.Assets.FirstOrDefault(a => a.AssetID == aid);
            if (asset == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            var item = AssetImportModel.Convert(data);
            item.UserCreatedID = uid;

            asset.Amount += data.Amount;
            asset.AssetImporteds.Add(item);

            _Context.SaveChanges();

            return AssetImportModel.Convert(item);
        }
        #endregion

        #region Asset Consume APIs
        public SearchResult<AssetConsumedModel> SearchConsumeAsset(string episodeID, string keySearch, int page = 1, int limit = 10)
        {
            var eid = Guid.Parse(episodeID);
            var query = _Context.AssetConsumeds.AsNoTracking()
                .Include(a => a.Asset)
                .Where(c => c.EpisodeID == eid);

            if (!string.IsNullOrEmpty(keySearch))
            {
                keySearch = keySearch.Standardizing();
                query = query.Where(c => ApplicationDBContext.Standardizing(c.Asset.AssetName).Contains(keySearch));
            }

            var totals = query.Count();
            var items = query
                .OrderByDescending(p => p.DateCreated)
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(AssetConsumedModel.Convert)
                .ToList();

            return new SearchResult<AssetConsumedModel>()
            {
                Totals = totals,
                Items = items
            };
        }

        public AssetConsumedModel GetConsumeAsset(string assetConsumeID)
        {
            var ac = Guid.Parse(assetConsumeID);
            var item = _Context.AssetConsumeds.AsNoTracking()
                .Include(a => a.Asset)
                .Where(c => c.AssetConsumedID == ac)
                .Select(AssetConsumedModel.Convert)
                .FirstOrDefault();

            if (item == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            return item;
        }

        public AssetConsumedModel CreateConsumeAsset(string userID, AssetConsumedCreate data)
        {
            var aid = Guid.Parse(data.AssetID);
            var uid = Guid.Parse(userID);

            if (data.Amount <= 0 || (data.Price.HasValue && data.Price.Value <= 0))
            {
                throw new ApiError((int)ErrorCodes.FailedValidationData);
            }

            var asset = _Context.Assets.FirstOrDefault(a => a.AssetID == aid);
            if (asset == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            var item = AssetConsumedModel.Convert(data);
            item.UserCreatedID = uid;

            // Update in-stock only when = consumable asset
            if (asset.Type == (byte)AssetType.Consumable)
            {
                // If amount in stock < updated amount
                if (asset.Amount < data.Amount)
                {
                    // throw error
                    throw new ApiError((int)ErrorCodes.FailedValidationData);
                }
                asset.Amount -= data.Amount;
            }
            asset.AssetConsumeds.Add(item);

            _Context.SaveChanges();

            return AssetConsumedModel.Convert(item);
        }

        public AssetConsumedModel UpdateConsumeAsset(string assetConsumeID, string userID, AssetConsumedUpdate data)
        {
            var acid = Guid.Parse(assetConsumeID);
            var uid = Guid.Parse(userID);

            if (data.Amount <= 0 || (data.Price.HasValue && data.Price.Value <= 0))
            {
                throw new ApiError((int)ErrorCodes.FailedValidationData);
            }

            var item = _Context.AssetConsumeds.Include(a => a.Asset).FirstOrDefault(a => a.AssetConsumedID == acid);
            if (item == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var asset = item.Asset;

            // update in-tock amount
            // Update in-stock only when = consumable asset
            if (asset.Type == (byte)AssetType.Consumable)
            {
                // If amount in stock + amount registered < updated amount
                if (asset.Amount + item.Amount < data.Amount)
                {
                    // throw error
                    throw new ApiError((int)ErrorCodes.FailedValidationData);
                }
                asset.Amount -= (data.Amount - item.Amount);
            }
            // then, update asset conbsume item
            item.UserCreatedID = uid;
            item.Amount = data.Amount;
            item.Price = data.Price;
            item.Description = data.Description;

            _Context.SaveChanges();

            return AssetConsumedModel.Convert(item);
        }

        public bool DeleteConsumeAsset(string assetConsumeID)
        {
            var acid = Guid.Parse(assetConsumeID);
            
            var item = _Context.AssetConsumeds.Include(a => a.Asset).FirstOrDefault(a => a.AssetConsumedID == acid);
            if (item == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }
            var asset = item.Asset;

            // Update in-stock only when = consumable asset
            if (asset.Type == (byte)AssetType.Consumable)
            {
                asset.Amount += item.Amount;
            }

            _Context.AssetConsumeds.Remove(item);
            _Context.SaveChanges();

            return true;
        }
        #endregion
    }
}
