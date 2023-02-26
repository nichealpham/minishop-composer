using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("AssetImported")]
    public partial class AssetImported : TrackEntity
    {
        [Key]
        public Guid AssetImportedID { get; set; }
        public Guid AssetID { get; set; }
        public int Amount { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Price { get; set; }
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        public Guid UserCreatedID { get; set; }

        [ForeignKey(nameof(AssetID))]
        [InverseProperty("AssetImporteds")]
        public virtual Asset Asset { get; set; }
    }
}
