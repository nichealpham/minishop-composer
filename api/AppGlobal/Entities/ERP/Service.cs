using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Service")]
    public partial class Service : AuditEntity
    {
        public Service()
        {
            Appointments = new HashSet<Appointment>();
            Providers = new HashSet<Provider>();
            Records = new HashSet<Record>();
        }

        [Key]
        public Guid ServiceID { get; set; }
        [Required]
        [StringLength(150)]
        public string ServiceName { get; set; }
        public Guid? ClinicID { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
        [StringLength(250)]
        public string Logo { get; set; }
        public Guid? ParentServiceID { get; set; }
        public bool? IsSaleOrder { get; set; }
        [ForeignKey(nameof(ClinicID))]
        [InverseProperty("Services")]
        public virtual Clinic Clinic { get; set; }
        [InverseProperty(nameof(Appointment.Service))]
        public virtual ICollection<Appointment> Appointments { get; set; }
        [InverseProperty(nameof(Provider.Service))]
        public virtual ICollection<Provider> Providers { get; set; }
        [InverseProperty(nameof(Record.Service))]
        public virtual ICollection<Record> Records { get; set; }
    }
}
