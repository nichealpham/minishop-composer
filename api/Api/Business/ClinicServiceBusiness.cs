using AppGlobal.Commons;
using AppGlobal.Constants;
using AppGlobal.DBContext;
using AppGlobal.Entities;
using AppGlobal.Extensions;
using AppGlobal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Business
{
    public class ClinicServiceBusiness
    {
        private readonly ERPContext _Context;

        public ClinicServiceBusiness(ERPContext context)
        {
            _Context = context;
        }

        // search services
        public SearchResult<ServiceModel> Search(string userID, string clinicID, int statusID = (int)StatusType.Active, string serviceName = "", int page = 1, int limit = 10)
        {
            var guid = Guid.Parse(userID);
            var clinicIDs = new List<Guid>();

            // get list services belong userID and clinicID
            var query = _Context.Services
                .Include(r => r.Providers)
                .AsNoTracking()
                .Where(c => c.StatusID == statusID);

            if (!string.IsNullOrEmpty(clinicID))
            {
                query = query.Where(s => s.ClinicID == Guid.Parse(clinicID));
            }
            else
            {
                clinicIDs = _Context.Roles.AsNoTracking()
                    .Where(r => r.UserID == guid)
                    .Select(c => c.ClinicID)
                    .ToList();
                query = query.Where(c =>
                    // user is one of the providers or...
                    (c.Providers.Any(r => r.UserID == Guid.Parse(userID))) ||
                    // user own these clinics, somehow.
                    (c.ClinicID != null && clinicIDs.Contains(c.ClinicID.Value)));
            }
            
            if (!string.IsNullOrEmpty(serviceName))
            {
                serviceName = serviceName.Standardizing();
                query = query.Where(c => ApplicationDBContext.Standardizing(c.ServiceName).Contains(serviceName));
            }

            var totals = query.Count();
            var result = query
                .OrderByDescending(d => d.DateCreated)
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(ServiceModel.Convert)
                .ToList();

            return new SearchResult<ServiceModel>()
            {
                Totals = totals,
                Items = result
            };
        }

        // get service by id
        public ServiceModelDetail GetByID(string serviceID)
        {
            var guid = Guid.Parse(serviceID);
            //get service by id
            var service = _Context.Services.AsNoTracking()
                .Include(s => s.Providers)
                    .ThenInclude(p => p.User)
                .Where(s => s.ServiceID == guid)
                .Select(service => new ServiceModelDetail()
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
                    ListProviders = service.Providers.Select(provider => new ProviderModelListItem()
                    {
                        ProviderID = provider.ProviderID,
                        UserID = provider.UserID,
                        FullName = provider.User.FullName,
                        Avatar = provider.User.Avatar,
                        Phone = provider.User.Phone,
                        ServiceID = provider.ServiceID,
                        ClinicID = provider.ClinicID,
                        SpecialPrice = provider.SpecialPrice,
                    }).ToList(),
                })
                .FirstOrDefault();

            if (service == null)
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);

            return service;
        }

        public ServiceModelDetail Create(ServiceModelCreate dataCreate)
        {
            // if sale order configed
            if (dataCreate.IsSaleOrder == true)
            {
                var services = _Context.Services.Where(s => s.ClinicID == dataCreate.ClinicID).ToList();
                services.ForEach(s => s.IsSaleOrder = false);
                _Context.SaveChanges();
            }

            // new clinic
            var service = new Service()
            {
                ServiceName = dataCreate.ServiceName,
                ClinicID = dataCreate.ClinicID,
                Price = dataCreate.Price,
                ShortDescription = dataCreate.ShortDescription,
                Logo = dataCreate.Logo,
                ParentServiceID = dataCreate.ParentServiceID,
                IsSaleOrder = dataCreate.IsSaleOrder,
            };

            // exist new provider
            if (dataCreate.Providers!= null && dataCreate.Providers.Count > 0)
            {
                foreach (var provider in dataCreate.Providers)
                {
                    service.Providers.Add(new Provider()
                    {
                        ClinicID = dataCreate.ClinicID,
                        UserID = Guid.Parse(provider.UserID),
                        SpecialPrice = provider.SpecialPrice,
                    });
                }
            }
            _Context.Services.Add(service);
            _Context.SaveChanges();

            return GetByID(service.ServiceID.ToString());
        }

        // update service without update providers
        public ServiceModelDetail Update(string serviceID, ServiceModelUpdate dataUpdate)
        {
            var guid = Guid.Parse(serviceID);
            // find service
            var service = _Context.Services.Find(guid);
            if (service == null)
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);

            // if sale order configed
            if (dataUpdate.IsSaleOrder == true)
            {
                var services = _Context.Services.Where(s => s.ClinicID == dataUpdate.ClinicID).ToList();
                services.ForEach(s => s.IsSaleOrder = false);
                _Context.SaveChanges();
            }

            // update service
            service.ServiceName = dataUpdate.ServiceName;
            service.ClinicID = dataUpdate.ClinicID;
            service.Price = dataUpdate.Price;
            service.ShortDescription = dataUpdate.ShortDescription;
            service.Logo = dataUpdate.Logo;
            service.ParentServiceID = dataUpdate.ParentServiceID;
            service.IsSaleOrder = dataUpdate.IsSaleOrder;

            var providers = _Context.Providers.Where(p => p.ServiceID == guid).ToList();
            _Context.Providers.RemoveRange(providers);
            // exist new provider
            if (dataUpdate.Providers != null && dataUpdate.Providers.Count > 0)
            {
                foreach (var provider in dataUpdate.Providers)
                {
                    service.Providers.Add(new Provider()
                    {
                        ClinicID = service.ClinicID,
                        UserID = Guid.Parse(provider.UserID),
                        SpecialPrice = provider.SpecialPrice,
                    });
                }
            }

            // save service
            _Context.SaveChanges();
            return GetByID(serviceID);
        }

        // soft delete service
        public bool Delete(string serviceID)
        {
            var service = _Context.Services.Find(Guid.Parse(serviceID));
            if (service == null)
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);

            _Context.SoftDelete(service);
            return true;
        }
    }
}
