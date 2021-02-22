using Process.Simulation.Elements;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace Process.Simulation.Services
{
    public class SimulationService
    {
        public event EventHandler<SimulationErrorEventArgs> RaiseSimulationErrorEvent;

        public SimulationService()
        { }

        public void SetupSimulation(object obj)
        {
            ISimulationViewModel simulationVM = obj as ISimulationViewModel;

            CancellationToken token = simulationVM.CTS.Token;

            if(simulationVM.SimulationCancelled)
            {
                ResetSimulation(simulationVM);
                simulationVM.SimulationCancelled = false;
            }

            System.Windows.Application.Current.Dispatcher.Invoke(() => { simulationVM.SimulationRunning = true; });
            System.Windows.Application.Current.Dispatcher.Invoke(() => { simulationVM.SimulationStarted = true; });

            while(simulationVM.SimulationRunning)
            {
                if(token.IsCancellationRequested)
                {
                    CancelSimulation(simulationVM);
                    break;
                }

                simulationVM.CurrentPoint = GetNextPoint(simulationVM.ProcessPointSimulationList, simulationVM.CurrentPoint);

                if(simulationVM.CurrentPoint == null)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() => { simulationVM.SimulationRunning = false; });
                    System.Windows.Application.Current.Dispatcher.Invoke(() => { simulationVM.SimulationComplete = true; });
                    break;
                }

                Simulate(simulationVM);
            }
        }

        public void ResetSimulation(ISimulationViewModel simulationVM)
        {
            foreach(SimulationModel processPoint in simulationVM.ProcessPointSimulationList)
            {
                processPoint.IsUnderProcess = null;
                processPoint.SimulationStatus = "Unprocessed";
                processPoint.SuccessPercentage = null;
                processPoint.SimulationError = false;
            }

            System.Windows.Application.Current.Dispatcher.Invoke(() => { simulationVM.SimulationRunning = false; });
            System.Windows.Application.Current.Dispatcher.Invoke(() => { simulationVM.SimulationStarted = false; });
            System.Windows.Application.Current.Dispatcher.Invoke(() => { simulationVM.SimulationComplete = false; });

            simulationVM.CurrentPoint = null;
        }

        public void PauseSimulation(ISimulationViewModel simulationVM)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() => { simulationVM.SimulationRunning = false; });
        }

        #region private methods

        private void CancelSimulation(ISimulationViewModel simulationVM)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() => { simulationVM.SimulationRunning = false; });
            System.Windows.Application.Current.Dispatcher.Invoke(() => { simulationVM.SimulationStarted = true; });
            System.Windows.Application.Current.Dispatcher.Invoke(() => { simulationVM.SimulationComplete = false; });

            simulationVM.CurrentPoint = null;
            simulationVM.SimulationCancelled = true;
            simulationVM.CTS.Dispose();
        }

        private SimulationModel GetNextPoint(ObservableCollection<SimulationModel> processPointSimulationList, SimulationModel currentPoint)
        {
            int newPointIndex = processPointSimulationList.IndexOf(currentPoint) + 1;

            bool deactivatedPoint = true;
            while(deactivatedPoint && newPointIndex < processPointSimulationList.Count)
            {
                SimulationModel processSimulationModel = processPointSimulationList[newPointIndex];
                deactivatedPoint = processSimulationModel.Deactivated;

                if(deactivatedPoint)
                {
                    newPointIndex++;
                    continue;
                }
                if(newPointIndex < processPointSimulationList.Count)
                {
                    return processPointSimulationList[newPointIndex];
                }
            }

            return null;
        }

        private void Simulate(ISimulationViewModel simulationVM)
        {
            simulationVM.CurrentPoint.IsUnderProcess = true;
            simulationVM.CurrentPoint.SimulationStatus = "Under Process";
            Thread.Sleep(TimeSpan.FromMilliseconds(100));
            Random randomNumber = new Random();
            double successNumber = randomNumber.NextDouble();
            simulationVM.CurrentPoint.SuccessPercentage = String.Format("Value: {0:P2}.", successNumber);
            if(successNumber < 0.2)
            {
                HandleSimulationError(simulationVM);
            }
            else
            {
                simulationVM.CurrentPoint.SimulationError = false;
                simulationVM.CurrentPoint.IsUnderProcess = false;
                simulationVM.CurrentPoint.SimulationStatus = "Processed";
            }

        }

        private void HandleSimulationError(ISimulationViewModel simulationVM)
        {
            PauseSimulation(simulationVM);
            using(SimulationErrorService simulationErrorService = new SimulationErrorService(this))
            {
                RaiseSimulationErrorEvent?.Invoke(this, new SimulationErrorEventArgs(simulationVM));
            }
        }

        #endregion
    }
}
