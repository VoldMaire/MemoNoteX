using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoNoteModel
{
    [AttributeUsage (AttributeTargets.Class)]
    public class DataBaseClassAttribute : Attribute
    {
        public string Name { get; set; }

        public DataBaseClassAttribute(string name)
        {
            Name = name;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class DataBasePropertyAttribute : Attribute
    {
        public string Name { get; set; }

        public Type type { get; set; }

        public DataBasePropertyAttribute(string name)
        {
            Name = name;
            type = this.GetType();
        }
    }
}
