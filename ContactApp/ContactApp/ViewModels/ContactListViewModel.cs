using ContactApp.Models;
using ContactApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactApp.ViewModels
{
    public class ContactListViewModel : BaseViewModel
    {
        private ContactViewModel _selectedContact;
        private HttpClient _client = new HttpClient();
        private readonly IContactService _contactService;
        private const string BaseURL = "http://192.168.100.20/api/Contacts";
        public ObservableCollection<ContactViewModel> Contacts { get; private set; } = new ObservableCollection<ContactViewModel>();

        public ContactViewModel SelectedContact
        {
            get { return _selectedContact; }
            set { SetValue(ref _selectedContact, value); }
        }

        public ICommand AddContactCommand { get; private set; }
        public ICommand SelectContactCommand { get; private set; }
        public ICommand LoadContactsCommand { get; private set; }
        public ICommand DeleteContactCommand { get; private set; }
        public ContactListViewModel(IContactService ContactService)
        {
            _contactService = ContactService;

            AddContactCommand = new Command(AddContact);
            SelectContactCommand = new Command<ContactViewModel>(async vm => await SelectContact(vm));
            LoadContactsCommand = new Command(LoadContacts);
            DeleteContactCommand = new Command<ContactViewModel>(async vm => await DeleteContact(vm));
        }
        private async void AddContact()
        {

            var page = new ContactDetailsPage(new ContactViewModel());
            page.ContactAdded += async (source, contact) =>
            {
                _client.DefaultRequestHeaders.ExpectContinue = false;

                var response = await _client.PostAsJsonAsync(BaseURL, contact);
                if (response.IsSuccessStatusCode)
                    Contacts.Add(contact);
                //_connection.InsertAsync(contact);
            };
            await _contactService.PushAsync(page);

        }

        private async void LoadContacts()
        {
            var response = await _client.GetAsync(BaseURL);
            var content = response.Content;
            string result = await content.ReadAsStringAsync();
            var contacts = JsonConvert.DeserializeObject<List<ContactViewModel>>(result);
            Contacts.Clear();
            foreach (var contact in contacts)
                Contacts.Add(contact);
        }

        private async Task SelectContact(ContactViewModel selectedContact)
        {
            if (selectedContact is null)
                return;

            var page = new ContactDetailsPage(selectedContact);
            page.ContactUpdated += async (source, contact) =>
            {
                var response = await _client.PutAsJsonAsync(BaseURL + "/" + contact.ID, contact);


                //_connection.UpdateAsync(contact);
            };
            await _contactService.PushAsync(page);
            SelectedContact = null;
        }


        private async Task DeleteContact(ContactViewModel contact)
        {
            if (await _contactService.DisplayAlert("Warning", $"Are you sure you want to delete {contact.FullName}?", "Yes", "No"))
            {
                var response = await _client.DeleteAsync(BaseURL + "/" + contact.ID);
                if (response.IsSuccessStatusCode)
                    Contacts.Remove(contact);
            }
            //await _connection.DeleteAsync(contact);
        }
    }
}
