using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using mypass.PasswordTools;

namespace mypass.Viewmodel.MainWindow
{
    internal class AddVM : INotifyPropertyChanged
    {
        private bool isOpen;
        public bool IsOpen
        {
            get => isOpen;
            set
            {
                isOpen = value;
                OnPropertyChanged();
            }
        }

        private bool isPasswordOk;
        public bool IsPasswordOk
        {
            get => isPasswordOk;
            set
            {
                isPasswordOk = value;
                CheckDataState();
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                CheckDataState();
                OnPropertyChanged();
            }
        }

        private bool canAdd;
        public bool CanAdd
        {
            get => canAdd;
            set
            {
                canAdd = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??= new RelayCommand(obj =>
                {
                    PasswordContainer.Add(new Password(PasswordHandler.GetHash(), Name, DateTime.Now));

                    Cancel();
                });
            }
        }

        private RelayCommand generateCommand;
        public RelayCommand GenerateCommand
        {
            get
            {
                return generateCommand ??= new RelayCommand(obj =>
                {
                    PasswordHandler.Generate();
                });
            }
        }

        private RelayCommand passwordChangedCommand;
        public RelayCommand PasswordChangedCommand
        {
            get
            {
                return passwordChangedCommand ??= new RelayCommand(obj =>
                {
                    IsPasswordOk = ((PasswordBox)obj).Password.Length >= 8;
                });
            }
        }

        public AddVM()
        {
            IsPasswordOk = false;
            CanAdd = false;
            Name = string.Empty;
        }

        private void Cancel()
        {
            IsOpen = false;
            IsPasswordOk = false;
            CanAdd = false;
            Name = string.Empty;
            PasswordHandler.Clear();
        }

        private void CheckDataState() => CanAdd = IsPasswordOk && Name.Length >= 2;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
