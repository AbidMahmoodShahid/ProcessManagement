using Microsoft.EntityFrameworkCore;
using Process.Editor.Elements;
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

        public void Test()
        {
            //AddMockData();
        }

        private void AddMockData()
        {
            List<ProcessModel> processModelList = new List<ProcessModel>();

            CreateMockData(processModelList);

            // add all processes in Table Processes
            foreach(ProcessModel processModel in processModelList)
            {
                Process.Add(processModel);
            }
        }

        private void CreateMockData(List<ProcessModel> processModelList)
        {
            // create mock process data
            for(int i = 1; i < 4; i++)
            {
                string processName = "Process " + i;
                string processId = "P_" + i;
                processModelList.Add(new ProcessModel(processName, processId, i));
            }

            // create mock processgroup data
            foreach(ProcessModel processModel in processModelList)
            {
                for(int i = 1; i < 4; i++)
                {
                    string processGroupName = processModel.Id + " Process Group " + i;
                    string processGroupId = processModel.Id + "_PG_" + i;
                    processModel.ItemCollection.Add(new ProcessGroupModel(processGroupName, processGroupId, i));
                }
            }

            // create mock processpoint data
            foreach(ProcessModel processModel in processModelList)
            {
                foreach(ProcessGroupModel processGroupModel in processModel.ItemCollection)
                {
                    for(int i = 1; i < 6; i++)
                    {
                        string processPointName = processGroupModel.Id + " Process Point " + i;
                        string processPointId = processGroupModel.Id + "_PP_" + i;
                        processGroupModel.ItemCollection.Add(new ProcessPointA(processPointName, processPointId, i));
                    }
                    for(int i = 6; i < 11; i++)
                    {
                        string processPointName = processGroupModel.Id + " Process Point " + i;
                        string processPointId = processGroupModel.Id + "_PP_" + i;
                        processGroupModel.ItemCollection.Add(new ProcessPointB(processPointName, processPointId, i));
                    }
                    for(int i = 11; i < 16; i++)
                    {
                        string processPointName = processGroupModel.Id + " Process Point " + i;
                        string processPointId = processGroupModel.Id + "_PP_" + i;
                        processGroupModel.ItemCollection.Add(new ProcessPointC(processPointName, processPointId, i));
                    }
                    for(int i = 16; i < 21; i++)
                    {
                        string processPointName = processGroupModel.Id + " Process Point " + i;
                        string processPointId = processGroupModel.Id + "_PP_" + i;
                        processGroupModel.ItemCollection.Add(new ProcessPointD(processPointName, processPointId, i));
                    }
                }
            }
        }
    }
}
