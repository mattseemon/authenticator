using Microsoft.Extensions.Options;
using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Contracts.ViewModels;
using Seemon.Authenticator.Helpers.ViewModels;
using Seemon.Authenticator.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace Seemon.Authenticator.ViewModels
{
    public class LicenseViewModel : ViewModelBase, INavigationAware
    {
        private readonly ISystemService _systemService;

        private ApplicationUrls _applicationUrls;
        private List<string> _lines;
        private ICommand _openInBrowserCommand;

        public LicenseViewModel(IOptions<ApplicationUrls> applicationUrls, ISystemService systemService)
        {
            _applicationUrls = applicationUrls.Value;
            _systemService = systemService;
        }

        public List<string> Lines
        {
            get => _lines; set => SetProperty(ref _lines, value);
        }

        public ICommand OpenInBrowserCommand => _openInBrowserCommand ??= RegisterCommand<string>(OnOpenInBrowser);

        private void OnOpenInBrowser(string parameter) => _systemService.OpenInWebBrowser(_applicationUrls[parameter]);

        public void OnNavigatedFrom() { }

        public void OnNavigatedTo(object parameter)
        {
            Lines = File.ReadLines(@".\LICENSE").ToList();
        }
    }
}
