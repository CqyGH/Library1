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
    /// DeleteBook 的摘要说明
    /// </summary>
    public class DeleteBook : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string BookID=context.Request["BookID"];
            if (string.IsNullOrEmpty(BookID))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }
            else
            {
                BorrowBll BorrowB = new BorrowBll();
                int BorrwCount = BorrowB.SelectBooktBorrowStatusCount(BookID);
                if (BorrwCount > 0)
                {
                    context.Response.Write(MessageHelper.UdfMsg("2", "删除失败，该图书仍有借阅信息"));
                }

                else
                {

                    BookBll BB = new BookBll();
                    if (BB.DeleteBook(BookID))
                    {
                        context.Response.Write(MessageHelper.UdfMsg("0", "删除成功"));
                    }
                    else
                    {
                        context.Response.Write(MessageHelper.UdfMsg("1", "删除失败，未知错误"));
                    }
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