using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryModel.Message
{
    public class UserMessage:message
    {
        public List<User> list { get; set; }
        public int count { get; set; }
    }
}
