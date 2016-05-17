using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryBLL;
using LibraryModel;
using LibraryModel.Message;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using System.Web.SessionState;

namespace Library.Handlers
{
    /// <summary>
    /// SelectUser 的摘要说明
    /// </summary>
    public class SelectUser : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string UserID = context.Request["UserID"];
            string StudentID = context.Request["StudentID"];
            string UserPWD = context.Request["UserPWD"];
            string UserName = context.Request["UserName"];
            int startIndex = Convert.ToInt32(context.Request["startIndex"]);
            int pageSize = Convert.ToInt32(context.Request["pageSize"]);

            int judge = Convert.ToInt32(context.Request["judge"]);
            UserBll UB = new UserBll();
            UserMessage m = new UserMessage();
            if (!string.IsNullOrEmpty(UserID))
            {
                if (!string.IsNullOrEmpty(UserPWD))
                {
                    try
                    {
                        string adminPWD = "";
                        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                        string temp = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(UserPWD)), 4, 8);
                        temp = temp.Replace("-", "");
                        adminPWD = temp;
                        m.list = UB.SelectUserPWD(UserID, adminPWD);
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
                        m.list = UB.SelectUser(UserID);
                        context.Response.Write(MessageHelper.GetSuccessMsg(m));
                    }
                    catch
                    {
                        context.Response.Write(MessageHelper.UdfMsg("1","查询失败，未知错误"));
                    }
                }
            }
            else 
            {
                if (startIndex == 0 || pageSize == 0)
                {
                    if (!string.IsNullOrEmpty(UserPWD))
                    {
                        try
                        {
                            string adminPWD = "";
                            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                            string temp = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(UserPWD)), 4, 8);
                            temp = temp.Replace("-", "");
                            adminPWD = temp;
                            var userInfo = UB.SelectUser(StudentID, adminPWD);
                            context.Session["userInfo"] = userInfo;
                            m.list = UB.SelectUser(StudentID, adminPWD);
                            context.Response.Write(MessageHelper.GetSuccessMsg(m));
                        }
                        catch
                        {
                            context.Response.Write(MessageHelper.UdfMsg("1", "查询失败，未知错误"));
                        }
                    }

                    else if (judge == 0) 
                    {
                        if (context.Session["userInfo"] != null)
                        {
                            m.MessageCode = "0";
                            m.MessageResult = "查询成功";
                            m.list = (List<User>)context.Session["userInfo"];
                            context.Response.Write(JsonConvert.SerializeObject(m));
                        }
                        else
                        {
                            context.Response.Write(MessageHelper.UdfMsg("1", "用户未登陆"));
                        }
                    }
                    else if (judge == 1)
                    {
                        context.Session["userInfo"] = null;
                        context.Response.Write(MessageHelper.UdfMsg("2", "用户退出成功"));
                    }

                    else
                    {
                        try
                        {
                            m.list = UB.SelectUserStudentID(StudentID);
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
                    try
                    {
                        m.list = UB.SelectUserPage( StudentID,  UserName, startIndex, pageSize);
                        m.count = UB.SelectUserPageCount(StudentID, UserName);
                        context.Response.Write(MessageHelper.GetSuccessMsg(m));
                    }
                    catch
                    {
                        context.Response.Write(MessageHelper.UdfMsg("1","查询失败，未知错误"));
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