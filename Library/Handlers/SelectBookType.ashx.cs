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
    /// SelectBookType 的摘要说明
    /// </summary>
    public class SelectBookType : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            BookTypeBll BB = new BookTypeBll();
            BookTypeMessage m = new BookTypeMessage();
            try
            {
                m.list = BB.GetBookType();
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