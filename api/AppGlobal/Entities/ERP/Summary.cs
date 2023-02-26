using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Summary")]
    public partial class Summary : AuditEntity
    {
        public Summary()
        {
            Diagnoses = new HashSet<Diagnosis>();
            Medications = new HashSet<Medication>();
            Referrals = new HashSet<Referral>();
        }

        [Key]
        public Guid SummaryID { get; set; }
        public Guid EpisodeID { get; set; }
        public byte DischargeType { get; set; }
        [StringLength(500)]
        public string Title { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }

        [ForeignKey(nameof(EpisodeID))]
        [InverseProperty("Summaries")]
        public virtual Episode Episode { get; set; }
        [InverseProperty(nameof(Diagnosis.Summary))]
        public virtual ICollection<Diagnosis> Diagnoses { get; set; }
        [InverseProperty(nameof(Medication.Summary))]
        public virtual ICollection<Medication> Medications { get; set; }
        [InverseProperty(nameof(Referral.Summary))]
        public virtual ICollection<Referral> Referrals { get; set; }
    }
}
