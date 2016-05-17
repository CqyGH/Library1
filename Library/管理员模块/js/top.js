var ManagementID = "";
var PWD = "";

function readerUserInfo() {
var judge=0;
$.ajax({
    type: 'POST',
    dataType: 'json',
    data: {
        judge: judge
    },
    url: '../../Handlers/SelectManage.ashx',
    cache: false,
    success: function (json) {
        if (json.MessageCode == '0') {
            var lists = json.list;
            var Name = "";
            for (var i = 0; i < lists.length; i++) {
                if (lists[i]["ManagementName"]) {
                    Name = lists[i]["ManagementName"];
                }
                else {
                    Name = "";
                }

                if (lists[i]["ManagementID"]) {
                    ManagementID = lists[i]["ManagementID"];
                }
                else {
                    ManagementID = "";
                }

                if (lists[i]["PWD"]) {
                    PWD = lists[i]["PWD"];
                }
                else {
                    PWD = "";
                }
//                alert(PWD);
//                alert(ManagementID);
            }
//            alert(lists);
//            alert(Name);
            $("#UserName").html(Name);
        }
        else {
            alert(json.MessageResult);
//            window.location.href = '';
            window.parent.location.href = "../../Login.htm";

        }

    },
    error: function () {
        alert('加载失败请检查您的网络!');
    }
});
}

function userLogout() {
    if (confirm("是否确定退出图书管理系统？")) {
        var judge = 1;
        $.ajax({
            type: 'POST',
            dataType: 'json',
            data: {
                judge: judge
            },
            url: '../../Handlers/SelectManage.ashx',
            cache: false,
            success: function (json) {
                if (json.MessageCode == '2') {
                    alert("退出成功");
                    window.parent.location.href = '../../Login.htm';

                }
                else {
                    alert("退出失败，系统错误");
                    return false;
                }
            },
            error: function () {
                alert('加载失败请检查您的网络!');
            }
        });
    }
    else {
        window.parent.location.href = "main.html";
    }
}


function Help() {
    alert("需要帮助请联系QQ：1183344265");
}

function AlertManagementPWD() {

    var OldPWD = $.trim($("#OldPWD").val());
    var UserPWD = $.trim($("#NewPWD").val());
    var UserPWD2 = $.trim($("#NewPWD2").val());
    var ManagementID =  ManagementID;
    var PWD = PWD;

    alert(PWD);
    alert(ManagementID);
    if (OldPWD != PWD) {
        alert("原密码输入错误，请重新输入!");
        return false;
    }
    else {
        if (UserPWD != UserPWD2) {
            alert("两次密码输入不相同，请重新输入");
        }
        else {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                data: {
                    ManagementID: ManagementID,
                    PWD: UserPWD
                },
                url: '../../Handlers/UpdateManagementPWD.ashx',
                success: function (json) {
                    if (json.MessageCode == '0') {
                        alert("修改成功");
                        //                    window.location.href = '../../Login.htm';
                    }
                    else {
                        alert("修改失败，系统错误");
                    }
                },
                error: function () {
                    alert('加载失败请检查您的网络!');
                }
            });
        }
    }


}



function Abount() {
    alert(" Copyright © 2016.陈清阳.毕业设计 All rights reserved");
}