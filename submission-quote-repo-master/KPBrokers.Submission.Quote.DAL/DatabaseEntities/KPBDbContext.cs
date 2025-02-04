using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KPBrokers.Submission.Quote.DAL.DatabaseEntities;

public partial class KPBDbContext : IdentityDbContext
{
    public KPBDbContext()
    {
    }

    public KPBDbContext(DbContextOptions<KPBDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<AgentContact> AgentContacts { get; set; }

    public virtual DbSet<AuditAction> AuditActions { get; set; }

    public virtual DbSet<AuditTrail> AuditTrails { get; set; }

    public virtual DbSet<Broker> Brokers { get; set; }

    public virtual DbSet<BrokerContact> BrokerContacts { get; set; }

    public virtual DbSet<Carrier> Carriers { get; set; }

    public virtual DbSet<CarrierContact> CarrierContacts { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Coverage> Coverages { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Insured> Insureds { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Metadata> Metadata { get; set; }

    public virtual DbSet<QuoteDocument> QuoteDocuments { get; set; }    

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }

    public virtual DbSet<SubmissionUnit> SubmissionUnits { get; set; }

    public virtual DbSet<SystemLog> SystemLogs { get; set; }

    public virtual DbSet<Title> Titles { get; set; }   

    public virtual DbSet<UserAccount> UserAccounts { get; set; }    

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=kpbsqldev.database.windows.net;Database=KPBQuoteSubmission;User Id=kpbrokersadmin;Password=KPBr0k3r$London2024!;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserAccount>()
            .ToTable("UserAccount");           

        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.Property(e => e.AddressLine1).HasMaxLength(100);
            entity.Property(e => e.AddressLine2).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.PostalCode).HasMaxLength(100);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Country).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Address_Country");
        });

        modelBuilder.Entity<Agent>(entity =>
        {
            entity.ToTable("Agent");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DBA).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Address).WithMany(p => p.Agents)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Agent_Address");
        });

        modelBuilder.Entity<AgentContact>(entity =>
        {
            entity.ToTable("AgentContact");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Agent).WithMany(p => p.AgentContacts)
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AgentContact_Agent");

            entity.HasOne(d => d.Title).WithMany(p => p.AgentContacts)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AgentContact_Title");
        });

        modelBuilder.Entity<AuditAction>(entity =>
        {
            entity.HasKey(e => e.ActionId);

            entity.ToTable("AuditAction");

            entity.Property(e => e.ActionName).HasMaxLength(50);
        });

        modelBuilder.Entity<AuditTrail>(entity =>
        {
            entity.ToTable("AuditTrail");

            entity.Property(e => e.ActionPerformedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Action).WithMany(p => p.AuditTrails)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditTrail_AuditAction");

            entity.HasOne(d => d.Submission).WithMany(p => p.AuditTrails)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditTrail_Submission");
        });       

        modelBuilder.Entity<Broker>(entity =>
        {
            entity.ToTable("Broker");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DBA).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Address).WithMany(p => p.Brokers)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Broker_Address");
        });

        modelBuilder.Entity<BrokerContact>(entity =>
        {
            entity.ToTable("BrokerContact");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Broker).WithMany(p => p.BrokerContacts)
                .HasForeignKey(d => d.BrokerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BrokerContact_Broker");

            entity.HasOne(d => d.Title).WithMany(p => p.BrokerContacts)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BrokerContact_Title");
        });

        modelBuilder.Entity<Carrier>(entity =>
        {
            entity.ToTable("Carrier");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DBA).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Address).WithMany(p => p.Carriers)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Carrier_Address");
        });

        modelBuilder.Entity<CarrierContact>(entity =>
        {
            entity.ToTable("CarrierContact");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Carrier).WithMany(p => p.CarrierContacts)
                .HasForeignKey(d => d.CarrierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarrierContact_Carrier");

            entity.HasOne(d => d.Title).WithMany(p => p.CarrierContacts)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarrierContact_Title");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.CountryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Coverage>(entity =>
        {
            entity.ToTable("Coverage");

            entity.Property(e => e.CoverageName).HasMaxLength(50);
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.ToTable("Driver");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.DriverLicenceExpiry).HasColumnType("datetime");
            entity.Property(e => e.DriverLicenceIssued).HasColumnType("datetime");
            entity.Property(e => e.DriverLicenceNumber).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Insured>(entity =>
        {
            entity.ToTable("Insured");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DBA).HasMaxLength(100);
            entity.Property(e => e.InsuredName).HasMaxLength(100);
            entity.Property(e => e.MCDocketNumber).HasMaxLength(50);
            entity.Property(e => e.MainContactEmail).HasMaxLength(50);
            entity.Property(e => e.MainContactName).HasMaxLength(100);
            entity.Property(e => e.MainContactPhone).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Address).WithMany(p => p.Insureds)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Insured_Address");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.LogID).HasName("PK__Logs__5E5499A8EF101872");

            entity.Property(e => e.ErrorClass).HasMaxLength(200);
            entity.Property(e => e.ErrorMethod).HasMaxLength(200);
            entity.Property(e => e.ErrorSource).HasMaxLength(200);
            entity.Property(e => e.EventDateTime).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.EventLevel).HasMaxLength(50);
            entity.Property(e => e.MachineName).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<Metadata>(entity =>
        {
            entity.Property(e => e.MetadataName).HasMaxLength(50);
        });

        modelBuilder.Entity<QuoteDocument>(entity =>
        {
            entity.ToTable("QuoteDocument");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DocumentName).HasMaxLength(100);
            entity.Property(e => e.DocumentSize).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Submission).WithMany(p => p.QuoteDocuments)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuoteDocument_Submission");
        });       

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.StatusName).HasMaxLength(50);
        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.ToTable("Submission");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.PossibleExpityDate).HasColumnType("datetime");
            entity.Property(e => e.PossibleInceptionDate).HasColumnType("datetime");
            entity.Property(e => e.Premium).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.QuotedReference).HasMaxLength(50);
            entity.Property(e => e.Reference).HasMaxLength(50);
            entity.Property(e => e.SubmittedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Agent).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Submission_Agent");

            entity.HasOne(d => d.Broker).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.BrokerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Submission_Broker");

            entity.HasOne(d => d.Carrier).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.CarrierId)
                .HasConstraintName("FK_Submission_Carrier");

            entity.HasOne(d => d.Coverage).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.CoverageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Submission_Coverage");

            entity.HasOne(d => d.Insured).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.InsuredId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Submission_Insured");

            entity.HasOne(d => d.Status).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Submission_Status");
        });

        modelBuilder.Entity<SubmissionUnit>(entity =>
        {
            entity.ToTable("SubmissionUnit");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Driver).WithMany(p => p.SubmissionUnits)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubmissionUnit_Driver");

            entity.HasOne(d => d.Submission).WithMany(p => p.SubmissionUnits)
                .HasForeignKey(d => d.SubmissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubmissionUnit_Submission");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.SubmissionUnits)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubmissionUnit_Vehicle");
        });

        modelBuilder.Entity<SystemLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Logs");

            entity.Property(e => e.ErrorClass).HasMaxLength(500);
            entity.Property(e => e.ErrorSource).HasMaxLength(100);
            entity.Property(e => e.EventDateTime).HasColumnType("datetime");
            entity.Property(e => e.EventLevel).HasMaxLength(100);
            entity.Property(e => e.MachineName).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.ToTable("Title");

            entity.Property(e => e.TitleName).HasMaxLength(50);
        });        

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("UserAccount");

            entity.Property(e => e.Fullname).HasMaxLength(100);
            entity.Property(e => e.IdentityId).HasMaxLength(128);            
        });   

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("Vehicle");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Make).HasMaxLength(100);
            entity.Property(e => e.Model).HasMaxLength(100);
            entity.Property(e => e.RegNumber).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.VIN).HasMaxLength(50);
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
