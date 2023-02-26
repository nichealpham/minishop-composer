using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Medication")]
    public partial class Medication
    {
        [Key]
        public Guid MedicationID { get; set; }
        public Guid MedicineID { get; set; }
        public Guid SummaryID { get; set; }
        [StringLength(500)]
        public string Note { get; set; }
        public short? MedicineRoute { get; set; }
        public short AmountPerDose { get; set; }
        public short UnitType { get; set; }
        public short DosePerDay { get; set; }
        public short DaysCount { get; set; }
        public short? TotalUnitsCount { get; set; }
        public short? TotalBuyingNumber { get; set; }
        public short? BuyingUnit { get; set; }
        [StringLength(2000)]
        public string Instruction { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateUpdated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        public Guid UserID { get; set; }
        public Guid UserCreateID { get; set; }

        [ForeignKey(nameof(MedicineID))]
        [InverseProperty("Medications")]
        public virtual Medicine Medicine { get; set; }
        [ForeignKey(nameof(SummaryID))]
        [InverseProperty("Medications")]
        public virtual Summary Summary { get; set; }
    }
}
