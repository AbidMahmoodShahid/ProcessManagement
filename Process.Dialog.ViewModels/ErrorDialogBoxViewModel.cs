using inotech.Core;

namespace Process.Dialog.ViewModels
{
    public class ErrorDialogBoxViewModel : PropertyChangedNotifier
    {
        public ErrorDialogBoxViewModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetField(ref _errorMessage, value); }
        }

    }
}
