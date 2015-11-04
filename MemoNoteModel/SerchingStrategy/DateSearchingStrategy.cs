using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoNoteModel
{
    public class DateSearchingStrategy : ISearchingStrategy
    {
        public List<Note> GetObject(object date)
        {
            List<Note> result = new List<Note>();
            foreach (Note item in Note.Objects.Values)
            {
                if(item.Date == (DateTime)date)
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
