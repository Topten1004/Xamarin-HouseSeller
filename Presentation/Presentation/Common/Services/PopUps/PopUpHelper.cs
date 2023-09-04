using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Immowert4You.Presentation.Common.Services.PopUps
{
    public static class PopUpHelper
    {
        public static async Task ShowAlert(string title, string message, string cancel = "OK")
        {
            await App.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public static async Task<bool> GetUserDecision(string title, string message, string accept = "JA", string cancel = "NEIN")
        {
            return await App.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        public static async Task<string> GetUserInput(string title, string message, string accept = "JA", string cancel = "NEIN")
        {
            return await App.Current.MainPage.DisplayPromptAsync(title, message, accept, cancel, keyboard: Keyboard.Plain);
        }
    }
}
