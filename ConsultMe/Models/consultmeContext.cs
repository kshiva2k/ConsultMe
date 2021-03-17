using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ConsultMe.Models
{
    public partial class consultmeContext : DbContext
    {
        public consultmeContext()
        {
        }

        public consultmeContext(DbContextOptions<consultmeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<Clinic> Clinic { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Customertimings> Customertimings { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<Lookupspecialist> Lookupspecialist { get; set; }
        public virtual DbSet<Lookupusertype> Lookupusertype { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Subscriptions> Subscriptions { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Server=127.0.0.1;User Id=root;Password=Mysql@2020;Database=consultme");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("appointment");

                entity.HasIndex(e => e.Customerid)
                    .HasName("appointment_customer_id_idx");

                entity.HasIndex(e => e.Userid)
                    .HasName("appointment_user_id_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Appointmentdate).HasColumnName("appointmentdate");

                entity.Property(e => e.Bookingnumber)
                    .HasColumnName("bookingnumber")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Createddate).HasColumnName("createddate");

                entity.Property(e => e.Customerid)
                    .HasColumnName("customerid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Patientid)
                    .HasColumnName("patientid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Symptoms)
                    .HasColumnName("symptoms")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.Customerid)
                    .HasConstraintName("appointment_customer_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("appointment_user_id");
            });

            modelBuilder.Entity<Clinic>(entity =>
            {
                entity.ToTable("clinic");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Area)
                    .HasColumnName("area")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Contactnumber)
                    .HasColumnName("contactnumber")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.Specialistid)
                    .HasName("customer_specialist_id_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Area)
                    .HasColumnName("area")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Ratings)
                    .HasColumnName("ratings")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Specialistid)
                    .HasColumnName("specialistid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Specialization)
                    .HasColumnName("specialization")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.HasOne(d => d.Specialist)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.Specialistid)
                    .HasConstraintName("customer_specialist_id");
            });

            modelBuilder.Entity<Customertimings>(entity =>
            {
                entity.ToTable("customertimings");

                entity.HasIndex(e => e.Clinicid)
                    .HasName("customertimings_clinic_idx");

                entity.HasIndex(e => e.Customerid)
                    .HasName("customertimings_customer_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Bookinglimit)
                    .HasColumnName("bookinglimit")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Clinicid)
                    .HasColumnName("clinicid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Clinictimings)
                    .HasColumnName("clinictimings")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Customerid)
                    .HasColumnName("customerid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Reviewtime)
                    .HasColumnName("reviewtime")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Customertimings)
                    .HasForeignKey(d => d.Clinicid)
                    .HasConstraintName("customertimings_clinic");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Customertimings)
                    .HasForeignKey(d => d.Customerid)
                    .HasConstraintName("customertimings_customer");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("feedback");

                entity.HasIndex(e => e.Appointmentid)
                    .HasName("feedbackappoint_idx");

                entity.HasIndex(e => e.Customerid)
                    .HasName("feedbackcustomer_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Appointmentid)
                    .HasColumnName("appointmentid")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Customerid)
                    .HasColumnName("customerid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Ratings)
                    .HasColumnName("ratings")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.Appointmentid)
                    .HasConstraintName("feedbackappoint");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.Customerid)
                    .HasConstraintName("feedbackcustomer");
            });

            modelBuilder.Entity<Lookupspecialist>(entity =>
            {
                entity.ToTable("lookupspecialist");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<Lookupusertype>(entity =>
            {
                entity.ToTable("lookupusertype");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("patient");

                entity.HasIndex(e => e.Userid)
                    .HasName("patientuserid_id_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Age)
                    .HasColumnName("age")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("patientuserid_id");
            });

            modelBuilder.Entity<Subscriptions>(entity =>
            {
                entity.ToTable("subscriptions");

                entity.HasIndex(e => e.Customerid)
                    .HasName("customerid_id_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Customerid)
                    .HasColumnName("customerid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Enddate).HasColumnName("enddate");

                entity.Property(e => e.Startdate).HasColumnName("startdate");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.Customerid)
                    .HasConstraintName("customerid_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Usertypeid)
                    .HasName("usertypeid_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Displayname)
                    .HasColumnName("displayname")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Mobilenumber)
                    .HasColumnName("mobilenumber")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Usertypeid)
                    .HasColumnName("usertypeid")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Usertype)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.Usertypeid)
                    .HasConstraintName("usertypeid_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
