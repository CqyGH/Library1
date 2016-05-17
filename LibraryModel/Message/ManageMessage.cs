using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryModel.Message
{
    public class ManageMessage:message
    {
        public List<Management> list { get; set; }
        public int count { get; set; }
    }
}
