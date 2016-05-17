
var AppointmentID = "";
function SelectAppointment() {

    var tmpArr, QueryString;
    var URL = document.location.toString(); //获取带参URL
    if (URL.lastIndexOf("?") != -1) {
        QueryString = URL.substring(URL.lastIndexOf("?") + 1, URL.length);
        tmpArr = QueryString.split("&"); //分离参数
        for (i = 0; i <= tmpArr.length; i++) {
            try { eval(tmpArr[i]); }
            catch (e) {
                var re = new RegExp("(.*)=(.*)", "ig");
                re.exec(tmpArr[i]);
                try { eval(RegExp.$1 + "=" + "\"" + RegExp.$2 + "\""); }
                catch (e) { }
            }
        }
    }
    else {
        QueryString = "";
    }
    alert(AppointmentID);

    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/SelectAppointment.ashx',
        data: {
            AppointmentID: AppointmentID
        },
        success: function (json) {
            if (json.MessageCode == '0') {
                var lists = json.list;
                var _BookName = "";
                var _StudentID = "";
                var _UserName = "";
                var _AppointmentTime = "";
                var reply = "";
                for (var i = 0; i < lists.length; i++) {
                    if (lists[i]["BookName"]) {
                        _BookName = lists[i]["BookName"];
                    }
                    else {
                        _BookName = "";
                    }
                    if (lists[i]["StudentID"]) {
                        _StudentID = lists[i]["StudentID"];
                    }
                    else {
                        _StudentID = "";
                    }
                    if (lists[i]["UserName"]) {
                        _UserName = lists[i]["UserName"];
                    }
                    else {
                        _UserName = "";
                    }
                    if (lists[i]["AppointmentTime"]) {
                        _AppointmentTime = lists[i]["AppointmentTime"];
                    }
                    else {
                        _AppointmentTime = "";
                    }

                    if (lists[i]["reply"]) {
                        reply = lists[i]["reply"];
                    }
                    else {
                        reply = "";
                    }

                    $("#BookName").attr("value", _BookName); //填充内容 
                    $("#StudentID").attr("value", _StudentID);
                    $("#UserName").attr("value", _UserName);
                    $("#AppointmentTime").attr("value", _AppointmentTime);
                    $("#reply").attr("value", reply);
                }
            }
            else {
                alert("查询失败，系统错误");
            }
        },
        error: function () {/// <reference path="../../Handlers/UpdateBook.ashx" />

            alert("请检查网络");
        }


    })
}


function exit() {
    if (confirm("是否退出预约详情?")) {
        window.location.href = "AppointmentList.htm";
    }
}
