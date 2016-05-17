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
    /// UpdateManagement 的摘要说明
    /// </summary>
    public class UpdateManagement : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string ManagementID = context.Request["ManagementID"];
            string ManagementName = context.Request["ManagementName"];
            string Sex = context.Request["Sex"];
            string Education = context.Request["Education"];
            string Birthday = context.Request["Birthday"];
            string Phone = context.Request["Phone"];
            string ManagementAddress = context.Request["ManagementAddress"];
            string Remark = context.Request["Remark"];
            if (string.IsNullOrEmpty(ManagementName) || string.IsNullOrEmpty(ManagementID))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }
            else
            {
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
                if (MB.UpdateManageInfo(ManagementID, ManagementName, Sex, Education, Birthday, Phone, ManagementAddress, Remark))
                {
                    context.Response.Write(MessageHelper.UdfMsg("0", "更新成功"));
                }
                else
                {
                    context.Response.Write(MessageHelper.UdfMsg("1", "更新失败，未知错误"));
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