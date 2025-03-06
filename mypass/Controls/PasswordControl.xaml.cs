using mypass.PasswordTools;
using mypass.Viewmodel.PasswordControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mypass.Controls
{
    public partial class PasswordControl : UserControl
    {
        public PasswordControl(UIPassword uIPassword)
        {
            InitializeComponent();
            var passHandler = new PasswordHandler(this);
            DataContext = new
            {
                edit = new EditVM(uIPassword, passHandler),
                visual = new VisualVM(uIPassword, new AnimManager(this)),
                func = new FunctionsVM(uIPassword, passHandler)
            };
        }
    }
}
