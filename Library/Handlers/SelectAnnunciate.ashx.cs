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
    /// SelectAnnunciate 的摘要说明
    /// </summary>
    public class SelectAnnunciate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Display = context.Request["Display"];
            string Information = context.Request["Information"];
            string Title = context.Request["Title"];
            string AnnunciateID = context.Request["AnnunciateID"];
            int judge = Convert.ToInt32(context.Request["judge"]);


            AnnunciateBll AB = new AnnunciateBll();
            AnnunciateMessage m = new AnnunciateMessage();
            if (string.IsNullOrEmpty(Display))
            {
                Display = "";
            }
            if (string.IsNullOrEmpty(Information))
            {
                Information = "";
            }
            if (string.IsNullOrEmpty(Title))
            {
                Title = "";
            }
            if (string.IsNullOrEmpty(AnnunciateID))
            {
                AnnunciateID = "";
            }
            try
            {
                m.list = AB.SelectAnnunciate(Display, Information, Title, AnnunciateID, judge);
                m.count = AB.SelectAnnunciateCountJudge();
                context.Response.Write(MessageHelper.GetSuccessMsg(m));
            }
            catch
            {
                context.Response.Write(MessageHelper.UdfMsg("1","查询失败，未知错误"));
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