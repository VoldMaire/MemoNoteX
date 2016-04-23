using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoNoteModel
{
    [DataBaseClass("Notepad")]
    public class Notepad : ActiveRecord<Notepad>
    {
        [DataBaseProperty("IdUserOwner")]
        public Guid IdUserOwner { get; set; }

        [DataBaseProperty("Name")]
        public string Name { get; set; }
        
        public User UserOwner 
        { 
            get
            {
                return User.Find("Id", IdUserOwner);
            }

            set 
            {
                IdUserOwner = value.Id;
            }
        }

        private ISearchingStrategy m_searchingStrategy = new TagSearchingStrategy();

        public Notepad()
        {
        }

        public Notepad(string name, User user)
        {
            Name = name;
            UserOwner = user;
        }

        public void ChangeStrategy(ISearchingStrategy searchingStrategy)
        {
            m_searchingStrategy = searchingStrategy;
        }

        public List<Note> GetSearchResult(object searchKey)
        {
            return m_searchingStrategy.GetObject(searchKey);
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
