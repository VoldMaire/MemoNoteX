﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoNoteModel
{
    public interface ISearchingStrategy
    {
        List<Note> GetObject(object searchKey);
    }
}
