using AppGlobal.Commons;
using AppGlobal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppGlobal.Models
{
    public class AttachmentAssetForSale
    {
        public Guid AttachmentID { get; set; }
        public string AttachmentUrl { get; set; }
        public byte AttachmentType { get; set; }
        public Guid AssetID { get; set; }
        public Guid ClinicID { get; set; }

        public static AttachmentAssetForSale Convert(Attachment a) => a == null ? null : new AttachmentAssetForSale()
        {
            AttachmentID = a.AttachmentID,
            AssetID = a.TargetItemID,
            AttachmentUrl = a.AttachmentUrl,
        };

    }
    public class AttachmentModel
    {
        public Guid AttachmentID { get; set; }
        public string AttachmentUrl { get; set; }
        public string AttachmentName { get; set; }
        public byte AttachmentType { get; set; }
        public Guid TargetItemID { get; set; }
        public byte TargetItemType { get; set; }
        public Guid ClinicID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public static AttachmentModel Convert(Attachment a) => a == null ? null : new AttachmentModel()
        {
            AttachmentID = a.AttachmentID,
            AttachmentUrl = a.AttachmentUrl,
            AttachmentType = a.AttachmentType,
            TargetItemID = a.TargetItemID,
            TargetItemType = a.TargetItemType,
            ClinicID = a.ClinicID,
            AttachmentName = a.AttachmentName,
            DateCreated = a.DateCreated.ToLocalTime(),
            DateUpdated = a.DateUpdated.ToLocalTime(),
        };
    }

    public class AttachmentModelCreate
    {
        public string AttachmentUrl { get; set; }
        public string AttachmentName { get; set; }
        public byte AttachmentType { get; set; }

        public static Attachment Convert(AttachmentModelCreate a) => new Attachment()
        {
            AttachmentUrl = a.AttachmentUrl,
            AttachmentName = a.AttachmentName,
            AttachmentType = (byte)a.AttachmentType,
        };
    }
}
