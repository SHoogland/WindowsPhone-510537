using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace WindowsPhone510537.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddMessagePage : Page {
        public AddMessagePage() {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += OnBackPressed;
        }

        public void OnBackPressed(object sender, BackPressedEventArgs e) {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack) {
                rootFrame.GoBack();
                e.Handled = true;
            }
        }

        private async void Post_Message(object sender, RoutedEventArgs e) {
            if (!string.IsNullOrWhiteSpace(messageTitle.Text) && !string.IsNullOrWhiteSpace(messageText.Text)) {
                HttpClient client = new HttpClient();
                var parameters = new Dictionary<string, string>
            {
                {"Title", messageTitle.Text},
                {"Text", messageText.Text}
            };
                var content = new FormUrlEncodedContent(parameters);
                try {
                    var response = await client.PostAsync("http://wpinholland.azurewebsites.net/API/Messages", content);
                    response.EnsureSuccessStatusCode();
                }
                catch {
                    var dialog = new MessageDialog("Something went wrong... Do you have internet?");
                    dialog.Commands.Add(new UICommand("ok"));
                    dialog.ShowAsync();
                }
                Frame rootFrame = Window.Current.Content as Frame;
                if (rootFrame != null && rootFrame.CanGoBack) {
                    App.DidPublishMessage = true;
                    rootFrame.GoBack();
                }
            }
            else {
                var dialog = new MessageDialog("Did you fill in all the fields?");
                dialog.Commands.Add(new UICommand("nope i didnt"));
                dialog.ShowAsync();
            }
        }
    }
}
