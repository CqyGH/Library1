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
    /// UpdateAnnunciate 的摘要说明
    /// </summary>
    public class UpdateAnnunciate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string AnnunciateID = context.Request["AnnunciateID"];
            string Information = context.Request["Information"];
            string Display = context.Request["Display"];
            string Title = context.Request["Title"];
            
            if (string.IsNullOrEmpty(Information))
            {
                Information = "";
            }
            if (string.IsNullOrEmpty(Display))
            {
                Display = "";
            }
            if (string.IsNullOrEmpty(Title))
            {
                Title = "";
            }

            AnnunciateBll AB = new AnnunciateBll();
            if (AB.UpdateAnnunciate(AnnunciateID, Information, Display, Title))
            {
                context.Response.Write(MessageHelper.UdfMsg("0", "修改成功"));
            }
            else
            {
                context.Response.Write(MessageHelper.UdfMsg("1", "修改失败，未知错误"));
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