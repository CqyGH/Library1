function SkipAddReader() {
    if (confirm("是否确定添加新读者？")) {
        window.location.href = "AddReader.htm";
    }
}

function SkipAlterPWD() {
    if (confirm("是否确定修改密码？")) {
        var UserID = "";
        $(":checkbox[name='CheckUserID'][checked]").each(function () {
            UserID = $(this).val();
            alert(UserID);
        });
        if (UserID == "") {
            alert("请选择学生");
            return false;
        }
        else {
            window.location.href = "AlertPWD.htm?UserID=" + UserID ;
        }
    }
}

function SkipUpdateReader() {
    if (confirm("是否确定修改读者信息？")) {
        var UserID = "";
        $(":checkbox[name='CheckUserID'][checked]").each(function () {
            UserID = $(this).val();
            alert(UserID);
        });
        if (UserID == "") {
            alert("请选择学生");
            return false;
        }
        else {
            window.location.href = "UpdateReader.htm?UserID=" + UserID;
        }
    }
}

function SkipReaderSpecifics() { 
if (confirm("是否确定查询读者信息？")) {
        var UserID = "";
        $(":checkbox[name='CheckUserID'][checked]").each(function () {
            UserID = $(this).val();
            alert(UserID);
        });
        if (UserID == "") {
            alert("请选择学生");
            return false;
        }
        else {
            window.location.href = "Reader.htm?UserID=" + UserID;
        }
    }
}



function createHtml(json) {
    $("#ReaderList").html("");

    var lists = json.list;

    var optionHtml = "";
    var _UserID = "";
    var _StudentID = "";
    var _UserName = "";
    var _Sex = "";
    var _Department = "";
    var _Profession = "";
    var _Class = "";
    var _ValidityTime = "";
    var _Loss = "";
    optionHtml += ['<thead><tr><th><input name="" type="checkbox" value="" /></th><th>学号<i class="sort"><img src="images/px.gif" /></i></th><th>姓名</th><th>性别</th><th>系部</th><th>专业</th><th>班级</th><th>是否有效</th><th>是否挂失</th><th>操作</th></tr></thead><tbody>'].join("");
    for (var i = 0; i < lists.length; i++) {
        if (lists[i]["UserID"]) {
            _UserID = lists[i]["UserID"];
        }
        else {
            _UserID = "";
        }
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
        if (lists[i]["ValidityTime"]) {
            _ValidityTime = lists[i]["ValidityTime"];
        }
        else {
            _ValidityTime = "";
        }
        if (lists[i]["Loss"]) {
            _Loss = lists[i]["Loss"];
        }
        else {
            _Loss = "";
        }
        optionHtml += [
         '<tr><td><input name="CheckUserID" id="CheckUserID" type="checkbox" value="'+_UserID+'" /></td>',
            '<td id="StudentID">'+_StudentID+'</td>',
            '<td>'+_UserName+'</td>',
            '<td>'+_Sex+'</td>',
            '<td>'+_Department+'</td>',
            '<td>'+_Profession+'</td>',
            '<td>'+_Class+'</td>',
            '<td>'+_ValidityTime+'</td>',
            '<td>'+_Loss+'</td>',
            '<td><a href="#" class="tablelink" onclick="SkipReaderSpecifics()">查看</a></td></tr> '
            ].join('');
     }
      optionHtml += [' </tbody> '].join('');
       $("#ReaderList").append(optionHtml);
   }



   function createPage(json) {
       $("#pageList").html("");
       var lists = json.list;

       var MaxNum = json.count; //数据总数
       var pageNum = parseInt(MaxNum - 1) / 10 + 1; //页码总数
       var optionHtml = "";
       optionHtml += ['<ul class="paginList">'].join('');
       for (var i = 1; i <= pageNum; i++) {
           optionHtml += ['<li class="paginItem"><a href="javascritt:;" onclick="Search(' + i + ', 10)">' + i + '</a> </li>'].join('');
       }
       optionHtml += [' </ul>'].join('');
       $("#pageList").append(optionHtml);
   }



function Search(index,allNum){
    var judge=$("#SelectName").find("option:selected").text();
//    alert(judge);
    var startIndex=index;
    var pageSize=allNum;
    var StudentID="";
    var UserName = "";

    if (judge == "请选择") {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '../../Handlers/SelectUser.ashx',
            data: {
                startIndex: startIndex,
                pageSize: pageSize,
                StudentID: StudentID,
                UserName: UserName
            },
            success: function (json) {
                if (json.MessageCode == '0') {
                    createPage(json);
                    createHtml(json);
                  
                }
                else {
                    alert("查询失败，系统错误");
                }
            },
            error: function () {
                alert("系统错误");
            }
        })
    }

    else if (judge == "姓名") {
        UserName = $.trim($("#SearchBox").val());
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '../../Handlers/SelectUser.ashx',
            data: {
                startIndex: startIndex,
                pageSize: pageSize,
                StudentID: StudentID,
                UserName: UserName
            },
            success: function (json) {
                if (json.MessageCode == '0') {
//                    createPage(json);
                    createHtml(json);
                   
                }
                else {
                    alert("查询失败，系统错误");
                }
            },
            error: function () {
                alert("系统错误");
            }
        })
    }

    else {
        StudentID = $.trim($("#SearchBox").val());
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '../../Handlers/SelectUser.ashx',
            data: {
                startIndex: startIndex,
                pageSize: pageSize,
                StudentID: StudentID,
                UserName: UserName
            },
            success: function (json) {
                if (json.MessageCode == '0') {
//                    createPage(json);
                    createHtml(json);
                  
                }
                else {
                    alert("查询失败，系统错误");
                }
            },
            error: function () {
                alert("系统错误");
            }
        })
    }
}


function deleteReader() {
    if (confirm("确定要删除该用户？")) {
        var UserID = "";
        $(":checkbox[name='CheckUserID'][checked]").each(function () {
            UserID = $(this).val();
//            alert(UserID);
        });

        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '../../Handlers/DeleteUser.ashx',
            data: {
                UserID: UserID
            },
            success: function (json) {
                if (json.MessageCode == '0') {
                    alert("删除成功");
                    Search(1, 10);
                }
                else {
                    alert("删除失败，系统错误");
                }
            },
            error: function () {
                alert("系统错误");
            }

        })
    }
}

function UpdateUserLoss() {
    var UserID = "";
    var Loss = "";
    $(":checkbox[name='CheckUserID'][checked]").each(function () {
        UserID = $(this).val();
        alert(UserID);
    });

    if (UserID != "") {
        var tbodyObj = document.getElementById('ReaderList');
        $("table :checkbox").each(function (key, value) {
            if ($(value).prop('checked')) {
                Loss = tbodyObj.rows[key].cells[8].innerHTML;
            }
        });
        alert(Loss);
        if (Loss == "False") {
            if (confirm("是否挂失该学生？")) {
                var lossNum = 1;
                LossAction(lossNum, UserID);
            }
            else {

            }
        }
        else {
            if (confirm("是否解除挂失？")) {
                var lossNum = 0;
                LossAction(lossNum, UserID);
            }
            else {

            }
        }
    }
    else {
        alert("请选择学生！");
    }
}

function LossAction(Loss, UserID) {
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/UpdateUserLoss.ashx',
        data: {
            Loss: Loss,
            UserID: UserID
        },
        success: function (json) {
            if (json.MessageCode == "0") {
                alert("操做完成");
                Search(1, 10);
            }
            else {
                alert("操作失败");
            }
        },
        error: function () {
            alert("操作失败");
        }
    })
}
