using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryBLL;
using LibraryModel;
using LibraryModel.Message;
using System.Security.Cryptography;
using System.Text;

namespace Library.Handlers
{
    /// <summary>
    /// UpdateManagementPWD 的摘要说明
    /// </summary>
    public class UpdateManagementPWD : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string PWD = context.Request["PWD"];
            string ManagementID = context.Request["ManagementID"];
            if (string.IsNullOrEmpty(ManagementID))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }
            else
            {
                string adminPWD = "";
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                string temp = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(PWD)), 4, 8);
                temp = temp.Replace("-", "");
                adminPWD = temp;

                ManagementBll MB = new ManagementBll();
                if (MB.UpdateManagementPWD(adminPWD, ManagementID))
                {
                    context.Response.Write(MessageHelper.UdfMsg("0", "修改成功"));
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