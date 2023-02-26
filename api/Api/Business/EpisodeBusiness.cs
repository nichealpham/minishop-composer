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
using System.Net.Http;
using System.Text;
using System.Text.Json;
using OfficeOpenXml.Style;
using Microsoft.Extensions.Configuration;

namespace Api.Business
{
    public static class EpisodeHelper
    {
        // PERFORMANCE !!!
        public static IQueryable<Episode> GetEpisodeQueryable(this ERPContext _Context)
        {
            return _Context.Episodes
                .AsNoTracking()
                .Include(a => a.Clinic)
                .Include(a => a.UserAdmitted)
                .Include(a => a.Records)
                    .ThenInclude(r => r.UserAppoint)
                .Include(a => a.Records)
                    .ThenInclude(r => r.Service)
                .Include(a => a.Invoices);
        }
        // PERFORMANCE !!!
        public static SearchResult<EpisodeModel> GetEpisodeResults(this IQueryable<Episode> query, string keySearch = "", bool deleted = false, int page = 1, int limit = 10)
        {
            if (deleted)
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
                var phoneSearch = keySearch.PreparePhone().Replace("+", "");
                query = query.Where(a =>
                    //(a.Clinic != null && ApplicationDBContext.Standardizing(a.Clinic.ClinicName).Contains(keySearch)) ||
                    (a.PublicTitle != null && ApplicationDBContext.Standardizing(a.PublicTitle).Contains(keySearch)) ||
                    (a.UserAdmitted != null && ApplicationDBContext.Standardizing(a.UserAdmitted.FullName).Contains(keySearch)) ||
                    (a.UserAdmitted != null && !string.IsNullOrEmpty(a.UserAdmitted.Phone) && a.UserAdmitted.Phone.Contains(phoneSearch)) ||
                    (a.Records != null && a.Records.Any(r =>
                        (r.UserAppoint != null && ApplicationDBContext.Standardizing(r.UserAppoint.FullName).Contains(keySearch)) ||
                        (r.Service != null && ApplicationDBContext.Standardizing(r.Service.ServiceName).Contains(keySearch))
                    )));
            }

            var totals = query.Count();
            var items = query
                .OrderByDescending(p => p.DateCreated)
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(EpisodeModel.Convert)
                .ToList();

