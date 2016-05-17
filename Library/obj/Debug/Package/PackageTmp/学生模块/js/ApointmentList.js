function createPage(json) {
    $("#pageList").html("");
    var lists = json.list;

    var MaxNum = json.count; //数据总数
    var pageNum = (MaxNum - 1) / 10 + 1; //页码总数
    var optionHtml = "";

    optionHtml += ['<ul class="paginList">'].join('');
    for (var i = 1; i <= pageNum; i++) {
        optionHtml += ['<li class="paginItem"><a href="javascript:;" onclick="SearchAppointment(' + i + ', 10)"><span>' + i + '</span></a></li>'].join('');
    }
    optionHtml += [' </ul>'].join('');
    $("#pageList").append(optionHtml);
}

function createHtml(json) {

    $("#AppointmentList").html("");

    var lists = json.list;

    var optionHtml = "";
    var _AppointmentID = "";
    var _StudentID = "";
    var _UserName = "";
    var _AppointmentTime = "";
    var _BookName = "";

    optionHtml += ['<thead><tr><th><input name="" type="checkbox"value=""/></th><th>预约信息<i class="sort"><img src="images/px.gif" /><i></th><th>学号</th><th>姓名</th><th>预约时间</th></tr></thead><tbody>'].join('');
    for (var i = 0; i < lists.length; i++) {
        if (lists[i]["AppointmentID"])
            _AppointmentID = lists[i]["AppointmentID"];
        else
            _AppointmentID = "";

        if (lists[i]["StudentID"])
            _StudentID = lists[i]["StudentID"];
        else
            _StudentID = "";

        if (lists[i]["UserName"])
            _UserName = lists[i]["UserName"];
        else
            _UserName = "";

        if (lists[i]["AppointmentTime"])
            _AppointmentTime = lists[i]["AppointmentTime"];
        else
            _AppointmentTime = "";

        if (lists[i]["BookName"])
            _BookName = lists[i]["BookName"];
        else
            _BookName = "";


        optionHtml += [
                            '<tr><td><input name="checkAppointmentID" id="checkAppointmentID" type="checkbox" value="' + _AppointmentID + '" /></td>',
                            '<td>' + _BookName + '</td>',
                            '<td>' + _StudentID + '</td>',
                            '<td>' + _UserName + '</td>',
                            '<td>' + _AppointmentTime + '</td></tr>'
                             ].join('');
    }
    optionHtml += [' </tbody> '].join('');
    $("#AppointmentList").append(optionHtml);
}

function AppointmentShow(BookName, startIndex, pageSize) {

    var StudentID = "";
    var judge = 0;
    var AppointmentJudge = 1;
    $.ajax({
        async: false,
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

                for (var i = 0; i < lists.length; i++) {
                    if (lists[i]["StudentID"]) {
                        StudentID = lists[i]["StudentID"];
                    }
                    else {
                        StudentID = "";
                    }
                }
            }
            else {
                alert(json.MessageResult);
            }

        },
        error: function () {
            alert('加载失败请检查您的网络!');
        }
    });

    $.ajax({
        async: false,
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/SelectAppointment.ashx',
        data: {
            AppointmentJudge: AppointmentJudge,
            BookName: BookName,
            StudentID: StudentID,
            startIndex: startIndex,
            pageSize: pageSize
        },
        success: function (json) {
            if (json.MessageCode == "0") {
                createPage(json);
                createHtml(json);
            }
            else {
                alert("系统错误");
            }
        },
        error: function () {
            alert("系统错误");
        }


    })
}

function SearchAppointment(index, allNum) {

    var startIndex = index;
    var pageSize = allNum;
    var BookName;
    BookName = $.trim($("#SearchName").val());
    AppointmentShow(BookName, startIndex, pageSize);
}


function deleteAppointment() {
    if (confirm("是否确定删除该预约信息？")) {
        var AppointmentID = "";
        $(":checkbox[name='checkAppointmentID'][checked]").each(function () {
            AppointmentID = $(this).val();
            alert(AppointmentID);
        });
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '../../Handlers/DeleteAppointment.ashx',
            data: {
                AppointmentID: AppointmentID
            },
            success: function (json) {
                if (json.MessageCode == '0') {
                    alert("删除成功");
                    SearchAppointment(1, 10);
                }
                else {
                    alert("删除失败，系统错误");
                }
            },
            error: function () {
                alert("请检查网络");
            }
        })
    }
    else {

    }
}


function SkipReplyAppointment() {
    if (confirm("是否查看该预约回复？")) {
        var AppointmentID = "";
        $(":checkbox[name='checkAppointmentID'][checked]").each(function () {
            AppointmentID = $(this).val();
            alert(AppointmentID);
        });
        if (AppointmentID == "") {
            alert("请选择预约信息");
            return false;
        }
        else {
            window.location.href = "ReplyAppointment.htm?AppointmentID=" + AppointmentID;
        }
    }
}






