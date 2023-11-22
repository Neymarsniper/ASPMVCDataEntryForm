using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MemberDataEntryForm.Models;

namespace MemberDataEntryForm.Models
{
    public class MemberDataContext : DbContext
    {
        public MemberDataContext()
        {
        }

        public MemberDataContext(DbContextOptions<MemberDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MemberData> MemberDirectoryData { get; set; }
        public virtual DbSet<UserData> UserDirectoryData { get; set; }
        public virtual DbSet<UserType> GetUserTypes { get; set; }
        public virtual DbSet<MembersFamilyData> MemberFamilyDirectoryData { get; set; }
        public virtual DbSet<MemberBusiessData> MemberBusinessDirectoryData { get; set; }
        public virtual DbSet<MemberAddressData> MemberAddressDirectoryData { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Define a relationship between UserData and UserType using the userType property
        //    modelBuilder.Entity<UserData>()
        //        .HasOne(u => u.UserType)
        //        .WithMany()
        //        .HasForeignKey(u => u.RoleId) // Assuming userType property in UserData is meant to store RoleName
        //        .IsRequired();

        //    modelBuilder.Entity<UserType>()
        //        .HasIndex(u => u.RoleName);
        //}
    }
}
