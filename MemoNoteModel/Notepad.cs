using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoNoteModel
{
    public class Notepad : Base<Notepad>
    {
        public User UserOwner { get; set; }

        public Notepad(string name, User user)
        {
            Name = name;
            UserOwner = user;
        }

        public override string ToString()
        {
            return Name;
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

            Notepad u = obj as Notepad;
            if (u == null)
            {
                return false;
            }

            return u.Name == this.Name && u.UserOwner.Equals(this.UserOwner);
        }
    }
}
