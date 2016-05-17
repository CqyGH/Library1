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
    /// SelectManagementPage 的摘要说明
    /// </summary>
    public class SelectManagementPage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int startIndex = Convert.ToInt32(context.Request["startIndex"]);
            int pageSize = Convert.ToInt32(context.Request["pageSize"]);
            try
            {
                ManagementBll MB = new ManagementBll();
                ManageMessage m = new ManageMessage();
                m.list = MB.SelectManagementPage(startIndex, pageSize);
                m.count = MB.SelectManagementPageCount();
                context.Response.Write(MessageHelper.GetSuccessMsg(m));
            }
            catch
            {
                context.Response.Write(MessageHelper.UdfMsg("-1","查询失败，未知错误"));
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