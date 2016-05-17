using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryModel;
using LibraryModel.Message;
using LibraryBLL;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Library.Handlers
{
    /// <summary>
    /// InsertManagement 的摘要说明
    /// </summary>
    public class InsertManagement : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string ManagementName = context.Request["ManagementName"];
            string Sex = context.Request["Sex"];
            string Education = context.Request["Education"];
            string Birthday = context.Request["Birthday"];
            string Phone = context.Request["Phone"];
            string ManagementAddress = context.Request["ManagementAddress"];
            string PWD = "123456";
            string Remark = context.Request["Remark"];
            string ManagePermission = "2";

            if (string.IsNullOrEmpty(ManagementName) || string.IsNullOrEmpty(PWD))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }

            else
            {
                //string adminPWD = "";
                //MD5 md5 = MD5.Create();//实例化一个md5对像
                //// 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
                //byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(PWD));
                //// 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
                //for (int i = 0; i < s.Length; i++)
                //{
                //    // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符

                //    adminPWD = adminPWD + s[i].ToString("X");

                //}



                string adminPWD = "";
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                string temp = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(PWD)), 4, 8);
                temp = temp.Replace("-", "");
                adminPWD = temp;


                if (string.IsNullOrEmpty(Sex))
                {
                    Sex = "";
                }
                if (string.IsNullOrEmpty(Education))
                {
                    Education = "";
                }
                if (string.IsNullOrEmpty(Birthday))
                {
                    Birthday = "";
                }
                if (string.IsNullOrEmpty(Phone))
                {
                    Phone = "";
                }
                if (string.IsNullOrEmpty(ManagementAddress))
                {
                    ManagementAddress = "";
                }
                if (string.IsNullOrEmpty(Remark))
                {
                    Remark = "";
                }

                ManagementBll MB = new ManagementBll();
                if (MB.InsertManagement(ManagementName, Sex, Education, Birthday, Phone, ManagementAddress, adminPWD, Remark, ManagePermission))
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