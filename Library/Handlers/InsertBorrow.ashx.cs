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
    /// InsertBorrow 的摘要说明
    /// </summary>
    public class InsertBorrow : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string StudentID = context.Request["StudentID"];
            string ISBN = context.Request["ISBN"];
            string BorrowTime = System.DateTime.Now.ToString();
            string ReturnTime = System.DateTime.Now.AddMonths(1).ToString();
            string Renew ="0";
            string BorrowStatus ="0";
            string Cost = "0";
            if(string.IsNullOrEmpty(StudentID) || string.IsNullOrEmpty(ISBN))
            {
                context.Response.Write(MessageHelper.GetMsg(SysCode.NeedParm));
            }
            else
            {
                int BorrowNum;
                BorrowBll BB = new BorrowBll();
                BorrowNum = BB.SelectStudentBorrow(StudentID);
                if (BorrowNum > 8)
                {
                    context.Response.Write(MessageHelper.UdfMsg("2", "新增失败，超出借阅数量"));
                }
                else
                {
                    UserBll UB = new UserBll();
                    if (UB.UpdateUserBorrow(StudentID))
                    {
                        BookBll BookB = new BookBll();
                        if (BookB.UpdateBookUserBorrow(ISBN))
                        {

                            if (BB.InsertBorrow(StudentID, ISBN, BorrowTime, ReturnTime, Renew, BorrowStatus,Cost))
                            {
                                context.Response.Write(MessageHelper.UdfMsg("0", "新增成功"));
                            }
                            else
                            {
                                context.Response.Write(MessageHelper.UdfMsg("1", "新增失败，未知错误"));
                            }
                        }
                        else
                        {
                            context.Response.Write(MessageHelper.UdfMsg("4", "新增失败，修改图书借阅信息失败"));
                        }
                    }
                    else
                    {
                        context.Response.Write(MessageHelper.UdfMsg("3", "新增失败，修改学生借阅数量失败"));
                    }
                    
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