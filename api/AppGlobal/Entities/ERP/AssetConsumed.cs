using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("AssetConsumed")]
    public partial class AssetConsumed : TrackEntity
    {
        [Key]
        public Guid AssetConsumedID { get; set; }
        public Guid AssetID { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Price { get; set; }
        public int Amount { get; set; }
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        public Guid ServiceID { get; set; }
        public Guid EpisodeID { get; set; }
        public Guid UserCreatedID { get; set; }

        [ForeignKey(nameof(AssetID))]
        [InverseProperty("AssetConsumeds")]
        public virtual Asset Asset { get; set; }
    }
}
