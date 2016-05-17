function AlterPWD(){
    var UserID = "";
    var OldPWD = $.trim($("#OldPWD").val());
    var UserPWD = $.trim($("#NewPWD").val());
    var UserPWD2=$.trim($("#NewPWD2").val());
    if (UserPWD == UserPWD2) {
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
        /*var temp = QueryString.split("");
        for (var i = 0; i < temp.length; i++) {
        alert(temp[i]);
        if (temp[i] >= 0 && temp[i] <= 9) {
        UserID += temp[i];
        }-
        }*/

        
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/SelectUser.ashx',
        data: {
            UserID: UserID,
            UserPWD: OldPWD
        },
        success: function (json) {
            if (json.MessageCode == "0" && json.list != null) {
               alert(UserID);
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
            else {
                return false;
            }
        },
        error: function () {
            return false;
        }
    })


            
        }
       

    else {
        alert("两次密码输入不相同，请重新输入");
        return false;
    }

}

function Validate(UserID, OldPWD) {
    var UserID = UserID;
    var OldPWD = OldPWD;

}