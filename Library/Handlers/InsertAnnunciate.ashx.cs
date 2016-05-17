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
    /// InsertAnnunciate 的摘要说明
    /// </summary>
    public class InsertAnnunciate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
          string Information = context.Request["Information"];
          string Title = context.Request["Title"];
            string PublishTime=System.DateTime.Now.ToString();
            string Display = "1";

            AnnunciateBll AB = new AnnunciateBll();
            AnnunciateMessage m = new AnnunciateMessage();
            if (AB.InsertAnnunciate(Information, PublishTime, Display, Title))
            {
                context.Response.Write(MessageHelper.UdfMsg("0", "新增成功"));
            }
            else
            {
                context.Response.Write(MessageHelper.UdfMsg("1", "新增失败，未知错误"));
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