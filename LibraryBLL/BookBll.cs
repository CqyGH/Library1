using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryDAL;
using LibraryModel;

namespace LibraryBLL
{
    public class BookBll
    {
        /// <summary>
        /// 查询图书
        /// </summary>
        /// <param name="BookName"></param>
        /// <param name="BookAuthor"></param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Book> SelectAllBook(string BookName, string BookAuthor, int startIndex, int pageSize)
        {
            List<Parameter> parameters = new List<Parameter>();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            DBContext context = new DBContext();
            string whereString = string.Empty;

            if (!string.IsNullOrEmpty(BookName))
            {
                dictionary.Add("BookName", "%" + BookName + "%");
                parameters.Add(new Parameter() { ParameterName="BookName",Type="string"});
                whereString = "AND BookName like @BookName";
            }
            else if (!string.IsNullOrEmpty(BookAuthor))
            {
                dictionary.Add("BookAuthor", "%" + BookAuthor + "%");
                parameters.Add(new Parameter() { ParameterName = "BookAuthor", Type = "string" });
                whereString = "AND BookAuthor like @BookAuthor";
            }

            else
            {
                whereString = "";
            }
            return context.GetDataListPager<Book>("Select_All_Book", dictionary, (startIndex - 1) * pageSize, startIndex * pageSize, whereString, parameters);
        }

        /// <summary>
        /// 查询图书数量
        /// </summary>
        /// <param name="BookName"></param>
        /// <param name="BookAuthor"></param>
        /// <returns></returns>
        public int SelectBookCount(string BookName, string BookAuthor)
        {
            //List<Parameter> parameters = new List<Parameter>();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            DBContext context = new DBContext();
            //string whereString = string.Empty;

            if (!string.IsNullOrEmpty(BookName))
            {
                dictionary.Add("BookName", "%" + BookName + "%");
                //parameters.Add(new Parameter() { ParameterName = "BookName", Type = "string" });
                //whereString = "AND BookName like @BookName";
                return Convert.ToInt32(context.GetScalar<Book>("Select_Book_Count_BookName",dictionary));
            }
            else if (!string.IsNullOrEmpty(BookAuthor))
            {
                dictionary.Add("BookAuthor", "%" + BookAuthor + "%");
                //parameters.Add(new Parameter() { ParameterName = "BookAuthor", Type = "string" });
                //whereString = "AND BookAuthor like @BookAuthor";
                return Convert.ToInt32(context.GetScalar<Book>("Select_Book_Count_BookAuthor",dictionary));
            }

            else
            {
                return Convert.ToInt32(context.GetScalar<Book>("Select_Book_Count"));
            }
            //return Convert.ToInt32(context.GetScalar<Book>("Select_Book_Count", dictionary, whereString, parameters));
        }

        /// <summary>
        /// 新增图书
        /// </summary>
        /// <param name="ISBN"></param>
        /// <param name="BookType"></param>
        /// <param name="BookName"></param>
        /// <param name="BookAuthor"></param>
        /// <param name="Publisher"></param>
        /// <param name="Price"></param>
        /// <param name="BookCase"></param>
        /// <param name="Extant"></param>
        /// <param name="Stock"></param>
        /// <param name="Appointment"></param>
        /// <param name="BookLoan"></param>
        /// <param name="Logout"></param>
        /// <param name="Summary"></param>
        /// <param name="PublishTime"></param>
        /// <returns></returns>
        public bool InsertBook(string ISBN,string BookType,string BookName,string BookAuthor,string Publisher,string Price,string BookCase,string Extant,string Stock,string Appointment,string BookLoan,string Logout,string Summary,string PublishTime)
        {
            DBContext context = new DBContext();
            Book book = new Book();
            book.ISBN = ISBN;
            book.BookType = BookType;
            book.BookName = BookName;
            book.BookAuthor = BookAuthor;
            book.Publisher = Publisher;
            book.Price = Price;
            book.BookCase = BookCase;
            book.Extant = Extant;
            book.Appointment = Appointment;
            book.Stock = Stock;
            book.BookLoan = BookLoan;
            book.Logout = Logout;
            book.Summary = Summary;
            book.PublishTime = PublishTime;
            return context.InsertData<Book>("Insert_Book",book);
        }

