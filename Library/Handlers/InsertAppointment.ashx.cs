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
    /// InsertAppointment 的摘要说明
    /// </summary>
    public class InsertAppointment : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string StudentID=context.Request["StudentID"];
            string AppointmentTime=System.DateTime.Now.ToString();
            string BookName = context.Request["BookName"];
            string ReadOver = "0";
            if (string.IsNullOrEmpty(StudentID) || string.IsNullOrEmpty(AppointmentTime))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }
            else
            {
                AppointmentBll AB = new AppointmentBll();
                if (AB.InsertAppointment(StudentID, AppointmentTime, BookName,ReadOver))
                {
                    context.Response.Write(MessageHelper.UdfMsg("0", "插入成功"));
                }
                else {
                    context.Response.Write(MessageHelper.UdfMsg("1","插入失败，未知错误"));
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