            return new SearchResult<EpisodeModel>()
            {
                Totals = totals,
                Items = items
            };
        }
    }

    public class EpisodeBusiness
    {
        private readonly ERPContext _Context;
        private readonly ActivityBusiness _ActivityBusiness;
        private readonly string _SandrasoftUri;
        private readonly string _EmailContentNewEpisode;

        public EpisodeBusiness(ERPContext context, ActivityBusiness ActivityBusiness, IConfiguration Configuration)
        {
            _Context = context;
            _SandrasoftUri = Configuration["SandrasoftUri"];
            _EmailContentNewEpisode = Configuration["EmailContentNewEpisode"];
            _ActivityBusiness = ActivityBusiness;
        }

        // Search by UserID
        public SearchResult<EpisodeModel> Search(string userID, List<string> clinicIDs, string keySearch, bool deleted = false, int page = 1, int limit = 10)
        {
            var guid = Guid.Parse(userID);
            var guids = clinicIDs.Select(c => Guid.Parse(c)).ToList();

            return _Context
                .GetEpisodeQueryable()
                .Where(e =>
                    (e.UserAdmittedID == guid) ||
                    (e.Records != null && e.Records.Any(r => r.UserAppointID == guid)) ||
                    (e.ClinicID.HasValue && guids.Contains(e.ClinicID.Value)))
                .GetEpisodeResults(keySearch, deleted, page, limit);
        }

        public SearchResult<EpisodeModel> SearchLastVisits(string userID, string keySearch, bool deleted = false, int page = 1, int limit = 10)
        {
            var guid = Guid.Parse(userID);

            return _Context
                .GetEpisodeQueryable()
                .Where(e => (e.UserAdmittedID == guid))
                .GetEpisodeResults(keySearch, deleted, page, limit);
        }

        // Search by List<clinicIDs>
        public SearchResult<EpisodeModel> Search(List<string> clinicIDs, string keySearch, bool deleted = false, int page = 1, int limit = 10)
        {
            var guids = clinicIDs.Select(c => Guid.Parse(c)).ToList();

            return _Context
                .GetEpisodeQueryable()
                .Where(e => e.ClinicID.HasValue && guids.Contains(e.ClinicID.Value))
                .GetEpisodeResults(keySearch, deleted, page, limit);
        }

        public EpisodeDetail GetByID(string episodeID)
        {
            var guid = Guid.Parse(episodeID);

            return _Context.GetEpisodeQueryable()
                .Where(e => e.EpisodeID == guid)
                .Select(EpisodeDetail.Convert)
                .FirstOrDefault();
        }

        public EpisodeDetail GetByIDWithPermission(string episodeID, string userID)
        {
            var result = GetByID(episodeID);
            // if user is patient, does not allow to read doctor note
            if (result.UserAdmittedID == Guid.Parse(userID))
            {
                result.Records.ForEach(r =>
                {
                    r.DoctorNote = null;
                });
            }
            return result;
        }

        public EpisodeDetail Create(string appointmentID)
        {
            var aguid = Guid.Parse(appointmentID);
            var appointment = _Context.Appointments
                .Include(a => a.Service)
                .Include(a => a.Clinic)
                .Where(a => a.AppointmentID == aguid)
                .FirstOrDefault();

            if (appointment == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }
            if (!appointment.UserAppointID.HasValue || !appointment.ServiceID.HasValue)
            {
                throw new ApiError((int)ErrorCodes.FailedValidationData);
            }
            if (_Context.Episodes.Any(e => e.AppointmentID == Guid.Parse(appointmentID)))
            {
                throw new ApiError((int)ErrorCodes.DuplicatedDataEntry);
            }

            var episode = new Episode()
            {
                AppointmentID = aguid,
                AdmissionType = (int)AdmissionType.CheckedIn,
                UserAdmittedID = appointment.UserCreateID,
                TimeStart = DateTime.Now.ToUniversalTime(),
                ClinicID = appointment.ClinicID,
                ReferralID = appointment.ReferralID,
                StatusID = (byte)EpisodeStatusType.CheckedIn,
            };

            episode.Records.Add(new Record()
            {
                UserAppointID = appointment.UserAppointID.Value,
                ServiceID = appointment.ServiceID.Value,
                Price = appointment.SpecialPrice ?? appointment.Service!.Price,
                ClinicalResult = appointment.Service!.ShortDescription,
                DoctorNote = appointment.ClinicalNote,
            });

            appointment.StatusID = (byte)AppointmentStatusType.CheckedIn;

            _Context.Episodes.Add(episode);
            _Context.SaveChanges();

            var userInfo = _Context.Users.AsNoTracking().Where(u => u.UserID == episode.UserAdmittedID).FirstOrDefault();

            // send mail confirm booking for doctor
            foreach (var record in episode.Records)
            {
                var doctorInfo = _Context.Users.AsNoTracking().Where(u => u.UserID == record.UserAppointID).FirstOrDefault();
                var service = _Context.Services.AsNoTracking().Where(s => s.ServiceID == record.ServiceID).FirstOrDefault();
                EpisodeSendMailAsync(new EpisodeSendMailContent()
                {
                    dest = doctorInfo.Email,
                    sender = appointment.Clinic?.ClinicName,
                    subject = "New Notification",
                    content = String.Format(_EmailContentNewEpisode, doctorInfo.FullName, service.ServiceName, userInfo.FullName, userInfo.Phone, _SandrasoftUri, episode.EpisodeID)
                });
            };

            var result = GetByID(episode.EpisodeID.ToString());

            //_ActivityBusiness.Create(appointment.UserCreateID.ToString(), new ActivityCreate()
            //{
            //    ServiceCode = (int)ServiceCode.Episode,
            //    ActionCode = (int)ActionCode.Checkin,
            //    StatusCode = (int)StatusCode.Info,
            //    ClinicID = appointment.ClinicID,
            //    TargetItemID = episode.EpisodeID,
            //    Metadata = result.ToJsonString(),
            //});
            //_ActivityBusiness.Create(appointment.UserAppointID.ToString(), new ActivityCreate()
            //{
            //    ServiceCode = (int)ServiceCode.Episode,
            //    ActionCode = (int)ActionCode.Checkin,
            //    StatusCode = (int)StatusCode.Info,
            //    ClinicID = appointment.ClinicID,
            //    TargetItemID = episode.EpisodeID,
            //    Metadata = result.ToJsonString(),
            //});

            return result;
        }

        public EpisodeDetail Create(string clinicID, EpisodeCreate data)
        {
            var cguid = Guid.Parse(clinicID);
            var clinic = _Context.Clinics.FirstOrDefault(c => c.ClinicID == cguid);

            if (data.Records == null || data.Records.Count() == 0)
            {
                throw new ApiError((int)ErrorCodes.FailedValidationData);
            }

            var serviceGuids = data.Records.Select(r => Guid.Parse(r.ServiceID)).ToList();
            var services = _Context.Services.AsNoTracking().Where(s => serviceGuids.Contains(s.ServiceID))
                .Select(s => new { ServiceID = s.ServiceID.ToString(), s.Price, s.ShortDescription }).ToList();

            var episode = new Episode()
            {
                ClinicID = cguid,
                UserAdmittedID = Guid.Parse(data.UserAdmittedID),
                AdmissionType = (int)AdmissionType.CheckedIn,
                TimeStart = DateTime.Now.ToUniversalTime(),
                StatusID = (byte)EpisodeStatusType.CheckedIn,
            };

            var userInfo = _Context.Users.AsNoTracking().Where(u => u.UserID == Guid.Parse(data.UserAdmittedID)).FirstOrDefault();

            data.Records.ForEach(r =>
            {
                episode.Records.Add(new Record()
                {
                    UserAppointID = Guid.Parse(r.UserAppointID),
                    ServiceID = Guid.Parse(r.ServiceID),
                    Price = r.SpecialPrice ?? services.FirstOrDefault(p => p.ServiceID.ToLower() == r.ServiceID.ToLower()).Price,
                    ClinicalResult = services.FirstOrDefault(p => p.ServiceID.ToLower() == r.ServiceID.ToLower()).ShortDescription,
                });
            });

            _Context.Episodes.Add(episode);
            _Context.SaveChanges();

            // send mail confirm booking for doctor
            foreach (var record in data.Records)
            {
                var doctorInfo = _Context.Users.AsNoTracking().Where(u => u.UserID == Guid.Parse(record.UserAppointID)).FirstOrDefault();
                var service = _Context.Services.AsNoTracking().Where(s => s.ServiceID == Guid.Parse(record.ServiceID)).FirstOrDefault();
                EpisodeSendMailAsync(new EpisodeSendMailContent()
                {
                    dest = doctorInfo.Email,
                    sender = clinic.ClinicName,
                    subject = "New Notification",
                    content = String.Format(_EmailContentNewEpisode, doctorInfo.FullName, service.ServiceName, userInfo.FullName, userInfo.Phone, _SandrasoftUri, episode.EpisodeID)
                });
            };

            var result = GetByID(episode.EpisodeID.ToString());

            //_ActivityBusiness.Create(episode.UserAdmittedID.ToString(), new ActivityCreate()
            //{
            //    ServiceCode = (int)ServiceCode.Episode,
            //    ActionCode = (int)ActionCode.Checkin,
            //    StatusCode = (int)StatusCode.Info,
            //    ClinicID = episode.ClinicID,
            //    TargetItemID = episode.EpisodeID,
            //    Metadata = result.ToJsonString(),
            //});
            //_ActivityBusiness.Create(episode.Records.FirstOrDefault().UserAppointID.ToString(), new ActivityCreate()
            //{
            //    ServiceCode = (int)ServiceCode.Episode,
            //    ActionCode = (int)ActionCode.Checkin,
            //    StatusCode = (int)StatusCode.Info,
            //    ClinicID = episode.ClinicID,
            //    TargetItemID = episode.EpisodeID,
            //    Metadata = result.ToJsonString(),
            //});

            return result;
        }


        public bool Delete(string episodeID, string userID)
        {
            var eguid = Guid.Parse(episodeID);
            var uguid = Guid.Parse(userID);

            var episode = _Context.Episodes
                .Include(a => a.Records)
                    .ThenInclude(r => r.UserAppoint)
                .Where(e => e.EpisodeID == eguid)
                .Where(a => a.UserAdmittedID == uguid || a.Records.Any(r => r.UserAppointID == uguid))
                .FirstOrDefault();

            if (episode == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            episode.TimeEnd = DateTime.Now.ToUniversalTime();
            episode.StatusID = (byte)EpisodeStatusType.Canceled;
            episode.Records.ToList().ForEach(r => r.StatusID = (byte)EpisodeStatusType.Canceled);

            // Remove all related assets inside this episode
            var consumes = _Context.AssetConsumeds.Include(c => c.Asset).Where(a => a.EpisodeID == eguid).ToList();
            if (consumes.Count > 0)
            {
                foreach (var consume in consumes)
                {
                    if (consume.Asset.Type == (byte)AssetType.Consumable)
                    {
                        consume.Asset.Amount += consume.Amount;
                    }
                }
                _Context.AssetConsumeds.RemoveRange(consumes);
            }
            _Context.SaveChanges();

            var result = GetByID(episodeID);

            _ActivityBusiness.Create(episode.UserAdmittedID.ToString(), new ActivityCreate()
            {
                ServiceCode = (int)ServiceCode.Episode,
                ActionCode = (int)ActionCode.Deleted,
                StatusCode = (int)StatusCode.Danger,
                ClinicID = episode.ClinicID,
                TargetItemID = episode.EpisodeID,
                Metadata = result.ToJsonString(),
            });
            _ActivityBusiness.Create(episode.Records.FirstOrDefault().UserAppointID.ToString(), new ActivityCreate()
            {
                ServiceCode = (int)ServiceCode.Episode,
                ActionCode = (int)ActionCode.Deleted,
                StatusCode = (int)StatusCode.Danger,
                ClinicID = episode.ClinicID,
                TargetItemID = episode.EpisodeID,
                Metadata = result.ToJsonString(),
            });

            return true;
        }

        public string PublicEpisode(string clinicID, string episodeID, string userID, EpisodePublicStatus body)
        {
            var cguid = Guid.Parse(clinicID);
            var uguid = Guid.Parse(userID);

            // validate user is part of this clinic
            if (!_Context.Roles.AsNoTracking().Any(r => r.ClinicID == cguid && r.UserID == uguid))
            {
                throw new ApiError((int)ErrorCodes.UserIsUnauthorized);
            }

            var eguid = Guid.Parse(episodeID);
            var episode = _Context.Episodes
                .Where(e => e.EpisodeID == eguid)
                .FirstOrDefault();

            if (episode == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            string stringUrl = null;

            if (body.Status == true)
            {
                if (string.IsNullOrEmpty(episode.PublicNameUrl))
                {
                    stringUrl = string.IsNullOrEmpty(episode.PublicTitle) ?
                        episodeID
                            .ToLower() :
                        episode.PublicTitle
                            .Standardizing()
                            .Replace(" ", "-");

                    var count = _Context.Episodes.AsNoTracking().Where(e => e.PublicNameUrl.Contains(stringUrl)).Count();
                    if (count > 0)
                    {
                        stringUrl += ('-' + count);
                    }

                    episode.PublicNameUrl = stringUrl;
                }
                stringUrl = episode.PublicNameUrl;
            }

            episode.PublicStatus = body.Status;
            _Context.SaveChanges();

            return stringUrl;
        }

        public bool UpdateRecordResult(string clinicID, string userID, string episodeID, string recordID, RecordUpdate data)
        {
            var cguid = Guid.Parse(clinicID);
            var uguid = Guid.Parse(userID);

            // validate user is part of this clinic
            if (!_Context.Roles.AsNoTracking().Any(r => r.ClinicID == cguid && r.UserID == uguid))
            {
                throw new ApiError((int)ErrorCodes.UserIsUnauthorized);
            }

            var eguid = Guid.Parse(episodeID);
            var rguid = Guid.Parse(recordID);

            var record = _Context.Records
                .Include(e => e.Episode)
                .Where(e => e.EpisodeID == eguid && e.RecordID == rguid)
                .Where(e => e.Episode.StatusID == (byte)EpisodeStatusType.CheckedIn)
                .FirstOrDefault();

            if (record == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            record.ClinicalResult = data.ClinicalResult;
            record.DateUpdated = DateTime.Now.ToUniversalTime();
            record.Episode.PublicTitle = data.PublicTitle ?? null;
            record.Episode.PublicHeadline = data.PublicHeadline ?? null;

            // only allow update doctor note if this user is not patient!
            if (uguid != record.Episode.UserAdmittedID)
            {
                record.DoctorNote = data.DoctorNote;
            }

            _Context.SaveChanges();
            return true;
        }

        public bool UpdateInvoice(string episodeID, InvoiceUpdate data)
        {
            var eguid = Guid.Parse(episodeID);

            var invoice = _Context.Invoices
                .Where(e => e.EpisodeID == eguid)
                .FirstOrDefault();

            if (invoice == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            invoice.FinalPrice = data.Price;
            _Context.SaveChanges();

            return true;
        }

        public bool Complete(string clinicID, string episodeID)
        {
            var eguid = Guid.Parse(episodeID);
            var cguid = Guid.Parse(clinicID);

            var episode = _Context.Episodes
                .Include(a => a.Records)
                .Where(e => e.StatusID == (byte)EpisodeStatusType.CheckedIn)
                .Where(e => e.EpisodeID == eguid && e.ClinicID == cguid)
                .FirstOrDefault();

            if (episode == null)
            {
                throw new ApiError((int)ErrorCodes.DataEntryIsNotExisted);
            }

            episode.StatusID = (byte)EpisodeStatusType.Success;
            episode.TimeEnd = DateTime.Now.ToUniversalTime();

            var records = episode.Records.ToList();
            var totalPrice = records.Where(r => !r.IsFree).Sum(r => r.Price);
            var invoice = new Invoice()
            {
                InvoiceNumber = DateTime.Now.ToString("yyyyMMddHHmmss"),
                TotalPrice = totalPrice,
                DiscountPrice = 0,
                FinalPrice = totalPrice,
                EpisodeID = eguid,
            };

            _Context.Invoices.Add(invoice);
            _Context.SaveChanges();

            records.ForEach(r =>
            {
                r.StatusID = (byte)EpisodeStatusType.Success;
                r.InvoiceID = invoice.InvoiceID;
            });

            var summary = new Summary()
            {
                EpisodeID = eguid,
            };

            _Context.Summaries.Add(summary);
            _Context.SaveChanges();

            var result = GetByID(episodeID);

            _ActivityBusiness.Create(episode.UserAdmittedID.ToString(), new ActivityCreate()
            {
                ServiceCode = (int)ServiceCode.Episode,
                ActionCode = (int)ActionCode.Success,
                StatusCode = (int)StatusCode.Success,
                ClinicID = episode.ClinicID,
                TargetItemID = episode.EpisodeID,
                Metadata = result.ToJsonString(),
            });
            _ActivityBusiness.Create(episode.Records.FirstOrDefault().UserAppointID.ToString(), new ActivityCreate()
            {
                ServiceCode = (int)ServiceCode.Episode,
                ActionCode = (int)ActionCode.Success,
                StatusCode = (int)StatusCode.Success,
                ClinicID = episode.ClinicID,
                TargetItemID = episode.EpisodeID,
                Metadata = result.ToJsonString(),
            });

            return true;
        }
        // sendmail episode
        public bool EpisodeSendMailAsync(EpisodeSendMailContent body)
        {
            if (String.IsNullOrEmpty(body.dest)) return false;

            string url = "https://us-central1-sandrasoft-xxxxx.cloudfunctions.net/sendMail";

            var json = JsonSerializer.Serialize(body);
            // Console.WriteLine(body.dest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = client.PostAsync(url, data);
            Console.WriteLine(response.Result); // Required so that the send request is called
            //string result = response.Content.ReadAsStringAsync().Result;

            return true;
        }
    }
}
