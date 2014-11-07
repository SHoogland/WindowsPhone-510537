using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WindowsPhone510537.Services;
using WindowsPhone510537.ViewModels;

namespace WindowsPhone510537 {

    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            DataContext = ViewModelLocator.Main;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if (App.DidPublishMessage) {
                App.DidPublishMessage = false;
                var messages = (MainViewModel)MessagesView.DataContext;
                var specialMessageList = (ObservableIncrementalLoadingCollection)messages.Messages;
                specialMessageList.Clear();
                specialMessageList.LatestId = 0;
                specialMessageList.LoadMoreItemsAsync(5);
            }

            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }
    }
}
