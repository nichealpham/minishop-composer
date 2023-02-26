using AppGlobal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppGlobal.Models
{
    public class ProviderModel
    {
        public Guid ProviderID { get; set; }
        public Guid UserID { get; set; }
        public Guid ServiceID { get; set; }
        public Guid? ClinicID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        // Doctor Info
        public UserModel Doctor { get; set; }
        // Service Info
        public ServiceModel Service { get; set; }
        // Clinic Info
        public ClinicModel Clinic { get; set; }
        public static ProviderModel Convert(Provider p) => p != null ? new ProviderModel()
        {
            ProviderID = p.ProviderID,
            UserID = p.UserID,
            ServiceID = p.ServiceID,
            ClinicID = p.ClinicID,
            DateCreated = p.DateCreated,
            DateUpdated = p.DateUpdated,
            Doctor = UserModel.Convert(p.User),
            Service = ServiceModel.Convert(p.Service),
            Clinic = ClinicModel.Convert(p.Clinic),
        } : null;
    }
}
