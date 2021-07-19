using Microsoft.EntityFrameworkCore;
using Process.Editor.Elements;
using Process.Simulation.Elements;
using ProcessManagement.DataStorage.EF.Configuration;

namespace ProcessManagement.DataStorage.EF
{
    public class PMDataContext : DbContext
    {
        public DbSet<ProcessModel> Process { get; set; }
        public DbSet<ProcessGroupModel> ProcessGroup { get; set; }
        public DbSet<ProcessPoint> ProcessPoint { get; set; }
        public DbSet<TableA> TableA { get; set; }
        public DbSet<TableB> TableB { get; set; }

        public DbSet<SimulationPointModel> SimulationPoint { get; set; }


        public PMDataContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ABIDMAHMOOD\AMSQLSERVER;Database=ProcessManagementDB;Trusted_Connection=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ProcessPointConfig.ApplyProcessConfig(modelBuilder);

            modelBuilder.Entity<TableAB>().HasKey(ab => new { ab.TableAId, ab.TableBId });
        }

    }
}
