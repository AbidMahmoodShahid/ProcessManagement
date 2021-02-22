using System.Collections.ObjectModel;

namespace Process.Simulation.Elements
{
    public interface ISimulationErrorHandler
    {
        SimulationModel CurrentPoint { get; set; }

        ObservableCollection<SimulationModel> ProcessPointSimulationList { get; set; }
    }
}
