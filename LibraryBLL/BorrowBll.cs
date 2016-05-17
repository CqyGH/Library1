using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryModel;
using LibraryDAL;


namespace LibraryBLL
{
    public  class BorrowBll
    {
        /// <summary>
        /// 新增借书
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="ISBN"></param>
        /// <param name="BorrowTime"></param>
        /// <param name="ReturnTime"></param>
        /// <param name="Renew"></param>
        /// <param name="BorrowStatus"></param>
        /// <returns></returns>
        public bool InsertBorrow(string StudentID, string ISBN, string BorrowTime, string ReturnTime, string Renew, string BorrowStatus, string Cost) 
        {
            DBContext context = new DBContext();
            Borrow borrow = new Borrow();
            borrow.StudentID = StudentID;
            borrow.ISBN = ISBN;
            borrow.BorrowTime = BorrowTime;
            borrow.ReturnTime = ReturnTime;
            borrow.Renew = Renew;
            borrow.BorrowStatus = BorrowStatus;
            borrow.Cost = Cost;
            return context.InsertData<Borrow>("Insert_Borrow", borrow);
        }

        /// <summary>
        /// 查询学生借阅图书数量
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public int SelectStudentBorrow(string StudentID) {
            DBContext context = new DBContext();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("StudentID", StudentID);
            return Convert.ToInt32(context.GetScalar<Borrow>("Select_Student_Borrow_Count",dictionary));
        }

        /// <summary>
        /// 根据学生ID查询学生借阅情况
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public List<Borrow> SelectBorrow(string StudentID)
        {
            DBContext context = new DBContext();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("StudentID", StudentID);
            return context.GetDataList<Borrow>("Select_StudentBorrow", dictionary);
        }

        public bool UpdateBorrowReturn(string BorrowStatus, string BorrowID)
        {
            DBContext context = new DBContext();
            Borrow borrow = new Borrow();
            borrow.BorrowStatus = BorrowStatus;
            borrow.BorrowID = BorrowID;
            return context.InsertData<Borrow>("Update_Borrow_Return", borrow);
        }


        /// <summary>
        /// 更新欠费
        /// </summary>
        /// <param name="Cost"></param>
        /// <param name="BorrowID"></param>
        /// <returns></returns>
        public bool UpdateBorrowCost(string Cost, string BorrowID)
        {
            DBContext context = new DBContext();
            Borrow borrow = new Borrow();
            borrow.Cost = Cost;
            borrow.BorrowID = BorrowID;
            return context.InsertData<Borrow>("Update_Borrow_Cost", borrow);
        }

        /// <summary>
        /// 查询被借阅但是未被归还的图书，在删除图书的时候使用该方法
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        public int SelectBooktBorrowStatusCount(string BookID)
        {
            DBContext context = new DBContext();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("BookID", BookID);
            return Convert.ToInt32(context.GetScalar<Borrow>("Select_Book_BorrowStatus_Count", dictionary));
        }



        public bool UpdateBorrowRenew(string BorrowID, string ReturnTime)
        {
            DBContext context = new DBContext();
            Borrow borrow = new Borrow();
            borrow.BorrowID = BorrowID;
            borrow.ReturnTime = ReturnTime;
            return context.UpdateData<Borrow>("Update_Borrow_Renew", borrow);
        }
    }
}
