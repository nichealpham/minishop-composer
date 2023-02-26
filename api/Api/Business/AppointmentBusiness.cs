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
    public static class AppointmentHelper
    {
        public static IQueryable<Appointment> GetAppointmentQueryable(this ERPContext _Context)
        {
            return _Context.Appointments
                .Include(a => a.Clinic)
                .Include(a => a.Service)
                .Include(a => a.UserAppoint)
                .Include(a => a.UserCreate)
                .AsNoTracking();
        }

        public static SearchResult<AppointmentModel> GetAppointmentResults(this IQueryable<Appointment> query, string keySearch, DateTime? timeStart = null, DateTime? timeEnd = null, int? statusID = null, int page = 1, int limit = 10)
        {
            if (statusID.HasValue)
            {
                query = query.Where(a => a.StatusID == statusID);
            }
            else
            {
                query = query.Where(a => a.StatusID != (int)StatusType.Deleted);
            }

            if (!string.IsNullOrEmpty(keySearch))
            {
                keySearch = keySearch.Standardizing();
                query = query.Where(a =>
                    (a.Service != null && !string.IsNullOrEmpty(a.Service.ServiceName) && ApplicationDBContext.Standardizing(a.Service.ServiceName).Contains(keySearch)) ||
                    (a.UserAppoint != null && !string.IsNullOrEmpty(a.UserAppoint.FullName) && ApplicationDBContext.Standardizing(a.UserAppoint.FullName).Contains(keySearch)) ||
                    (a.UserCreate != null && !string.IsNullOrEmpty(a.UserCreate.FullName) && ApplicationDBContext.Standardizing(a.UserCreate.FullName).Contains(keySearch)));
            }

            if (timeStart.HasValue)
            {
                timeStart = timeStart.Value.StartOfDay().ToUniversalTime();
                query = query.Where(a => a.TimeStart >= timeStart);
            }

            if (timeEnd.HasValue)
            {
                timeEnd = timeEnd.Value.EndOfDay().ToUniversalTime();
                query = query.Where(a => a.TimeEnd <= timeEnd);
            }

            var totals = query.Count();
            var items = query
                .OrderBy(p => p.TimeStart)
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(AppointmentModel.Convert)
                .ToList();

            return new SearchResult<AppointmentModel>()
            {
                Totals = totals,
                Items = items
            };
        }
    }

    public class AppointmentBusiness
    {
        private readonly ERPContext _Context;
        private readonly ActivityBusiness _ActivityBusiness;
        public AppointmentBusiness(ERPContext context, ActivityBusiness ActivityBusiness)
        {
            _Context = context;
            _ActivityBusiness = ActivityBusiness;
        }

        public SearchResult<AppointmentModel> Search(string userID, string keySearch, DateTime? timeStart, DateTime? timeEnd, int? statusID = null, int page = 1, int limit = 10)
        {
            var guid = Guid.Parse(userID);

            return _Context
                .GetAppointmentQueryable()
                .Where(a => a.UserAppointID == guid || a.UserCreateID == guid)
                .GetAppointmentResults(keySearch, timeStart, timeEnd, statusID, page, limit);
        }

        public SearchResult<AppointmentModel> Search(List<string> clinicIDs, string keySearch, DateTime? timeStart, DateTime? timeEnd, int? statusID = null, int page = 1, int limit = 10)
        {
            var guids = clinicIDs.Select(c => Guid.Parse(c)).ToList();

            return _Context
                .GetAppointmentQueryable()
                .Where(a =>
                    a.ClinicID.HasValue &&
                    guids.Contains(a.ClinicID.Value))
                .GetAppointmentResults(keySearch, timeStart, timeEnd, statusID, page, limit);
        }

        public AppointmentDetail GetByID(string appointmentID)
        {
            var guid = Guid.Parse(appointmentID);

            var result = _Context.GetAppointmentQueryable()
                .Where(a => a.AppointmentID == guid)
                .Select(AppointmentDetail.Convert)
                .FirstOrDefault();

            // Map Episode, ClinicalResult, DoctorNote if existed
            var record = _Context.Records.Include(r => r.Episode)
                .AsNoTracking()
                .Where(r => r.Episode.AppointmentID == guid)
                .Select(r => new { r.ClinicalResult, r.DoctorNote, r.Episode.EpisodeID })
                .FirstOrDefault();

            if (record != null)
            {
                result.EpisodeID = record.EpisodeID;
                result.ClinicalResult = record.ClinicalResult;
                result.DoctorNote = record.DoctorNote;
            }

            return result;
        }

        public AppointmentDetail Create(AppointmentPatientCreate data, string userCreateID)
        {
            var pguid = Guid.Parse(data.ProviderID);
            var uguid = Guid.Parse(userCreateID);

            var provider = _Context.Providers.AsNoTracking()
                .Where(p => p.ProviderID == pguid)
                .FirstOrDefault();

            if (provider == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            var appointment = new Appointment()
            {
                TimeStart = data.TimeStart.ToUniversalTime(),
                TimeEnd = data.TimeEnd.ToUniversalTime(),
                ChiefComplaint = data.ChiefComplaint,
                UserAppointID = provider.UserID,
                ServiceID = provider.ServiceID,
                ClinicID = provider.ClinicID,
                SpecialPrice = provider.SpecialPrice,
                UserCreateID = uguid,
            };

            _Context.Appointments.Add(appointment);

            // Also link user to clinic if not existed
            if (provider.ClinicID.HasValue && !_Context.Roles.Any(r => r.ClinicID == provider.ClinicID && r.UserID == uguid))
            {
                var role = new Role()
                {
                    ClinicID = provider.ClinicID.Value,
                    UserID = uguid,
                    RoleType = (byte)RoleType.Patient
                };

                _Context.Roles.Add(role);
            }

            _Context.SaveChanges();

            var result = GetByID(appointment.AppointmentID.ToString());

            _ActivityBusiness.Create(appointment.UserCreateID.ToString(), new ActivityCreate()
            {
                ServiceCode = (int)ServiceCode.Booking,
                ActionCode = (int)ActionCode.Created,
                StatusCode = (int)StatusCode.Success,
                ClinicID = appointment.ClinicID,
                TargetItemID = appointment.AppointmentID,
                Metadata = result.ToJsonString(),
            });
            _ActivityBusiness.Create(appointment.UserAppointID.ToString(), new ActivityCreate()
            {
                ServiceCode = (int)ServiceCode.Booking,
                ActionCode = (int)ActionCode.Booked,
                StatusCode = (int)StatusCode.Info,
                ClinicID = appointment.ClinicID,
                TargetItemID = appointment.AppointmentID,
                Metadata = result.ToJsonString(),
            });

            return result;
        }

        public AppointmentDetail Create(string clinicID, AppointmentClinicCreate data)
        {
            var cguid = Guid.Parse(clinicID);
            var pguid = Guid.Parse(data.PatientID);
            var dguid = Guid.Parse(data.DoctorID);
            var sguid = Guid.Parse(data.ServiceID);

            var appointment = new Appointment()
            {
                TimeStart = data.TimeStart.ToUniversalTime(),
                TimeEnd = data.TimeEnd.ToUniversalTime(),
                ChiefComplaint = data.ChiefComplaint,
                ClinicalNote = data.ClinicalNote,
                UserAppointID = dguid,
                UserCreateID = pguid,
                ServiceID = sguid,
                ClinicID = cguid,
                SpecialPrice = data.SpecialPrice,
            };

            _Context.Appointments.Add(appointment);

            // Also link user to clinic if not existed
            if (!_Context.Roles.Any(r => r.ClinicID == cguid && r.UserID == pguid))
            {
                var role = new Role()
                {
                    ClinicID = cguid,
                    UserID = pguid,
                    RoleType = (byte)RoleType.Patient
                };

                _Context.Roles.Add(role);
            }

            _Context.SaveChanges();

            var result = GetByID(appointment.AppointmentID.ToString());

            _ActivityBusiness.Create(appointment.UserCreateID.ToString(), new ActivityCreate()
            {
                ServiceCode = (int)ServiceCode.Booking,
                ActionCode = (int)ActionCode.Booked,
                StatusCode = (int)StatusCode.Info,
                ClinicID = appointment.ClinicID,
                TargetItemID = appointment.AppointmentID,
                Metadata = result.ToJsonString(),
            });
            _ActivityBusiness.Create(appointment.UserAppointID.ToString(), new ActivityCreate()
            {
                ServiceCode = (int)ServiceCode.Booking,
                ActionCode = (int)ActionCode.Created,
                StatusCode = (int)StatusCode.Success,
                ClinicID = appointment.ClinicID,
                TargetItemID = appointment.AppointmentID,
                Metadata = result.ToJsonString(),
            });

            return result;
        }

        public AppointmentDetail Update(string appointmentID, AppointmentUpdate data, string userUpdateID)
        {
            var aguid = Guid.Parse(appointmentID);
            var uguid = Guid.Parse(userUpdateID);

            var appointment = _Context.Appointments
                .Where(a => a.AppointmentID == aguid && a.UserCreateID == uguid)
                .FirstOrDefault();

            if (appointment == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            appointment.TimeStart = data.TimeStart.ToUniversalTime();
            appointment.TimeEnd = data.TimeEnd.ToUniversalTime();
            appointment.ChiefComplaint = data.ChiefComplaint;

            _Context.SaveChanges();
            return GetByID(appointmentID);
        }

        public bool Delete(string appointmentID, string userUpdateID)
        {
            var aguid = Guid.Parse(appointmentID);
            var uguid = Guid.Parse(userUpdateID);

            var appointment = _Context.Appointments
                .Where(a => a.AppointmentID == aguid)
                //.Where(a => a.UserCreateID == uguid || a.UserAppointID == uguid)
                .FirstOrDefault();

            if (appointment == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            _Context.SoftDelete(appointment);
            _Context.SaveChanges();

            var result = GetByID(appointmentID);

            _ActivityBusiness.Create(appointment.UserCreateID.ToString(), new ActivityCreate()
            {
                ServiceCode = (int)ServiceCode.Booking,
                ActionCode = (int)ActionCode.Deleted,
                StatusCode = (int)StatusCode.Danger,
                ClinicID = appointment.ClinicID,
                TargetItemID = appointment.AppointmentID,
                Metadata = result.ToJsonString(),
            });
            _ActivityBusiness.Create(appointment.UserAppointID.ToString(), new ActivityCreate()
            {
                ServiceCode = (int)ServiceCode.Booking,
                ActionCode = (int)ActionCode.Deleted,
                StatusCode = (int)StatusCode.Danger,
                ClinicID = appointment.ClinicID,
                TargetItemID = appointment.AppointmentID,
                Metadata = result.ToJsonString(),
            });

            return true;
        }
    }
}
