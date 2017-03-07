using Prism.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class ViewModel : Prism.Mvvm.BindableBase
    {
        #region Commands

        RelayCommand<object> _startupCommand;
        public ICommand StartupCommand
        {
            get
            {
                if (_startupCommand == null)
                {
                    _startupCommand = new RelayCommand<object>(param => this._Startup(),
                        param => true);
                }
                return _startupCommand;
            }
        }

        RelayCommand<object> _shutdownCommand;
        public ICommand ShutdownCommand
        {
            get
            {
                if (_shutdownCommand == null)
                {
                    _shutdownCommand = new RelayCommand<object>(param => this._Shutdown(),
                        param => true);
                }
                return _shutdownCommand;
            }
        }

        #endregion

        private IEventAggregator _eventAggregator;

        public NearbyDevices NearbyDevices
        {
            get { return _nearby; }
            set { _nearby = value; }
        }
        private NearbyDevices _nearby;

        public ViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;

            NearbyDevices = new NearbyDevices(_eventAggregator);

        }

        private void _Startup() { }
        private void _Shutdown() { }
    }
}
