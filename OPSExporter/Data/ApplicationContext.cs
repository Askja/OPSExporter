using Microsoft.EntityFrameworkCore;
using OPSExporter.Data.Entity;

namespace OPSExporter.Data;

public class ApplicationContext : DbContext {
    public DbSet<Node> Nodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=admin");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Node>(entity => {
            entity
                .HasNoKey()
                .ToView("nodehistoryview");

            entity.Property(e => e.ActualTime)
                .HasColumnName("actualtime");
            entity.Property(e => e.DeviceName).HasColumnName("tagname");
            entity.Property(e => e.Time)
                .HasColumnName("time");
            entity.Property(e => e.ValueDouble).HasColumnName("valdouble");
            entity.Property(e => e.ValueInteger).HasColumnName("valint");
            entity.Property(e => e.ValueUInt).HasColumnName("valuint");
        });
    }
}