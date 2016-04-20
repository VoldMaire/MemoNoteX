using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoNoteModel
{
    public class Chapter : ActiveRecord<Chapter>
    {
        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public Notepad Owner { get; set; }

        public Chapter(string name, Notepad owner)
        {
            Name = name;
            Owner = owner;
            CreationDate = DateTime.Now;
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

            Chapter u = obj as Chapter;
            if (u == null)
            {
                return false;
            }

            return u.Name == this.Name && u.Owner == this.Owner 
                && u.CreationDate == this.CreationDate;
        }
    }
}
