function exit() {
    if (confirm("是否退出图书详情？")) {
        window.location.href = "BookList.htm";
    }
}

function SelectBook() {
    var BookID = "";
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
                var _BookName = "";
                var _BookType = "";
                var _BookAuthor = "";
                var _Publisher = "";
                var _Price = "";
                var _BookCase = "";
                var _Stock = "";
                var _Summary = "";
                var _Extant = "";
                var _Appointment = "";
                var _BookLoan = "";
                var _Logout = "";
                var _PublishTime = "";
                for (var i = 0; i < lists.length; i++) {
                    if (lists[i]["ISBN"]) {
                        _ISBN = lists[i]["ISBN"];
                    }
                    else {
                        _ISBN = "";
                    }

                    if (lists[i]["BookName"]) {
                        _BookName = lists[i]["BookName"];
                    }
                    else {
                        _BookName = "";
                    }

                    if (lists[i]["BookType"]) {
                        _BookType = lists[i]["BookType"];
                    }
                    else {
                        _BookType = "";
                    }

                    if (lists[i]["BookAuthor"]) {
                        _BookAuthor = lists[i]["BookAuthor"];
                    }
                    else {
                        _BookAuthor = "";
                    }

                    if (lists[i]["Publisher"]) {
                        _Publisher = lists[i]["Publisher"];
                    }
                    else {
                        _Publisher = "";
                    }

                    if (lists[i]["Price"]) {
                        _Price = lists[i]["Price"];
                    }
                    else {
                        _Price = "";
                    }

                    if (lists[i]["BookCase"]) {
                        _BookCase = lists[i]["BookCase"];
                    }
                    else {
                        _BookCase = "";
                    }

                    if (lists[i]["Stock"]) {
                        _Stock = lists[i]["Stock"];
                    }
                    else {
                        _Stock = "";
                    }

                    if (lists[i]["Appointment"]) {
                        _Appointment = lists[i]["Appointment"];
                    }
                    else {
                        _Appointment = "";
                    }

                    if (lists[i]["Extant"]) {
                        _Extant = lists[i]["Extant"];
                    }
                    else {
                        _Extant = "";
                    }

                    if (lists[i]["Summary"]) {
                        _Summary = lists[i]["Summary"];
                    }
                    else {
                        _Summary = "";
                    }

                    if (lists[i]["BookLoan"]) {
                        _BookLoan = lists[i]["BookLoan"];
                    }
                    else {
                        _BookLoan = "";
                    }

                    if (lists[i]["PublishTime"]) {
                        _PublishTime = lists[i]["PublishTime"];
                    }
                    else {
                        _PublishTime = "";
                    }

                    if (lists[i]["Logout"]) {
                        _Logout = lists[i]["Logout"];
                    }
                    else {
                        _Logout = "";
                    }

                    $("#ISBN").attr("value", _ISBN); //填充内容 
                    $("#BookName").attr("value", _BookName);
                    $("#BookType").attr("value", _BookType);
                    $("#BookAuthor").attr("value", _BookAuthor);
                    $("#Publisher").attr("value", _Publisher);
                    $("#Price").attr("value", _Price);
                    $("#BookCase").attr("value", _BookCase);
                    $("#Stock").attr("value", _Stock);
                    $("#Summary").attr("value", _Summary);
                    $("#Extant").attr("value", _Extant);
                    $("#Appointment").attr("value", _Appointment);
                    $("#PublishTime").attr("value", _PublishTime);
                    $("#BookLoan").attr("value", _BookLoan);
                    $("#Logout").attr("value", _Logout);

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
