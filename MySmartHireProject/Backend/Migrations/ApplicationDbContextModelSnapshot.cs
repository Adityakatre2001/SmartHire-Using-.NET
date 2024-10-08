﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace SmartHire.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("SmartHire.Models.Company", b =>
                {
                    b.Property<long>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("CompanyId"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("company_name");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)");

                    b.Property<string>("Industry")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("CompanyId");

                    b.ToTable("company", (string)null);
                });

            modelBuilder.Entity("SmartHire.Models.JobApplication", b =>
                {
                    b.Property<long>("ApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("ApplicationId"));

                    b.Property<DateTime>("ApplicationDate")
                        .HasColumnType("date")
                        .HasColumnName("application_date");

                    b.Property<long>("JobPostingId")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("ApplicationId");

                    b.HasIndex("JobPostingId");

                    b.HasIndex("UserId");

                    b.ToTable("application", (string)null);
                });

            modelBuilder.Entity("SmartHire.Models.JobPosting", b =>
                {
                    b.Property<long>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("JobId"));

                    b.Property<DateTime?>("CloseDate")
                        .HasColumnType("date")
                        .HasColumnName("close_date");

                    b.Property<long>("EmployerId")
                        .HasColumnType("bigint");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)");

                    b.Property<string>("JobLocation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("PostDate")
                        .HasColumnType("date")
                        .HasColumnName("post_date");

                    b.Property<double>("Salary")
                        .HasColumnType("double");

                    b.HasKey("JobId");

                    b.HasIndex("EmployerId");

                    b.ToTable("job_posting", (string)null);
                });

            modelBuilder.Entity("SmartHire.Models.Message", b =>
                {
                    b.Property<long>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("MessageId"));

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)")
                        .HasColumnName("message_content");

                    b.Property<DateTime>("MessageTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("message_time");

                    b.Property<long>("ReceiverId")
                        .HasColumnType("bigint");

                    b.Property<long>("SenderId")
                        .HasColumnType("bigint");

                    b.HasKey("MessageId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("message", (string)null);
                });

            modelBuilder.Entity("SmartHire.Models.Profile", b =>
                {
                    b.Property<long>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("ProfileId"));

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Experience")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)");

                    b.Property<byte[]>("Resume")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("Skills")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("ProfileId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles", (string)null);
                });

            modelBuilder.Entity("SmartHire.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("SmartHire.Models.JobApplication", b =>
                {
                    b.HasOne("SmartHire.Models.JobPosting", "JobPosting")
                        .WithMany("Applications")
                        .HasForeignKey("JobPostingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartHire.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobPosting");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartHire.Models.JobPosting", b =>
                {
                    b.HasOne("SmartHire.Models.Company", "Employer")
                        .WithMany("JobPostings")
                        .HasForeignKey("EmployerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employer");
                });

            modelBuilder.Entity("SmartHire.Models.Message", b =>
                {
                    b.HasOne("SmartHire.Models.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SmartHire.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("SmartHire.Models.Profile", b =>
                {
                    b.HasOne("SmartHire.Models.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("SmartHire.Models.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartHire.Models.Company", b =>
                {
                    b.Navigation("JobPostings");
                });

            modelBuilder.Entity("SmartHire.Models.JobPosting", b =>
                {
                    b.Navigation("Applications");
                });

            modelBuilder.Entity("SmartHire.Models.User", b =>
                {
                    b.Navigation("Profile")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
