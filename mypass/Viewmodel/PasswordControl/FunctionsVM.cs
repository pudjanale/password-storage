using mypass.PasswordTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace mypass.Viewmodel.PasswordControl
{
    internal class FunctionsVM : INotifyPropertyChanged
    {
        private UIPassword uiPassword;
        private PasswordHandler passwordHandler;

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??= new RelayCommand(obj =>
                {
                    PasswordContainer.Remove(uiPassword.Id);
                });
            }
        }

        private RelayCommand moveUpCommand;
        public RelayCommand MoveUpCommand
        {
            get
            {
                return moveUpCommand ??= new RelayCommand(obj =>
                {
                    PasswordContainer.MoveUp(uiPassword.Id);
                });
            }
        }

        private RelayCommand moveDownCommand;
        public RelayCommand MoveDownCommand
        {
            get
            {
                return moveDownCommand ??= new RelayCommand(obj =>
                {
                    PasswordContainer.MoveDown(uiPassword.Id);
                });
            }
        }

        private RelayCommand toClipboardCommand;
        public RelayCommand ToClipboardCommand
        {
            get
            {
                return toClipboardCommand ??= new RelayCommand(obj =>
                {
                    passwordHandler.ToClipboard(uiPassword.Id);
                });
            }
        }

        public FunctionsVM(UIPassword uIPassword, PasswordHandler passwordHandler)
        {
            this.uiPassword = uIPassword;
            this.passwordHandler = passwordHandler;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
