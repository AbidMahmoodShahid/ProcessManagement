﻿using inotech.Core;
using System.ComponentModel.DataAnnotations;

namespace Process.Simulation.Elements
{
    public class SimulationPointModel : PropertyChangedNotifier
    {
        public SimulationPointModel(string processPointId, string processPointName, int sortingNumber)
        {
            ProcessPointId = processPointId;
            ProcessPointName = processPointName;
            SortingNumber = sortingNumber;
            IsUnderProcess = null;
            SimulationStatus = "Unprocessed";
        }
        public SimulationPointModel()
        { }

        //(Kann mit Shadow Property auch gemacht werden) mit Fluent API
        [Key]
        public int SimulationPointId { get; set; }

        public string ProcessPointId { get; private set; }

        public string ProcessPointName { get; private set; }

        public int SortingNumber { get; private set; }

        private bool? _isUnderProcess;
        public bool? IsUnderProcess
        {
            get { return _isUnderProcess; }
            set
            {
                SetField(ref _isUnderProcess, value);
            }
        }

        private string _simulationStatus;
        public string SimulationStatus
        {
            get { return _simulationStatus; }
            set { SetField(ref _simulationStatus, value); }
        }

        private bool _deactivated;
        public bool Deactivated
        {
            get { return _deactivated; }
            set { SetField(ref _deactivated, value); }
        }

        private bool _simulationError;
        public bool SimulationError
        {
            get { return _simulationError; }
            set { SetField(ref _simulationError, value); }
        }

        private string _successPercentage;
        public string SuccessPercentage
        {
            get { return _successPercentage; }
            set { SetField(ref _successPercentage, value); }
        }
    }
}