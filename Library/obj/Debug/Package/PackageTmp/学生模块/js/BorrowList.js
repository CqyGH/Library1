function careateHtml(json) {
    $("#BorrowList").html("");

    var lists = json.list;

    var optionHtml = "";
    var _BorrowID = "";
    var _StudentID = "";
    var _UserName = "";
    var _BookName = "";
    var _ISBN = "";
    var _BorrowTime = "";

    var ReturnTime = "";

    optionHtml += ['<thead><tr><th><input name="" type="checkbox" value="" /></th><th>学号<i class="sort"><img src="images/px.gif" /></i></th> <th>姓名</th><th>书名</th> <th>ISBN</th><th>借阅时间</th><th>应还时间</th><th>续借次数</th></tr></thead><tbody>'].join('');
    for (var i = 0; i < lists.length; i++) {
        if (lists[i]["BorrowID"])
            _BorrowID = lists[i]["BorrowID"];
        else
            _BorrowID = "";

        if (lists[i]["StudentID"])
            _StudentID = lists[i]["StudentID"];
        else
            _StudentID = "";

        if (lists[i]["UserName"])
            _UserName = lists[i]["UserName"];
        else
            _UserName = "";

        if (lists[i]["ISBN"])
            _ISBN = lists[i]["ISBN"];
        else
            _ISBN = "";

        if (lists[i]["BookName"])
            _BookName = lists[i]["BookName"];
        else
            _BookName = "";

        if (lists[i]["BorrowTime"])
            _BorrowTime = lists[i]["BorrowTime"];
        else
            _BorrowTime = "";

        if (lists[i]["Renew"])
            Renew = lists[i]["Renew"];
        else
            Renew = "";

        if (lists[i]["ReturnTime"])
            ReturnTime = lists[i]["ReturnTime"];
        else
            ReturnTime = "";

        optionHtml += [
                            '<tr><td><input type="checkbox"  name="BorrowID" id="BorrowID"  value="' + _BorrowID + '" /></td>',
                            '<td>' + _StudentID + '</td>',
                            '<td>' + _UserName + '</td>',
                            '<td>' + _BookName + '</td>',
                            '<td>' + _ISBN + '</td>',
                            '<td>' + _BorrowTime + '</td>',
                             '<td>' + ReturnTime + '</td>',
                           '<td>' + Renew + '</td></tr>'
                             ].join('');
    }
    optionHtml += [' </tbody> '].join('');
    $("#BorrowList").append(optionHtml);
}



function SearchBorrow() {
    var StudentID = "";
    var judge = 0;
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
                for (var i = 0; i < lists.length; i++) {

                    if (lists[i]["StudentID"]) {
                        StudentID = lists[i]["StudentID"];
                    }

                    else {
                        StudentID = "";
                    }
                }

                $.ajax({
                    type: "POST",
                    dataType: "json",
                    url: "../../Handlers/SelectBorrow.ashx",
                    data: {
                        StudentID: StudentID
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
                });
            }
            else {
                alert(json.MessageResult);

            }

        },
        error: function () {
            alert('加载失败请检查您的网络!');
        }
    })
}

function Rennew() {
    var BorrowID;

    $(":checkbox[name='BorrowID'][checked]").each(function () {
        BorrowID = $(this).val();
        //            alert(BookID);
    });
    if (BorrowID == "") {
        alert("请选择需要续借书籍！");
        return false;
    }
    else {
        alert(BorrowID);
        if (GetChecked()) {
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "../../Handlers/UpdateBorrowRenew.ashx",
                data: {
                    BorrowID: BorrowID
                },
                success: function (json) {
                    if (json.MessageCode == "0") {
                        alert("续借成功");
                        SearchBorrow();
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
        else {
            alert("此书无法续借，请联系管理员!");
            return false;
        }
    }
}


//查询图书是否可以续借
function GetChecked() {
    var ReturnTime;
    var Renew;
    var SystemTime = new Date();
    var tbodyObj = document.getElementById('BorrowList');
    $("table :checkbox").each(function (key, value) {
        if ($(value).prop('checked')) {
            ReturnTime = tbodyObj.rows[key].cells[6].innerHTML;
            Renew = tbodyObj.rows[key].cells[7].innerHTML;
        }
    })
    var date1 = Date.parse(SystemTime);
    var date2 = Date.parse(ReturnTime);
    if (Renew >= 4) {
        return false;
    }
    else if (date1 >= date2) {
        return false;
    }
    else {
        return true;
    }
}