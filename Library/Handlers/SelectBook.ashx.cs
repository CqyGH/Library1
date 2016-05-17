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
    /// SelectBook 的摘要说明
    /// </summary>
    public class SelectBook : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string ISBN = context.Request["ISBN"];

            string BookID = context.Request["BookID"];
            int startIndex = Convert.ToInt32(context.Request["startIndex"]);
            int pageSize = Convert.ToInt32(context.Request["pageSize"]);
            string BookName = context.Request["BookName"];
            string BookAuthor = context.Request["BookAuthor"];
            BookBll BB = new BookBll();
            BookMessage m = new BookMessage();
            if (string.IsNullOrEmpty(BookID))
            {
                if (string.IsNullOrEmpty(ISBN))
                {
                    try
                    {
                        m.count = BB.SelectBookCount(BookName, BookAuthor);
                        m.list = BB.SelectAllBook(BookName, BookAuthor, startIndex, pageSize);
                        context.Response.Write(MessageHelper.GetSuccessMsg(m));
                    }
                    catch
                    {
                        context.Response.Write(MessageHelper.UdfMsg("1", "查询失败，未知错误"));
                    }
                }
                else
                {
                    try
                    {

                        m.list = BB.SelectBookISBN(ISBN);
                        context.Response.Write(MessageHelper.GetSuccessMsg(m));
                    }
                    catch
                    {
                        context.Response.Write(MessageHelper.UdfMsg("1", "查询失败，未知错误"));
                    }
                }
            }
            else
            {
                try
                {
                    m.list = BB.SelectBook(BookID);
                    context.Response.Write(MessageHelper.GetSuccessMsg(m));
                }
                catch
                {
                    context.Response.Write(MessageHelper.UdfMsg("1", "查询失败，未知错误"));
                }
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