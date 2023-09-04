using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Common.Components
{
    public class CustomEntry : Entry
    {
        public event EventHandler<FocusEventArgs> FocuseChanged;
        public CustomEntry()
        {
        }

        public static readonly BindableProperty HideBottomLineProperty = BindableProperty.Create(
            propertyName: nameof(HideBottomLine),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay);
        public bool HideBottomLine
        {
            get { return (bool)GetValue(HideBottomLineProperty); }
            set { SetValue(HideBottomLineProperty, value); }
        }
        public static readonly BindableProperty RemoveHorizontalPaddingProperty = BindableProperty.Create(
            propertyName: nameof(RemoveHorizontalPadding),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay);
        public bool RemoveHorizontalPadding
        {
            get { return (bool)GetValue(RemoveHorizontalPaddingProperty); }
            set { SetValue(RemoveHorizontalPaddingProperty, value); }
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (nameof(IsFocused) == propertyName)
            {
                FocuseChanged?.Invoke(this, new FocusEventArgs(this, IsFocused));
            }
        }
    }
}
