using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ContactApp.Models;
using ContactApp.ViewModels;

namespace ContactApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactDetailsPage : ContentPage
	{
		public event EventHandler<ContactViewModel> ContactAdded;
		public event EventHandler<ContactViewModel> ContactUpdated;
		public ContactDetailsPage (ContactViewModel contact)
		{
			if (contact is null)
				throw new ArgumentNullException(nameof(contact));
			InitializeComponent ();

			BindingContext = new ContactViewModel
            {
				ID = contact.ID,
				FirstName = contact.FirstName,
				LastName = contact.LastName,
				Phone = contact.Phone,
				Email = contact.Email,
				Blocked= contact.Blocked,
                ImageURL=contact.ImageURL,
                
			};
		}

		private async void btnSave_Clicked(object sender, EventArgs e)
		{
            try
            {
                var contact = BindingContext as ContactViewModel;
                if (string.IsNullOrWhiteSpace(contact.FirstName))
                {
                    await DisplayAlert("Error", "Please Enter the name.", "OK");
                    return;
                }

                if (contact.ID == 0)
                {
                    ContactAdded?.Invoke(this, contact);
                }
                else
                {
                    ContactUpdated.Invoke(this, contact);
                }
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
		}
	}
}