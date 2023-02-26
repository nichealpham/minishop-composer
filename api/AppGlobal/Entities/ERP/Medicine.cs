using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Medicine")]
    public partial class Medicine
    {
        public Medicine()
        {
            Medications = new HashSet<Medication>();
        }

        [Key]
        public Guid MedicineID { get; set; }
        [Required]
        [StringLength(200)]
        public string MedicineName { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public short? DefaultMedRoute { get; set; }
        public short? DefaultUsageNum { get; set; }
        public short UnitType { get; set; }
        public short? DefaultDosePerDay { get; set; }
        public short? DefaultAmountPerDose { get; set; }
        public short? DefaultBuyingNumber { get; set; }
        public short? DefaultBuyingUnit { get; set; }
        public byte StatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateUpdated { get; set; }

        [InverseProperty(nameof(Medication.Medicine))]
        public virtual ICollection<Medication> Medications { get; set; }
    }
}
