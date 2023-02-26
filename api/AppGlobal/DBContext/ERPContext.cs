using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AppGlobal.Entities;

#nullable disable

namespace AppGlobal.DBContext
{
    public partial class ERPContext : ApplicationDBContext
    {
        public ERPContext()
        {
        }

        public ERPContext(DbContextOptions<ERPContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<AssetConfig> AssetConfigs { get; set; }
        public virtual DbSet<AssetConsumed> AssetConsumeds { get; set; }
        public virtual DbSet<AssetImported> AssetImporteds { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<Clinic> Clinics { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryItem> CategoryItems { get; set; }
        public virtual DbSet<Diagnosis> Diagnoses { get; set; }
        public virtual DbSet<Disease> Diseases { get; set; }
        public virtual DbSet<Episode> Episodes { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Medication> Medications { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Record> Records { get; set; }
        public virtual DbSet<Referral> Referrals { get; set; }
        public virtual DbSet<Relative> Relatives { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Summary> Summaries { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.ActivityID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ActionCode).HasDefaultValueSql("((0))");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ServiceCode).HasDefaultValueSql("((0))");

                entity.Property(e => e.StatusCode).HasDefaultValueSql("((0))");

                entity.Property(e => e.Metadata).HasDefaultValueSql("('{}')");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Activities)
                    .HasForeignKey(d => d.ClinicID)
                    .HasConstraintName("FK_Activity_Clinic");

                entity.HasOne(d => d.UserTargeted)
                    .WithMany(p => p.ActivityUserTargeteds)
                    .HasForeignKey(d => d.UserTargetedID)
                    .HasConstraintName("FK_Activity_User1");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusID).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ClinicID)
                    .HasConstraintName("FK_Appointment_Clinic1");

                entity.HasOne(d => d.Referral)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ReferralID)
                    .HasConstraintName("FK_Appointment_Referral");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ServiceID)
                    .HasConstraintName("FK_Appointment_Service1");

                entity.HasOne(d => d.UserAppoint)
                    .WithMany(p => p.AppointmentUserAppoints)
                    .HasForeignKey(d => d.UserAppointID)
                    .HasConstraintName("FK_Appointment_User2");

