function exit() {
    if (confirm("是否退出读者详情？")) {
        window.location.href = "index.html";
    }
}

function SelectReader() {
    var UserID = "";
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

    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/SelectUser.ashx',
        data: {
            UserID: UserID
        },
        success: function (json) {
            if (json.MessageCode == '0') {
            

                var lists = json.list;
                var StudentID = "";
                var UserName = "";
                var Phone = "";
                var Sex = "";
                var Department = "";
                var Class = "";
                var Profession = "";
                var Birthday = "";
                var StartTime = "";
                var ValidityTime = "";
                var UserBorrow = "";
                var Loss = "";
                var Remark = "";

                for (var i = 0; i < lists.length; i++) {
                    if (lists[i]["StudentID"]) {
                        StudentID = lists[i]["StudentID"];
                    }
                    else {
                        StudentID = "";
                    }

                    if (lists[i]["UserName"]) {
                        UserName = lists[i]["UserName"];
                    }
                    else {
                        UserName = "";
                    }

                    if (lists[i]["Phone"]) {
                        Phone = lists[i]["Phone"];
                    }
                    else {
                        Phone = "";
                    }

                    if (lists[i]["Sex"]) {
                        Sex = lists[i]["Sex"];
                    }
                    else {
                        Sex = "";
                    }

                    if (lists[i]["Department"]) {
                        Department = lists[i]["Department"];
                    }
                    else {
                        Department = "";
                    }

                    if (lists[i]["Class"]) {
                        Class = lists[i]["Class"];
                    }
                    else {
                        Class = "";
                    }

                    if (lists[i]["Profession"]) {
                        Profession = lists[i]["Profession"];
                    }
                    else {
                        Profession = "";
                    }

                    if (lists[i]["Birthday"]) {
                        Birthday = lists[i]["Birthday"];
                    }
                    else {
                        Birthday = "";
                    }

                    if (lists[i]["StartTime"]) {
                        StartTime = lists[i]["StartTime"];
                    }
                    else {
                        StartTime = "";
                    }

                    if (lists[i]["Loss"]) {
                        Loss = lists[i]["Loss"];
                    }
                    else {
                        Loss = "";
                    }

                    if (lists[i]["ValidityTime"]) {
                        ValidityTime = lists[i]["ValidityTime"];
                    }
                    else {
                        ValidityTime = "";
                    }

                    if (lists[i]["UserBorrow"]) {
                        UserBorrow = lists[i]["UserBorrow"];
                    }
                    else {
                        UserBorrow = "";
                    }

                    if (lists[i]["Remark"]) {
                        Remark = lists[i]["Remark"];
                    }
                    else {
                        Remark = "";
                    }
                  
//                    alert();
//                    alert();
//                    alert();
//                    alert();
//                    alert();
//                    alert();
//                    alert();
//                    alert();
//                    alert();
//                    alert();
//                    alert();
                    $("#StudentID").attr("value", StudentID); //填充内容 
                    $("#UserName").attr("value", UserName);
                    $("#Phone").attr("value", Phone);
                    $("#Sex").attr("value", Sex);
                    $("#Department").attr("value", Department);
                    $("#Class").attr("value", Class);
                    $("#Profession").attr("value", Profession);
                    $("#Birthday").attr("value", Birthday);
                    $("#StartTime").attr("value", StartTime);
                    $("#ValidityTime").attr("value", ValidityTime);
                    $("#UserBorrow").attr("value", UserBorrow);
                    $("#Loss").attr("value", Loss);
                    $("#Remark").attr("value", Remark);

                }
            }
            else {
                alert("查询失败，系统错误");

            }
        },
        error: function () {
            alert("请检查网络");
        }
    })
}
