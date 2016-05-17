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
    /// UpdateAnnunciateDisplay 的摘要说明
    /// </summary>
    public class UpdateAnnunciateDisplay : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Display = context.Request["Display"];
            string AnnunciateID = context.Request["AnnunciateID"];
            if (string.IsNullOrEmpty(Display) || string.IsNullOrEmpty(AnnunciateID))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }
            else
            {
                AnnunciateBll AB = new AnnunciateBll();
                if (AB.UpdateAnnunciateDisplay(Display, AnnunciateID))
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