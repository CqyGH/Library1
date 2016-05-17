using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryDAL;
using LibraryModel;

namespace LibraryBLL
{
    public class BookTypeBll
    {
        /// <summary>
        /// 新增图书类型
        /// </summary>
        /// <param name="TypeName"></param>
        /// <param name="CreatTime"></param>
        /// <returns></returns>
        public bool AddBookType(string TypeName,string CreatTime) 
        {
            DBContext context = new DBContext();
            BookType bookType = new BookType();
            bookType.TypeName = TypeName;
            bookType.CreatTime = CreatTime;
            return context.InsertData<BookType>("Insert_BookType", bookType);
        }

        /// <summary>
        /// 查询所有图书类型
        /// </summary>
        /// <returns></returns>
        public List<BookType> GetBookType()
        {
            DBContext context = new DBContext();
            return context.GetDataList<BookType>("Select_BookType");
        }

        /// <summary>
        /// 删除图书类型
        /// </summary>
        /// <param name="BookTypeID"></param>
        /// <returns></returns>
        public bool DeleteBookType(string BookTypeID)
        {
            DBContext context = new DBContext();
            BookType bookType = new BookType();
            bookType.BookTypeID = BookTypeID;
            return context.DeleteData<BookType>("Delete_BookType", bookType);
        }
    }
}
