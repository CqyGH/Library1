using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryModel.Message
{
    public class AppointmentMessage:message
    {
        public int count { get; set; }
        public List<Appointment> list { get; set; }
    }
}
