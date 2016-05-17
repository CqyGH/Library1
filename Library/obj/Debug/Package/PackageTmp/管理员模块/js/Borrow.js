var BookID = "";

function SelectBook() {
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

    alert(BookID);

    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/SelectBook.ashx',
        data: {
            BookID: BookID
        },
        success: function (json) {
            if (json.MessageCode == '0') {
                var lists = json.list;
                var _ISBN = "";
                var _Extant = "";
                for (var i = 0; i < lists.length; i++) {
                    if (lists[i]["ISBN"]) {
                        _ISBN = lists[i]["ISBN"];
                    }
                    else {
                        _ISBN = "";
                    }
                    if (lists[i]["Extant"]) {
                        _Extant = lists[i]["Extant"];
                    }
                    else {
                        _Extant = "";
                    }

//                    alert(_Extant);
                    if (_Extant <= 0) {
                        alert("此书暂无库藏，无法借阅");
                        return false;
                    }

                    $("#ISBN").attr("value", _ISBN); //填充内容

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



function InsertBorrow() {
    var StudentID = $.trim($("#StudentID").val());
    var ISBN = $.trim($("#ISBN").val());
    alert(StudentID);
    //if (SelectStudent(StudentID)) {
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/InsertBorrow.ashx',
        data: {
            StudentID: StudentID,
            ISBN: ISBN
        },
        success: function (json) {
            if (json.MessageCode == '0') {
                alert("借阅成功");
                $("#StudentID").attr("value", "");
                $("#ISBN").attr("value", "");
            }
            else if (json.MessageCode == '2') {
                alert("借阅失败，超出借阅数量");
            }
            else {
                alert("借阅失败，系统错误");
            }
        },
        error: function () {
            alert("请检查网络");
        }
    })
//    }
//    else {
//        alert("该图书证号无效");
//    }

   // SelectStudent(StudentID);
}

function exit() {
    if (confirm("是否取消借阅图书？")) {
        window.location.href = "index.html";
    }
}

//用来判断学生的借书证是否有效
function SelectStudent(StudentID) {
    var StudentID = StudentID;
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/SelectUser.ashx',
        data: {
            StudentID: StudentID
        },
        success: function (json) {
            if (json.MessageCode == '0') {
                var mydate = new Date();
                var t = mydate.toLocaleString();
                var lists = json.list;
                var _ValidityTime = "";
                for (var i = 0; i < lists.length; i++) {
                    if (lists[i]["ValidityTime"]) {
                        _ValidityTime = lists[i]["ValidityTime"];
                    }
                    else {
                        _ValidityTime = "";
                    }
                }

                var studentTime = ""; //学生的借书有效时间
                var SysTime = ""; //当前系统时间
                var StudentTemp = _ValidityTime.split("");
                for (var i = 0; i < StudentTemp.length; i++) {
                    if (StudentTemp[i] >= 0 && StudentTemp[i] <= 9 && StudentTemp[i] != "") {
                        studentTime += StudentTemp[i];
                    }
                }
                studentTime = studentTime.replace(" ", "");
                var SysTemp = t.split("");
                for (var i = 0; i < SysTemp.length; i++) {

                    if (SysTemp[i] >= 0 && SysTemp[i] <= 9 && SysTemp[i] != "") {
                        SysTime += SysTemp[i];
                    }
                }
                SysTime = SysTime.replace(" ", "");
                alert(studentTime);
                alert(SysTime);
                if (studentTime > SysTime) {
                    return true;
                }
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

