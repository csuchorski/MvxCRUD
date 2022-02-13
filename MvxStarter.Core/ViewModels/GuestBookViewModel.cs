using MvvmCross.Commands;
using MvvmCross.ViewModels;
using MvxStarter.Core.Models;
using MvxStarter.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvxStarter.Core.ViewModels
{
    public class GuestBookViewModel : MvxViewModel
    {
        private string _name="";
        private string _country="";
        private string _email="";
        public string Name
        {
            get { return _name; }
            set 
            {
                _name = value;
                RaisePropertyChanged(() => Name);
                RaisePropertyChanged(() => CanAddPerson);
                RaisePropertyChanged(() => CanUpdatePerson);
            }
        }
        public string Country
        {
            get { return _country; }
            set 
            { 
                _country = value;
                RaisePropertyChanged(() => Country);
                RaisePropertyChanged(() => CanAddPerson);
                RaisePropertyChanged(() => CanUpdatePerson);
            }
        }
        public string Email
        {
            get { return _email; }
            set
            { 
                _email = value;
                RaisePropertyChanged(() => Email);
                RaisePropertyChanged(() => CanAddPerson);
                RaisePropertyChanged(() => CanUpdatePerson);
            }
        }

        //Local list of people
        private ObservableCollection<PersonModel> _people = new(PersonDA.GetListOfPeople(""));
        public ObservableCollection<PersonModel> People
        {
            get => _people;
            set => SetProperty(ref _people, value);
        }
        private PersonModel _selectedPerson;
        public PersonModel SelectedPerson
        {
            get
            {
                return _selectedPerson;
            }
            set
            {
                _selectedPerson = value;
                RaisePropertyChanged(() => CanModifyPerson);
                RaisePropertyChanged(() => CanUpdatePerson);
            }
        }
        public bool CanModifyPerson => SelectedPerson != null;

        //Searched Person prop

        private string _searchedPerson;
        public string SearchedPerson
        {
            get => _searchedPerson;
            set
            {
                SetProperty(ref _searchedPerson, value);
                SetProperty(ref _people, new(PersonDA.GetListOfPeople(SearchedPerson)));
                RaisePropertyChanged(() => People);
            }
        }


        //Add person
        public bool CanAddPerson => Name.Length > 0 && Country.Length > 0 && Email.Length > 0;
        public void AddPerson()
        {
            PersonModel p = new() { Name = Name, Country = Country, Email = Email };
            PersonDA.AddPersonToDB(p);
            AddPersonToList();

            Name = String.Empty;
            Country = String.Empty;
            Email = String.Empty;
        }
        public void AddPersonToList()
        {
            People.Add(PersonDA.FindLastPerson());
        }      
        public IMvxCommand AddPersonCommand { get; set; }

        //Delete person
        public void DeletePerson()
        {
            PersonDA.DeletePersonFromDB(SelectedPerson);
            DeletePersonFromList();
        }
        public void DeletePersonFromList()
        {
            People.Remove(SelectedPerson);
        }
        public IMvxCommand DeletePersonCommand { get; set; }

        //Update person
        public bool CanUpdatePerson => CanModifyPerson && CanAddPerson;

        public void UpdatePerson()
        {
            UpdatePersonFromList();
            PersonDA.UpdatePersonFromDB(SelectedPerson);
            Name = String.Empty;
            Email = String.Empty;
            Country = String.Empty;
        }
        public void UpdatePersonFromList()
        {
            int IndexOsoby = People.IndexOf(SelectedPerson);
            People[IndexOsoby].Name = Name;
            People[IndexOsoby].Country = Country;
            People[IndexOsoby].Email = Email;
        }
        public IMvxCommand UpdatePersonCommand { get; set; }



        //Constructors
        public GuestBookViewModel()
        {
            AddPersonCommand = new MvxCommand(AddPerson);
            DeletePersonCommand = new MvxCommand(DeletePerson);
            UpdatePersonCommand = new MvxCommand(UpdatePerson);
        }
    }
}
