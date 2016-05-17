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
    /// SelectBorrow 的摘要说明
    /// </summary>
    public class SelectBorrow : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string StudentID = context.Request["StudentID"];
            if (string.IsNullOrEmpty(StudentID))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }
            else
            {
                BorrowMessage m = new BorrowMessage();
                BorrowBll BB = new BorrowBll();
                try
                {
                    m.list = BB.SelectBorrow(StudentID);
                    context.Response.Write(MessageHelper.GetSuccessMsg(m));
                }
                catch
                {
                    context.Response.Write(MessageHelper.UdfMsg("1","查询失败，未知错误"));
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