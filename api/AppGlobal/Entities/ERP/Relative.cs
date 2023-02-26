using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Relative")]
    public partial class Relative
    {
        [Key]
        public Guid RelativeID { get; set; }
        public Guid UserID { get; set; }
        public Guid UserRelativeID { get; set; }
        public byte RelativeType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateUpdated { get; set; }

        [ForeignKey(nameof(UserID))]
        [InverseProperty("RelativeUsers")]
        public virtual User User { get; set; }
        [ForeignKey(nameof(UserRelativeID))]
        [InverseProperty("RelativeUserRelatives")]
        public virtual User UserRelative { get; set; }
    }
}
