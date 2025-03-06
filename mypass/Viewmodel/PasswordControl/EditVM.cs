using mypass.PasswordTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace mypass.Viewmodel.PasswordControl
{
    internal class EditVM : INotifyPropertyChanged
    {
        private UIPassword uiPassword;
        private PasswordHandler passwordHandler;

        private bool isPasswordValid;

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

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                CheckPassword();
                OnPropertyChanged();
            }
        }

        private bool canApply;
        public bool CanApply
        {
            get => canApply;
            set
            {
                canApply = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand applyCommand;
        public RelayCommand ApplyCommand
        {
            get
            {
                return applyCommand ??= new RelayCommand(obj =>
                {
                    IsOpen = false;
                    PasswordContainer.Change(uiPassword.Id, passwordHandler.GetHash(), Name);
                });
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??= new RelayCommand(obj =>
                {

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
                    isPasswordValid = ((PasswordBox)obj).Password.Length >= 8;
                    CheckPassword();
                });
            }
        }

        public EditVM(UIPassword uIPassword, PasswordHandler passwordHandler)
        {
            this.uiPassword = uIPassword;
            this.passwordHandler = passwordHandler;
            passwordHandler.SetPassword(uiPassword.Id);
            Name = uiPassword.Name;
            isPasswordValid = passwordHandler.GetLength(uiPassword.Id) >= 8;
            CheckPassword();
        }

        private void CheckPassword() => CanApply = isPasswordValid && Name.Length >= 2;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
