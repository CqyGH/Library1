using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryModel;
using LibraryModel.Message;
using LibraryBLL;
using System.Security.Cryptography;
using System.Text;

namespace Library.Handlers
{
    /// <summary>
    /// UpdateUser 的摘要说明
    /// </summary>
    public class UpdateUser : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string StudentID = context.Request["StudentID"];
            string UserName = context.Request["UserName"];
            string Phone = context.Request["Phone"];
            string Sex = context.Request["Sex"];
            string Department = context.Request["Department"];
            string Profession = context.Request["Profession"];
            string Class = context.Request["Class"];
            string Birthday = context.Request["Birthday"];
            string UserID = context.Request["UserID"];
            string Remark = context.Request["Remark"];
            string UserPWD = context.Request["UserPWD"];
            UserBll UB = new UserBll();

            if (string.IsNullOrEmpty(UserPWD))
            {
                if (string.IsNullOrEmpty(Remark))
                {
                    Remark = "";
                }
                if (string.IsNullOrEmpty(Birthday))
                {
                    Birthday = "1910-01-01";
                }
              
                if (UB.UpdateUser(StudentID, UserName, Phone, Sex, Department, Profession, Class, Birthday, Remark, UserID))
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
                string adminPWD = "";
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                string temp = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(UserPWD)), 4, 8);
                temp = temp.Replace("-", "");
                adminPWD = temp;

                if (UB.UpdateUser(adminPWD, UserID))
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