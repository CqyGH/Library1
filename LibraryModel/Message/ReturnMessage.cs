using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryModel.Message
{
    public class ReturnMessage:message
    {
        public List<Return> list { get; set; }
    }
}
