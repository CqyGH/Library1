function Login() {
    var StudentID = $.trim($("#LoginName").val());
    var UserPWD = $.trim($("#LoginPWD").val());
    var role = $('input:radio:checked').val();
      if (StudentID == "") {
        alert("请输入用户名");
        return false;
    }
    if (UserPWD == "") {
        alert("请输入密码");
        return false;
    }
  
    if (role == 0) {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "Handlers/SelectUser.ashx",
            data: {
                StudentID: StudentID,
                UserPWD: UserPWD
            },
            success: function (json) {
                if (json.MessageCode == "0" && json.list != null) {
                    UserLoss(json);
//                   
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

    else if(role==1){
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "Handlers/SelectManage.ashx",
            data: {
                StudentID: StudentID,
                UserPWD: UserPWD
            },
            success: function (json) {
                if (json.MessageCode == "0" && json.list != null) {
                    window.location.href = "../管理员模块/main.html";
                  

                } /// <reference path="../管理员模块/main.html" />

                else {
                    alert("登录失败，用户名或密码不正确");
                }
            },
            error: function () {
                alert("系统错误");
            }
        })
    }
}

function UserLoss(json) {
    var loss = "";
    var lists = json.list;
    for (var i = 0; i < lists.length; i++) {
        if (lists[i]["Loss"]) {
            loss = lists[i]["Loss"];
        }
        else {
            loss = "";
        }
    }
//    alert(loss);
    if (loss == "True") {
        alert("该用户已挂失，无法登陆，请联系管理员！");
        return false;
    }
    else {
        window.location.href = "../学生模块/main.html";
    }
}


function exit() {
    if(confirm("确定要离开当前页面吗？")){
        window.location = "index.html";
    }
    else{
    
    }

}


