using System;
using System.Threading;
using System.Threading.Tasks;

namespace Immowert4You.Presentation.Common.Services.Loading
{
    public class BusyManager : IBusyManager
    {
        // Set the minimum time to show as busy
        private readonly TimeSpan MinimumActiveTimeMillisecs = TimeSpan.FromMilliseconds(500);

        // Holds the last time we changed from Ready to Busy.
        private DateTime LastChangeFromReady = DateTime.MinValue;


        private int _busyCounter = 0;


        public BusyManager()
        {
        }

        public event EventHandler<BusyChangedEventArgs> BusyChangedEvent;

        public bool IsBusy
        {
            get
            {
                return _busyCounter > 0;
            }
        }

        public bool IsReady
        {
            get
            {
                return _busyCounter == 0;
            }
        }

        public async Task SetBusy()
        {
            if (IsReady)
            {
                LastChangeFromReady = DateTime.Now;
            }
            _busyCounter++;

            System.Diagnostics.Debug.WriteLine($"Busy - count={_busyCounter}");
            await InvokeBusyChangedEvent();
        }

        public async Task SetUnBusy()
        {
            if (!IsBusy)
            {
                return;
            }
            _busyCounter--;

            // If we now have no busy items
            if (IsReady)
            {
                // If the time since we last went from Ready to Busy is less than the minimum,
                // sleep for the minimum. This avoids momentary flickers of busy indicators.
                if (DateTime.Now.Subtract(LastChangeFromReady) < MinimumActiveTimeMillisecs)
                {
                    System.Diagnostics.Debug.WriteLine($"Unbusy - throttling");
                    Thread.Sleep(MinimumActiveTimeMillisecs);
                }
            }

            System.Diagnostics.Debug.WriteLine($"Unbusy - count={_busyCounter}");
            // Invoke the event to notify any subscribed UI elements.
            await InvokeBusyChangedEvent();
        }

        private async Task InvokeBusyChangedEvent()
        {
            await Task.Run(() => BusyChangedEvent?.Invoke(this, new BusyChangedEventArgs(IsBusy)));
            await Task.Delay(20);
        }
    }
}
