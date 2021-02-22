using System;

namespace Process.Simulation.Elements
{
    public class SimulationErrorEventArgs : EventArgs
    {
        public ISimulationErrorHandler SimulationErrorHandler;

        public SimulationErrorEventArgs(ISimulationErrorHandler simulationErrorHandler)
        {
            SimulationErrorHandler = simulationErrorHandler;
        }
    }
}
