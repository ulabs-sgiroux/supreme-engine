using Prism.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.Devices.Bluetooth.Advertisement;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BluetoothLEAdvertisementWatcher advertisementWatcher = new BluetoothLEAdvertisementWatcher();
        DeviceDictionary devices = new DeviceDictionary();
        private IEventAggregator _eventAggregator;

        // This viewmodel contains the data that most controls are bound to.
        // It also hosts most of the application logic for this program.
        private ViewModels.ViewModel _viewModel;

        public MainWindow()
        {
            this.InitializeComponent();

            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // This event may be raised multiple times.
            // We only care about the first time so unsubscribe from future notifications
            this.Loaded -= MainWindow_Loaded;

            // must be constructed on the ui thread
            _eventAggregator = new EventAggregator();
            
            _viewModel = new ViewModels.ViewModel(_eventAggregator);
            this.DataContext = _viewModel;

            // Signal to the viewmodel it is time to run startup code
            _viewModel.StartupCommand.Execute(null);

            advertisementWatcher.Received += W_Received;
            advertisementWatcher.Start();
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            advertisementWatcher.Stop();
            // Notify viewmodel that we are shutting down now -- time to clean up
            _viewModel.ShutdownCommand.Execute(null);
        }

        private void W_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            var sred = new Events.ScanReceivedEventData(args);
            Debug.WriteLine(sred);
            _eventAggregator.GetEvent<Events.ScanReceivedEvent>().Publish(sred);
        }
    }
}
