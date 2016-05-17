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
    /// InsertBookType 的摘要说明
    /// </summary>
    public class InsertBookType : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string TypeName = context.Request["TypeName"];
            string CreatTime =System.DateTime.Now.ToString();

            BookTypeBll BB = new BookTypeBll();
            if (BB.AddBookType(TypeName, CreatTime))
            {
                CaseBll CB = new CaseBll();
                CB.AddCase(TypeName, CreatTime);
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