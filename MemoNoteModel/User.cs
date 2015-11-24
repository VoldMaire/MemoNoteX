using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoNoteModel
{
    public class User : ActiveRecord<User>
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            return Name + "(" + Login + ")";
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            User u = obj as User;
            if (u == null)
            {
                return false;
            }

            return u.Login == this.Login && u.Name == this.Name &&
                   u.Password == this.Password;
        }
    }
}
