using LibraryBLL;
using LibraryModel;
using LibraryModel.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Handlers
{
    /// <summary>
    /// UpdateBook 的摘要说明
    /// </summary>
    public class UpdateBook : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string ISBN = context.Request["ISBN"];
            string BookType = context.Request["BookType"];
            string BookName = context.Request["BookName"];
            string BookAuthor = context.Request["BookAuthor"];
            string Publisher = context.Request["Publisher"];
            string Price = context.Request["Price"];
            string BookCase = context.Request["BookCase"];
            string Stock = context.Request["Stock"];
            string Summary = context.Request["Summary"];
            string BookID = context.Request["BookID"];
            BookBll BB = new BookBll();
            if (BB.UpdateBook(ISBN, BookName, BookType, BookAuthor, Publisher, Price, Summary, BookCase, Stock, BookID))
            {
                context.Response.Write(MessageHelper.UdfMsg("0", "更新成功"));
            }
            else 
            {
                context.Response.Write(MessageHelper.UdfMsg("1", "更新失败，未知错误"));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}