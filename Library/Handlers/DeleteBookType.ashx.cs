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
    /// DeleteBookType 的摘要说明
    /// </summary>
    public class DeleteBookType : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string BookTypeID = context.Request["BookTypeID"];
            if (string.IsNullOrEmpty(BookTypeID))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }
            else
            {
                BookTypeBll BB = new BookTypeBll();
                if (BB.DeleteBookType(BookTypeID))
                {
                   
                    context.Response.Write(MessageHelper.UdfMsg("0", "删除成功"));
                }
                else
                {
                    context.Response.Write(MessageHelper.UdfMsg("0", "删除失败，未知错误"));
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