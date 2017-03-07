using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class NearbyDevices : ObservableCollection<Events.ScanReceivedEventData>
    {
        private SubscriptionToken scanArrivedSubscriptionToken;
        private IEventAggregator _eventAggregator;

        public NearbyDevices(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;

            var scanArrivedEvent = _eventAggregator.GetEvent<Events.ScanReceivedEvent>();
            if (scanArrivedSubscriptionToken != null)
            {
                scanArrivedEvent.Unsubscribe(scanArrivedSubscriptionToken);
            }
            scanArrivedSubscriptionToken = scanArrivedEvent.Subscribe(ScanReceivedEventHandler, ThreadOption.UIThread, false);
        }
        public void ScanReceivedEventHandler(Events.ScanReceivedEventData keyScan)
        {
            lock (this)
            {
                if (this.Where((a) => a.Address == keyScan.Address).Count() == 0)
                {
                    this.Add(keyScan);
                    return;
                }
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].Address == keyScan.Address)
                    {
                        this[i] = keyScan;
                        //var eventArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, this.Items[i]);
                        //this.OnCollectionChanged(eventArgs);
                        break;
                    }
                }
            }
        }
    }
}
