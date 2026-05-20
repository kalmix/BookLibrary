using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using BookLibrary.Views;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BookLibrary
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            // init el tema segun el sistema
            var initialTheme = App.Current.RequestedTheme == ApplicationTheme.Dark ? ElementTheme.Dark : ElementTheme.Light;
            UpdateThemeUI(initialTheme);
            
            NavView.SelectedItem = NavView.MenuItems[0];
            ContentFrame.Navigate(typeof(HomePage), null, new SuppressNavigationTransitionInfo());
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem navItem)
            {
                switch (navItem.Tag?.ToString())
                {
                    case "Home":
                        ContentFrame.Navigate(typeof(HomePage), null, new SuppressNavigationTransitionInfo());
                        break;
                    case "AddBook":
                        ContentFrame.Navigate(typeof(AddBookPage), null, new SuppressNavigationTransitionInfo());
                        break;
                }
            }
        }

        private async void TitleBarSearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (ContentFrame.Content is not HomePage)
                {
                    ContentFrame.Navigate(typeof(HomePage), null, new SuppressNavigationTransitionInfo());
                    NavView.SelectedItem = NavView.MenuItems[0];
                }
                
                if (ContentFrame.Content is HomePage homePage)
                {
                    homePage.ViewModel.SearchQuery = sender.Text;
                    await homePage.ViewModel.SearchCommand.ExecuteAsync(null);
                }
            }
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer == ThemeNavItem)
            {
                ToggleTheme();
            }
        }

        private void ToggleTheme()
        {
            var currentTheme = RootGrid.RequestedTheme;
            if (currentTheme == ElementTheme.Default)
            {
                currentTheme = App.Current.RequestedTheme == ApplicationTheme.Dark ? ElementTheme.Dark : ElementTheme.Light;
            }

            if (currentTheme == ElementTheme.Dark)
            {
                RootGrid.RequestedTheme = ElementTheme.Light;
                UpdateThemeUI(ElementTheme.Light);
            }
            else
            {
                RootGrid.RequestedTheme = ElementTheme.Dark;
                UpdateThemeUI(ElementTheme.Dark);
            }
        }

        private void UpdateThemeUI(ElementTheme theme)
        {
            if (ThemeNavItem.Icon is FontIcon fontIcon)
            {
                if (theme == ElementTheme.Dark)
                {
                    fontIcon.Glyph = "\xE708";
                    ThemeNavItem.Content = "Modo Oscuro";
                }
                else
                {
                    fontIcon.Glyph = "\xE706";
                    ThemeNavItem.Content = "Modo Claro";
                }
            }
        }
    }
}
