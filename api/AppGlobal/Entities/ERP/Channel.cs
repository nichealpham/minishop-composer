using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Channel")]
    public partial class Channel
    {
        [Key]
        public Guid ChannelID { get; set; }
        public Guid UserID { get; set; }
        public byte PlatformType { get; set; }
        [Required]
        [StringLength(250)]
        public string Link { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateUpdated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }

        [ForeignKey(nameof(UserID))]
        [InverseProperty("Channels")]
        public virtual User User { get; set; }
    }
}
