using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ConsultMe.Data.Models
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
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Clinic> Clinic { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Doctortimings> Doctortimings { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<Lookupappointmentstatus> Lookupappointmentstatus { get; set; }
        public virtual DbSet<Lookupspecialist> Lookupspecialist { get; set; }
        public virtual DbSet<Lookupusertype> Lookupusertype { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Subscriptions> Subscriptions { get; set; }
        public virtual DbSet<User> User { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;user=root;password=Mysql@2020;database=consultme");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("appointment");

                entity.HasIndex(e => e.Clinicid)
                    .HasName("appointment_clinic_id_idx");

                entity.HasIndex(e => e.Doctorid)
                    .HasName("appointment_doctor_id_idx");

                entity.HasIndex(e => e.Timingid)
                    .HasName("appointment_timing_id_idx");

                entity.HasIndex(e => e.Userid)
                    .HasName("appointment_user_id_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Appointmentdate).HasColumnName("appointmentdate");

                entity.Property(e => e.Appointmenttype).HasColumnName("appointmenttype");

                entity.Property(e => e.Bookingnumber)
                    .HasColumnName("bookingnumber")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Clinicid).HasColumnName("clinicid");

                entity.Property(e => e.Createddate).HasColumnName("createddate");

                entity.Property(e => e.Doctorid).HasColumnName("doctorid");

                entity.Property(e => e.Patientid).HasColumnName("patientid");

                entity.Property(e => e.Symptoms)
                    .HasColumnName("symptoms")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Timingid).HasColumnName("timingid");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.Clinicid)
                    .HasConstraintName("appointment_clinic_id");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.Doctorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("appointment_doctor_id");

                entity.HasOne(d => d.Timing)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.Timingid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("appointment_timing_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("appointment_user_id");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("bit(1)");
            });

            modelBuilder.Entity<Clinic>(entity =>
            {
                entity.ToTable("clinic");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
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

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("doctor");

                entity.HasIndex(e => e.Cityid)
                    .HasName("doctor_city_id_idx");

                entity.HasIndex(e => e.Specialistid)
                    .HasName("doctor_specialist_id_idx");

                entity.HasIndex(e => e.Userid)
                    .HasName("doctor_userid_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Area)
                    .HasColumnName("area")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cityid).HasColumnName("cityid");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Includesearch)
                    .HasColumnName("includesearch")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Ratings).HasColumnName("ratings");

                entity.Property(e => e.Specialistid).HasColumnName("specialistid");

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
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Subscriber)
                    .HasColumnName("subscriber")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.Cityid)
                    .HasConstraintName("doctor_city_id");

                entity.HasOne(d => d.Specialist)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.Specialistid)
                    .HasConstraintName("doctor_specialist_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("doctor_userid");
            });

            modelBuilder.Entity<Doctortimings>(entity =>
            {
                entity.ToTable("doctortimings");

                entity.HasIndex(e => e.Clinicid)
                    .HasName("doctortimings_clinic_idx");

                entity.HasIndex(e => e.Doctorid)
                    .HasName("doctortimings_doctor_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bookinglimit).HasColumnName("bookinglimit");

                entity.Property(e => e.Clinicid).HasColumnName("clinicid");

                entity.Property(e => e.Doctorid).HasColumnName("doctorid");

                entity.Property(e => e.Endtime)
                    .HasColumnName("endtime")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.Reviewtime).HasColumnName("reviewtime");

                entity.Property(e => e.Starttime)
                    .HasColumnName("starttime")
                    .HasColumnType("decimal(5,2)");

                entity.HasOne(d => d.Clinic)
                    .WithMany(p => p.Doctortimings)
                    .HasForeignKey(d => d.Clinicid)
                    .HasConstraintName("doctortimings_clinic");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Doctortimings)
                    .HasForeignKey(d => d.Doctorid)
                    .HasConstraintName("doctortimings_customer");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("feedback");

                entity.HasIndex(e => e.Appointmentid)
                    .HasName("feedbackappoint_idx");

                entity.HasIndex(e => e.Doctorid)
                    .HasName("feedbackdoctor_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Appointmentid).HasColumnName("appointmentid");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Doctorid).HasColumnName("doctorid");

                entity.Property(e => e.Ratings).HasColumnName("ratings");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.Appointmentid)
                    .HasConstraintName("feedbackappoint");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.Doctorid)
                    .HasConstraintName("feedbackdoctor");
            });

            modelBuilder.Entity<Lookupappointmentstatus>(entity =>
            {
                entity.ToTable("lookupappointmentstatus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Lookupspecialist>(entity =>
            {
                entity.ToTable("lookupspecialist");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<Lookupusertype>(entity =>
            {
                entity.ToTable("lookupusertype");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("patient");

                entity.HasIndex(e => e.Userid)
                    .HasName("patientuserid_id_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Refno)
                    .HasColumnName("refno")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("patientuserid_id");
            });

            modelBuilder.Entity<Subscriptions>(entity =>
            {
                entity.ToTable("subscriptions");

                entity.HasIndex(e => e.Doctorid)
                    .HasName("doctorid_id_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Doctorid).HasColumnName("doctorid");

                entity.Property(e => e.Enddate).HasColumnName("enddate");

                entity.Property(e => e.Startdate).HasColumnName("startdate");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.Doctorid)
                    .HasConstraintName("doctorid_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Doctorid)
                    .HasName("usertype_doctorid_idx");

                entity.HasIndex(e => e.Usertypeid)
                    .HasName("usertypeid_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Displayname)
                    .HasColumnName("displayname")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Doctorid).HasColumnName("doctorid");

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

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Usertypeid).HasColumnName("usertypeid");

                entity.HasOne(d => d.DoctorNavigation)
                    .WithMany(p => p.UserNavigation)
                    .HasForeignKey(d => d.Doctorid)
                    .HasConstraintName("usertype_doctorid");

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
