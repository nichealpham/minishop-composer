using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("AssetConfig")]
    public partial class AssetConfig : TrackEntity
    {
        [Key]
        public Guid AssetConfigID { get; set; }
        public Guid AssetID { get; set; }
        public int Amount { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Price { get; set; }
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        public Guid ServiceID { get; set; }
        public Guid UserCreatedID { get; set; }

        [ForeignKey(nameof(AssetID))]
        [InverseProperty("AssetConfigs")]
        public virtual Asset Asset { get; set; }
    }
}
