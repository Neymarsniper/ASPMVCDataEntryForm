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
        public virtual DbSet<MemberBusinessData> MemberBusinessDirectoryData { get; set; }
        public virtual DbSet<MemberAddressData> MemberAddressDirectoryData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberData>().HasOne(md => md.FamilyData).WithOne(md => md.MemberData).HasForeignKey<MembersFamilyData>(fd => fd.MemNo);

            modelBuilder.Entity<MemberData>().HasOne(md => md.BusinessData).WithOne(md => md.MemberData).HasForeignKey<MemberBusinessData>(bd => bd.MemNo);

            modelBuilder.Entity<MemberData>().HasOne(md => md.AddressData).WithOne(md => md.MemberData).HasForeignKey<MemberAddressData>(ad => ad.MemNo);

            //modelBuilder.Entity<UserType>().HasMany(md => md.userData).WithOne(md => md.UserType).HasForeignKey<UserData>(ud => ud.UserRoleId);
        }

    }
}
