using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace mypass.PasswordTools
{
    internal static class IdRules
    {
        public const int MIN = 100_000_000;
        public const int MAX = 999_999_999;

        public static bool Check(int id) => id >= MIN && id <= MAX;
    }
}
