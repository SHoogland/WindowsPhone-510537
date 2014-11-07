using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WindowsPhone510537.Views;

namespace WindowsPhone510537.Commands {
    public sealed class NavigateToAddMessagePage : ICommand {
        public bool CanExecute(object parameter) {
            return true;
        }
        public void Execute(object parameter) {
            ((Frame)Window.Current.Content).Navigate(typeof(AddMessagePage));
        }

        public event EventHandler CanExecuteChanged;
    }
}
