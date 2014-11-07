using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count) {
            return AsyncInfo.Run(async c => {
                HttpClient client = new HttpClient();
                string json = await client.GetStringAsync(new Uri("http://wpinholland.azurewebsites.net/API/Messages"));

                var messages = new MessagesResponse();
                messages = JsonConvert.DeserializeObject<MessagesResponse>(json);
                
                foreach (var message in messages.Messages) {
                    if (!string.IsNullOrWhiteSpace(message.ImageUrl)) {
                        message.Image = new BitmapImage(new Uri(message.ImageUrl));
                    }
                    else {
                        message.Image = new BitmapImage(new Uri("http://www.silvermorgandollar.com/images/no_image.gif"));
                    }
                    Add(message);
                }

                return new LoadMoreItemsResult {
                    Count = 50
                };
            });
        }
        public bool HasMoreItems { get { return true; } }
    }
}
