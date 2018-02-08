using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ContactApp.Models;
using ContactApp.Views;
using Xamarin.Forms;

namespace ContactApp.ViewModels
{
    class ContactService : IContactService 
    {
        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
        }

        public async Task DisplayAlert(string title, string message, string ok)
        {
             await Application.Current.MainPage.DisplayAlert(title, message, ok);
        }

        public async Task PushAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
