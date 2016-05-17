using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryDAL;
using LibraryModel;

namespace LibraryBLL
{
    public class ReturnBll
    {
        public bool InsertReturn(string StudentID,string UserName,string BookName,string ReturnTime,string Confirm,string ISBN)
        {
            DBContext context = new DBContext();
            Return rt =new  Return();
            rt.StudentID = StudentID;
            rt.UserName = UserName;
            rt.BookName = BookName;
            rt.ReturnTime = ReturnTime;
            rt.Confirm = Confirm;
            rt.ISBN = ISBN;
            return context.InsertData<Return>("Insert_Return",rt);
        }
    }
}
