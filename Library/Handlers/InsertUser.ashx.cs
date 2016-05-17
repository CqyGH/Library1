using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryBLL;
using LibraryModel;
using LibraryModel.Message;
using System.Text;
using System.Security.Cryptography;

namespace Library.Handlers
{
    /// <summary>
    /// InsertUser 的摘要说明
    /// </summary>
    public class InsertUser : IHttpHandler
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
            string StartTime = DateTime.Now.ToString();
            string ValidityTime = DateTime.Now.AddYears(4).ToString();
            string UserBorrow = "0";
            string Loss = "false";
            string UserPWD = "123456";
            string Remark = context.Request["Remark"];

            if (string.IsNullOrEmpty(StudentID) || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Department) || string.IsNullOrEmpty(Profession) || string.IsNullOrEmpty(Class))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }

            else
            {
                string adminPWD = "";
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                string temp = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(UserPWD)), 4, 8);
                temp = temp.Replace("-", "");
                adminPWD = temp;


                if (string.IsNullOrEmpty(Sex))
                {
                    Sex = "";
                }
                if (string.IsNullOrEmpty(Phone))
                {
                    Phone = "";
                }
                if (string.IsNullOrEmpty(Birthday))
                {
                    Birthday = "";
                }
                if (string.IsNullOrEmpty(Remark))
                {
                    Remark = "";
                }

                UserBll MB = new UserBll();
                if (MB.InsertUser(StudentID, UserName, Phone, Sex, Department, Profession, Class, Birthday, StartTime, ValidityTime, UserBorrow, Loss, adminPWD, Remark))
                {
                    context.Response.Write(MessageHelper.UdfMsg("0", "新增成功"));
                }
                else
                {
                    context.Response.Write(MessageHelper.UdfMsg("1", "新增失败，未知错误"));
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