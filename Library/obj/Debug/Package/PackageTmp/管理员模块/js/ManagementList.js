var ManagePermission;
function GetManagePermission() {
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
                    if (lists[i]["ManagePermission"]) {
                        ManagePermission = lists[i]["ManagePermission"];
                    }
                    else {
                        ManagePermission = "";
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
    return ManagePermission;
}

function SkipAddManage() {
    var ManagePermission = GetManagePermission();
    if (confirm("是否确定添加新管理员？")) { 
        if (ManagePermission != 1) {     
            alert("无此权限");
            return false;
        }
        else {
            window.location.href = "AddManage.htm";
        }      
    }
}

function SkipAlterPWD() {
    var ManagePermission = GetManagePermission();
    if (confirm("是否确定修改密码？")) {
        var ManagementID = "";
        $(":checkbox[name='CheckUserID'][checked]").each(function () {
            ManagementID = $(this).val();
         
        });
        if (ManagementID == "") {
            alert("请选择需要修改密码的管理员");
            return false;
        }
        else {
            readSession();
            if (ManagePermission != 1) {
                alert("无此权限");
                return false;
            }
            else {
                window.location.href = ".htm?ManagementID=" + ManagementID;
            }
            
        }
    }
}

function SkipUpdateManage() {
    var ManagePermission = GetManagePermission();
    if (confirm("是否确定修改管理员信息？")) {
        var ManagementID = "";
        $(":checkbox[name='CheckUserID'][checked]").each(function () {
            ManagementID = $(this).val();
            alert(ManagementID);
        });
        if (ManagementID == "") {
            alert("请选择管理员");
            return false;
        }
        else {
        
            if (ManagePermission != 1) {
                alert("无此权限");
                return false;
            }
            else {
                window.location.href = "UpdateManage.htm?ManagementID=" + ManagementID;
            }
        }
    }
}

function SkipManegeSpecifics() {
    if (confirm("是否确定查询管理员信息？")) {
        var ManagementID = "";
        $(":checkbox[name='CheckUserID'][checked]").each(function () {
            ManagementID = $(this).val();
            alert(ManagementID);
        });
        if (ManagementID == "") {
            alert("请选择管理员");
            return false;
        }
        else {
            window.location.href = "Manage.htm?ManagementID=" + ManagementID;
        }
    }
}


function createPage(json) {
    $("#pageList").html("");
    var lists = json.list;

    var MaxNum = json.count; //数据总数
    var pageNum = (MaxNum - 1) / 10 + 1; //页码总数
    var optionHtml = "";


    optionHtml += ['<ul class="paginList">'].join('');
    for (var i = 1; i <= pageNum; i++) {
        optionHtml += ['<li class="paginItem"><a href="javascritt:;" onclick="SearchBook(' + i + ', 10)">' + i + '</a> </li>'].join('');
    }
    optionHtml += [' </ul>'].join('');
    $("#pageList").append(optionHtml);
}

function createHtml(json) {
    $("#ReaderList").html("");

    var lists = json.list;

    var optionHtml = "";
    var ManagementID = "";
    var ManagementName = "";
    var Sex = "";
    var Education = "";
    var ManagementAddress = "";
    var Phone = "";
  
    optionHtml += ['<thead><tr><th><input name="" type="checkbox" value="" /></th><th>姓名<i class="sort"><img src="images/px.gif" /></i></th><th>性别</th><th>学历</th><th>住址</th><th>联系电话</th></tr></thead><tbody>'].join("");
    for (var i = 0; i < lists.length; i++) {
        if (lists[i]["ManagementID"]) {
            ManagementID = lists[i]["ManagementID"];
        }
        else {
            ManagementID = "";
        }
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

        if (lists[i]["ManagementAddress"]) {
            ManagementAddress = lists[i]["ManagementAddress"];
        }
        else {
            ManagementAddress = "";
        }

        if (lists[i]["Phone"]) {
            Phone = lists[i]["Phone"];
        }
        else {
            Phone = "";
        }
        optionHtml += [
         '<tr><td><input name="CheckUserID" id="CheckUserID" type="checkbox" value="' + ManagementID + '" /></td>',
            '<td id="StudentID">' + ManagementName + '</td>',
            '<td>' + Sex + '</td>',
            '<td>' + Education + '</td>',
            '<td>' + ManagementAddress + '</td>',
            '<td>' + Phone + '</td>'
            ].join('');
    }
    optionHtml += [' </tbody> '].join('');
    $("#ReaderList").append(optionHtml);
}

function Search(index, allNum) {
    var startIndex = index;
    var pageSize = allNum;
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '../../Handlers/SelectManagementPage.ashx',
            data: {
                startIndex: startIndex,
                pageSize: pageSize
            },
            success: function (json) {
                if (json.MessageCode == '0') {
                    createHtml(json);
                    createPage(json)
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


function deleteManage() {
    var ManagePermission = GetManagePermission();
    if (confirm("确定要删除该管理员？")) {
        var ManagementID = "";
        $(":checkbox[name='CheckUserID'][checked]").each(function () {
            ManagementID = $(this).val();
            alert(ManagementID);
        });

        if (ManagePermission != 1) {
            alert("无此权限");
            return false;
        }
        else {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                url: '../../Handlers/DeleteManagement.ashx',
                data: {
                    ManagementID: ManagementID
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
}


