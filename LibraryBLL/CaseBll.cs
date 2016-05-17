using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryDAL;
using LibraryModel;

namespace LibraryBLL
{
    public class CaseBll
    {
        /// <summary>
        /// 新增书架
        /// </summary>
        /// <param name="TypeName"></param>
        /// <param name="CreatTime"></param>
        /// <returns></returns>
        public bool AddCase(string CaseName, string CreatTime)
        {
            DBContext context = new DBContext();
            Case bc = new Case();
            bc.CaseName = CaseName;
            bc.CreatTime = CreatTime;
            return context.InsertData<Case>("Insert_Case", bc);
        }

        /// <summary>
        /// 查询所有书架
        /// </summary>
        /// <returns></returns>
        public List<Case> GetCase()
        {
            DBContext context = new DBContext();
            return context.GetDataList<Case>("Select_Case");
        }

        /// <summary>
        /// 删除刷机
        /// </summary>
        /// <param name="BookTypeID"></param>
        /// <returns></returns>
        public bool DeleteCase(string CaseID)
        {
            DBContext context = new DBContext();
            Case bc = new Case();
            bc.CaseID = CaseID;
            return context.DeleteData<Case>("Delete_Case", bc);
        }
    }
}
