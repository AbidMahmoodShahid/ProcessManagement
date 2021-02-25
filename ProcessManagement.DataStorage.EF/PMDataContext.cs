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
        //entities TODO AM: Check Loading Abstract Class
        public DbSet<ProcessModel> Process { get; set; }
        public DbSet<ProcessGroupModel> ProcessGroup { get; set; }
        public DbSet<ProcessPoint> ProcessPoint { get; set; }

        public DbSet<SimulationPointModel> SimulationPoint { get; set; }


        public PMDataContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ABIDMAHMOOD\AMSQLSERVER;Database=ProcessManagementDB;Trusted_Connection=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProcessPoint>()
           .ToTable("ProcessPoint")
           .HasDiscriminator<string>("ProcessPointType")
           .HasValue<ProcessPointA>("ProcessPointA")
           .HasValue<ProcessPointB>("ProcessPointB")
           .HasValue<ProcessPointC>("ProcessPointC")
           .HasValue<ProcessPointD>("ProcessPointD");

        }

    }
}
