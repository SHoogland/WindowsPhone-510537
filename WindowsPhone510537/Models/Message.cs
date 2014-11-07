using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace WindowsPhone510537.Models {
    public class Message {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public ImageSource Image { get; set; }
        public int Timestamp { get; set; }
    }
}
