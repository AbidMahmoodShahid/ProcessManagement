using Process.Editor.ViewModels;
using Process.Simulation.ViewModels;
using System.Collections.Generic;

namespace ProcessManagement.Elements
{
    public interface ISimulationManager
    {
        Dictionary<string, SimulationViewModel> SimulationViewModelDictionary { get; set; }

        ProcessEditorViewModel ProcessEditorViewModel { get; set; }

        SimulationViewModel CurrentSimulationViewModel { get; set; }
    }
}
