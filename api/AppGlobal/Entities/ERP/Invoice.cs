using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Invoice")]
    public partial class Invoice : AuditEntity
    {
        public Invoice()
        {
            Records = new HashSet<Record>();
        }

        [Key]
        public Guid InvoiceID { get; set; }
        [Required]
        [StringLength(100)]
        public string InvoiceNumber { get; set; }
        [StringLength(500)]
        public string Title { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal TotalPrice { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal DiscountPrice { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal FinalPrice { get; set; }
        public Guid EpisodeID { get; set; }

        [ForeignKey(nameof(EpisodeID))]
        [InverseProperty("Invoices")]
        public virtual Episode Episode { get; set; }
        [InverseProperty(nameof(Record.Invoice))]
        public virtual ICollection<Record> Records { get; set; }
    }
}
