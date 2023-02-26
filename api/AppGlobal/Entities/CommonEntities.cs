using AppGlobal.Constants;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppGlobal.Entities
{
    public class TrackEntity
    {   
        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateUpdated { get; set; }
    }
    public class AuditEntity
    {
        [Required]
        [DefaultValue((int)StatusType.Active)]
        public byte StatusID { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateUpdated { get; set; }
    }
}
