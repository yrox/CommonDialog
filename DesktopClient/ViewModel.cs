using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BaseEntyties;

namespace DesktopClient
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        { }

        public ObservableCollection<Account> Accs;
        public ObservableCollection<MetaContact> metaContacts;

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

        private MetaContact _selectedMetaContact;
        public MetaContact SelectedMetaContact
        {
            get { return _selectedMetaContact; }
            set
            {
                if (_selectedMetaContact != value)
                {
                    SelectedMetaContact = value;
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
