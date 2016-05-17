function readerUserInfo() {
var judge=0;
$.ajax({
    type: 'POST',
    dataType: 'json',
    data: {
        judge: judge
    },
    url: '../../Handlers/SelectUser.ashx',
    cache: false,
    success: function (json) {
        if (json.MessageCode == '0') {
            var lists = json.list;
            var Name = "";
            for (var i = 0; i < lists.length; i++) {
                if (lists[i]["UserName"]) {
                    Name = lists[i]["UserName"];
                }
                else {
                    Name = "";
                }
                $("#UserName").html(Name);

            }
        }
        else {
            alert(json.MessageResult);
            window.parent.location.href = "../../Login.htm";

        }

    },
    error: function () {
        alert('加载失败请检查您的网络!');
    }
});
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
            window.location.href = '../../Login.htm';
        },
        error: function () {
            alert('加载失败请检查您的网络!');
        }
    });
}


function GetUserID() {
    var userID = $("#StudentID").text();
    return userID;
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
            url: '../../Handlers/SelectUser.ashx',
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
