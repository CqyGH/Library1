using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryModel
{
    public class Borrow
    {
        public string BorrowID{ get; set; }
    public string StudentID{ get; set; }
    public string UserName{ get; set; }
    public string BookID{ get; set; }
    public string BookName{ get; set; }
    public string BorrowTime{ get; set; }
    public string ReturnTime{ get; set; }
    public string Renew{ get; set; }
    public string BorrowStatus { get; set; }
    public string ISBN{ get; set; }
    public string Cost { get; set; }
    }
}
