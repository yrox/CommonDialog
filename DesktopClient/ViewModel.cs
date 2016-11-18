using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using System.Runtime.CompilerServices;
using BaseEntyties;

namespace DesktopClient
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        { }

        public ObservableCollection<Account> Accs;
        public ObservableCollection<GeneralContact> GenContacts;

        private Account _selectedAccount;
        public Account SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                if (_selectedAccount != value)
                {
                    SelectedAccount = value;
                    OnPropertyChanged();
                }

            }
        }

        private GeneralContact _selectedGeneralContact;
        public GeneralContact SelectedGeneralContact
        {
            get { return _selectedGeneralContact; }
            set
            {
                if (_selectedGeneralContact != value)
                {
                    SelectedGeneralContact = value;
                    OnPropertyChanged();
                }
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
