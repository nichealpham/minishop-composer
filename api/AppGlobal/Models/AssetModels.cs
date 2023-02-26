using AppGlobal.Constants;
using AppGlobal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppGlobal.Models
{
    public class AssetModel
    {
        public Guid AssetID { get; set; }
        public string AssetName { get; set; }
        public string AssetCode { get; set; }
        public string Description { get; set; }
        public byte Type { get; set; }
        public int Amount { get; set; }
        public decimal? Price { get; set; }
        public Guid ClinicID { get; set; }
        public Guid UserCreatedID { get; set; }
        public string AssetQrUri { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool? IsSale { get; set; }
        public string SaleDescription { get; set; }

        public static AssetModel Convert(Asset a) => a == null ? null : new AssetModel()
        {
            AssetID = a.AssetID,
            AssetName = a.AssetName,
            AssetCode = a.AssetCode,
            Description = a.Description,
            Type = a.Type,
            Amount = a.Amount,
            Price = a.Price,
            ClinicID = a.ClinicID,
            AssetQrUri = string.Format(@"/home?assetid={0}", a.AssetID.ToString()),
            UserCreatedID = a.UserCreatedID,
            DateCreated = a.DateCreated.ToLocalTime(),
            DateUpdated = a.DateUpdated.ToLocalTime(),
            IsSale = a.IsSale,
            SaleDescription = a.SaleDescription,
        };

        public static Asset Convert(AssetModelCreate a) => a == null ? null : new Asset()
        {
            AssetName = a.AssetName,
            AssetCode = a.AssetCode,
            Description = a.Description,
            Type = a.Type ?? (byte)AssetType.Asset,
            Amount = a.Amount ?? 1,
            Price = a.Price,
            IsSale = a.IsSale,
            SaleDescription = a.SaleDescription,
        };
    }

    public class AssetModelCreate
    {
        public string AssetName { get; set; }
        public string AssetCode { get; set; }
        public string Description { get; set; }
        public byte? Type { get; set; }
        public int? Amount { get; set; }
        public decimal? Price { get; set; }
        public bool? IsSale { get; set; }
        public string SaleDescription { get; set; }
    }

    public class AssetModelUpdate
    {
        public string AssetName { get; set; }
        public string AssetCode { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public bool? IsSale { get; set; }
        public string SaleDescription { get; set; }
    }

    public class AssetImportModel
    {
        public Guid AssetImportedID { get; set; }
        public Guid AssetID { get; set; }
        public int Amount { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public Guid UserCreatedID { get; set; }
        public AssetModel Asset { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public static AssetImportModel Convert(AssetImported a) => a == null ? null : new AssetImportModel()
        {
            AssetImportedID = a.AssetImportedID,
            AssetID = a.AssetID,
            Amount = a.Amount,
            Price = a.Price,
            Description = a.Description,
            UserCreatedID = a.UserCreatedID,
            Asset = AssetModel.Convert(a.Asset),
            DateCreated = a.DateCreated.ToLocalTime(),
            DateUpdated = a.DateUpdated.ToLocalTime(),
        };

        public static AssetImported Convert(AssetImportCreate a) => a == null ? null : new AssetImported()
        {
            Amount = a.Amount,
            Description = a.Description,
        };
    }

    public class AssetImportCreate
    {
        public int Amount { get; set; }
        public string Description { get; set; }
    }

    public class AssetConsumedModel
    {
        public Guid AssetConsumedID { get; set; }
        public Guid AssetID { get; set; }
        public decimal? Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public Guid ServiceID { get; set; }
        public Guid EpisodeID { get; set; }
        public Guid UserCreatedID { get; set; }
        public AssetModel Asset { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public static AssetConsumedModel Convert(AssetConsumed a) => a == null ? null : new AssetConsumedModel()
        {
            AssetConsumedID = a.AssetConsumedID,
            AssetID = a.AssetID,
            Price = a.Price ?? a.Asset?.Price,
            Amount = a.Amount,
            Description = a.Description,
            ServiceID = a.ServiceID,
            EpisodeID = a.EpisodeID,
            UserCreatedID = a.UserCreatedID,
            Asset = AssetModel.Convert(a.Asset),
            DateCreated = a.DateCreated.ToLocalTime(),
            DateUpdated = a.DateUpdated.ToLocalTime(),
        };

        public static AssetConsumed Convert(AssetConsumedCreate a) => a == null ? null : new AssetConsumed()
        {
            AssetID = Guid.Parse(a.AssetID),
            Price = a.Price,
            Amount = a.Amount,
            Description = a.Description,
            ServiceID = Guid.Parse(a.ServiceID),
            EpisodeID = Guid.Parse(a.EpisodeID),
        };
    }

    public class AssetConsumedCreate
    {
        public string AssetID { get; set; }
        public decimal? Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string ServiceID { get; set; }
        public string EpisodeID { get; set; }
    }

    public class AssetConsumedUpdate
    {
        public decimal? Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
    }
}
