using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using mypass.Controls;
using mypass.PasswordTools;

namespace mypass.Viewmodel.MainWindow
{
    internal class ItemsVM : INotifyPropertyChanged
    {
        public ObservableCollection<Controls.PasswordControl> Items { get; private set; }

        public ItemsVM()
        {
            UpdateItems();
            PasswordContainer.CollectionChanged += () => 
            {
                UpdateItems();
            };
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Items));
        }

        private void UpdateItems()
        {
            var uiPasswords = PasswordContainer.GetAll();
            Items = new ObservableCollection<Controls.PasswordControl>(uiPasswords.Select(uip => new Controls.PasswordControl(uip)));
            Items.CollectionChanged += Items_CollectionChanged;
            OnPropertyChanged(nameof(Items));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
