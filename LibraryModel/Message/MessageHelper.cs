using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LibraryModel.Message
{
    public enum SysCode
    { 
        NeedParm=-1
    }
    public class MessageHelper
    {
        private static message message = new message();

        public static String GetMsg(SysCode syscode)
        {
            switch (syscode)
            {
                case SysCode.NeedParm:
                    message.MessageCode = "-1";
                    message.MessageResult = "缺少参数";
                    break;
            }
            return JsonConvert.SerializeObject(message);
        }

        public static string GetSuccessMsg(message message)
        {
            message.MessageCode = "0";
            message.MessageResult = "查询成功";
            return JsonConvert.SerializeObject(message);
        }

        public static string UdfMsg(string code, string msg)
        {
            message.MessageCode = code;
            message.MessageResult = msg;
            return JsonConvert.SerializeObject(message);
        }
    }
}
