using mypass.PasswordTools;
using mypass.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace mypass.Viewmodel.PasswordControl
{
    internal class VisualVM : INotifyPropertyChanged
    {
        private UIPassword uiPassword;
        private AnimManager animManager;

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private bool isOpen;
        public bool IsOpen
        {
            get => isOpen;
            set
            {
                isOpen = value;

                DoubleAnimation da = new DoubleAnimation();
                da.Duration = TimeSpan.FromMilliseconds(100);
                da.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut };
                if (value)
                {
                    ButtonsVisibility = Visibility.Visible;
                    da.To = 200;

                    if (uiPassword.MarkColor == MarkColors.White)
                        NameBorderBrush = Brushes.Black;
                    else
                     NameBorderBrush = MarkManager.GetColor(uiPassword.MarkColor);
                }
                else
                {
                    da.To = 0;
                    da.Completed += (sender, args) =>
                    {
                        ButtonsVisibility = Visibility.Collapsed;
                    };

                    NameBorderBrush = Brushes.Black;
                }
                animManager.BeginSP(da);

                OnPropertyChanged();
            }
        }

        private Brush nameBorderBrush;
        public Brush NameBorderBrush
        {
            get => nameBorderBrush;
            set
            {
                nameBorderBrush = value;
                OnPropertyChanged();
            }
        }

        private Brush markBrush;
        public Brush MarkBrush
        {
            get => markBrush;
            set
            {
                markBrush = value;
                OnPropertyChanged();
            }
        }

        private string changedDate;
        public string ChangedDate
        {
            get => changedDate;
            set
            {
                changedDate = value;
                OnPropertyChanged();
            }
        }

        private Visibility buttonsVisibility;
        public Visibility ButtonsVisibility
        {
            get => buttonsVisibility;
            set
            {
                buttonsVisibility = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand clickCommand;
        public RelayCommand ClickCommand
        {
            get
            {
                return clickCommand ??= new RelayCommand(obj =>
                {
                    IsOpen = !IsOpen;
                });
            }
        }

        private RelayCommand onLoadedCommand;
        public RelayCommand OnLoadedCommand
        {
            get
            {
                return onLoadedCommand ??= new RelayCommand(obj =>
                {
                    IsOpen = false;
                });
            }
        }

        private RelayCommand pickColorCommand;
        public RelayCommand PickColorCommand
        {
            get
            {
                return pickColorCommand ??= new RelayCommand(obj =>
                {
                    PasswordContainer.ChangeMark(uiPassword.Id, MarkManager.GetMark((string)obj));
                });
            }
        }

        public VisualVM(UIPassword uIPassword, AnimManager animManager)
        {
            this.uiPassword = uIPassword;
            Name = uIPassword.Name;
            MarkBrush = MarkManager.GetColor(uiPassword.MarkColor);
            ChangedDate = uiPassword.ChangedDate.ToString("dd.MM.yyyy HH:mm");
            this.animManager = animManager;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
