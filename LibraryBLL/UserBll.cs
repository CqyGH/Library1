using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryDAL;
using LibraryModel;

namespace LibraryBLL
{
   public class UserBll
    {
       /// <summary>
       /// 新增用户
       /// </summary>
       /// <param name="StudentID"></param>
       /// <param name="UserName"></param>
       /// <param name="Phone"></param>
       /// <param name="Sex"></param>
       /// <param name="Department"></param>
       /// <param name="Profession"></param>
       /// <param name="Class"></param>
       /// <param name="Birthday"></param>
       /// <param name="StartTime"></param>
       /// <param name="ValidityTime"></param>
       /// <param name="UserBorrow"></param>
       /// <param name="Loss"></param>
       /// <param name="UserPWD"></param>
       /// <param name="Remark"></param>
       /// <returns></returns>
       public bool InsertUser(string StudentID,string UserName,string Phone,string Sex,string Department,string Profession,string Class,string Birthday,string StartTime,string ValidityTime,string UserBorrow,string Loss,string UserPWD,string Remark)
       {
           DBContext context = new DBContext();
           User user = new User();
           user.StudentID = StudentID;
           user.UserName = UserName;
           user.Phone = Phone;
           user.Sex = Sex;
           user.Department = Department;
           user.Profession = Profession;
           user.Class = Class;
           user.Birthday = Birthday;
           user.StartTime = StartTime;
           user.ValidityTime = ValidityTime;
           user.UserBorrow = UserBorrow;
           user.Loss = Loss;
           user.UserPWD = UserPWD;
           user.Remark = Remark;
           return context.InsertData<User>("InsertUser",user);
           
       }
       /// <summary>
       /// 根据用户名和密码查询用户，用在用户登录
       /// </summary>
       /// <param name="StudentID"></param>
       /// <param name="UserPWD"></param>
       /// <returns></returns>
       public List<User> SelectUser(string StudentID, string UserPWD)
       {
           DBContext context = new DBContext();
           List<Parameter> parameters = new List<Parameter>();
           Dictionary<string, string> dictionary = new Dictionary<string, string>();
           dictionary.Add("StudentID", StudentID);
           dictionary.Add("UserPWD", UserPWD);
           parameters.Add(new Parameter() { ParameterName = "StudentID",Type="string" });
           parameters.Add(new Parameter() { ParameterName = "UserPWD", Type = "string" });
           string whereString = string.Empty;
           whereString = "AND StudentID=@StudentID AND UserPWD=@UserPWD";
           return context.GetDataList<User>("SelectUsers",dictionary,whereString,parameters);
       }

       /// <summary>
       /// 通过学号查询学生信息
       /// </summary>
       /// <param name="StudentID"></param>
       /// <returns></returns>
       public List<User> SelectUserStudentID(string StudentID) 
       {
           DBContext context = new DBContext();
           List<Parameter> parameters = new List<Parameter>();
           Dictionary<string, string> dictionary = new Dictionary<string, string>();
           dictionary.Add("StudentID", StudentID);
           parameters.Add(new Parameter() { ParameterName = "StudentID", Type = "string" });
           string whereString = string.Empty;
           whereString = "AND StudentID=@StudentID";
           return context.GetDataList<User>("SelectUsers", dictionary, whereString, parameters);
       }

       /// <summary>
       /// 分页查询用户，主要用于管理管搜索用户
       /// </summary>
       /// <param name="StudentID"></param>
       /// <param name="UserName"></param>
       /// <param name="startIndex"></param>
       /// <param name="pageSize"></param>
       /// <returns></returns>
       public List<User> SelectUserPage(string StudentID, string UserName,int startIndex,int pageSize)
       {
           List<Parameter> Parameters = new List<Parameter>();
           Dictionary<string, string> dictionary = new Dictionary<string, string>();
           string wherestring = string.Empty;
           DBContext context = new DBContext();
           if (!string.IsNullOrEmpty(StudentID))
           {
               dictionary.Add("StudentID", "%" + StudentID + "%");
               Parameters.Add(new Parameter() { ParameterName = "StudentID", Type = "string" });
               wherestring = "AND StudentID like @StudentID";
           }
           else if (!string.IsNullOrEmpty(UserName))
           {
               dictionary.Add("UserName", "%" + UserName + "%");
               Parameters.Add(new Parameter() { ParameterName = "UserName", Type = "string" });
               wherestring = "AND UserName like @UserName";
           }
           else
           {
               wherestring = "";
           }
           return context.GetDataListPager<User>("Select_User_Page", dictionary, (startIndex - 1) * pageSize, startIndex * pageSize, wherestring, Parameters);
       }



