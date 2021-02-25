using Process.Simulation.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Process.Simulation.Repo
{
    public interface ISimulationPointRepo
    {
        List<SimulationPointModel> GetAll();

        void Attach(SimulationPointModel processModel);

        void AttachRange(ObservableCollection<SimulationPointModel> simulationModelList);

        void Delete(SimulationPointModel processModel);

        void Update(SimulationPointModel processModel);

        void UpdateAll(ObservableCollection<SimulationPointModel> simulationModelList);
    }
}
