using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoNoteModel
{
    public class Media<T> : Base<T> where T : Media<T> 
    {
        public User Owner { get; set; }

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

            Media<T> u = obj as Media<T>;
            if (u == null)
            {
                return false;
            }

            return u.Name == this.Name && u.Owner == this.Owner;
        }
    }
}
