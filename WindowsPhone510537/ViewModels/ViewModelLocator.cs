using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhone510537.ViewModels {
    public static class ViewModelLocator {
        private static MainViewModel _main;
        public static MainViewModel Main { get { return _main ?? (_main = new MainViewModel()); } }
    }
}
