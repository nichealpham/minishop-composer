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
    public class ClinicBusiness
    {
        private readonly ERPContext _Context;

        public ClinicBusiness(ERPContext context)
        {
            _Context = context;
        }

        //search clinics owner user
        public SearchResult<ClinicModel> Search(string userID, int statusID = (int)StatusType.Active, string clinicName = "", int page = 1, int limit = 10)
        {
            var guid = Guid.Parse(userID);
            // get list clinic belong userID
            var query = _Context.Clinics
                .Include(r => r.Roles)
                .AsNoTracking()
                .Where(c =>
                    c.Roles.Any(r => r.UserID == guid && r.RoleType != (byte)RoleType.Patient) &&
                    c.StatusID == statusID);

            if (!string.IsNullOrEmpty(clinicName))
            {
                clinicName = clinicName.Standardizing();
                query = query.Where(c => ApplicationDBContext.Standardizing(c.ClinicName).Contains(clinicName));
            }

            var totals = query.Count();
            var result = query
                .OrderByDescending(d => d.DateCreated)
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(ClinicModel.Convert)
                .ToList();

            return new SearchResult<ClinicModel>()
            {
                Totals = totals,
                Items = result
            };
        }

        //get clinic by id
        public ClinicModelDetail GetByID(string clinicID)
        {
            var guid = Guid.Parse(clinicID);
            var clinic = _Context.Clinics
                .Include(c => c.Services)
                .AsNoTracking()
                .Where(c => c.ClinicID == guid)
                .Select(clinic => new ClinicModelDetail()
                {
                    ClinicID = clinic.ClinicID,
                    ClinicName = clinic.ClinicName,
                    ShortDescription = clinic.ShortDescription,
                    Logo = clinic.Logo,
                    Phone = clinic.Phone,
                    TaxNumber = clinic.TaxNumber,
                    Address = clinic.Address,
                    DateUpdated = clinic.DateUpdated,
                    StatusID = (byte)clinic.StatusID,
                    PublicNameUrl = clinic.PublicNameUrl,
                    PublicStatus = clinic.PublicStatus ?? false,
                    Services = clinic.Services.Select(s => new ClinicService() { 
                        ServiceID = s.ServiceID.ToString(),
                        ServiceName = s.ServiceName,
                        Price = s.Price,
                    }).ToList(),
                })
                .FirstOrDefault();

            if (clinic == null)
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);

            return clinic;
        }

        public ClinicModelDetail Create(string userID, ClinicModelCreate dataCreate)
        {
            // new clinic
            var clinic = new Clinic()
            {
                ClinicName = dataCreate.ClinicName,
                ShortDescription = dataCreate.ShortDescription,
                Logo = dataCreate.Logo,
                Phone = dataCreate.Phone,
                TaxNumber = dataCreate.TaxNumber,
                Address = dataCreate.Address,
            };
            // add role for new clinic
            var role = new Role()
            {
                UserID = Guid.Parse(userID),
                RoleType = (int)RoleType.Owner,
            };
            clinic.Roles.Add(role);

            _Context.Clinics.Add(clinic);
            _Context.SaveChanges();

            return GetByID(clinic.ClinicID.ToString());
        }

        // update clinic
        public ClinicModelDetail Update(string clinicID, ClinicModelUpdate dataUpdate)
        {
            var guid = Guid.Parse(clinicID);
            var clinic = _Context.Clinics
                .Where(c => c.ClinicID == guid)
                .FirstOrDefault();

            if (clinic == null)
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);

            clinic.ClinicName = dataUpdate.ClinicName;
            clinic.ShortDescription = dataUpdate.ShortDescription;
            clinic.Logo = dataUpdate.Logo;
            clinic.Phone = dataUpdate.Phone;
            clinic.TaxNumber = dataUpdate.TaxNumber;
            clinic.Address = dataUpdate.Address;
            clinic.PublicNameUrl = dataUpdate.PublicNameUrl;

            _Context.SaveChanges();

            return GetByID(clinicID);
        }

        // soft delete clinic
        public bool Delete(string clinicID)
        {
            var guid = Guid.Parse(clinicID);
            var clinic = _Context.Clinics
                .Where(c => c.ClinicID == guid)
                .FirstOrDefault();

            if (clinic == null)
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);

            _Context.SoftDelete(clinic);
            return true;
        }
    }
}
