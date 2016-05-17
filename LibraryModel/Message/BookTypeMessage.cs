using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryModel.Message
{
    public class BookTypeMessage:message
    {
        public List<BookType> list { get; set; }
    }
}
