using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace mypass.Viewmodel.PasswordControl
{
    internal class AnimManager
    {
        private Controls.PasswordControl passwordControl;

        public AnimManager(Controls.PasswordControl passControl)
        {
            passwordControl = passControl;
        }

        public void BeginSP(DoubleAnimation da)
        {
            passwordControl.AnimStackPanel.BeginAnimation(StackPanel.WidthProperty, da);
        }
    }
}
