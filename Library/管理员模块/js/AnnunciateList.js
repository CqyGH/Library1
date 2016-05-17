function SkipAddAnnunciate() {
    if (confirm("是否确定添加新通告？")) {
        window.location.href = "AddAnnunciate.htm";
    }
}


function careateHtml(json) {
    $("#AnnunciateList").html("");

    var lists = json.list;

    var optionHtml = "";
    var AnnunciateID = "";
    var Information = "";
    var PublishTime = "";
    var Display = "";
    var Title = "";

    optionHtml += ['<thead><tr><th><input name="" type="checkbox" value="" /></th><th>标题<i class="sort"></i></th> <th>内容</th><th>时间</th> <th>是否展示</th></tr></thead><tbody>'].join('');
    for (var i = 0; i < lists.length; i++) {
        if (lists[i]["Information"])
            Information = lists[i]["Information"];
        else
            Information = "";

        if (lists[i]["PublishTime"])
            PublishTime = lists[i]["PublishTime"];
        else
            PublishTime = "";

        if (lists[i]["Display"])
            Display = lists[i]["Display"];
        else
            Display = "";

        if (lists[i]["Title"])
            Title = lists[i]["Title"];
        else
            Title = "";

        if (lists[i]["AnnunciateID"])
            AnnunciateID = lists[i]["AnnunciateID"];
        else
            AnnunciateID = "";

        optionHtml += [
                            '<tr><td><input name="checkBookID" id="checkBookID" type="checkbox" value="' + AnnunciateID + '" /></td>',
                            '<td>' + Title + '</td>',
                            '<td>' + Information + '</td>',
                            '<td>' + PublishTime + '</td>',
                            '<td>' + Display + '</td></tr>'
                             ].join('');
    }
    optionHtml += [' </tbody> '].join('');
    $("#AnnunciateList").append(optionHtml);
}

function AnnunciateShow(Display, Information, Title) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../../Handlers/SelectAnnunciate.ashx",
        data: {
            Display: Display,
            Information: Information,
            Title: Title
        },
        success: function (json) {
            if (json.MessageCode == "0") {
                careateHtml(json);
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

function SearchAnnunciate() {
    var judge = $("#selectName").find("option:selected").text();
    var Display = "";
    var Information = "";
    var Title = "";
  

    if (judge == "请选择") {
        AnnunciateShow(Display, Information, Title);
    }
    else if (judge == "标题") {
        Title = $.trim($("#SearchName").val());
        AnnunciateShow(Display, Information, Title);
    }
    else {
        Information = $.trim($("#SearchName").val());
        AnnunciateShow(Display, Information, Title);
    }
}


function deleteAnnunciate() {
    if (confirm("确定要删除该通告？")) {
        var AnnunciateID = "";
        $(":checkbox[name='checkBookID'][checked]").each(function () {
            AnnunciateID = $(this).val();
            alert(AnnunciateID);
        });

        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '../../Handlers/DeleteAnnunciate.ashx',
            data: {
                AnnunciateID: AnnunciateID
            },
            success: function (json) {
                if (json.MessageCode == '0') {
                    alert("删除成功");
                    SearchAnnunciate();
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



function SkipUpdateAnnunciate() {
    if (confirm("是否确定修改通告？")) {
        var AnnunciateID = ""
        $(":checkbox[name='checkBookID'][checked]").each(function () {
            AnnunciateID = $(this).val();
            alert(AnnunciateID);
        });
        if (AnnunciateID == "") {
            alert("请选择需要修改的通告");
            return false;
        }
        else {
            window.location.href = "UpdateAnnunciate.htm?AnnunciateID=" + AnnunciateID;
        }
    }
}


function SkipBookSpecifics() {
    if (confirm("是否确定查看通告？")) {
        var AnnunciateID = ""
        $(":checkbox[name='checkBookID'][checked]").each(function () {
            AnnunciateID = $(this).val();
            alert(AnnunciateID);
        });
        if (AnnunciateID == "") {
            alert("请选择需要查看的通告");
            return false;
        }
        else {
            window.location.href = "Annunciate.htm?AnnunciateID=" + AnnunciateID;
        }
    }
}


function UpdateAnnunciateDisplay() {
    var dis;
    var AnnunciateID = ""
    $(":checkbox[name='checkBookID'][checked]").each(function () {
        AnnunciateID = $(this).val();
        alert(AnnunciateID);
    });
    if (AnnunciateID == "") {
        alert("请选择需要修改的通告");
        return false;
    }
    else {
        var tbodyObj = document.getElementById('AnnunciateList');
        $("table :checkbox").each(function (key, value) {
            if ($(value).prop('checked')) {
                dis = tbodyObj.rows[key].cells[4].innerHTML;
//                alert(dis);
            }
        })

        if (dis == "False") {
            var Display = "1";
            DisplayAction(Display, AnnunciateID);
        }
        else {
            var Display = "0";
            DisplayAction(Display, AnnunciateID);
        }
    }
}

function DisplayAction(Display, AnnunciateID) {
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/UpdateAnnunciateDisplay.ashx',
        data: {
            Display: Display,
            AnnunciateID: AnnunciateID
        },
        success: function (json) {
            if (json.MessageCode == "0") {
                alert("修改成功!");
                SearchAnnunciate();
            }
            else {
                alert("修改失败");
            }
        },
        error: function () {
            alert("系统错误");
        }
    })
}