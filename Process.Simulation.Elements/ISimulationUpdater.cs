using Process.Editor.Elements;

namespace Process.Simulation.Elements
{
    public interface ISimulationUpdater
    {
        ProcessModel ProcessModel { get; set; }
    }
}
