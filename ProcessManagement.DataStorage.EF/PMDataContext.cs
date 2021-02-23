using Microsoft.EntityFrameworkCore;
using Process.Editor.Elements;
using Process.Simulation.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.DataStorage.EF
{
    public class PMDataContext : DbContext
    {
        //entities
        public DbSet<ProcessModel> Process { get; set; }
        public DbSet<ProcessGroupModel> ProcessGroup { get; set; }
        public DbSet<ProcessPointA> ProcessPointA { get; set; }
        public DbSet<ProcessPointB> ProcessPointB { get; set; }
        public DbSet<ProcessPointC> ProcessPointC { get; set; }
        public DbSet<ProcessPointD> ProcessPointD { get; set; }
        public DbSet<SimulationModel> SimulationModel { get; set; }


        public PMDataContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ABIDMAHMOOD\AMSQLSERVER;Database=ProcessManagementDB;Trusted_Connection=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ensures that all process point entities has the baseclass properties in the table
            modelBuilder.Entity<ProcessPoint>().ToTable("ProcessPoint");

            // mapping a .NET property as a discriminator
            modelBuilder.Entity<ProcessPointA>().HasDiscriminator(p => p.ProcessPointTypeName);
            modelBuilder.Entity<ProcessPointB>().HasDiscriminator(p => p.ProcessPointTypeName);
            modelBuilder.Entity<ProcessPointC>().HasDiscriminator(p => p.ProcessPointTypeName);
            modelBuilder.Entity<ProcessPointD>().HasDiscriminator(p => p.ProcessPointTypeName);
        }

    }
}
