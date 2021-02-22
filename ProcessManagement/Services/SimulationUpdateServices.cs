using Process.Editor.Elements;
using Process.Simulation.ViewModels;
using ProcessManagement.Elements;
using System.Collections.Generic;

namespace ProcessManagement.Services
{
    public static class SimulationUpdateServices
    {
        public static void SetSimulationViewModelDictionary(ISimulationManager simulationManager)
        {
            simulationManager.SimulationViewModelDictionary = new Dictionary<string, SimulationViewModel>();
            foreach(ProcessModel processModel in simulationManager.ProcessEditorViewModel.ItemCollection)
            {
                SimulationViewModel newSimulationViewModel = new SimulationViewModel(processModel);
                simulationManager.SimulationViewModelDictionary.Add(processModel.Id, newSimulationViewModel);
            }
            SetCurrentSimulationViewModel(simulationManager);
        }

        public static void SetCurrentSimulationViewModel(ISimulationManager simulationManager)
        {
            simulationManager.CurrentSimulationViewModel = simulationManager.SimulationViewModelDictionary[simulationManager.ProcessEditorViewModel.SelectedProcess.Id];
        }
    }
}
