using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Process.Simulation.Elements
{
    public class SimulationProcessModel
    {
        //(Kann mit Shadow Property auch gemacht werden) mit Fluent API
        [Key]
        public int ProcessModelId { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }


        //Collection Navigation Property(OneToMany)
        public List<SimulationPointModel> SimulationPointModelList { get; set; }


        public SimulationProcessModel()
        {
            SimulationPointModelList = new List<SimulationPointModel>();
        }
    }
}
