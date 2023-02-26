using AppGlobal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppGlobal.Models
{
    public class ServiceModel
    {
        public Guid ServiceID { get; set; }
        public string ServiceName { get; set; }
        public Guid? ClinicID { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
        public string Logo { get; set; }
        public Guid? ParentServiceID { get; set; }
        public DateTime DateUpdated { get; set; }
        public byte StatusID { get; set; }
        public bool? IsSaleOrder { get; set; }
        public static ServiceModel Convert(Service service)
            => service != null ? new ServiceModel()
            {
                ServiceID = service.ServiceID,
                ServiceName = service.ServiceName,
                ClinicID = service.ClinicID,
                Price = service.Price,
                ShortDescription = service.ShortDescription,
                Logo = service.Logo,
                ParentServiceID = service.ParentServiceID,
                DateUpdated = service.DateUpdated,
                StatusID = service.StatusID,
                IsSaleOrder = service.IsSaleOrder,
            } : null;
    }
    public class ServiceModelDetail
    {
        public Guid ServiceID { get; set; }
        public string ServiceName { get; set; }
        public Guid? ClinicID { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
        public string Logo { get; set; }
        public Guid? ParentServiceID { get; set; }
        public DateTime DateUpdated { get; set; }
        public byte StatusID { get; set; }
        public bool? IsSaleOrder { get; set; }
        public List<ProviderModelListItem> ListProviders { get; set; }
        public static ServiceModelDetail Convert(Service service)
            => new ServiceModelDetail()
            {
                ServiceID = service.ServiceID,
                ServiceName = service.ServiceName,
                ClinicID = service.ClinicID,  
                Price = service.Price,
                ShortDescription = service.ShortDescription,
                Logo = service.Logo,
                ParentServiceID = service.ParentServiceID,
                DateUpdated = service.DateUpdated,
                StatusID = service.StatusID,
                IsSaleOrder = service.IsSaleOrder,
                ListProviders = new List<ProviderModelListItem>(),
            };
    }
    public class ServiceModelCreate
    {
        public string ServiceName { get; set; }
        public Guid? ClinicID { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
        public string Logo { get; set; }
        public Guid? ParentServiceID { get; set; }
        public bool? IsSaleOrder { get; set; }
        public List<ServiceProviderModel> Providers { get; set; }
    }
    public class ServiceModelUpdate
    {
        public string ServiceName { get; set; }
        public Guid? ClinicID { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
        public string Logo { get; set; }
        public Guid? ParentServiceID { get; set; }
        public bool? IsSaleOrder { get; set; }
        public List<ServiceProviderModel> Providers { get; set; }
    }
    public class ServiceProviderModel
    {
        public string UserID { get; set; }
        public decimal? SpecialPrice { get; set; }
    }
    public class ProviderModelListItem
    {
        public Guid ProviderID { get; set; }
        public Guid UserID { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public Guid ServiceID { get; set; }
        public Guid? ClinicID { get; set; }
        public decimal? SpecialPrice { get; set; }
        public static ProviderModelListItem Convert(Provider provider)
            => new ProviderModelListItem()
            {
                ProviderID = provider.ProviderID,
                UserID = provider.UserID,
                FullName = "",
                Avatar = "",
                Phone = "",
                ServiceID = provider.ServiceID,
                ClinicID = provider.ClinicID,
                SpecialPrice = provider.SpecialPrice,
            };
    }
}
