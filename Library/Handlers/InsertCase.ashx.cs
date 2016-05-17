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
    /// InsertCase 的摘要说明
    /// </summary>
    public class InsertCase : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string CaseName = context.Request["CaseName"];
            string CreatTime = System.DateTime.Now.ToString();

            if (string.IsNullOrEmpty(CaseName))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }
            else
            {
                CaseBll CB = new CaseBll();
                if (CB.AddCase(CaseName, CreatTime))
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