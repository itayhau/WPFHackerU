using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFUI.Models;

namespace WPFUI.ViewModels
{
    // Conductor == multiple forms
    public class ShellViewModel : Conductor<object>
    {
        private string _firstName = "Itay";
        private string _lastName = "Hauptman";

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;

                // notify that the FirstName property has changed!
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => FullName);
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;

                // notify that the FirstName property has changed!
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => FullName);
            }
        }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }

        }

        private BindableCollection<PersonModel> _people = new BindableCollection<PersonModel>(); // avoid crash

        public BindableCollection<PersonModel> People
        {
            get
            {
                return _people;
            }
        }


        private PersonModel _selectedPerson = new PersonModel();

        public PersonModel SelectedPerson
        {
            get
            {
                return _selectedPerson;
            }
            set
            {
                _selectedPerson = value;
                NotifyOfPropertyChange(() => SelectedPerson);
            }
        }

        // add ctor
        public ShellViewModel()
        {
            People.Add(new PersonModel { FirstName = "Dana", LastName = "Mor" });
            People.Add(new PersonModel { FirstName = "David", LastName = "Shlomo" });
            People.Add(new PersonModel { FirstName = "Maya", LastName = "Cohen" });
            People.Add(new PersonModel { FirstName = "Samantha", LastName = "Benet" });
        }

        public void ClearText()
        {
            FirstName = "";
            LastName = "";
        }

        public bool CanClearText(string firstName, string lastName)
        {
            return !String.IsNullOrEmpty(firstName) || !String.IsNullOrEmpty(lastName);
        }

        public void LoadPageOne()
        {
            ActivateItem(new FirstChildViewModel());
        }

        public void LoadPageTwo()
        {
            ActivateItem(new SecondChildViewModel());
        }
    }
}
