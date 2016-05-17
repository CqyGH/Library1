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
    /// UpdateUserLoss 的摘要说明
    /// </summary>
    public class UpdateUserLoss : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Loss = context.Request["Loss"];
            string UserID = context.Request["UserID"];
            if (string.IsNullOrEmpty(UserID))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }
            else
            {
                UserBll UB = new UserBll();
                if (UB.UpdateUserLoss(Loss, UserID))
                {
                    context.Response.Write(MessageHelper.UdfMsg("0", "修改成功"));
                }
                else
                {
                    context.Response.Write(MessageHelper.UdfMsg("0", "修改失败，未知错误"));
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