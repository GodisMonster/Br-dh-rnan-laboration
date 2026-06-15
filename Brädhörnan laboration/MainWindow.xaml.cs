using Brädhörnan_laboration.ViewModels;
using System.Windows;

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
