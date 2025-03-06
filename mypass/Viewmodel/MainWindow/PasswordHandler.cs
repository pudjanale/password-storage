using mypass.PasswordTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mypass.Viewmodel.MainWindow
{
    internal class PasswordHandler
    {
        public static mypass.MainWindow MainWindow { private get; set; }

        public static void Generate()
        {
            MainWindow.AddPasswordBox.Password = Generator.GeneratePassword();
        }

        public static void Clear()
        {
            MainWindow.AddPasswordBox.Password = string.Empty;
        }

        public static string GetHash()
        {
            return AesEncryption.Encrypt(MainWindow.AddPasswordBox.Password);
        }
    }
}
