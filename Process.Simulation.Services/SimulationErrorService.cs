using Process.Dialog.UI;
using Process.Dialog.ViewModels;
using Process.Simulation.Elements;
using System;

namespace Process.Simulation.Services
{
    public class SimulationErrorService : IDisposable
    {
        private SimulationService _simulationService;
        public SimulationErrorService(SimulationService simulationService)
        {
            _simulationService = simulationService;
            _simulationService.RaiseSimulationErrorEvent += HandleSimulationError;
        }

        private void HandleSimulationError(object sender, SimulationErrorEventArgs simulationErrorEventArgs)
        {
            SetPointError(simulationErrorEventArgs.SimulationErrorHandler.CurrentPoint);
            ResetErrorPoint(simulationErrorEventArgs.SimulationErrorHandler);
        }

        private void SetPointError(SimulationModel currentPoint)
        {
            currentPoint.SimulationError = true;
            currentPoint.IsUnderProcess = null;
            currentPoint.SimulationStatus = "Error";

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                ErrorDialogBoxViewModel errorDialogBoxViewModel = new ErrorDialogBoxViewModel(String.Format("An error ocured in the Simulation Point: {0}.", currentPoint.ProcessPointName));
                ErrorDialogBox errorDialogBox = new ErrorDialogBox();
                errorDialogBox.DataContext = errorDialogBoxViewModel;
                errorDialogBox.ShowDialog();
            });
        }

        private void ResetErrorPoint(ISimulationErrorHandler simulationErrorHandler)
        {
            int currentPointIndex = simulationErrorHandler.ProcessPointSimulationList.IndexOf(simulationErrorHandler.CurrentPoint);

            if(currentPointIndex > 0)
            {
                simulationErrorHandler.CurrentPoint = simulationErrorHandler.ProcessPointSimulationList[currentPointIndex - 1];
            }
            else
            {
                simulationErrorHandler.CurrentPoint = null;
            }
        }

        public void Dispose()
        {
            _simulationService.RaiseSimulationErrorEvent -= HandleSimulationError;
        }

    }
}
