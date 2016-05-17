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
    /// SelectAppointment 的摘要说明
    /// </summary>
    public class SelectAppointment : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string AppointmentID = context.Request["AppointmentID"];
            string StudentID=context.Request["StudentID"];
            string BookName=context.Request["BookName"];
            int startIndex = Convert.ToInt32(context.Request["startIndex"]);
            int pageSize = Convert.ToInt32(context.Request["pageSize"]);
            int AppointmentJudge = Convert.ToInt32(context.Request["AppointmentJudge"]);

            AppointmentBll AB = new AppointmentBll();
            AppointmentMessage m = new AppointmentMessage();
            if (AppointmentJudge != 0)
            {
                if (string.IsNullOrEmpty(AppointmentID))
                {
                    try
                    {
                        m.count = AB.SelectAppointmentCountReadOver(StudentID, BookName);
                        m.list = AB.SelectAppointmentNoReadOver(StudentID, BookName, startIndex, pageSize);
                        context.Response.Write(MessageHelper.GetSuccessMsg(m));
                    }
                    catch
                    {
                        context.Response.Write(MessageHelper.UdfMsg("1", "查询失败，未知错误"));
                    }
                }
                else
                {
                    try
                    {
                        m.list = AB.SelectAppointment(AppointmentID);
                        context.Response.Write(MessageHelper.GetSuccessMsg(m));
                    }
                    catch
                    {
                        context.Response.Write(MessageHelper.UdfMsg("1", "查询失败，未知错误"));
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(AppointmentID))
                {
                    try
                    {
                        m.count = AB.SelectAppointmentCount(StudentID, BookName);
                        m.list = AB.SelectAppointment(StudentID, BookName, startIndex, pageSize);
                        context.Response.Write(MessageHelper.GetSuccessMsg(m));
                    }
                    catch
                    {
                        context.Response.Write(MessageHelper.UdfMsg("1", "查询失败，未知错误"));
                    }
                }
                else
                {
                    try
                    {
                        m.list = AB.SelectAppointment(AppointmentID);
                        context.Response.Write(MessageHelper.GetSuccessMsg(m));
                    }
                    catch
                    {
                        context.Response.Write(MessageHelper.UdfMsg("1", "查询失败，未知错误"));
                    }
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