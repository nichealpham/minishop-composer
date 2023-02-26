using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Clinic")]
    public partial class Clinic : AuditEntity
    {
        public Clinic()
        {
            Activities = new HashSet<Activity>();
            Appointments = new HashSet<Appointment>();
            Episodes = new HashSet<Episode>();
            Providers = new HashSet<Provider>();
            Roles = new HashSet<Role>();
            Schedules = new HashSet<Schedule>();
            Services = new HashSet<Service>();
        }

        [Key]
        public Guid ClinicID { get; set; }
        [Required]
        [StringLength(150)]
        public string ClinicName { get; set; }
        [StringLength(250)]
        public string ShortDescription { get; set; }
        [StringLength(250)]
        public string Logo { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(20)]
        public string TaxNumber { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        public bool? PublicStatus { get; set; }
        [StringLength(200)]
        public string PublicNameUrl { get; set; }

        [InverseProperty(nameof(Activity.Clinic))]
        public virtual ICollection<Activity> Activities { get; set; }
        [InverseProperty(nameof(Appointment.Clinic))]
        public virtual ICollection<Appointment> Appointments { get; set; }
        [InverseProperty(nameof(Episode.Clinic))]
        public virtual ICollection<Episode> Episodes { get; set; }
        [InverseProperty(nameof(Provider.Clinic))]
        public virtual ICollection<Provider> Providers { get; set; }
        [InverseProperty(nameof(Role.Clinic))]
        public virtual ICollection<Role> Roles { get; set; }
        [InverseProperty(nameof(Schedule.Clinic))]
        public virtual ICollection<Schedule> Schedules { get; set; }
        [InverseProperty(nameof(Service.Clinic))]
        public virtual ICollection<Service> Services { get; set; }
    }
}
