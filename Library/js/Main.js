function login() {
    var adminName = $.trim($("#userName").val());
    var adminPWD = $.trim($("#PWD").val());

    if (adminName == "") {
        alert("请输入用户名");
    }
    if (adminPWD == "") {
        alert("请输入密码");
    }

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "Handlers/SelectUser.ashx",
        data: {
            adminName: adminName,
            adminPWD: adminPWD
        },
        success: function (json) {
            if (json.ResultCode == "0") {
                alert("登录成功");
                SetCookie(logStyle, true);
                window.location.href = "ManageMain.htm";
            }
            else {
                alert("登录失败，用户名或密码不正确");
            }
        },
        error: function () {
            alert("系统错误");
        }
    })

   

  
}