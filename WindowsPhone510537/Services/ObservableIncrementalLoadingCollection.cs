using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;
using WindowsPhone510537.Models;

namespace WindowsPhone510537.Services {
    public class ObservableIncrementalLoadingCollection : ObservableCollection<Message>, ISupportIncrementalLoading {
        public bool IsGettingData = false;
        public int LatestId = 0;

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count) {
            return AsyncInfo.Run(async c => {
                if (!IsGettingData) {
                    IsGettingData = true;
                    HttpClient client = new HttpClient();
                    string json = "";
                    if (LatestId == 0) {
                        json = await client.GetStringAsync(new Uri("http://wpinholland.azurewebsites.net/API/Messages"));
                    }
                    else {
                        json = await client.GetStringAsync(new Uri("http://wpinholland.azurewebsites.net/API/Messages/" + LatestId));
                    }

                    var messages = new MessagesResponse();
                    messages = JsonConvert.DeserializeObject<MessagesResponse>(json);
                    Debug.WriteLine("request done" + LatestId);
                    foreach (var message in messages.Messages) {
                        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
                        epoch = epoch.AddSeconds(message.Timestamp);
                        message.TimePosted = epoch.ToString("hh:mm dd-MM-yyyy");
                        if (!string.IsNullOrWhiteSpace(message.ImageUrl)) {
                            message.Image = new BitmapImage(new Uri(message.ImageUrl));
                        }
                        else {
                            message.Image = new BitmapImage(new Uri("http://www.silvermorgandollar.com/images/no_image.gif"));
                        }
                        if (message.Text.Count() > 31) {
                            message.TextPreview = message.Text.Substring(0, 30).Replace("\n", "").Replace("\r", "");
                        }
                        else {
                            message.TextPreview = message.Text.Replace("\n", "").Replace("\r", "");
                        }
                        Add(message);
                    }
                    LatestId = messages.Messages.Last().ID;
                    IsGettingData = false;
                }
                return new LoadMoreItemsResult {
                    Count = 20
                };
            });
        }
        public bool HasMoreItems { get { return true; } }
    }
}
