namespace SMSService.DBAccessLayer.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SMSContext : DbContext
    {
        public SMSContext()
            : base("name=SMSContextDB")
        {
        }

        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<ApplicationSender> ApplicationSenders { get; set; }
        public virtual DbSet<InCommingSM> InCommingSMS { get; set; }
        public virtual DbSet<MobileNumber> MobileNumbers { get; set; }
        public virtual DbSet<OutGoingSMSBasicInfo> OutGoingSMSBasicInfoes { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Respons> Responses { get; set; }
        public virtual DbSet<SMSSenderNumber> SMSSenderNumbers { get; set; }
        public virtual DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .HasMany(e => e.ApplicationSenders)
                .WithRequired(e => e.Application)
                .HasForeignKey(e => e.AppId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationSender>()
                .HasMany(e => e.OutGoingSMSBasicInfoes)
                .WithOptional(e => e.ApplicationSender)
                .HasForeignKey(e => e.AppSenderId);

            modelBuilder.Entity<MobileNumber>()
                .HasMany(e => e.InCommingSMS)
                .WithRequired(e => e.MobileNumber)
                .HasForeignKey(e => e.MNumberId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MobileNumber>()
                .HasMany(e => e.SMSSenderNumbers)
                .WithOptional(e => e.MobileNumber)
                .HasForeignKey(e => e.MNumberId);

            modelBuilder.Entity<OutGoingSMSBasicInfo>()
                .HasMany(e => e.SMSSenderNumbers)
                .WithOptional(e => e.OutGoingSMSBasicInfo)
                .HasForeignKey(e => e.SMSId);

            modelBuilder.Entity<Respons>()
                .HasMany(e => e.OutGoingSMSBasicInfoes)
                .WithOptional(e => e.Respons)
                .HasForeignKey(e => e.ResponseId);
        }
    }
}
