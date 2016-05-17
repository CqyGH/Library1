using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryBLL;
using LibraryModel;
using LibraryModel.Message;
using System.Text;
using System.Security.Cryptography;
using System.Web.SessionState;
using Newtonsoft.Json;

namespace Library.Handlers
{
    /// <summary>
    /// SelectManage 的摘要说明
    /// </summary>
    public class SelectManage : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string ManagementName = context.Request["StudentID"];
            string PWD = context.Request["UserPWD"];
            int judge = Convert.ToInt32(context.Request["judge"]);
            string ManagementID = context.Request["ManagementID"];
            ManagementBll MB = new ManagementBll();
            ManageMessage m = new ManageMessage();
            if (!string.IsNullOrEmpty(ManagementID))
            {
                try
                {
                    m.list = MB.SelectManage(ManagementName, PWD, ManagementID);
                    context.Response.Write(MessageHelper.GetSuccessMsg(m));
                }
                catch
                {
                    context.Response.Write(MessageHelper.UdfMsg("1", "查询失败，未知错误"));
                }

            }
            else
            {
                ManagementID = "";
                if (!string.IsNullOrEmpty(ManagementName))
                {
                    try
                    {
                        string adminPWD = "";
                        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                        string temp = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(PWD)), 4, 8);
                        temp = temp.Replace("-", "");
                        adminPWD = temp;
                        m.list = MB.SelectManage(ManagementName, adminPWD, ManagementID);
                        var userInfo = MB.SelectManage(ManagementName, adminPWD, ManagementID);
                        context.Session["userInfo"] = userInfo;
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
                        m.list = (List<Management>)context.Session["userInfo"];
                        context.Response.Write(JsonConvert.SerializeObject(m));
                    }
                    else
                    {
                        context.Response.Write(MessageHelper.UdfMsg("1", "用户未登陆"));
                    }

                }
                else
                {
                    context.Session["userInfo"] = null;
                    context.Response.Write(MessageHelper.UdfMsg("2", "用户退出成功"));
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