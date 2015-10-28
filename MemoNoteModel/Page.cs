using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoNoteModel
{
    public class Page : Base<Page>
    {
        public Chapter Chapter { get; set; }

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

            return u.Name == this.Name;
        }
    }
}
