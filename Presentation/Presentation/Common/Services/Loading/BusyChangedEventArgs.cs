namespace Immowert4You.Presentation.Common.Services.Loading
{
    public class BusyChangedEventArgs : System.EventArgs
    {
        public BusyChangedEventArgs(bool busy) : base()
        {
            this.Busy = busy;
        }

        public bool Busy
        {
            get;
            set;
        }

    }
}
