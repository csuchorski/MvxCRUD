using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvxStarter.Core.Models
{
    public class PersonModel : MvxNotifyPropertyChanged
    {
        private int _personID;
        private string _name;
        private string _country;
        private string _email;

        public int PersonID
        {
            get { return _personID; }
            set 
            { 
                _personID = value;
            }
        }
        public string Name
        {
            get { return _name; }
            set 
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }
        public string Country
        {
            get { return _country; }
            set 
            {
                _country = value;
                RaisePropertyChanged(() => Country);
            }
        }
        public string Email
        {
            get { return _email; }
            set 
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }
    }
}