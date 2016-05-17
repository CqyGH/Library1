var UserID = "";

function SelectReader() {
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

    alert(UserID);

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
                var _StudentID = "";
                var _UserName = "";
                var _Phone = "";
                var _Sex = "";
                var _Department = "";
                var _Profession = "";
                var _Class = "";
                var _Birthday = "";
                var _Remark = "";
                for (var i = 0; i < lists.length; i++) {
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

                    if (lists[i]["Phone"]) {
                        _Phone = lists[i]["Phone"];
                    }
                    else {
                        _Phone = "";
                    }

                    if (lists[i]["Sex"]) {
                        _Sex = lists[i]["Sex"];
                    }
                    else {
                        _Sex = "";
                    }

                    if (lists[i]["Department"]) {
                        _Department = lists[i]["Department"];
                    }
                    else {
                        _Department = "";
                    }

                    if (lists[i]["Profession"]) {
                        _Profession = lists[i]["Profession"];
                    }
                    else {
                        _Profession = "";
                    }

                    if (lists[i]["Class"]) {
                        _Class = lists[i]["Class"];
                    }
                    else {
                        _Class = "";
                    }

                    if (lists[i]["Birthday"]) {
                        _Birthday = lists[i]["Birthday"];
                    }
                    else {
                        _Birthday = "";
                    }

                    if (lists[i]["Remark"]) {
                        _Remark = lists[i]["Remark"];
                    }
                    else {
                        _Remark = "";
                    }

                    $("#StudentID").attr("value", _StudentID); //填充内容 
                    $("#UserName").attr("value", _UserName);
                    $("#Phone").attr("value", _Phone);
                    $("#Department").attr("value", _Department);
                    $("#Sex").attr("value", _Sex);
                    $("#Class").attr("value", _Class);
                    $("#Profession").attr("value", _Profession);
                    $("#Birthday").attr("value", _Birthday);
                    $("#Remark").attr("value", _Remark);

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

function UpdateReader() {
    var StudentID = $.trim($("#StudentID").val());
    var UserName = $.trim($("#UserName").val());
    var Phone = $.trim($("#Phone").val());
    var Department = $.trim($("#Department").val());
    var Profession = $.trim($("#Profession").val());
    var Birthday = $.trim($("#Birthday").val());
    var Remark = $.trim($("#Remark").val());
    var Class = $("#Class").find("option:selected").text();
    var Sex = $("#Sex").find("option:selected").text();

    if (StudentID == "" || UserName == "" || Department == "" || Profession == "") {
        alert("请填写完整信息");
        return false;
    }
    else {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '../../Handlers/UpdateUser.ashx',
            data: {
                StudentID: StudentID,
                UserName: UserName,
                Phone: Phone,
                Department: Department,
                Profession: Profession,
                Birthday: Birthday,
                Remark: Remark,
                Class: Class,
                Sex: Sex,
                UserID: UserID
            },
            success: function (json) {
                if (json.MessageCode == '0') {
                    alert("修改成功");
                    window.location.href = "ReaderList.htm";
                }
                else {
                    alert("修改失败，系统错误");
                }
            },
            error: function () {
                alert("请检查网络");
            }
        })
    }
}

function exit() {
    if (confirm("是否确定取消修改读者？")) {
        window.location.href = "ReaderList.htm";
    }
}