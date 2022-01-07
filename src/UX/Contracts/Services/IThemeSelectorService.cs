using System;

using Seemon.Authenticator.Models;

namespace Seemon.Authenticator.Contracts.Services
{
    public interface IThemeSelectorService
    {
        void InitializeTheme();

        void SetTheme(AppTheme theme);

        AppTheme GetCurrentTheme();
    }
}
