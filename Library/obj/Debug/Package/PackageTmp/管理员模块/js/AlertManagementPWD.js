
function ReadUserInfo() {
    var ManagementID;
    var judge = 0;
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/SelectManage.ashx',
        data: {
            judge: judge
        },
        async: false,
        success: function (json) {
            if (json.MessageCode == '0') {
                var lists = json.list;

                for (var i = 0; i < lists.length; i++) {
                    if (lists[i]["ManagementID"]) {
                        ManagementID = lists[i]["ManagementID"];
                    }
                    else {
                        ManagementID = "";
                    }
                }
            }
            else {
                alert("查询失败，系统错误");
            }
        },
        error: function () {
            alert("系统错误");
        }
    })
    return ManagementID;
}


function AlertPWD() {
    var ManagementID = ReadUserInfo();
    alert(ManagementID);

    var NewPWD = $.trim($("#NewPWD").val());
    var NewPWD2 = $.trim($("#NewPWD2").val());
    if (NewPWD == "" || NewPWD2 == "") {
        alert("修改密码不可以为空");
        return false;
    }
    if (NewPWD == NewPWD2) {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '../../Handlers/UpdateManagementPWD.ashx',
            data: {
                PWD: NewPWD,
                ManagementID: ManagementID
            },
            async: false,
            success: function (json) {
                if (json.MessageCode == '0') {
                    alert("密码修改成功");
                    userLogout();
                }
                else {
                    alert("密码修改失败，系统错误");
                }
            },
            error: function () {
                alert("系统错误");
            }
        })
    }
    else {
        alert("两次密码输入不相同");
        return false;
    }
}

function userLogout() {

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
    })
}

function exit() {
    if (confirm("是否退出修改密码？")) {
        window.location.href = "index.html";
    }
}