                entity.HasOne(d => d.UserCreate)
                    .WithMany(p => p.AppointmentUserCreates)
                    .HasForeignKey(d => d.UserCreateID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_User1");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.Property(e => e.AttachmentID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AttachmentType)
                    .HasDefaultValueSql("((1))")
                    .HasComment("1 = Image"); ;

                entity.Property(e => e.TargetItemType)
                    .HasDefaultValueSql("((1))")
                    .HasComment("1 = Service, 2 = Asset");
            });

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.Property(e => e.AssetID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Amount).HasDefaultValueSql("((1))");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusID).HasDefaultValueSql("((1))");

                entity.Property(e => e.Type)
                    .HasDefaultValueSql("((1))")
                    .HasComment("1 = Asset, 2 = Consumable");
            });

            modelBuilder.Entity<AssetConfig>(entity =>
            {
                entity.Property(e => e.AssetConfigID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Amount).HasDefaultValueSql("((1))");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetConfigs)
                    .HasForeignKey(d => d.AssetID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetConfig_Asset");
            });

            modelBuilder.Entity<AssetConsumed>(entity =>
            {
                entity.Property(e => e.AssetConsumedID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Amount).HasDefaultValueSql("((1))");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetConsumeds)
                    .HasForeignKey(d => d.AssetID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetConsumed_Asset");
            });

            modelBuilder.Entity<AssetImported>(entity =>
            {
                entity.Property(e => e.AssetImportedID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Amount).HasDefaultValueSql("((1))");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetImporteds)
                    .HasForeignKey(d => d.AssetID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssetImported_Asset");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusID).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<CategoryItem>(entity =>
            {
                entity.Property(e => e.CategoryItemID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ItemType)
                    .HasDefaultValueSql("((1))")
                    .HasComment("1 = Service, 2 = Asset");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryItems)
                    .HasForeignKey(d => d.CategoryID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryItem_Category");
            });

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.Property(e => e.ChannelID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PlatformType).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Channels)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Channel_User");
            });

            modelBuilder.Entity<Clinic>(entity =>
            {
                entity.Property(e => e.ClinicID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Logo).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.StatusID).HasDefaultValueSql("((1))");

                entity.Property(e => e.TaxNumber).IsUnicode(false);
            });

            modelBuilder.Entity<Diagnosis>(entity =>
            {
                entity.Property(e => e.DiagnosisID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Disease)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(d => d.DiseaseID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Diagnosis_Disease");

                entity.HasOne(d => d.Summary)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(d => d.SummaryID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Diagnosis_Summary");
            });

            modelBuilder.Entity<Disease>(entity =>
            {
                entity.Property(e => e.DiseaseID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ICD10).IsUnicode(false);

                entity.Property(e => e.StatusID).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Episode>(entity =>
            {
                entity.Property(e => e.EpisodeID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AdmissionType).HasDefaultValueSql("((1))");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusID).HasDefaultValueSql("((1))");

                entity.Property(e => e.TimeStart).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Episodes)
                    .HasForeignKey(d => d.AppointmentID)
                    .HasConstraintName("FK_Episode_Appointment");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Episodes)
                    .HasForeignKey(d => d.ClinicID)
                    .HasConstraintName("FK_Episode_Clinic");

                entity.HasOne(d => d.Referral)
                    .WithMany(p => p.Episodes)
                    .HasForeignKey(d => d.ReferralID)
                    .HasConstraintName("FK_Episode_Referral");

                entity.HasOne(d => d.UserAdmitted)
                    .WithMany(p => p.Episodes)
                    .HasForeignKey(d => d.UserAdmittedID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Episode_User");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.InvoiceID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusID).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Episode)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.EpisodeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Episode");
            });

            modelBuilder.Entity<Medication>(entity =>
            {
                entity.Property(e => e.MedicationID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AmountPerDose).HasDefaultValueSql("((1))");

                entity.Property(e => e.BuyingUnit).HasDefaultValueSql("((1))");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DaysCount).HasDefaultValueSql("((1))");

                entity.Property(e => e.DosePerDay).HasDefaultValueSql("((1))");

                entity.Property(e => e.MedicineRoute).HasDefaultValueSql("((1))");

                entity.Property(e => e.TotalBuyingNumber).HasDefaultValueSql("((1))");

                entity.Property(e => e.TotalUnitsCount).HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitType).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.Medications)
                    .HasForeignKey(d => d.MedicineID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Medication_Medicine");

                entity.HasOne(d => d.Summary)
                    .WithMany(p => p.Medications)
                    .HasForeignKey(d => d.SummaryID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Medication_Summary");
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.Property(e => e.MedicineID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DefaultAmountPerDose).HasDefaultValueSql("((1))");

                entity.Property(e => e.DefaultBuyingNumber).HasDefaultValueSql("((1))");

                entity.Property(e => e.DefaultBuyingUnit).HasDefaultValueSql("((1))");

                entity.Property(e => e.DefaultDosePerDay).HasDefaultValueSql("((1))");

                entity.Property(e => e.DefaultMedRoute).HasDefaultValueSql("((1))");

                entity.Property(e => e.DefaultUsageNum).HasDefaultValueSql("((1))");

                entity.Property(e => e.StatusID).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.Property(e => e.ProviderID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserID).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Providers)
                    .HasForeignKey(d => d.ClinicID)
                    .HasConstraintName("FK_Provider_Clinic");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Providers)
                    .HasForeignKey(d => d.ServiceID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Provider_Service");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Providers)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Provider_User");
            });

            modelBuilder.Entity<Record>(entity =>
            {
                entity.Property(e => e.RecordID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusID).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Episode)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.EpisodeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Record_Episode");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.InvoiceID)
                    .HasConstraintName("FK_Record_Invoice");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.ServiceID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Record_Service");

                entity.HasOne(d => d.UserAppoint)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.UserAppointID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Record_User");
            });

            modelBuilder.Entity<Referral>(entity =>
            {
                entity.Property(e => e.ReferralID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusID).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Summary)
                    .WithMany(p => p.Referrals)
                    .HasForeignKey(d => d.SummaryID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Referral_Summary");
            });

            modelBuilder.Entity<Relative>(entity =>
            {
                entity.Property(e => e.RelativeID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RelativeType).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RelativeUsers)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Relative_User2");

                entity.HasOne(d => d.UserRelative)
                    .WithMany(p => p.RelativeUserRelatives)
                    .HasForeignKey(d => d.UserRelativeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Relative_User3");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoleType).HasDefaultValueSql("((1))");

                entity.Property(e => e.UserID).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.ClinicID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_Clinic");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Role_User");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.ScheduleID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.ClinicID)
                    .HasConstraintName("FK_Schedule_Clinic");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.UserID)
                    .HasConstraintName("FK_Schedule_User");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.ServiceID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Logo).IsUnicode(false);

                entity.Property(e => e.StatusID).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.ClinicID)
                    .HasConstraintName("FK_Service_Clinic");
            });

            modelBuilder.Entity<Summary>(entity =>
            {
                entity.Property(e => e.SummaryID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusID).HasDefaultValueSql("((1))");

                entity.Property(e => e.Title).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Episode)
                    .WithMany(p => p.Summaries)
                    .HasForeignKey(d => d.EpisodeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Summary_Episode1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserID).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Avatar).IsUnicode(false);

                entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.IdentityID).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.StatusID).HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
