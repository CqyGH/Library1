using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryDAL;
using LibraryModel;

namespace LibraryBLL
{
    public class AnnunciateBll
    {
        /// <summary>
        /// 新增通告
        /// </summary>
        /// <param name="Information"></param>
        /// <param name="PublishTime"></param>
        /// <param name="Dispaly"></param>
        /// <returns></returns>
        public bool InsertAnnunciate(string Information, string PublishTime, string Display, string Title) 
        {
            DBContext context = new DBContext();
            Annunciate annunciate = new Annunciate();
            annunciate.Information = Information;
            annunciate.PublishTime = PublishTime;
            annunciate.Display = Display;
            annunciate.Title = Title;
            return context.InsertData<Annunciate>("Insert_Annunciate", annunciate);
        
        }

        /// <summary>
        /// 查询通知
        /// </summary>
        /// <param name="Display"></param>
        /// <returns></returns>
        public List<Annunciate> SelectAnnunciate(string Display, string Information, string Title, string AnnunciateID, int judge)
        {
            DBContext context = new DBContext();
            List<Parameter> parameters = new List<Parameter>();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string whereString = string.Empty;

            if (Display != "")
            {
                dictionary.Add("Display", Display);
                parameters.Add(new Parameter(){ParameterName="Display",Type="string"});
                whereString="AND Display=@Display";
            }

            else if (Information != "")
            {
                dictionary.Add("Information", "%" + Information + "%");
                parameters.Add(new Parameter() { ParameterName = "Information", Type = "string" });
                whereString = "AND Information LIKE @Information";
            }
            else if (Title != "")
            {
                dictionary.Add("Title", "%" + Title + "%");
                parameters.Add(new Parameter() { ParameterName = "Title", Type = "string" });
                whereString = "AND Title LIKE @Title";
            }
            else if (AnnunciateID != "")
            {
                dictionary.Add("AnnunciateID", AnnunciateID);
                parameters.Add(new Parameter() { ParameterName = "AnnunciateID", Type = "string" });
                whereString = "AND AnnunciateID=@AnnunciateID";
            }
            else if (judge != 0)
            {
                whereString = "AND Display=1";
            }
            else
            {
                whereString = "";
            }
            return context.GetDataList<Annunciate>("Select_Annunciate",dictionary,whereString, parameters);
        }

        /// <summary>
        /// 返回总数
        /// </summary>
        /// <returns></returns>
        public int SelectAnnunciateCountJudge()
        {
            DBContext context = new DBContext();
            return Convert.ToInt32(context.GetScalar<Annunciate>("Select_Annunciate_Count_Judge"));
        }

        /// <summary>
        /// 修改通告
        /// </summary>
        /// <param name="AnnunciateID"></param>
        /// <param name="Information"></param>
        /// <param name="Display"></param>
        /// <returns></returns>
        public bool UpdateAnnunciate(string AnnunciateID, string Information, string Display, string Title)
        {
            DBContext context = new DBContext();
            Annunciate annunciate = new Annunciate();
            annunciate.AnnunciateID = AnnunciateID;
            if (Information != "")
            {
                annunciate.Title = Title;
                annunciate.Information = Information;
                return context.UpdateData<Annunciate>("Update_Annunciate_Information", annunciate);
            }
            else 
            {
                annunciate.Display = Display;
                return context.UpdateData<Annunciate>("Update_Annunciate_Display", annunciate);
            }
        }

        /// <summary>
        /// 删除通告信息
        /// </summary>
        /// <param name="AnnunciateID"></param>
        /// <returns></returns>
        public bool DeleteAnnunciate(string AnnunciateID)
        {
            DBContext context = new DBContext();
            Annunciate annunciate = new Annunciate();
            annunciate.AnnunciateID = AnnunciateID;
            return context.DeleteData<Annunciate>("Delete_Annunciate", annunciate);
        }

        /// <summary>
        /// 更新通告是否展示
        /// </summary>
        /// <param name="Display"></param>
        /// <param name="AnnunciateID"></param>
        /// <returns></returns>
        public bool UpdateAnnunciateDisplay(string Display,string AnnunciateID)
        {
            DBContext context = new DBContext();
            Annunciate annunciate = new Annunciate();
            annunciate.Display = Display;
            annunciate.AnnunciateID = AnnunciateID;
            return context.UpdateData<Annunciate>("Update_Annunciate_Display", annunciate);
        }
    }
}
