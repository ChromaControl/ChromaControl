// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ChromaControl
{
    /// <summary>
    /// The main page
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// If the page has loaded yet
        /// </summary>
        private bool _pageLoaded = false;

        /// <summary>
        /// Creates the main page
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            // Configure the title bar
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            // Hide the default title bar
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            UpdateTitleBarLayout(coreTitleBar);

            // Change the title bar to be our own
            Window.Current.SetTitleBar(AppTitleBar);

            // Add various event handlers to handle window change events
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;
            Window.Current.Activated += CurrentWindow_Activated;
        }

        /// <summary>
        /// Updates the title bar layout
        /// </summary>
        /// <param name="coreTitleBar">The core title bar</param>
        private void UpdateTitleBarLayout(CoreApplicationViewTitleBar coreTitleBar)
        {
            var currMargin = AppTitleBar.Margin;
            AppTitleBar.Height = coreTitleBar.Height;
            AppTitleBar.Margin = new Thickness(currMargin.Left, currMargin.Top, coreTitleBar.SystemOverlayRightInset, currMargin.Bottom);
        }

        /// <summary>
        /// Occurs when the core title bar layout metrics change
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="args">The arguments</param>
        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            UpdateTitleBarLayout(sender);
        }

        /// <summary>
        /// Occurs when the visibility of the core title bar changes
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="args">The arguments</param>
        private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
        {
            if (sender.IsVisible)
                AppTitleBar.Visibility = Visibility.Visible;
            else
                AppTitleBar.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Occurs when the current window is activated
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The arguments</param>
        private void CurrentWindow_Activated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            var defaultForegroundBrush = (SolidColorBrush)Application.Current.Resources["TextFillColorPrimaryBrush"];
            var inactiveForegroundBrush = (SolidColorBrush)Application.Current.Resources["TextFillColorDisabledBrush"];

            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
                AppTitle.Foreground = inactiveForegroundBrush;
            else
                AppTitle.Foreground = defaultForegroundBrush;
        }

        /// <summary>
        /// Occurs when the display mode changes on the navigation view
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="args">The arguments</param>
        private void NavigationViewControl_DisplayModeChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewDisplayModeChangedEventArgs args)
        {
            const int TopIndent = 16;
            const int ExpandedIndent = 48;
            var minimalIndent = 104;

            if (NavigationViewControl.IsBackButtonVisible.Equals(Microsoft.UI.Xaml.Controls.NavigationViewBackButtonVisible.Collapsed))
            {
                minimalIndent = 48;
            }

            var currMargin = AppTitleBar.Margin;

            if (sender.PaneDisplayMode == Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Top)
                AppTitleBar.Margin = new Thickness(TopIndent, currMargin.Top, currMargin.Right, currMargin.Bottom);
            else if (sender.DisplayMode == Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode.Minimal)
                AppTitleBar.Margin = new Thickness(minimalIndent, currMargin.Top, currMargin.Right, currMargin.Bottom);
            else
                AppTitleBar.Margin = new Thickness(ExpandedIndent, currMargin.Top, currMargin.Right, currMargin.Bottom);
        }

        /// <summary>
        /// Occurs when the page has loaded
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The arguments</param>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var packageVersion = Package.Current.Id.Version;

            VersionNumberText.Text = $"{packageVersion.Major}.{packageVersion.Minor}.{packageVersion.Build}";

            var asusStartupTask = await StartupTask.GetAsync("Asus");

            var GHUBStartupTask = await StartupTask.GetAsync("GHUB");
            var corsairStartupTask = await StartupTask.GetAsync("Corsair");


            if (asusStartupTask.State == StartupTaskState.Enabled)
                AsusToggleSwitch.IsOn = true;

            if (corsairStartupTask.State == StartupTaskState.Enabled)
                CorsairToggleSwitch.IsOn = true;

            if (GHUBStartupTask.State == StartupTaskState.Enabled)
                GHUBToggleSwitch.IsOn = true;

            var debugMode = ApplicationData.Current.LocalSettings.Values["DebugMode"];

            if (debugMode != null && (bool)debugMode)
                DebugToggleSwitch.IsOn = (bool)debugMode;

            _pageLoaded = true;
        }

        /// <summary>
        /// Occurs when the Asus toggle switch is toggled
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The arguments</param>
        private async void AsusToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (_pageLoaded)
                await ToggleModule("Asus", AsusToggleSwitch.IsOn);
        }

        /// <summary>
        /// Occurs when the Corsair toggle switch is toggled
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The arguments</param>
        private async void CorsairToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (_pageLoaded)
                await ToggleModule("Corsair", CorsairToggleSwitch.IsOn);
        }

        /// <summary>
        /// Occurs when the GHub toggle switch is toggled
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The arguments</param>
        private async void GHUBToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (_pageLoaded)
                await ToggleModule("GHUB", GHUBToggleSwitch.IsOn);
        }


        /// <summary>
        /// Toggles a module on or off
        /// </summary>
        /// <param name="moduleName">The module name</param>
        /// <param name="enable">If the module should be enabled, else disabled</param>
        /// <returns>A task</returns>
        private async Task ToggleModule(string moduleName, bool enable)
        {
            if (enable)
                ApplicationData.Current.LocalSettings.Values["LauncherCommand"] = "Enable";
            else
                ApplicationData.Current.LocalSettings.Values["LauncherCommand"] = "Disable";

            await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync(moduleName);
        }

        private void DebugToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (_pageLoaded)
            {
                if (DebugToggleSwitch.IsOn)
                    ApplicationData.Current.LocalSettings.Values["DebugMode"] = true;
                else
                    ApplicationData.Current.LocalSettings.Values["DebugMode"] = false;
            }
        }
    }
}
