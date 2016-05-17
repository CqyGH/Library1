using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryDAL;
using LibraryModel;

namespace LibraryBLL
{
    public class AppointmentBll
    {
        /// <summary>
        /// 增加预约
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="AppointmentTime"></param>
        /// <param name="BookName"></param>
        /// <param name="ReadOver"></param>
        /// <returns></returns>
        public bool InsertAppointment(string StudentID, string AppointmentTime, string BookName, string ReadOver)
        {
            DBContext context = new DBContext();
            Appointment appointment = new Appointment();
            appointment.StudentID = StudentID;
            appointment.AppointmentTime = AppointmentTime;
            appointment.BookName = BookName;
            appointment.ReadOver = ReadOver;
            return context.InsertData<Appointment>("Insert_Appointment", appointment);
        }

        /// <summary>
        /// 根据条件分页查询预约信息
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="BookName"></param>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Appointment> SelectAppointment(string StudentID, string BookName, int startIndex, int pageSize)
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
            else if (!string.IsNullOrEmpty(StudentID))
            {
                dictionary.Add("BookName", "%" + BookName + "%");
                parameters.Add(new Parameter() { ParameterName = "BookName", Type = "string" });
                whereString = "AND BookName LIKE @BookName";
            }
            else
            {
                whereString = "";
            }
            return context.GetDataListPager<Appointment>("Select_Appointment_Page", dictionary, (startIndex - 1) * pageSize, startIndex * pageSize, whereString, parameters);
        }


        public List<Appointment> SelectAppointmentNoReadOver(string StudentID, string BookName, int startIndex, int pageSize)
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
            else if (!string.IsNullOrEmpty(StudentID))
            {
                dictionary.Add("BookName", "%" + BookName + "%");
                parameters.Add(new Parameter() { ParameterName = "BookName", Type = "string" });
                whereString = "AND BookName LIKE @BookName";
            }
            else
            {
                whereString = "";
            }
            return context.GetDataListPager<Appointment>("Select_Appointment_Page_ReadOver", dictionary, (startIndex - 1) * pageSize, startIndex * pageSize, whereString, parameters);
        }


        /// <summary>
        /// 分页查询预约信息时的数据总数
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="BookName"></param>
        /// <returns></returns>
        public int SelectAppointmentCount(string StudentID, string BookName)
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
            else if (!string.IsNullOrEmpty(StudentID))
            {
                dictionary.Add("BookName", "%" + BookName + "%");
                parameters.Add(new Parameter() { ParameterName = "BookName", Type = "string" });
                whereString = "AND BookName LIKE @BookName";
            }
            else
            {
                whereString = "";
            }
            return Convert.ToInt32(context.GetScalar<Appointment>("Select_Appointment_Count", dictionary, whereString, parameters));
        }

        public int SelectAppointmentCountReadOver(string StudentID, string BookName)
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
            else if (!string.IsNullOrEmpty(StudentID))
            {
                dictionary.Add("BookName", "%" + BookName + "%");
                parameters.Add(new Parameter() { ParameterName = "BookName", Type = "string" });
                whereString = "AND BookName LIKE @BookName";
            }
            else
            {
                whereString = "";
            }
            return Convert.ToInt32(context.GetScalar<Appointment>("Select_Appointment_Count_ReadOver", dictionary, whereString, parameters));
        }

        /// <summary>
        /// 删除预约
        /// </summary>
        /// <param name="AppointmentID"></param>
        /// <returns></returns>
        public bool DeleteAppointment(string AppointmentID)
        {
            DBContext context = new DBContext();
            Appointment appointment = new Appointment();
            appointment.AppointmentID = AppointmentID;
            return context.DeleteData<Appointment>("Delete_Appointment", appointment);
        }

        /// <summary>
        /// 修改预约信息是否被查看
        /// </summary>
        /// <param name="ReadOver"></param>
        /// <param name="AppointmentID"></param>
        /// <returns></returns>
        public bool UpdateAppointmentReadOver(string ReadOver, string AppointmentID)
        {
            DBContext context = new DBContext();
            Appointment appointment = new Appointment();
            appointment.ReadOver = ReadOver;
            appointment.AppointmentID = AppointmentID;
            return context.UpdateData<Appointment>("Update_Appointment_ReadOver", appointment);
        }

        /// <summary>
        /// 修改预约信息管理员回复
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="AppointmentID"></param>
        /// <returns></returns>
        public bool UpdateAppointmentReply(string reply, string AppointmentID)
        {
            DBContext context = new DBContext();
            Appointment appointment = new Appointment();
            appointment.reply = reply;
            appointment.AppointmentID = AppointmentID;
            return context.UpdateData<Appointment>("Update_Appointment_Reply", appointment);
        }

        /// <summary>
        /// 根据AppointmentID查询预约信息
        /// </summary>
        /// <param name="AppointmentID"></param>
        /// <returns></returns>
        public List<Appointment> SelectAppointment(string AppointmentID) 
        {
            DBContext context = new DBContext();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("AppointmentID", AppointmentID);
            return context.GetDataList<Appointment>("Select_Appointment_AppointmentID", dictionary);
               
        }
    }
}
