using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Disease")]
    public partial class Disease
    {
        public Disease()
        {
            Diagnoses = new HashSet<Diagnosis>();
        }

        [Key]
        public Guid DiseaseID { get; set; }
        [Required]
        [StringLength(200)]
        public string DiseaseName { get; set; }
        [StringLength(50)]
        public string ICD10 { get; set; }
        public byte StatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateUpdated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }

        [InverseProperty(nameof(Diagnosis.Disease))]
        public virtual ICollection<Diagnosis> Diagnoses { get; set; }
    }
}
