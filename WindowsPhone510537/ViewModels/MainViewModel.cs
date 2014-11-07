using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPhone510537.Models;
using WindowsPhone510537.Services;

namespace WindowsPhone510537.ViewModels {
    public sealed class MainViewModel {
        public ObservableCollection<Message> Messages { get { return DataSource.Messages; } }
    }
}
