using AppGlobal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGlobal.Models
{
    public class PublicCompanyModel
    {
        public Guid CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string ShortDescription { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateUpdated { get; set; }
        public byte StatusID { get; set; }
        public static PublicCompanyModel Convert(Clinic clinic)
            => new PublicCompanyModel()
            {
                CompanyID = clinic.ClinicID,
                CompanyName = clinic.ClinicName,
                ShortDescription = clinic.ShortDescription,
                Logo = clinic.Logo,
                Phone = clinic.Phone,
                TaxNumber = clinic.TaxNumber,
                Address = clinic.Address,
                DateUpdated = clinic.DateUpdated,
                StatusID = (byte)clinic.StatusID,
            };
    }

    public class PublicOrderCreate
    {
        public string ShippingAddress { get; set; }
        public string OrderNote { get; set; }
        public List<PublicOrderItem> OrderItems { get; set; }
    }

    public class PublicOrderItem
    {
        public string ProductID { get; set; }
        public int Amount { get; set; }
    }

    public class PublicImageModel
    {
        public Guid AttachmentID { get; set; }
        public string ImageUrl { get; set; }
    }

    public class PublicCategoryModel
    {
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
    }

    public class PublicBlogModel
    {
        public Guid BlogID { get; set; }
        public Guid? CompanyID { get; set; }
        public string BlogTitle { get; set; }
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public List<PublicImageModel> Images { get; set; }
        public List<PublicCategoryModel> Categories { get; set; }
        public string AuthorName { get; set; }
        public int ViewCount { get; set; }

        public static PublicBlogModel Convert(Record e) => e == null ? null : new PublicBlogModel()
        {
            BlogID = e.EpisodeID,
            CompanyID = e.Episode.ClinicID,
            BlogTitle = e.Episode.PublicTitle,
            ShortDescription = e.Episode.PublicHeadline,
            DetailDescription = "",
            DateCreated = e.DateCreated.ToLocalTime(),
            DateUpdated = e.DateUpdated.ToLocalTime(),
            Images = new List<PublicImageModel>(),
            Categories = new List<PublicCategoryModel>(),
            AuthorName = e.UserAppoint?.FullName,
            ViewCount = e.Episode.ViewCount ?? 0,
        };

        public static PublicBlogModel ConvertDetail(Record e) => e == null ? null : new PublicBlogModel()
        {
            BlogID = e.EpisodeID,
            CompanyID = e.Episode.ClinicID,
            BlogTitle = e.Episode.PublicTitle,
            ShortDescription = e.Episode.PublicHeadline,
            DetailDescription = e.ClinicalResult,
            DateCreated = e.DateCreated.ToLocalTime(),
            DateUpdated = e.DateUpdated.ToLocalTime(),
            Images = new List<PublicImageModel>(),
            Categories = new List<PublicCategoryModel>(),
            AuthorName = e.UserAppoint?.FullName,
            ViewCount = e.Episode.ViewCount ?? 0,
        };
    }

    public class PublicProductModel
    {
        public Guid ProductID { get; set; }
        public Guid CompanyID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public byte Type { get; set; }
        public int Amount { get; set; }
        public decimal? Price { get; set; }
        public string ProductQrUri { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string ShortDescription { get; set; }
        public string DetailDescription { get; set; }
        public List<PublicImageModel> Images { get; set; }
        public List<PublicCategoryModel> Categories { get; set; }

        public static PublicProductModel Convert(Asset a) => a == null ? null : new PublicProductModel()
        {
            ProductID = a.AssetID,
            CompanyID = a.ClinicID,
            ProductName = a.AssetName,
            ProductCode = a.AssetCode,
            Type = a.Type,
            Amount = a.Amount,
            Price = a.Price,
            ProductQrUri = "",
            DetailDescription = "",
            DateCreated = a.DateCreated.ToLocalTime(),
            DateUpdated = a.DateUpdated.ToLocalTime(),
            ShortDescription = a.SaleDescription,
            Images = new List<PublicImageModel>(),
            Categories = new List<PublicCategoryModel>(),
        };

        public static PublicProductModel ConvertDetail(Asset a) => a == null ? null : new PublicProductModel()
        {
            ProductID = a.AssetID,
            CompanyID = a.ClinicID,
            ProductName = a.AssetName,
            ProductCode = a.AssetCode,
            Type = a.Type,
            Amount = a.Amount,
            Price = a.Price,
            ProductQrUri = "",
            DetailDescription = a.Description,
            DateCreated = a.DateCreated.ToLocalTime(),
            DateUpdated = a.DateUpdated.ToLocalTime(),
            ShortDescription = a.SaleDescription,
            Images = new List<PublicImageModel>(),
            Categories = new List<PublicCategoryModel>(),
        };
    }
}