        /// <summary>
        /// 删除图书
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        public bool DeleteBook(string BookID)
        {
            DBContext context = new DBContext();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("BookID", BookID);
            return context.DeleteData<Book>("Delete_Book", dictionary);
        }

        /// <summary>
        /// 修改图书信息
        /// </summary>
        /// <param name="ISBN"></param>
        /// <param name="BookName"></param>
        /// <param name="BookType"></param>
        /// <param name="BookAuthor"></param>
        /// <param name="Publisher"></param>
        /// <param name="Price"></param>
        /// <param name="Summary"></param>
        /// <param name="BookCase"></param>
        /// <param name="Stock"></param>
        /// <param name="BookID"></param>
        /// <returns></returns>
        public bool UpdateBook(string ISBN,string BookName,string BookType,string BookAuthor,string Publisher,string Price,string Summary,string BookCase,string Stock,string BookID)
        {
            DBContext context = new DBContext();
            Book book = new Book();
            book.ISBN = ISBN;
            book.BookType = BookType;
            book.BookName = BookName;
            book.BookAuthor = BookAuthor;
            book.Publisher = Publisher;
            book.Price = Price;
            book.BookCase = BookCase;
            book.Stock = Stock;
            book.Summary = Summary;
            book.BookID = BookID;
            return context.UpdateData<Book>("Update_Book", book);
        }
        
        /// <summary>
        /// 根据图书ID查询图书信息
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        public List<Book> SelectBook(string BookID) 
        {
            DBContext context = new DBContext();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("BookID", BookID);
            return context.GetDataList<Book>("Select_Book_BookID", dictionary);
        }

        /// <summary>
        /// 学生借阅图书室，修改图书的库存和借阅数量
        /// </summary>
        /// <param name="ISBN"></param>
        /// <returns></returns>
        public bool UpdateBookUserBorrow(string ISBN) 
        {
            DBContext context = new DBContext();
            Book book = new Book();
            book.ISBN = ISBN;
            return context.UpdateData<Book>("Update_Book_UserBorrow",book);
        }

        /// <summary>
        /// 通过ISBN查询图书
        /// </summary>
        /// <param name="ISBN"></param>
        /// <returns></returns>
        public List<Book> SelectBookISBN(string ISBN)
        {  
            DBContext context = new DBContext();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("ISBN", ISBN);
            return context.GetDataList<Book>("Select_Book_ISBN", dictionary);
        }


        /// <summary>
        /// 归还图书，设置图书的现存量+1
        /// </summary>
        /// <param name="ISBN"></param>
        /// <returns></returns>
        public bool UpdateBookReturn(string ISBN)
        {
            DBContext context = new DBContext();
            Book book = new Book();
            book.ISBN = ISBN;
            return context.UpdateData<Book>("Update_Book_Return", book);
        }

        /// <summary>
        /// 查询借阅前15的图书
        /// </summary>
        /// <returns></returns>
        public List<Book> SelectTop15Book() 
        {
            DBContext context = new DBContext();
            return context.GetDataList<Book>("Select_Top15_Book");
        }


        public int SelectBookLoanAll()
        {
            DBContext context = new DBContext();
            return Convert.ToInt32(context.GetScalar<Book>("Select_Book_BookLoan_All"));
        }

        /// <summary>
        /// 修改图书注销信息
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        public bool UpdateBookLogout(string BookID, string Logout) 
        {
            DBContext context = new DBContext();
            Book book = new Book();
            book.BookID = BookID;
            book.Logout = Logout;
            return context.UpdateData<Book>("Update_Book_Logout",book);
        }
    }
}
