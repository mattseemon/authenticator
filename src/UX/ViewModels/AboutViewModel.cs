using Microsoft.Extensions.Options;
using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Contracts.ViewModels;
using Seemon.Authenticator.Helpers.ViewModels;
using Seemon.Authenticator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Seemon.Authenticator.ViewModels
{
    public class AboutViewModel : ViewModelBase, INavigationAware
    {
        private readonly ApplicationUrls _applicationUrls;
        private readonly IApplicationInfoService _applicationInfoService;
        private readonly INavigationService _navigationService;
        private readonly ISystemService _systemService;

        private ICommand _openInBrowserCommand;
        private ICommand _showLicenseCommand;

        public AboutViewModel(IOptions<ApplicationUrls> applicationUrls, IApplicationInfoService applicationInfoService,
            ISystemService systemService, INavigationService navigationService)
        {
            _applicationUrls = applicationUrls.Value;
            _applicationInfoService = applicationInfoService;
            _systemService = systemService;
            _navigationService = navigationService;
        }

        public string Title => _applicationInfoService.Title;
        
        public string Version => _applicationInfoService.Version;
        
        public string Author => _applicationInfoService.Author;
        
        public string Description => _applicationInfoService.Description;
        
        public string Copyright => _applicationInfoService.Copyright;

        public ICommand OpenInBrowserCommand => _openInBrowserCommand ??= RegisterCommand<string>(OnOpenInBrowser);

        public ICommand ShowLicenseCommand => _showLicenseCommand ??= RegisterCommand(OnShowLicense);

        public void OnNavigatedFrom() { }

        public void OnNavigatedTo(object parameter) { }

        private void OnOpenInBrowser(string parameter)
        {
            var url = _applicationUrls[parameter];
            _systemService.OpenInWebBrowser(url);
        }

        private void OnShowLicense() => _navigationService.NavigateTo(typeof(LicenseViewModel).FullName);
    }
}
