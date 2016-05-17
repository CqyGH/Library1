using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryModel.Message;
using LibraryModel;
using LibraryBLL;

namespace Library.Handlers
{
    /// <summary>
    /// InsertBook 的摘要说明
    /// </summary>
    public class InsertBook : IHttpHandler
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
            string Extant = Stock;
            string Appointment = "0";
            string BookLoan = "0";
            string Logout = "0";
            string Summary = context.Request["Summary"];
            string PublishTime = System.DateTime.Now.ToString();
            if (string.IsNullOrEmpty(Summary))
            {
                Summary = "";
            }
            BookBll BB = new BookBll();
            if (BB.InsertBook(ISBN, BookType, BookName, BookAuthor, Publisher, Price, BookCase, Extant, Stock, Appointment, BookLoan, Logout, Summary, PublishTime))
            {
                context.Response.Write(MessageHelper.UdfMsg("0", "插入成功"));
            }
            else
            {
                context.Response.Write(MessageHelper.UdfMsg("1", "插入失败，未知错误"));
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