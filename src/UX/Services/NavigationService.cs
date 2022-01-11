using Seemon.Authenticator.Contracts.Services;
using Seemon.Authenticator.Contracts.ViewModels;
using Seemon.Authenticator.Helpers.Extensions;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Authenticator.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IPageService _pageService;
        private Frame _frame;
        private object _lastParameterUsed;
        private Page _currentPage;

        public event EventHandler<string> Navigated;

        public NavigationService(IPageService pageService)
        {
            _pageService = pageService;
        }

        public bool CanGoBack => _frame.CanGoBack;

        public Page CurrentPage => _currentPage;

        public void Initialize(Frame shellFrame)
        {
            if (_frame == null)
            {
                _frame = shellFrame;
                _frame.Navigated += OnNavigated;
            }
        }

        public void UnsubscribeNavigation()
        {
            if(_frame != null)
            {
                _frame.Navigated -= OnNavigated;
                _frame = null;
            }
        }

        public void GoBack()
        {
            if (_frame.CanGoBack)
            {
                var vmBeforeNavigation = _frame.GetDataContext();
                _frame.GoBack();
                if (vmBeforeNavigation is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedFrom();
                }
            }
        }

        public bool NavigateTo(string pageKey, object parameter = null, bool clearNavigation = false)
        {
            var pageType = _pageService.GetPageType(pageKey);

            if (_frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParameterUsed)))
            {
                _frame.Tag = clearNavigation;
                var page = _pageService.GetPage(pageKey);
                var navigated = _frame.Navigate(page, parameter);
                if (navigated)
                {
                    _lastParameterUsed = parameter;
                    var dataContext = _frame.GetDataContext();
                    if (dataContext is INavigationAware navigationAware)
                    {
                        navigationAware.OnNavigatedFrom();
                    }
                }

                return navigated;
            }

            return false;
        }

        public void CleanNavigation()
            => _frame.CleanNavigation();

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (sender is Frame frame)
            {
                bool clearNavigation = (bool)frame.Tag;
                if (clearNavigation)
                {
                    frame.CleanNavigation();
                }

                var dataContext = frame.GetDataContext();
                if (dataContext is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedTo(e.ExtraData);
                }
                _currentPage = frame.Content as Page;
                Navigated?.Invoke(sender, dataContext.GetType().FullName);
            }
        }
    }
}
