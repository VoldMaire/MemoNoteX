using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoNoteModel
{
    public class TagSearchingStrategy : ISearchingStrategy
    {
        public List<Note> GetObject(object tag)
        {
            List<Note> result = new List<Note>();
            foreach (Note item in Note.Objects.Values)
            {
                if (item.Tag == (string)tag)
                {
                    result.Add(item);
                }

            }
            return result;
        }
    }
}
