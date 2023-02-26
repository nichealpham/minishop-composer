using AppGlobal.Constants;
using AppGlobal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppGlobal.Models
{
    public class UserStatistics
    {
        public int CountPatients { get; set; }
        public int CountDoctors { get; set; }
        public int CountClinics { get; set; }
        public int CountEpisodes { get; set; }
        public int CountAppointments { get; set; }
    }

    public class RevenueStatistics
    {
        public decimal Day { get; set; }
        public decimal Month { get; set; }
        public decimal Quater { get; set; }
    }

    public class AssetInStock
    {
        public Guid AssetID { get; set; }
        public string AssetName { get; set; }
        public string AssetCode { get; set; }
        public string TypeName { get; set; }
        public int Amount { get; set; }
        public decimal? Price { get; set; }
        public string AssetQrUri { get; set; }
        public Guid UserCreatedID { get; set; }
        public string UserCreatedUri { get; set; }
        public string UserCreatedName { get; set; }
        public DateTime DateUpdated { get; set; }

        public static AssetInStock Convert(Asset a) => a == null ? null : new AssetInStock()
        {
            AssetID = a.AssetID,
            AssetName = a.AssetName,
            AssetCode = a.AssetCode,
            TypeName = Enum.GetName(typeof(AssetType), a.Type),
            Amount = a.Amount,
            Price = a.Price,
            AssetQrUri = string.Format(@"/home?assetid={0}", a.AssetID.ToString()),
            UserCreatedID = a.UserCreatedID,
            UserCreatedUri = string.Format(@"/patient?id={0}", a.UserCreatedID.ToString()),
            DateUpdated = a.DateUpdated.ToLocalTime(),
        };
    }

    public class AssetImport
    {
        public decimal? Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }

        public Guid UserCreatedID { get; set; }
        public string UserCreatedName { get; set; }
        public string UserCreatedUri { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public AssetInStock Asset { get; set; }

        public static AssetImport Convert(AssetImported a) => a == null ? null : new AssetImport()
        {
            Price = a.Price ?? a.Asset?.Price,
            Amount = a.Amount,
            Description = a.Description,

            UserCreatedID = a.UserCreatedID,
            UserCreatedName = "",
            UserCreatedUri = string.Format(@"/patient?id={0}", a.UserCreatedID.ToString()),

            DateCreated = a.DateCreated.ToLocalTime(),
            DateUpdated = a.DateUpdated.ToLocalTime(),

            Asset = AssetInStock.Convert(a.Asset),
        };
    }

    public class AssetRevenue
    {
        public decimal? Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public Guid ServiceID { get; set; }
        public string ServiceName { get; set; }
        public Guid EpisodeID { get; set; }
        public string EpisodeUri { get; set; }
        public Guid UserCreatedID { get; set; }
        public string UserCreatedName { get; set; }
        public string UserCreatedUri { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public AssetInStock Asset { get; set; }
        public static AssetRevenue Convert(AssetConsumed a) => a == null ? null : new AssetRevenue()
        {
            Price = a.Price ?? a.Asset?.Price,
            Amount = a.Amount,
            Description = a.Description,

            ServiceID = a.ServiceID,
            ServiceName = "",

            EpisodeID = a.EpisodeID,
            EpisodeUri = string.Format(@"/episode?id={0}", a.EpisodeID.ToString()),

            UserCreatedID = a.UserCreatedID,
            UserCreatedName = "",
            UserCreatedUri = string.Format(@"/patient?id={0}", a.UserCreatedID.ToString()),

            DateCreated = a.DateCreated.ToLocalTime(),
            DateUpdated = a.DateUpdated.ToLocalTime(),

            Asset = AssetInStock.Convert(a.Asset),
        };
    }

    public class ServiceRevenue
    {
        // General section
        public string EpisodeID { get; set; }
        public string EpisodeUri { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public decimal TotalPrice { get; set; }
        // Patient Section
        public string UserAdmittedUri { get; set; }
        public string UserAdmittedName { get; set; }
        public string UserAdmittedPhone { get; set; }
        // Dcotor Section
        public string UserAppointUri { get; set; }
        public string UserAppointName { get; set; }
        public string UserAppointPhone { get; set; }
        // Service Section
        public string ServiceName { get; set; }
        public static ServiceRevenue Convert(Episode e) {
            if (e == null) return null;

            var total = e.Invoices.FirstOrDefault().FinalPrice;
            var record = e.Records?.FirstOrDefault();

            var item = new ServiceRevenue()
            {
                EpisodeID = e.EpisodeID.ToString(),
                EpisodeUri = string.Format(@"/episode?id={0}", e.EpisodeID.ToString()),
                TimeStart = e.TimeStart.ToLocalTime(),
                TimeEnd = e.TimeEnd.HasValue ? e.TimeEnd.Value.ToLocalTime() : e.TimeStart.ToLocalTime().AddMinutes(30),
                TotalPrice = total,

                UserAdmittedUri = string.Format(@"/patient?id={0}", e.UserAdmittedID.ToString()),
                UserAdmittedName = e.UserAdmitted?.FullName,
                UserAdmittedPhone = e.UserAdmitted?.Phone,

                UserAppointUri = string.Format(@"/patient?id={0}", record.UserAppointID.ToString()),
                UserAppointName = record.UserAppoint?.FullName,
                UserAppointPhone = record.UserAppoint?.Phone,

                ServiceName = record.Service?.ServiceName,
            };

            return item;
        } 
    }
}
