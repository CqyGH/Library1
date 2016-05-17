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
    /// SelectTop15Book 的摘要说明
    /// </summary>
    public class SelectTop15Book : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            BookBll BB = new BookBll();
            BookMessage m = new BookMessage();
            try
            {
                m.list = BB.SelectTop15Book();
                m.borrowCount = BB.SelectBookLoanAll();
                context.Response.Write(MessageHelper.GetSuccessMsg(m));
            }
            catch
            {
                context.Response.Write(MessageHelper.UdfMsg("1","查询失败，未知错误"));
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