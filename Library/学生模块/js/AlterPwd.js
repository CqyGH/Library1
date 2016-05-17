function AlterPWD() {
    var UserID = "";
    var UserPWD = $.trim($("#NewPWD").val());
    var UserPWD2 = $.trim($("#NewPWD2").val());
    var judge = 0;
    if (UserPWD == "" || UserPWD2 == "") {
        alert("新密码不能为空");
        return false;
    }
    $.ajax({
        type: 'POST',
        dataType: 'json',
        data: {
            judge: judge
        },
        url: '../../Handlers/SelectUser.ashx',
        async:false,
        cache: false,
        success: function (json) {
            if (json.MessageCode == '0') {
                var lists = json.list;
                for (var i = 0; i < lists.length; i++) {
                    if (lists[i]["UserID"]) {
//                        alert("我找到了！");
                        UserID = lists[i]["UserID"];
                    }
                    else {
//                        alert("我没找到！");
                        UserID = "";
                    }
                }
            }
            else {
//                alert("我查询失败");
                alert(json.MessageResult);
            }
        },
        error: function () {
//            alert("我联网失败");
            alert('加载失败请检查您的网络!');
        }
    })

    alert(UserID);

    if (UserPWD == UserPWD2) {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '../../Handlers/UpdateUser.ashx',
            data: {
                UserID: UserID,
                UserPWD: UserPWD
            },
            success: function (json) {
                if (json.MessageCode == '0') {
                    alert("修改成功");
                    userLogout();
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

    else {
        alert("两次密码输入不相同，请重新输入");
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
            url: '../../Handlers/SelectUser.ashx',
            cache: false,
            success: function (json) {
                if (json.MessageCode == '2') {
                    window.parent.location.href = '../../Login.htm';

                }
                else {
                    window.parent.location.href = '../../Login.htm';
                }
            },
            error: function () {
                window.parent.location.href = '../../Login.htm';
            }
        })
}



