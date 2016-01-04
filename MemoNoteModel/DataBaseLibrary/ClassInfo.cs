using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoNoteModel
{
    public class ClassInfo
    {
        public string NameTable { get; set; }

        public Type TypeClass { get; set; }

        public List<string> NameProperties { get; set; }

        public List<string> Properties { get; set; }

        public List<Type> TypePropereties { get; set; }

        public ClassInfo() 
        {
            NameProperties = new List<string>();
            TypePropereties = new List<Type>();
            Properties = new List<string>();
        }

        public ClassInfo(string table, Type typeclass, List<string> props, List<Type> typeprop)
        {
            NameTable = table;
            TypeClass = typeclass;
            NameProperties = props;
            TypePropereties = typeprop;
        }
    }
}
