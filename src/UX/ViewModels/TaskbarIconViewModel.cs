using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Helpers.ViewModels;
using System.Windows.Input;

namespace Seemon.Authenticator.ViewModels
{
    public class TaskbarIconViewModel : ViewModelBase
    {
        private readonly ITaskbarIconService _taskbarIconService;

        private ICommand _showCommand;
        private ICommand _exitCommand;

        public TaskbarIconViewModel(ITaskbarIconService taskbarIconService) => _taskbarIconService = taskbarIconService;

        public ICommand ShowCommand => _showCommand ??= RegisterCommand(OnShow);

        public ICommand ExitCommand => _exitCommand ??= RegisterCommand(OnExit);

        private void OnShow() => _taskbarIconService.Show();

        private void OnExit() => App.Current.Shutdown();
    }
}
