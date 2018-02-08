using ContactApp.Models;
using ContactApp.ViewModels;
using Newtonsoft.Json;
using Plugin.Battery;
using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactApp.Views
{
    public partial class ContactList : ContentPage
    {
        //private SQLiteAsyncConnection _connection;
        //private ObservableCollection<Contact> _contacts;
        //private HttpClient _client = new HttpClient();
        //private const string BaseURL = "http://192.168.100.20/api/Contacts";
        public ContactListViewModel ViewModel { get { return BindingContext as ContactListViewModel; } set { BindingContext = value; } }
        public ContactList()
        {
            ViewModel = new ContactListViewModel(new ContactService());
            InitializeComponent();
            txtBatteryPowerSource.Text= $"Power Source: {CrossBattery.Current.PowerSource}";
            txtBatteryRemainingChargePercent.Text = $"Remaining Charge Percent {CrossBattery.Current.RemainingChargePercent}";
            txtBatteryStatus.Text = $"Status: {CrossBattery.Current.Status}";
            CrossBattery.Current.BatteryChanged += Current_BatteryChanged;
            //_connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        private void Current_BatteryChanged(object sender, Plugin.Battery.Abstractions.BatteryChangedEventArgs e)
        {
            txtBatteryPowerSource.Text = $"Power Source: {CrossBattery.Current.PowerSource}";
            txtBatteryRemainingChargePercent.Text = $"Remaining Charge Percent {CrossBattery.Current.RemainingChargePercent}";
            txtBatteryStatus.Text = $"Status: {CrossBattery.Current.Status}";
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadContactsCommand.Execute(null);
            base.OnAppearing();
            //await _connection.CreateTableAsync<Contact>();
            //var contacts = await _connection.Table<Contact>().ToListAsync();

        }
        private void lstContacts_Refreshing(object sender, EventArgs e)
        {
            ViewModel.LoadContactsCommand.Execute(null);
            lstContacts.EndRefresh();
        }

        private void lstContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectContactCommand.Execute(e.SelectedItem);
        }
    }
}
