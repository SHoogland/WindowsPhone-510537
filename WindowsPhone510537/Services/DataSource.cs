using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsPhone510537.Services {
    public static class DataSource {
        static DataSource() {
            Messages = new ObservableIncrementalLoadingCollection();
        }
        public static ObservableIncrementalLoadingCollection Messages { get; private set; }
    }
}
