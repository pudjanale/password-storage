using mypass.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace mypass.PasswordTools
{
    internal class PasswordContainer
    {
        static ObservableCollection<Password> passwords;
        
        public PasswordContainer()
        {
            Deserialize();
            passwords.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e)
            {
                OnColChanged();
            };
        }

        public static void Add(Password password)
        {
            if (!IdRules.Check(password.Id))
                throw new ArgumentNullException(nameof(password));
            if (String.IsNullOrEmpty(password.Hash))
                throw new ArgumentNullException(nameof(password));
            if (String.IsNullOrEmpty(password.Name))
                throw new ArgumentNullException(nameof(password));

            passwords.Add(password);
        }

        public static void Change(int id, string newHash, string newName)
        {
            if (String.IsNullOrEmpty(newHash))
                throw new ArgumentNullException(nameof(newHash));
            if (String.IsNullOrEmpty(newName))
                throw new ArgumentNullException(nameof(newName));
            if (!IdRules.Check(id))
                throw new Exception("Bad id");
            if (!passwords.Any())
                throw new NullReferenceException(nameof(passwords));

            int index = -1;
            MarkColors color = MarkColors.White;
            foreach (var pass in passwords)
            {
                if (pass.Id == id)
                {
                    index = passwords.IndexOf(pass);
                    color = pass.MarkColor;
                }
            }

            if (index != -1)
            {
                passwords[index] = new Password(newHash, newName, DateTime.Now, color);
            }

            OnColChanged();
        }

        public static void Remove(int id)
        {
            if (!IdRules.Check(id))
                throw new ArgumentNullException(nameof(id));
            if (!passwords.Any())
                throw new NullReferenceException(nameof(passwords));

            foreach (var p in passwords.ToArray())
            {
                if (p.Id == id)
                {
                    passwords.Remove(p);
                }
            }
        }

        public static void ChangeMark(int id, MarkColors mark)
        {
            Password target = (from pass in passwords
                               where pass.Id == id
                               select pass).FirstOrDefault();

            int targetIndex = passwords.IndexOf(target);

            if (targetIndex != -1)
            {
                passwords[targetIndex].MarkColor = mark;
                OnColChanged();
            }
        }

        public static void MoveUp(int id)
        {
            Password target = (from pass in passwords
                               where pass.Id == id
                               select pass).FirstOrDefault();

            int targetIndex = passwords.IndexOf(target);

            if (targetIndex != -1)
            {
                if (targetIndex != 0)
                    passwords.Move(targetIndex, targetIndex - 1);
            }
        }

        public static void MoveDown(int id)
        {
            Password target = (from pass in passwords
                               where pass.Id == id
                               select pass).FirstOrDefault();

            int targetIndex = passwords.IndexOf(target);

            if (targetIndex != -1)
            {
                if (targetIndex != passwords.Count - 1)
                    passwords.Move(targetIndex, targetIndex + 1);
            }
        }

        public static string GetHashById(int id)
        {
            return (from pass in passwords
                    where pass.Id == id
                    select pass.Hash).FirstOrDefault();
        }

        public static IEnumerable<UIPassword> GetAll()
        {
            var list = new List<UIPassword>();
            foreach (var pass in passwords)
            {
                list.Add(new UIPassword() { Id = pass.Id, Name = pass.Name, ChangedDate = pass.ChangedDate, MarkColor = pass.MarkColor });
            }
            return list;
        }

        private static void CheckCollection()
        {
            if (passwords != null && passwords.Any())
            {
                var idList = new List<int>();

                foreach (var p in passwords.ToArray())
                {
                    if (idList.Any())
                    {
                        foreach (var id in idList)
                        {
                            if (p.Id == id)
                            {
                                int index = passwords.IndexOf(p);
                                if (index != -1)
                                {
                                    passwords[index] = new Password(p.Hash, p.Name, p.ChangedDate, p.MarkColor);
                                    break;
                                }
                            }
                        }
                    }

                    idList.Add(p.Id);
                }

                OnColChanged();
            }
        }

        private static void Serialize()
        {
            string jsonString = string.Empty;
            try
            {
                jsonString = JsonSerializer.Serialize(passwords, new JsonSerializerOptions() { WriteIndented = true });
            }
            catch { }

            if (!String.IsNullOrEmpty(jsonString))
            {
                File.WriteAllText(PathManager.PasswordFile, jsonString);
            }
        }

        private static void Deserialize()
        {
            string jsonString = File.ReadAllText(PathManager.PasswordFile);

            if (!String.IsNullOrEmpty(jsonString))
            {
                passwords = JsonSerializer.Deserialize<ObservableCollection<Password>>(jsonString);
            }
            else
            {
                passwords = new ObservableCollection<Password>();
            }

            CheckCollection();
        }

        private static void OnColChanged()
        {
            Serialize();
            CollectionChanged?.Invoke();
        }

        public static event PassChangedEventHandler CollectionChanged;
        public delegate void PassChangedEventHandler();
    }
}
