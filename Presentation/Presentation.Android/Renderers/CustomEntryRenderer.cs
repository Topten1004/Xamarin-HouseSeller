using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Immowert4You.Presentation.Common.Components;
using Presentation.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace Presentation.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control == null || Element == null)
                return;
            if (Element is CustomEntry view)
            {
                if (view.HideBottomLine)
                {
                    Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
                }
                if (view.RemoveHorizontalPadding)
                {
                    Control.SetPadding(Control.PaddingLeft, 1, Control.PaddingRight, 1);
                }
            }
        }
        bool _isDisposed;
        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            base.Dispose(disposing);
            GC.SuppressFinalize(this);
        }
    }
}