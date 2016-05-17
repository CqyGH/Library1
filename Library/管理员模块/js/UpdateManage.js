﻿var ManagementID = "";

function exit() {
    if (confirm("是否退出修改管理员？")) {
        window.location.href = "ManagementList.htm";
    }
}

function SelectManage() {

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
        url: '../../Handlers/SelectManage.ashx',
        data: {
            ManagementID: ManagementID
        },
        success: function (json) {
            if (json.MessageCode == '0') {
                var lists = json.list;
                var ManagementName = "";
                var Sex = "";
                var Education = "";
                var Birthday = "";
                var Phone = "";
                var ManagementAddress = "";
                var Remark = "";

                for (var i = 0; i < lists.length; i++) {
                    if (lists[i]["ManagementName"]) {
                        ManagementName = lists[i]["ManagementName"];
                    }
                    else {
                        ManagementName = "";
                    }

                    if (lists[i]["Sex"]) {
                        Sex = lists[i]["Sex"];
                    }
                    else {
                        Sex = "";
                    }

                    if (lists[i]["Education"]) {
                        Education = lists[i]["Education"];
                    }
                    else {
                        Education = "";
                    }

                    if (lists[i]["Birthday"]) {
                        Birthday = lists[i]["Birthday"];
                    }
                    else {
                        Birthday = "";
                    }

                    if (lists[i]["Phone"]) {
                        Phone = lists[i]["Phone"];
                    }
                    else {
                        Phone = "";
                    }

                    if (lists[i]["ManagementAddress"]) {
                        ManagementAddress = lists[i]["ManagementAddress"];
                    }
                    else {
                        ManagementAddress = "";
                    }

                    if (lists[i]["Remark"]) {
                        Remark = lists[i]["Remark"];
                    }
                    else {
                        Remark = "";
                    }


                    $("#ManagementName").attr("value", ManagementName); //填充内容 
                    $("#Sex").attr("value", Sex);
                    $("#Education").attr("value", Education);
                    $("#Birthday").attr("value", Birthday);
                    $("#Phone").attr("value", Phone);
                    $("#ManagementAddress").attr("value", ManagementAddress);
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


function UpdateManage() {
    var ManagementName = $.trim($("#ManagementName").val());
    var Sex = $("#Sex").find("option:selected").text();
    var Education = $.trim($("#Education").val());
    var Birthday = $.trim($("#Birthday").val());
    var Phone = $.trim($("#Phone").val());
    var ManagementAddress = $.trim($("#ManagementAddress").val());
    var Remark = $.trim($("#Remark").val());
  
    if (ManagementName == "") {
        alert("请输入管理员名！");
        return false;
    }
    else {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '../../Handlers/UpdateManagement.ashx',
            data: {
                ManagementID: ManagementID,
                ManagementName: ManagementName,
                Sex: Sex,
                Education: Education,
                Birthday: Birthday,
                Phone: Phone,
                ManagementAddress: ManagementAddress,
                Remark: Remark
            },
            success: function (json) {
                if (json.MessageCode == '0') {
                    alert("修改成功!");

                }
                else {
                    alert("修改失败，请检查网络!");
                }
            },
            error: function () {
                alert("系统错误");
            }
        })
    }
}