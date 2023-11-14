using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Telekocsi;

namespace Telekocsi.Datadirectory
{
    public partial class DataCTX : DbContext
    {
        public DataCTX()
        {
        }

        public DataCTX(DbContextOptions<DataCTX> options)
            : base(options)
        {
        }

        public virtual DbSet<Cars> Cars { get; set; } = null!;
        public virtual DbSet<Lines> Lines { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string p = Environment.GetEnvironmentVariable("DB");
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
// optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Ricardo\\Documents\\telecar.mdf;Integrated Security=True;Connect Timeout=30");
                optionsBuilder.UseSqlServer($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={p}\\telecar.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cars>(entity =>
            {
                entity.Property(e => e.Start).HasColumnType("datetime");
            });

            modelBuilder.Entity<Lines>(entity =>
            {
                entity.Property(e => e.Arrival).HasColumnType("text");

                entity.Property(e => e.Start).HasColumnType("text");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
