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
    /// DeleteManagement 的摘要说明
    /// </summary>
    public class DeleteManagement : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string ManagementID = context.Request["ManagementID"];
            if (string.IsNullOrEmpty(ManagementID))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }
            else
            {
                ManagementBll MB = new ManagementBll();
                if (MB.DeleteManage(ManagementID))
                {
                    context.Response.Write(MessageHelper.UdfMsg("0", "删除成功"));
                }
                else
                {
                    context.Response.Write(MessageHelper.UdfMsg("1", "删除失败，未知错误"));
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