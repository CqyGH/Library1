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
    /// UpdateAppointment 的摘要说明
    /// </summary>
    public class UpdateAppointment : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string reply = context.Request["reply"];
            string AppointmentID = context.Request["AppointmentID"];
            string ReadOver="1";

            if (string.IsNullOrEmpty(AppointmentID))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }
            else
            { 
                AppointmentBll AB=new AppointmentBll();
                if (AB.UpdateAppointmentReply(reply, AppointmentID))
                {
                    if (AB.UpdateAppointmentReadOver(ReadOver, AppointmentID))
                    {
                        context.Response.Write(MessageHelper.UdfMsg("0", "修改成功"));
                    }
                    else
                    {
                        context.Response.Write(MessageHelper.UdfMsg("1", "修改失败，未知错误"));
                    }
                }
                else
                {
                    context.Response.Write(MessageHelper.UdfMsg("1", "修改失败，未知错误"));
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