       public int SelectUserPageCount(string StudentID, string UserName)
       {
           List<Parameter> parameters = new List<Parameter>();
           Dictionary<string, string> dictionary = new Dictionary<string, string>();
           DBContext context = new DBContext();
           string whereString = string.Empty;
           if (!string.IsNullOrEmpty(StudentID))
           {
               dictionary.Add("StudentID", "%" + StudentID + "%");
               parameters.Add(new Parameter() { ParameterName = "StudentID", Type = "string" });
               whereString = "AND StudentID LIKE @StudentID";
           }
           else if (!string.IsNullOrEmpty(UserName))
           {
               dictionary.Add("UserName", "%" + UserName + "%");
               parameters.Add(new Parameter() { ParameterName = "UserName", Type = "string" });
               whereString = "AND UserName LIKE @UserName";
           }
           else
           {
               whereString = "";
           }
           return Convert.ToInt32(context.GetScalar<User>("Select_User_Page_Count", dictionary, whereString, parameters));
       }

       /// <summary>
       ///删除用户
       /// </summary>
       /// <param name="StudentID"></param>
       /// <returns></returns>
       public bool DeleteUser(string UserID)
       {
           DBContext context = new DBContext();
           Dictionary<string, string> dictionary = new Dictionary<string, string>();
           dictionary.Add("UserID", UserID);
           return context.DeleteData<User>("Delete_User", dictionary);
       }

       /// <summary>
       ///修改读者基本信息
       /// </summary>
       /// <param name="StudentID"></param>
       /// <param name="UserName"></param>
       /// <param name="Phone"></param>
       /// <param name="Sex"></param>
       /// <param name="Department"></param>
       /// <param name="Profession"></param>
       /// <param name="Class"></param>
       /// <param name="Birthday"></param>
       /// <param name="ValidityTime"></param>
       /// <param name="Remark"></param>
       /// <param name="UserID"></param>
       /// <returns></returns>
       public bool UpdateUser(string StudentID, string UserName, string Phone, string Sex, string Department, string Profession, string Class, string Birthday, string Remark, string UserID)
       {
           DBContext context = new DBContext();
           User user = new User();
           user.StudentID = StudentID;
           user.UserName = UserName;
           user.Phone = Phone;
           user.Sex = Sex;
           user.Department = Department;
           user.Profession = Profession;
           user.Class = Class;
           user.Birthday = Birthday;
           user.UserID = UserID;
           user.Remark = Remark;
           return context.UpdateData<User>("Update_User", user);
       }

       /// <summary>
       /// 修改读者密码
       /// </summary>
       /// <param name="UserPWD"></param>
       /// <param name="UserID"></param>
       /// <returns></returns>
       public bool UpdateUser(string UserPWD, string UserID) {
           DBContext context = new DBContext();
           Dictionary<string, string> dictionary = new Dictionary<string, string>();
           dictionary.Add("UserPWD", UserPWD);
           dictionary.Add("UserID", UserID);
           return context.UpdateData<User>("Update_User_PWD", dictionary);
       }

       /// <summary>
       /// 通过用户名和密码查询用户，用在用户更改密码的时候
       /// </summary>
       /// <param name="UserID"></param>
       /// <param name="UserPWD"></param>
       /// <returns></returns>
       public List<User> SelectUserPWD(string UserID, string UserPWD)
       {
           DBContext context = new DBContext();
           Dictionary<string, string> dictionary = new Dictionary<string, string>();
           dictionary.Add("UserPWD", UserPWD);
           dictionary.Add("UserID", UserID);
           return context.GetDataList<User>("Select_User_PWD", dictionary);
       
       }

       /// <summary>
       /// 通过用户id查询用户
       /// </summary>
       /// <param name="UserID"></param>
       /// <returns></returns>
       public List<User> SelectUser(string UserID)
       {
           DBContext context = new DBContext();
           Dictionary<string, string> dictionary = new Dictionary<string, string>();
           dictionary.Add("UserID", UserID);
           return context.GetDataList<User>("Select_User_UserID", dictionary);
       }

       /// <summary>
       /// 更新读者的借书数量
       /// </summary>
       /// <param name="StudentID"></param>
       /// <returns></returns>
       public bool UpdateUserBorrow(string StudentID) 
       {
           DBContext context = new DBContext();
           User user = new User();
           user.StudentID = StudentID;
           return context.UpdateData<User>("Update_User_Borrow", user);
       }

       public bool UpdateUserLoss(string Loss,string UserID)
       {
           DBContext context = new DBContext();
           User user = new User();
           user.Loss = Loss;
           user.UserID = UserID;
           return context.UpdateData<User>("Update_User_Loss", user);
       }
    }
}
