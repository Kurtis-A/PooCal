using PooCal.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace PooCal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Calculation();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void Minimise_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void Maximise_Click(object sender, RoutedEventArgs e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        private void Close_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    }
}
