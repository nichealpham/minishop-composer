using AppGlobal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppGlobal.Models
{
    public class AppointmentModel
    {
        public Guid AppointmentID { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }

        public Guid UserCreateID { get; set; }
        public string UserCreateName { get; set; }
        public string UserCreateAvatar { get; set; }
        public string UserCreatePhone { get; set; }

        public Guid? UserAppointID { get; set; }
        public string UserAppointName { get; set; }
        public string UserAppointAvatar { get; set; }
        public string UserAppointPhone { get; set; }

        public Guid? ServiceID { get; set; }
        public string ServiceName { get; set; }

        public Guid? ClinicID { get; set; }
        public string ClinicName { get; set; }

        public DateTime DateCreated { get; set; }

        public static AppointmentModel Convert(Appointment a) => a == null ? null : new AppointmentModel()
        {
            AppointmentID = a.AppointmentID,
            TimeStart = a.TimeStart.ToLocalTime(),
            TimeEnd = a.TimeEnd.ToLocalTime(),

            UserCreateID = a.UserCreateID,
            UserCreateName = a.UserCreate?.FullName,
            UserCreateAvatar = a.UserCreate?.Avatar,
            UserCreatePhone = a.UserCreate?.Phone,

            UserAppointID = a.UserAppointID,
            UserAppointName = a.UserAppoint?.FullName,
            UserAppointAvatar = a.UserAppoint?.Avatar,
            UserAppointPhone = a.UserAppoint?.Phone,

            ServiceID = a.ServiceID,
            ServiceName = a.Service?.ServiceName,
            DateCreated = a.DateCreated.ToLocalTime(),

            ClinicID = a.ClinicID,
            ClinicName = a.Clinic?.ClinicName,
        };
    }

    public class AppointmentDetail
    {
        public Guid AppointmentID { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }

        public Guid UserCreateID { get; set; }
        public UserModel UserCreate { get; set; }

        public Guid? UserAppointID { get; set; }
        public UserModel UserAppoint { get; set; }

        public Guid? ServiceID { get; set; }
        public ServiceModel Service { get; set; }

        public Guid? ClinicID { get; set; }
        public ClinicModel Clinic { get; set; }

        public Guid? EpisodeID { get; set; }
        public string ClinicalResult { get; set; }
        public string DoctorNote { get; set; }

        public Guid? ReferralID { get; set; }
        public decimal? SpecialPrice { get; set; }
        public byte StatusID { get; set; }
        public decimal Price { get; set; }

        public string ChiefComplaint { get; set; }
        public string ClinicalNote { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public static AppointmentDetail Convert(Appointment a) => a == null ? null : new AppointmentDetail()
        {
            AppointmentID = a.AppointmentID,
            TimeStart = a.TimeStart.ToLocalTime(),
            TimeEnd = a.TimeEnd.ToLocalTime(),

            UserCreateID = a.UserCreateID,
            UserCreate = UserModel.Convert(a.UserCreate),

            UserAppointID = a.UserAppointID,
            UserAppoint = UserModel.Convert(a.UserAppoint),

            ServiceID = a.ServiceID,
            Service = ServiceModel.Convert(a.Service),

            ClinicID = a.ClinicID,
            Clinic = ClinicModel.Convert(a.Clinic),

            ReferralID = a.ReferralID,
            SpecialPrice = a.SpecialPrice,
            StatusID = a.StatusID,

            Price = a.SpecialPrice ?? a.Service.Price,

            ChiefComplaint = a.ChiefComplaint,
            ClinicalNote = a.ClinicalNote,
            DateCreated = a.DateCreated,
            DateUpdated = a.DateUpdated,
        };
    }

    public class AppointmentPatientCreate
    {
        public string ProviderID { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string ChiefComplaint { get; set; }
    }

    public class AppointmentClinicCreate
    {
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string ChiefComplaint { get; set; }
        public string ClinicalNote { get; set; }
        public string PatientID { get; set; }
        public string ServiceID { get; set; }
        public string DoctorID { get; set; }
        public decimal? SpecialPrice { get; set; }
    }

    public class AppointmentUpdate
    {
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string ChiefComplaint { get; set; }
    }
}
