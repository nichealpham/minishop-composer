using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Appointment")]
    public partial class Appointment : AuditEntity
    {
        public Appointment()
        {
            Episodes = new HashSet<Episode>();
        }

        [Key]
        public Guid AppointmentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime TimeStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime TimeEnd { get; set; }
        public Guid UserCreateID { get; set; }
        public Guid? UserAppointID { get; set; }
        public Guid? ServiceID { get; set; }
        public Guid? ClinicID { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? SpecialPrice { get; set; }
        [StringLength(500)]
        public string ChiefComplaint { get; set; }
        [StringLength(500)]
        public string ClinicalNote { get; set; }
        public Guid? ReferralID { get; set; }

        [ForeignKey(nameof(ClinicID))]
        [InverseProperty("Appointments")]
        public virtual Clinic Clinic { get; set; }
        [ForeignKey(nameof(ReferralID))]
        [InverseProperty("Appointments")]
        public virtual Referral Referral { get; set; }
        [ForeignKey(nameof(ServiceID))]
        [InverseProperty("Appointments")]
        public virtual Service Service { get; set; }
        [ForeignKey(nameof(UserAppointID))]
        [InverseProperty(nameof(User.AppointmentUserAppoints))]
        public virtual User UserAppoint { get; set; }
        [ForeignKey(nameof(UserCreateID))]
        [InverseProperty(nameof(User.AppointmentUserCreates))]
        public virtual User UserCreate { get; set; }
        [InverseProperty(nameof(Episode.Appointment))]
        public virtual ICollection<Episode> Episodes { get; set; }
    }
}
