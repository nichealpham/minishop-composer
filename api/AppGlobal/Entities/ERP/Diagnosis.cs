using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Diagnosis")]
    public partial class Diagnosis
    {
        [Key]
        public Guid DiagnosisID { get; set; }
        public Guid DiseaseID { get; set; }
        public Guid SummaryID { get; set; }
        [StringLength(500)]
        public string Note { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateUpdated { get; set; }
        public Guid UserID { get; set; }
        public Guid UserCreateID { get; set; }

        [ForeignKey(nameof(DiseaseID))]
        [InverseProperty("Diagnoses")]
        public virtual Disease Disease { get; set; }
        [ForeignKey(nameof(SummaryID))]
        [InverseProperty("Diagnoses")]
        public virtual Summary Summary { get; set; }
    }
}
