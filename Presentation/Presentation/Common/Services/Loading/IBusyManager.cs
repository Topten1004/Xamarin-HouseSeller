using System;
using System.Threading.Tasks;

namespace Immowert4You.Presentation.Common.Services.Loading
{
    public interface IBusyManager
    {
        /// <summary>
        /// Occurs when the busy stack changes.
        /// </summary>
        event EventHandler<BusyChangedEventArgs> BusyChangedEvent;

        /// <summary>
        /// Sets a busy status.
        /// </summary>
        /// <remarks>
        /// Note that this just increases the count of things which have set a Busy
        /// status.
        /// </remarks>
        Task SetBusy();

        /// <summary>
        /// Sets an UnBusy status.
        /// </summary>
        /// <remarks>
        /// Note that this just decreases the count of things which have a Busy status.
        /// </remarks>
        Task SetUnBusy();

        /// <summary>
        /// Returns True if the internal count of busy items is > 0, otherwise false.
        /// </summary>
        bool IsBusy { get; }

        /// <summary>
        /// Returns True if the internal count of busy items is = 0, otherwise false.
        /// </summary>
        bool IsReady { get; }

    }
}
