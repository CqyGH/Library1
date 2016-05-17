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
    /// DeleteAnnunciate 的摘要说明
    /// </summary>
    public class DeleteAnnunciate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string AnnunciateID = context.Request["AnnunciateID"];
            AnnunciateBll AB = new AnnunciateBll();
            if (AB.DeleteAnnunciate(AnnunciateID))
            {
                context.Response.Write(MessageHelper.UdfMsg("0", "删除成功"));
            }
            else
            {
                context.Response.Write(MessageHelper.UdfMsg("1", "删除失败，未知错误"));
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