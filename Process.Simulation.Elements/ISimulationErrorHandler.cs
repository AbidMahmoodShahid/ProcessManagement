using System.Collections.ObjectModel;

namespace Process.Simulation.Elements
{
    public interface ISimulationErrorHandler
    {
        SimulationPointModel CurrentPoint { get; set; }

        ObservableCollection<SimulationPointModel> ProcessPointSimulationList { get; set; }
    }
}
