using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

namespace DemoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    [INotifyPropertyChanged]
    public partial class MainWindow : Window
    {
        [ObservableProperty] List<string> datas = new ();
        public MainWindow()
        {
            InitializeComponent ();

            this.DataContext = this;

            for (int i = 0; i < 10; i++)
            {
                Datas.Add (i.ToString ());
            }
        }
    }
}