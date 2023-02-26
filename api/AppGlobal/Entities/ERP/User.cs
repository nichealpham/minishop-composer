using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("User")]
    public partial class User : AuditEntity
    {
        public User()
        {
            ActivityUserTargeteds = new HashSet<Activity>();
            AppointmentUserAppoints = new HashSet<Appointment>();
            AppointmentUserCreates = new HashSet<Appointment>();
            Channels = new HashSet<Channel>();
            Episodes = new HashSet<Episode>();
            Providers = new HashSet<Provider>();
            Records = new HashSet<Record>();
            RelativeUserRelatives = new HashSet<Relative>();
            RelativeUsers = new HashSet<Relative>();
            Roles = new HashSet<Role>();
            Schedules = new HashSet<Schedule>();
        }

        [Key]
        public Guid UserID { get; set; }
        [Required]
        [StringLength(150)]
        public string FullName { get; set; }
        public bool? GenderType { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Dob { get; set; }
        [StringLength(250)]
        public string Avatar { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string IdentityID { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        [StringLength(150)]
        public string Occupation { get; set; }
        [StringLength(150)]
        public string Country { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        [InverseProperty(nameof(Activity.UserTargeted))]
        public virtual ICollection<Activity> ActivityUserTargeteds { get; set; }

        [InverseProperty(nameof(Appointment.UserAppoint))]
        public virtual ICollection<Appointment> AppointmentUserAppoints { get; set; }
        [InverseProperty(nameof(Appointment.UserCreate))]
        public virtual ICollection<Appointment> AppointmentUserCreates { get; set; }
        [InverseProperty(nameof(Channel.User))]
        public virtual ICollection<Channel> Channels { get; set; }
        [InverseProperty(nameof(Episode.UserAdmitted))]
        public virtual ICollection<Episode> Episodes { get; set; }
        [InverseProperty(nameof(Provider.User))]
        public virtual ICollection<Provider> Providers { get; set; }
        [InverseProperty(nameof(Record.UserAppoint))]
        public virtual ICollection<Record> Records { get; set; }
        [InverseProperty(nameof(Relative.UserRelative))]
        public virtual ICollection<Relative> RelativeUserRelatives { get; set; }
        [InverseProperty(nameof(Relative.User))]
        public virtual ICollection<Relative> RelativeUsers { get; set; }
        [InverseProperty(nameof(Role.User))]
        public virtual ICollection<Role> Roles { get; set; }
        [InverseProperty(nameof(Schedule.User))]
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
