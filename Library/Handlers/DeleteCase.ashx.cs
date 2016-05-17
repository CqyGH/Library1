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
    /// DeleteCase 的摘要说明
    /// </summary>
    public class DeleteCase : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string CaseID = context.Request["CaseID"];
            if (string.IsNullOrEmpty(CaseID))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }
            else
            {
                CaseBll CB = new CaseBll();
                if (CB.DeleteCase(CaseID))
                {
                    context.Response.Write(MessageHelper.UdfMsg("0", "删除成功"));
                }
                else
                {
                    context.Response.Write(MessageHelper.UdfMsg("1", "删除失败，未知错误"));
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