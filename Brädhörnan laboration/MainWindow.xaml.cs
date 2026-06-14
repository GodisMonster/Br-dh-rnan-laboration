using Brädhörnan_laboration.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Brädhörnan_laboration
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
     
    }
}
