using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryBLL;
using LibraryModel;
using LibraryModel.Message;

namespace Library.Handlers
{
    /// <summary>
    /// UpdateBookLogout 的摘要说明
    /// </summary>
    public class UpdateBookLogout : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string BookID = context.Request["BookID"];
            string Logout = context.Request["Logout"];
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
                    if (BB.UpdateBookLogout(BookID, Logout))
                    {
                        context.Response.Write(MessageHelper.UdfMsg("0", "修改成功"));
                    }
                    else
                    {
                        context.Response.Write(MessageHelper.UdfMsg("0", "修改失败，未知错误"));
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