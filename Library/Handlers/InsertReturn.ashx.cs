using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryModel;
using LibraryModel.Message;
using LibraryBLL;

namespace Library.Handlers
{
    /// <summary>
    /// InsertReturn 的摘要说明
    /// </summary>
    public class InsertReturn : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string BorrowStatus="1";
            string BorrowID = context.Request["BorrowID"];
            string StudentID = context.Request["StudentID"];
            string UserName = context.Request["UserName"];
            string BookName = context.Request["BookName"];
            string ReturnTime = System.DateTime.Now.ToString();
            string Confirm = "1";
            string ISBN = context.Request["ISBN"];
            ReturnBll RB=new ReturnBll();
            BookBll BB = new BookBll();
            if (BB.UpdateBookReturn(ISBN))
            {
                BorrowBll BorrowB = new BorrowBll();
                if (BorrowB.UpdateBorrowReturn(BorrowStatus, BorrowID))
                {
                    if (RB.InsertReturn(StudentID, UserName, BookName, ReturnTime, Confirm, ISBN))
                    {
                        context.Response.Write(MessageHelper.UdfMsg("0", "增加成功"));
                    }
                    else
                    {
                        context.Response.Write(MessageHelper.UdfMsg("1", "增加失败，未知错误"));
                    }
                }
                else
                {
                    context.Response.Write(MessageHelper.UdfMsg("1", "增加失败，未知错误"));
                }
            }
            else
            {
                context.Response.Write(MessageHelper.UdfMsg("1", "增加失败，未知错误"));
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