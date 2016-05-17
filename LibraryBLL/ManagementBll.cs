using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryDAL;
using LibraryModel;

namespace LibraryBLL
{
    public class ManagementBll
    {
     /// <summary>
     /// 新增管理员账号
     /// </summary>
     /// <param name="ManagementName"></param>
     /// <param name="Sex"></param>
     /// <param name="Education"></param>
     /// <param name="Birthday"></param>
     /// <param name="Phone"></param>
     /// <param name="ManagementAddress"></param>
     /// <param name="PWD"></param>
     /// <param name="Remark"></param>
     /// <returns></returns>

        public bool InsertManagement(string ManagementName, string Sex, string Education, string Birthday, string Phone, string ManagementAddress, string PWD, string Remark, string ManagePermission)
        {
            DBContext context = new DBContext();
            Management management = new Management();
            management.ManagementName = ManagementName;
            management.Sex = Sex;
            management.Education = Education;
            management.Birthday = Birthday;
            management.Phone = Phone;
            management.ManagementAddress = ManagementAddress;
            management.PWD = PWD;
            management.Remark = Remark;
            management.ManagePermission = ManagePermission;
            return context.InsertData<Management>("InsertManagement", management);
        }

        /// <summary>
        /// 查询管理员信息，用户登录或者在页面上查询管理员
        /// </summary>
        /// <param name="ManagementName"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        public List<Management> SelectManage(string ManagementName, string PWD,string ManagementID)
        {
            DBContext context = new DBContext();
            List<Parameter> parameters = new List<Parameter>();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string whereString = string.Empty;
            if (string.IsNullOrEmpty(ManagementID))
            {
                dictionary.Add("ManagementName", ManagementName);
                dictionary.Add("PWD", PWD);
                parameters.Add(new Parameter() { ParameterName = "ManagementName", Type = "string" });
                parameters.Add(new Parameter() { ParameterName = "PWD", Type = "string" });
                whereString = "AND ManagementName=@ManagementName AND PWD=@PWD";
            }
            else
            {
                dictionary.Add("ManagementID", ManagementID);
                parameters.Add(new Parameter() { ParameterName = "ManagementID", Type = "string" });
                whereString = "AND ManagementID=@ManagementID";
            }
            return context.GetDataList<Management>("SelectManage", dictionary, whereString, parameters);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="PWD"></param>
        /// <param name="ManagementID"></param>
        /// <returns></returns>
        public bool UpdateManagementPWD(string PWD, string ManagementID)
        {
            DBContext context = new DBContext();
            Management m = new Management();
            m.PWD = PWD;
            m.ManagementID = ManagementID;
            return context.UpdateData<Management>("Update_Management_PWD",m);
        }

        /// <summary>
        /// 分页查询管理员信息
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Management> SelectManagementPage(int startIndex, int pageSize)
        {
            DBContext context = new DBContext();
            return context.GetDataListPager<Management>("SelectManegePage", startIndex * pageSize, (startIndex - 1) * pageSize);
        }

        public int SelectManagementPageCount()
        {
            DBContext context = new DBContext();
            return Convert.ToInt32(context.GetScalar<Management>("SelectManagePageCount"));
        }

        /// <summary>
        /// 根据管理员ID删除管理员
        /// </summary>
        /// <param name="ManagementID"></param>
        /// <returns></returns>
        public bool DeleteManage(string ManagementID)
        {
            DBContext context = new DBContext();
            Management manage = new Management();
            manage.ManagementID = ManagementID;
            return context.DeleteData<Management>("DeleteManage",manage);
        }

        /// <summary>
        /// 更新管理员账号信息
        /// </summary>
        /// <param name="ManagementID"></param>
        /// <param name="ManagementName"></param>
        /// <param name="Sex"></param>
        /// <param name="Education"></param>
        /// <param name="Birthday"></param>
        /// <param name="Phone"></param>
        /// <param name="ManagementAddress"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        public bool UpdateManageInfo(string ManagementID, string ManagementName, string Sex, string Education, string Birthday, string Phone, string ManagementAddress, string Remark)
        {
            DBContext context = new DBContext();
            Management manage = new Management();
            manage.ManagementID = ManagementID;
            manage.ManagementName = ManagementName;
            manage.Sex = Sex;
            manage.Education = Education;
            manage.Birthday = Birthday;
            manage.Phone = Phone;
            manage.ManagementAddress = ManagementAddress;
            manage.Remark = Remark;
            return context.UpdateData<Management>("UpdateManageInfo", manage);
        }
    }
}
