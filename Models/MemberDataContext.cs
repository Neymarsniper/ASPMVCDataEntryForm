using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
    }
}
