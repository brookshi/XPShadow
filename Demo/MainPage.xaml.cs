using Demo.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Demo
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            MainFrame.Navigate(typeof(ButtonPage));
        }

        private void ShowSliptView(object sender, RoutedEventArgs e)
        {
            SplitView.IsPaneOpen = !SplitView.IsPaneOpen;
        }

        private void ButtonPage_Click(object sender, RoutedEventArgs e)
        {
            SplitView.IsPaneOpen = false;
            MainFrame.Navigate(typeof(ButtonPage));
        }

        private void CardViewPage_Click(object sender, RoutedEventArgs e)
        {
            SplitView.IsPaneOpen = false;
            MainFrame.Navigate(typeof(CardViewPage));
        }
    }
}
