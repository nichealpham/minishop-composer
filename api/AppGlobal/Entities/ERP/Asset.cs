using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Asset")]
    public partial class Asset : AuditEntity
    {
        public Asset()
        {
            AssetConfigs = new HashSet<AssetConfig>();
            AssetConsumeds = new HashSet<AssetConsumed>();
            AssetImporteds = new HashSet<AssetImported>();
        }

        [Key]
        public Guid AssetID { get; set; }
        [Required]
        [StringLength(250)]
        public string AssetName { get; set; }
        [StringLength(100)]
        public string AssetCode { get; set; }
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        public byte Type { get; set; }
        public int Amount { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Price { get; set; }
        public Guid ClinicID { get; set; }
        public Guid UserCreatedID { get; set; }
        public bool? IsSale { get; set; }
        public string SaleDescription { get; set; }

        [InverseProperty(nameof(AssetConfig.Asset))]
        public virtual ICollection<AssetConfig> AssetConfigs { get; set; }
        [InverseProperty(nameof(AssetConsumed.Asset))]
        public virtual ICollection<AssetConsumed> AssetConsumeds { get; set; }
        [InverseProperty(nameof(AssetImported.Asset))]
        public virtual ICollection<AssetImported> AssetImporteds { get; set; }
    }
}
