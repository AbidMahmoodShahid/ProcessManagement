using System.Threading;

namespace Process.Simulation.Elements
{
    public interface ISimulationViewModel : ISimulationUpdater, ISimulationErrorHandler
    {
        bool SimulationStarted { get; set; }

        bool SimulationRunning { get; set; }

        bool SimulationCancelled { get; set; }

        bool SimulationComplete { get; set; }

        CancellationTokenSource CTS { get; set; }
    }
}
