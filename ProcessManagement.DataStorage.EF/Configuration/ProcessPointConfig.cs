using Microsoft.EntityFrameworkCore;
using Process.Editor.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManagement.DataStorage.EF.Configuration
{
    static class ProcessPointConfig
    {
        public static void ApplyProcessConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProcessPoint>().ToTable("ProcessPoint").HasDiscriminator<string>("ProcessPointType")
                .HasValue<ProcessPointA>("ProcessPointA")
                .HasValue<ProcessPointB>("ProcessPointB")
                .HasValue<ProcessPointC>("ProcessPointC")
                .HasValue<ProcessPointD>("ProcessPointD");

        }
    }
}
