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
    /// DeleteAppointment 的摘要说明
    /// </summary>
    public class DeleteAppointment : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string AppointmentID = context.Request["AppointmentID"];
            if (string.IsNullOrEmpty(AppointmentID))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }
            else
            {
                AppointmentBll AB = new AppointmentBll();
                if (AB.DeleteAppointment(AppointmentID))
                {
                    context.Response.Write(MessageHelper.UdfMsg("0", "删除成功 "));
                }
                else
                {
                    context.Response.Write(MessageHelper.UdfMsg("1","删除失败，未知错误"));
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