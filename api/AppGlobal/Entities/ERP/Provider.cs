using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Provider")]
    public partial class Provider : TrackEntity
    {
        [Key]
        public Guid ProviderID { get; set; }
        public Guid UserID { get; set; }
        public Guid ServiceID { get; set; }
        public Guid? ClinicID { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? SpecialPrice { get; set; }
        [ForeignKey(nameof(ClinicID))]
        [InverseProperty("Providers")]
        public virtual Clinic Clinic { get; set; }
        [ForeignKey(nameof(ServiceID))]
        [InverseProperty("Providers")]
        public virtual Service Service { get; set; }
        [ForeignKey(nameof(UserID))]
        [InverseProperty("Providers")]
        public virtual User User { get; set; }
    }
}
