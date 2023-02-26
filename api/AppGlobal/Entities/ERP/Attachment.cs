using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppGlobal.Entities
{
    [Table("Attachment")]
    public partial class Attachment : TrackEntity
    {
        public Attachment()
        {
        }

        [Key]
        public Guid AttachmentID { get; set; }
        [Required]
        [StringLength(2500)]
        public string AttachmentUrl { get; set; }
        public byte AttachmentType { get; set; }
        [StringLength(500)]
        public string AttachmentName { get; set; }
        public Guid TargetItemID { get; set; }
        [Required]
        public byte TargetItemType { get; set; }
        public Guid ClinicID { get; set; }
    }
}
