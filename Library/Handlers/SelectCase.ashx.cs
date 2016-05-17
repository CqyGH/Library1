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
    /// SelectCase 的摘要说明
    /// </summary>
    public class SelectCase : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            CaseBll CB = new CaseBll();
            CaseMessage m = new CaseMessage();
            try 
            {
                m.list = CB.GetCase();
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