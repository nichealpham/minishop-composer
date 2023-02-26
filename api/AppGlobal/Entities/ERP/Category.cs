using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AppGlobal.Entities
{
    [Table("Category")]
    public partial class Category : AuditEntity
    {
        public Category()
        {
            CategoryItems = new HashSet<CategoryItem>();
        }

        [Key]
        public Guid CategoryID { get; set; }
        [Required]
        [StringLength(500)]
        public string CategoryName { get; set; }
        public Guid ClinicID { get; set; }

        [InverseProperty(nameof(CategoryItem.Category))]
        public virtual ICollection<CategoryItem> CategoryItems { get; set; }
    }

    [Table("CategoryItem")]
    public partial class CategoryItem
    {
        public CategoryItem()
        {
        }

        [Key]
        public Guid CategoryItemID { get; set; }
        public Guid CategoryID { get; set; }
        public Guid ItemID { get; set; }
        [Required]
        public byte ItemType { get; set; }

        [ForeignKey(nameof(CategoryID))]
        [InverseProperty("CategoryItems")]
        public virtual Category Category { get; set; }
    }
}

    
