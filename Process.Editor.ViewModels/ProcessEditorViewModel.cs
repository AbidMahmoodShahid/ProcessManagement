using inotech.Core;
using Process.Editor.Elements;
using Process.Editor.Services;
using ProcessManagement.Core;
using System.Collections.ObjectModel;
using System.Windows;

namespace Process.Editor.ViewModels
{
    public partial class ProcessEditorViewModel : PropertyChangedNotifier
    {
        public ProcessEditorViewModel(bool includeMockData)
        {
            //DelegateCommands
            AddProcessCommand = new DelegateCommand(ExecuteAddProcess);
            AddProcessGroupCommand = new DelegateCommand(ExecuteAddProcessGroup, CanExecuteAddProcessGroup);
            AddProcessPointCommand = new DelegateCommand(ExecuteAddProcessPoint, CanExecuteAddProcessPoint);
            ProcessGroupDownCommand = new DelegateCommand(param => ExecuteProcessGroupDown(param));
            ProcessGroupUpCommand = new DelegateCommand(param => ExecuteProcessGroupUp(param));
            ProcessPointDownCommand = new DelegateCommand(param => ExecuteProcessPointDown(param));
            ProcessPointUpCommand = new DelegateCommand(param => ExecuteProcessPointUp(param));
            DeleteProcessCommand = new DelegateCommand(ExecuteDeleteProcess, CanExecuteDeleteProcess);
            DeleteProcessGroupCommand = new DelegateCommand(ExecuteDeleteProcessGroup, CanExecuteDeleteProcessGroup);
            DeleteProcessPointCommand = new DelegateCommand(ExecuteDeleteProcessPoint, CanExecuteDeleteProcessPoint);
            ExportProcessCommand = new DelegateCommand(ExecuteExportProcess, CanExecuteExportProcess);
            ImportProcessCommand = new DelegateCommand(ExecuteImportProcess);

            ItemCollection = new ObservableCollection<ProcessModel>();
            SortingNumberVisibility = Visibility.Hidden;
            EditorUCVisibility = Visibility.Visible;
            IsEditorMode = true;

            //Services
            _getterService = new GetterService();
            _processPointTypeList = _getterService.GetInheritedClassesArray(typeof(ProcessPoint));
            _pointTypeNameList = _getterService.GetProcessPointTypeNameList(_processPointTypeList);

            if(includeMockData)
                MockDataService.CreateMockData(ItemCollection);
        }

        private GetterService _getterService;

        private bool _isEditorMode;
        public bool IsEditorMode
        {
            get { return _isEditorMode; }
            set { SetField(ref _isEditorMode, value); }
        }

        private Visibility _editorUCVisibility;
        public Visibility EditorUCVisibility
        {
            get { return _editorUCVisibility; }
            set
            {
                SetField(ref _editorUCVisibility, value);
                SetToolBarButtonsIsEnabled();
            }
        }

        private void SetToolBarButtonsIsEnabled()
        {
            if(EditorUCVisibility == Visibility.Visible)
            {
                IsEditorMode = true;
            }
            else
            {
                IsEditorMode = false;
            }
        }
    }
}
