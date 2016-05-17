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
    /// UpdateBorrowCost 的摘要说明
    /// </summary>
    public class UpdateBorrowCost : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Cost = context.Request["Cost"];
            string BorrowID = context.Request["BorrowID"];
            if (string.IsNullOrEmpty(BorrowID))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }
            else
            {
                BorrowBll BB = new BorrowBll();
                if (BB.UpdateBorrowCost(Cost, BorrowID))
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