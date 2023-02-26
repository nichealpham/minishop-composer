using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Referral")]
    public partial class Referral
    {
        public Referral()
        {
            Appointments = new HashSet<Appointment>();
            Episodes = new HashSet<Episode>();
        }

        [Key]
        public Guid ReferralID { get; set; }
        public Guid SummaryID { get; set; }
        [StringLength(500)]
        public string Title { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public Guid? ServiceID { get; set; }
        public Guid? ClinicID { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? SpecialPrice { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateUpdated { get; set; }
        public byte StatusID { get; set; }
        public Guid UserID { get; set; }
        public Guid UserCreateID { get; set; }
        public Guid? UserAppointID { get; set; }

        [ForeignKey(nameof(SummaryID))]
        [InverseProperty("Referrals")]
        public virtual Summary Summary { get; set; }
        [InverseProperty(nameof(Appointment.Referral))]
        public virtual ICollection<Appointment> Appointments { get; set; }
        [InverseProperty(nameof(Episode.Referral))]
        public virtual ICollection<Episode> Episodes { get; set; }
    }
}
