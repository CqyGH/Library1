using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryModel.Message
{
   public class BookMessage:message
   {
       public int count { get; set; }
       public int borrowCount { get; set; }
       public List<Book> list { get; set; }
    }
}
