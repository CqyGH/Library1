function careateHtml(json) {

    var now = new Date();
    var yr = now.getYear() + 1900;
    var mName = now.getMonth() + 1;
    var dName = now.getDay() + 1;
    var dayNr = ((now.getDate() < 10) ? "0" : "") + now.getDate();
    var hours = now.getHours();
    //    hours = ((hours < 10) ? "0" : "") + now.getHours();
    var minutes = now.getMinutes();   //((now.getMinutes() < 10) ? "0" : "") + 
    var seconds = now.getSeconds();   // ((now.getSeconds() < 10) ? "0" : "") +
    if (mName == 1) Month = "1";
    if (mName == 2) Month = "2";
    if (mName == 3) Month = "3";
    if (mName == 4) Month = "4";
    if (mName == 5) Month = "5";
    if (mName == 6) Month = "6";
    if (mName == 7) Month = "7";
    if (mName == 8) Month = "8";
    if (mName == 9) Month = "9";
    if (mName == 10) Month = "10";
    if (mName == 11) Month = "11";
    if (mName == 12) Month = "12";
    var DayDateTime = (yr + '/' + Month + '/' + dayNr + ' ' + hours + ':' + minutes + ':' + seconds);
//    alert("DayDateTime：" + DayDateTime);

    //    var date = new Date();
    //    alert(date);





    $("#BorrowList").html("");


    var lists = json.list;

    var optionHtml = "";
    var _BorrowID = "";
    var _StudentID = "";
    var _UserName = "";
    var _BookName = "";
    var _ISBN = "";
    var _BorrowTime = "";
    var _ReturnTime = "";
    var _Cost = "";
    

    optionHtml += ['<thead><tr><th><input name="" type="checkbox" value="" /></th><th>学号<i class="sort"><img src="images/px.gif" /></i></th> <th>姓名</th><th>书名</th> <th>ISBN</th><th>借阅时间</th><th>欠费</th><th>操作</th></tr></thead><tbody>'].join('');
    for (var i = 0; i < lists.length; i++) {
        var differ;     //计算当前系统时间与应还图书相差时间
        var DifferCost;       //计算欠费
        if (lists[i]["BorrowID"])
            _BorrowID = lists[i]["BorrowID"];
        else
            _BorrowID = "";

        if (lists[i]["Cost"])
            _Cost = lists[i]["Cost"];
        else
            _Cost = "";

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

        if (lists[i]["ReturnTime"])
            _ReturnTime = lists[i]["ReturnTime"];
        else
            _ReturnTime = "";
//        alert("_ReturnTime:" + _ReturnTime);


        //        var strDate1 = "2004-09-17   03:03:00.0";
        //        var strDate2 = "2004-09-18   04:05:00.0";
        //        strDate1 = _ReturnTime.substring(0, strDate1.lastIndexOf(".")).replace(/-/g, "/ ");
        //        strDate2 = DayDateTime.substring(0, strDate2.lastIndexOf(".")).replace(/-/g, "/ ");
        //        alert("strDate1:" + strDate1);
        //        alert("strDate2" + strDate2);
        //去掉毫秒 把-替换成/ 如果不替换转成时间戳类型火狐会出问题
        var date1 = Date.parse(DayDateTime);
        var date2 = Date.parse(_ReturnTime);
//        alert("date1:" + date1);
//        alert("date2" + date2);
//        alert("strDate2与strDate1相差 " + ((date2 - date1) / (60 * 60 * 1000)) / 24 + "天 ")
        if (date1 > date2) {
            differ = Math.ceil(((date1 - date2) / (60 * 60 * 1000)) / 24);
//            alert("differ：" + differ);
            DifferCost = (differ * 0.2).toFixed(1);

            if (DifferCost > _Cost) {
                UpdateBorrowCost(DifferCost, _BorrowID);
            }
//            alert("_Cost：" + _Cost);
//            alert("DifferCost：" + DifferCost);

        }
        else {
            DifferCost = 0;
        }
      



        var temp = _ReturnTime.replace(/[^0-9]/ig, "");
//        alert(temp);

        optionHtml += [
                            '<tr><td><input name="BorrowID" id="BorrowID" type="checkbox" value="' + _BorrowID + '" /></td>',
                            '<td>' + _StudentID + '</td>',
                            '<td>' + _UserName + '</td>',
                            '<td>' + _BookName + '</td>',
                            '<td>' + _ISBN + '</td>',
                            '<td>' + _BorrowTime + '</td>',
                             '<td>' + DifferCost + '</td>',
                            '<td><a href="#" onclick="SkipBookSpecifics()" class="tablelink">查看</a></td></tr>'
                             ].join('');
    }
    optionHtml += [' </tbody> '].join('');
    $("#BorrowList").append(optionHtml);
}

function SearchBorrow() {
    var StudentID = $.trim($("#SearchName").val());
//    alert(StudentID);
    if (StudentID == "") {
        alert("请输入学号");
        return false;
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
    })
}


function returnBook() {
    var BorrowID = "";
    $(":checkbox[name='BorrowID'][checked]").each(function () {
        BorrowID = $(this).val();
        alert(BorrowID);
    });
    if (BorrowID == "") {
        alert("请选择需要修改的书籍");
        return false;
    }

    var StudentID = "";
    var UserName = "";
    var BookName = "";
    var ISBN = "";

    var array = $("table input[type=checkbox]:checked").map(function () {
        return { "cell1": $.trim($(this).closest("tr").find("td:eq(1)").text()), "cell2": $.trim($(this).closest("tr").find("td:eq(2)").text()), "cell3": $.trim($(this).closest("tr").find("td:eq(3)").text()), "cell4": $.trim($(this).closest("tr").find("td:eq(4)").text()) };
    }).get();

    $.each(array, function (i, d) {

        StudentID = d.cell1;
        UserName = d.cell2;
        BookName = d.cell3;
        ISBN = d.cell4;
        //                     alert(d.cell2 + "|" + d.cell4);
    })



    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../../Handlers/InsertReturn.ashx",
        data: {
            UserName: UserName,
            StudentID: StudentID,
            BorrowID: BorrowID,
            BookName: BookName,
            ISBN: ISBN
        },
        success: function (json) {
            if (json.MessageCode == "0") {
                alert("还书成功");
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

function UpdateBorrowCost(Cost, BorrowID) {
    var Cost = Cost;
    var BorrowID = BorrowID;

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../../Handlers/UpdateBorrowCost.ashx",
        data: {
            Cost: Cost,
            BorrowID: BorrowID
        },
        success: function (json) {
            if (json.MessageCode == "0") {
                return true;
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