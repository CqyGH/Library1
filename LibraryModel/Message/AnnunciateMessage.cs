using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryModel.Message
{
    public class AnnunciateMessage:message
    {
        public List<Annunciate> list { get; set; }
        public int count { get; set; }
    }
}
