﻿using System.Windows.Controls;

namespace Seemon.Authenticator.Contracts.Views
{
    public interface IShellWindow
    {
        Frame GetNavigationFrame();

        void ShowWindow();

        void CloseWindow();
    }
}
