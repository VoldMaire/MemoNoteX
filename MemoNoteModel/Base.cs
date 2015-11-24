using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoNoteModel
{
    public class Base<T> where T : Base<T>
    {
        public static Dictionary<Guid, T> Objects = new Dictionary<Guid, T>();

        public Guid Id { get; set; }

        []
        public string Name { get; set; }

        public Base()
        {
            Id = Guid.NewGuid();
            Objects.Add(Id, (T)this);
        }
    }
}
