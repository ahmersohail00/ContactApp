using System;
using System.Collections.Generic;
using System.Text;

namespace ContactApp.ViewModels
{
    public class ContactViewModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImageURL { get; set; }
        public bool Blocked { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }
}
