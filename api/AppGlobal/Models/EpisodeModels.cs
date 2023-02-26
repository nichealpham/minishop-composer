using AppGlobal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AppGlobal.Models
{
    public class EpisodeModel
    {
        // Common Info
        public Guid EpisodeID { get; set; }
        public byte AdmissionType { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public DateTime DateCreated { get; set; }
        public byte StatusID { get; set; }
        public decimal TotalPrice { get; set; }
        // Patient Section
        public Guid UserAdmittedID { get; set; }
        public string UserAdmittedName { get; set; }
        public string UserAdmittedAvatar { get; set; }
        public string UserAdmittedPhone { get; set; }
        // Clinic Attmited
        public Guid? ClinicID { get; set; }
        public string ClinicName { get; set; }
        public string ClinicLogo { get; set; }
        // Publicity
        public string PublicNameUrl { get; set; }
        public string PublicTitle { get; set; }
        public bool PublicStatus { get; set; }
        public int? ViewCount { get; set; }
        // Records relationship
        public List<EpisodeRecordModel> Records { get; set; }
        
        public static EpisodeModel Convert(Episode e) => e == null ? null : new EpisodeModel()
        {
            EpisodeID = e.EpisodeID,
            AdmissionType = e.AdmissionType,
            DateCreated = e.DateCreated.ToLocalTime(),
            TimeStart = e.TimeStart.ToLocalTime(),
            TimeEnd = e.TimeEnd?.ToLocalTime(),
            StatusID = e.StatusID,

            TotalPrice = e.Records.Where(r => !r.IsFree).Sum(r => r.Price),

            UserAdmittedID = e.UserAdmittedID,
            UserAdmittedName = e.UserAdmitted?.FullName,
            UserAdmittedAvatar = e.UserAdmitted?.Avatar,
            UserAdmittedPhone = e.UserAdmitted?.Phone,

            ClinicID = e.ClinicID,
            ClinicName = e.Clinic?.ClinicName,
            ClinicLogo = e.Clinic?.Logo,

            PublicNameUrl = e.PublicNameUrl,
            PublicTitle = e.PublicTitle,
            PublicStatus = e.PublicStatus ?? false,
            ViewCount = e.ViewCount,

            Records = e.Records?.Select(EpisodeRecordModel.Convert).ToList(),
        };
    }

    public class EpisodeRecordModel
    {
        // Doctor Section
        public Guid? UserAppointID { get; set; }
        public string UserAppointName { get; set; }
        public string UserAppointAvatar { get; set; }
        public string UserAppointPhone { get; set; }
        // Service Attmited
        public Guid? ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceLogo { get; set; }

        public static EpisodeRecordModel Convert(Record r) => r == null ? null : new EpisodeRecordModel()
        {
            UserAppointID = r.UserAppointID,
            UserAppointName = r.UserAppoint?.FullName,
            UserAppointAvatar = r.UserAppoint?.Avatar,
            UserAppointPhone = r.UserAppoint?.Phone,

            ServiceID = r.ServiceID,
            ServiceName = r.Service?.ServiceName,
            ServiceLogo = r.Service?.Logo,
        };
    }

    public class EpisodeModelPublic
    {
        // Common Info
        public string PublicNameUrl { get; set; }
        public string PublicTitle { get; set; }
        public string PublicHeadline { get; set; }
        public DateTime DateCreated { get; set; }
        public int? ViewCount { get; set; }
        public static EpisodeModelPublic Convert(Episode e) => e == null ? null : new EpisodeModelPublic()
        {
            DateCreated = e.DateCreated.ToLocalTime(),
            PublicNameUrl = e.PublicNameUrl,
            PublicHeadline = e.PublicHeadline,
            ViewCount = e.ViewCount,
            PublicTitle = string.IsNullOrEmpty(e.PublicTitle) ?
                e.Records?.FirstOrDefault()?.Service?.ServiceName :
                e.PublicTitle,
        };
    }

    public class EpisodeDetailPublic
    {
        // Common Info
        public DateTime TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public string PublicNameUrl { get; set; }
        public string PublicTitle { get; set; }
        public string PublicHeadline { get; set; }
        public string PublicDetail { get; set; }
        public byte StatusID { get; set; }
        public DateTime DateCreated { get; set; }
        public int? ViewCount { get; set; }

        public static EpisodeDetailPublic Convert(Episode e) => e == null ? null : new EpisodeDetailPublic()
        {
            TimeStart = e.TimeStart.ToLocalTime(),
            TimeEnd = e.TimeEnd?.ToLocalTime(),
            StatusID = e.StatusID,
            DateCreated = e.DateCreated.ToLocalTime(),
            ViewCount = e.ViewCount,

            PublicNameUrl = e.PublicNameUrl,
            PublicHeadline = e.PublicHeadline,
            PublicDetail = e.Records?.FirstOrDefault()?.ClinicalResult,
            PublicTitle = string.IsNullOrEmpty(e.PublicTitle) ?
                e.Records?.FirstOrDefault()?.Service?.ServiceName:
                e.PublicTitle,
        };
    }

    public class EpisodeDetail
    {
        // Common Info
        public Guid EpisodeID { get; set; }
        public byte AdmissionType { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public Guid? ReferralID { get; set; }
        public DateTime DateCreated { get; set; }
        public byte StatusID { get; set; }
        public decimal TotalPrice { get; set; }
        // Patient Section
        public Guid UserAdmittedID { get; set; }
        public UserModel UserAdmitted { get; set; }
        // Clinic Attmited
        public Guid? ClinicID { get; set; }
        public ClinicModel Clinic { get; set; }
        // Invoice section
        public Guid? InvoiceID { get; set; }
        // Publicity
        public string PublicNameUrl { get; set; }
        public string PublicTitle { get; set; }
        public string PublicHeadline { get; set; }
        public bool PublicStatus { get; set; }
        public int? ViewCount { get; set; }
        // Relationships
        public List<RecordDetail> Records { get; set; }

        public static EpisodeDetail Convert(Episode e) => e == null ? null : new EpisodeDetail()
        {
            EpisodeID = e.EpisodeID,
            AdmissionType = e.AdmissionType,
            DateCreated = e.DateCreated.ToLocalTime(),
            TimeStart = e.TimeStart.ToLocalTime(),
            TimeEnd = e.TimeEnd?.ToLocalTime(),
            ReferralID = e.ReferralID,
            StatusID = e.StatusID,

            TotalPrice = e.Invoices?.FirstOrDefault()?.FinalPrice ?? 0,

            UserAdmittedID = e.UserAdmittedID,
            UserAdmitted = UserModel.Convert(e.UserAdmitted),

            ClinicID = e.ClinicID,
            Clinic = ClinicModel.Convert(e.Clinic),
            InvoiceID = e.Invoices?.FirstOrDefault()?.InvoiceID,

            PublicNameUrl = e.PublicNameUrl,
            PublicTitle = e.PublicTitle,
            PublicHeadline = e.PublicHeadline,
            PublicStatus = e.PublicStatus ?? false,
            ViewCount = e.ViewCount,

            Records = e.Records?.Select(r => RecordDetail.Convert(r)).ToList(),
        };
    }

    public class RecordDetail
    {
        public Guid RecordID { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        
        public decimal Price { get; set; }
        public bool IsFree { get; set; }

        public string ClinicalResult { get; set; }
        public string DoctorNote { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public byte StatusID { get; set; }

        public Guid UserAppointID { get; set; }
        public UserModel UserAppoint { get; set; }

        public Guid ServiceID { get; set; }
        public ServiceModel Service { get; set; }

        public Guid? InvoiceID { get; set; }
        public static RecordDetail Convert(Record r) => r == null ? null : new RecordDetail()
        {
            RecordID = r.RecordID,
            TimeStart = r.TimeStart?.ToLocalTime(),
            TimeEnd = r.TimeEnd?.ToLocalTime(),
            Price = r.Price,
            IsFree = r.IsFree,
            DateCreated = r.DateCreated.ToLocalTime(),
            DateUpdated = r.DateUpdated.ToLocalTime(),
            StatusID = r.StatusID,

            DoctorNote = r.DoctorNote,
            ClinicalResult = r.ClinicalResult,

            UserAppointID = r.UserAppointID,
            UserAppoint = UserModel.Convert(r.UserAppoint),

            ServiceID = r.ServiceID,
            Service = ServiceModel.Convert(r.Service),
        };
    }

    public class EpisodeCreate
    {
        public byte AdmissionType { get; set; }
        public string UserAdmittedID { get; set; }
        public List<EpisodeRecordCreate> Records { get; set; }
    }

    public class RecordUpdate
    {
        public string ClinicalResult { get; set; }
        public string DoctorNote { get; set; }
        public string PublicTitle { get; set; }
        public string PublicHeadline { get; set; }
    }

    public class InvoiceUpdate
    {
        public decimal Price { get; set; }
    }

    public class EpisodeRecordCreate
    {
        public string UserAppointID { get; set; }
        public string ServiceID { get; set; }
        public decimal? SpecialPrice { get; set; }
    }

    public class EpisodePublicStatus
    {
        public bool Status { get; set; }
    }

    public class EpisodeSendMailContent
    {
        public string dest { get; set; }
        public string sender { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
    };
}
