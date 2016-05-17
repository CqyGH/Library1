using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryModel.Message
{
    public class BorrowMessage:message
    {
        public List<Borrow> list { get; set; }
        public int count { get; set; }
    }
}
