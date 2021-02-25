using Process.Simulation.Elements;
using ProcessManagement.DataStorage.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Process.Simulation.Repo
{
    public class SimulationPointRepo : ISimulationPointRepo
    {
        private PMDataContext _pMDataContext;

        public SimulationPointRepo(PMDataContext pMDataContext)
        {
            _pMDataContext = pMDataContext;
        }

        public List<SimulationPointModel> GetAll()
        {
            return _pMDataContext.SimulationPoint.ToList();
        }

        public void Attach(SimulationPointModel simulationModel)
        {
            _pMDataContext.SimulationPoint.Attach(simulationModel);
        }
        public void AttachRange(ObservableCollection<SimulationPointModel> simulationModelList)
        {
            _pMDataContext.SimulationPoint.AttachRange(simulationModelList);
        }

        public void Delete(SimulationPointModel simulationModel)
        {
            _pMDataContext.SimulationPoint.Remove(simulationModel);
        }

        public void Update(SimulationPointModel simulationModel)
        {
            _pMDataContext.SimulationPoint.Update(simulationModel);
        }

        public void UpdateAll(ObservableCollection<SimulationPointModel> simulationModelList)
        {
            _pMDataContext.SimulationPoint.UpdateRange(simulationModelList);
        }
    }
}
