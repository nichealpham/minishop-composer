using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Role")]
    public partial class Role : TrackEntity
    {
        [Key]
        public Guid RoleID { get; set; }
        public Guid UserID { get; set; }
        public Guid ClinicID { get; set; }
        public byte RoleType { get; set; }
        [StringLength(2000)]
        public string ProfileUrl { get; set; }
        [ForeignKey(nameof(ClinicID))]
        [InverseProperty("Roles")]
        public virtual Clinic Clinic { get; set; }
        [ForeignKey(nameof(UserID))]
        [InverseProperty("Roles")]
        public virtual User User { get; set; }
    }
}
