using mypass.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mypass.PasswordTools
{
    internal class Password
    {
        public int Id { get; private set; }
        public string Hash { get; private set; }
        public string Name { get; private set; }
        
        public MarkColors MarkColor { get; set; }
        public DateTime ChangedDate { get; set; }

        public Password(string hash, string name, DateTime changedDate, MarkColors markColor = MarkColors.White, int id = -1)
        {
            Hash = hash;
            Name = name;
            ChangedDate = changedDate;
            MarkColor = markColor;
            Id = !IdRules.Check(id) ? GenerateId() : id;
        }

        private int GenerateId() => random.Next(IdRules.MIN, IdRules.MAX);

        private static Random random = new Random();
    }
}
