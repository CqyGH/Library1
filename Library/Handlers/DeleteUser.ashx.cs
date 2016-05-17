using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryModel;
using LibraryBLL;
using LibraryModel.Message;

namespace Library.Handlers
{
    /// <summary>
    /// DeleteUser 的摘要说明
    /// </summary>
    public class DeleteUser : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string UserID = context.Request["UserID"];
            if (string.IsNullOrEmpty(UserID))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }
            else
            {
                UserBll UB = new UserBll();
                if (UB.DeleteUser(UserID))
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