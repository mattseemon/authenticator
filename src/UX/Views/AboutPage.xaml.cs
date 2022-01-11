﻿using Seemon.Authenticator.ViewModels;
using System.Windows.Controls;

namespace Seemon.Authenticator.Views
{
    /// <summary>
    /// Interaction logic for AboutPage.xaml
    /// </summary>
    public partial class AboutPage : Page
    {
        public AboutPage(AboutViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
