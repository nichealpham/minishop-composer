using AppGlobal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppGlobal.Models
{
    public class ActivityModel
    {
        public Guid ActivityID { get; set; }
        public bool IsRead { get; set; }
        public short ServiceCode { get; set; }
        public short ActionCode { get; set; }
        public short StatusCode { get; set; }
        public DateTime DateCreated { get; set; }
        public string Metadata { get; set; }

        public Guid? ClinicID { get; set; }
        public Guid? TargetItemID { get; set; }

        public Guid? UserTargetedID { get; set; }
        public UserModel UserTargeted { get; set; }

        public static ActivityModel Convert(Activity a) => a == null ? null : new ActivityModel()
        {
            ActivityID = a.ActivityID,
            IsRead = a.IsRead,
            ServiceCode = a.ServiceCode,
            ActionCode = a.ActionCode,
            StatusCode = a.StatusCode,
            ClinicID = a.ClinicID,
            TargetItemID = a.TargetItemID,
            Metadata = a.Metadata,
            DateCreated = a.DateCreated.ToLocalTime(),

            UserTargetedID = a.UserTargetedID,
            UserTargeted = UserModel.Convert(a.UserTargeted),
        };
    }

    public class ActivityCreate
    {
        public short ServiceCode { get; set; }
        public short ActionCode { get; set; }
        public short StatusCode { get; set; }
        public string Metadata { get; set; }

        public Guid? ClinicID { get; set; }
        public Guid? TargetItemID { get; set; }
    }
}
