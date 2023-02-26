using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Activity")]
    public partial class Activity
    {
        [Key]
        public Guid ActivityID { get; set; }
        public bool IsRead { get; set; }
        public short ServiceCode { get; set; }
        public short ActionCode { get; set; }
        public short StatusCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        public Guid? UserTargetedID { get; set; }
        public Guid? ClinicID { get; set; }
        public Guid? TargetItemID { get; set; }
        public string Metadata { get; set; }

        [ForeignKey(nameof(ClinicID))]
        [InverseProperty("Activities")]
        public virtual Clinic Clinic { get; set; }
        [ForeignKey(nameof(UserTargetedID))]
        [InverseProperty(nameof(User.ActivityUserTargeteds))]
        public virtual User UserTargeted { get; set; }
    }
}
