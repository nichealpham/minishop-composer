using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppGlobal.Entities
{
    [Table("Schedule")]
    public partial class Schedule
    {
        [Key]
        public Guid ScheduleID { get; set; }
        public Guid? ClinicID { get; set; }
        public Guid? UserID { get; set; }
        public short TimeStart { get; set; }
        public short TimeEnd { get; set; }
        public byte DayOfWeek { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateUpdated { get; set; }

        [ForeignKey(nameof(ClinicID))]
        [InverseProperty("Schedules")]
        public virtual Clinic Clinic { get; set; }
        [ForeignKey(nameof(UserID))]
        [InverseProperty("Schedules")]
        public virtual User User { get; set; }
    }
}
