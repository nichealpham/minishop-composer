using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Episode")]
    public partial class Episode : AuditEntity
    {
        public Episode()
        {
            Invoices = new HashSet<Invoice>();
            Records = new HashSet<Record>();
            Summaries = new HashSet<Summary>();
        }

        [Key]
        public Guid EpisodeID { get; set; }
        public byte AdmissionType { get; set; }
        public Guid UserAdmittedID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime TimeStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TimeEnd { get; set; }
        public Guid? AppointmentID { get; set; }
        public Guid? ClinicID { get; set; }
        public Guid? ReferralID { get; set; }

        public bool? PublicStatus { get; set; }
        [StringLength(200)]
        public string PublicNameUrl { get; set; }
        [StringLength(500)]
        public string PublicTitle { get; set; }
        [StringLength(2500)]
        public string PublicHeadline { get; set; }
        public int? ViewCount { get; set; }

        [ForeignKey(nameof(AppointmentID))]
        [InverseProperty("Episodes")]
        public virtual Appointment Appointment { get; set; }
        [ForeignKey(nameof(ClinicID))]
        [InverseProperty("Episodes")]
        public virtual Clinic Clinic { get; set; }
        [ForeignKey(nameof(ReferralID))]
        [InverseProperty("Episodes")]
        public virtual Referral Referral { get; set; }
        [ForeignKey(nameof(UserAdmittedID))]
        [InverseProperty(nameof(User.Episodes))]
        public virtual User UserAdmitted { get; set; }
        [InverseProperty(nameof(Invoice.Episode))]
        public virtual ICollection<Invoice> Invoices { get; set; }
        [InverseProperty(nameof(Record.Episode))]
        public virtual ICollection<Record> Records { get; set; }
        [InverseProperty(nameof(Summary.Episode))]
        public virtual ICollection<Summary> Summaries { get; set; }
    }
}
