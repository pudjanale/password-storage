using mypass.PasswordTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mypass.Viewmodel.PasswordControl
{
    internal class PasswordHandler
    {
        private Controls.PasswordControl passwordControl;

        public PasswordHandler(Controls.PasswordControl passControl)
        {
            passwordControl = passControl;
        }

        public string GetHash()
        {
            return AesEncryption.Encrypt(passwordControl.EditPasswordBox.Password);
        }

        public void SetPassword(int id)
        {
            passwordControl.EditPasswordBox.Password = AesEncryption.Decrypt(PasswordContainer.GetHashById(id));
        }

        public int GetLength(int id)
        {
            return AesEncryption.Decrypt(PasswordContainer.GetHashById(id)).Length;
        }

        public void ToClipboard(int id)
        {
            Clipboard.SetText(AesEncryption.Decrypt(PasswordContainer.GetHashById(id)));
        }
    }
}
