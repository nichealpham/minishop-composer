using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Record")]
    public partial class Record : AuditEntity
    {
        [Key]
        public Guid RecordID { get; set; }
        public Guid EpisodeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TimeStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TimeEnd { get; set; }
        public Guid UserAppointID { get; set; }
        public Guid ServiceID { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Price { get; set; }
        public bool IsFree { get; set; }
        public string ClinicalResult { get; set; }
        public string DoctorNote { get; set; }
        public Guid? InvoiceID { get; set; }

        [ForeignKey(nameof(EpisodeID))]
        [InverseProperty("Records")]
        public virtual Episode Episode { get; set; }
        [ForeignKey(nameof(InvoiceID))]
        [InverseProperty("Records")]
        public virtual Invoice Invoice { get; set; }
        [ForeignKey(nameof(ServiceID))]
        [InverseProperty("Records")]
        public virtual Service Service { get; set; }
        [ForeignKey(nameof(UserAppointID))]
        [InverseProperty(nameof(User.Records))]
        public virtual User UserAppoint { get; set; }
    }
}
