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
            modelBuilder.Entity<ProcessGroupModel>()
                .HasOne<ProcessModel>(pG => pG.ProcessModel)
                .WithMany(p => p.ItemCollection)
                .HasForeignKey(pG => pG.ProcessModelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProcessPoint>()
                .HasOne<ProcessGroupModel>(pP => pP.ProcessGroupModel)
                .WithMany(pG => pG.ProcessPoints)
                .HasForeignKey(pP => pP.ProcessGroupModelId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ProcessPoint>().ToTable("ProcessPoint").HasDiscriminator<string>("ProcessPointType")
                .HasValue<ProcessPointA>("ProcessPointA")
                .HasValue<ProcessPointB>("ProcessPointB")
                .HasValue<ProcessPointC>("ProcessPointC")
                .HasValue<ProcessPointD>("ProcessPointD");

        }
    }
}
