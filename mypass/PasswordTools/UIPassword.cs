using mypass.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace mypass.PasswordTools
{
    public class UIPassword
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MarkColors MarkColor { get; set; }
        public DateTime ChangedDate { get; set; }
    }
}
