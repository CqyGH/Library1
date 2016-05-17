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
    /// UpdateBorrowRenew 的摘要说明
    /// </summary>
    public class UpdateBorrowRenew : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string BorrowID = context.Request["BorrowID"];
           string ReturnTime = System.DateTime.Now.AddMonths(1).ToString();
           if (string.IsNullOrEmpty(BorrowID))
           {
               context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
           }
           else
           {
               BorrowBll bb = new BorrowBll();
               if (bb.UpdateBorrowRenew(BorrowID, ReturnTime))
               {
                   context.Response.Write(MessageHelper.UdfMsg("0", "续借成功"));
               }
               else
               {
                   context.Response.Write(MessageHelper.UdfMsg("0", "续借失败，未知错误"));
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