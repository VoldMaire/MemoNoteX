using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoNoteModel
{
    public class NameSearchingStrategy : ISearchingStrategy
    {
        public List<Note> GetObject(object name)
        {
            List<Note> result = new List<Note>();
            foreach (Note item in Note.Objects.Values)
            {
                if (item.Name == (string)name)
                {
                    result.Add(item);
                }

            }

            return result;
        }
    }
}
