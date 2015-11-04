using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoNoteModel
{
    public class Note : Base<Note>
    {
        public DateTime Date { get; set; }

        public string Tag { get; set; }

        public string Text { get; set; }

        public Note()
        {
            Date = DateTime.Now;
        }

        public Note(string name, string tag, string text)
        {
            Name = name;
            Tag = tag;
            Text = text;
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return Name + "(" + Tag + ")";
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

            Note u = obj as Note;
            if (u == null)
            {
                return false;
            }

            return u.Name == this.Name && u.Date == u.Date && u.Tag == this.Tag;
        }
    }
}
