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
                var _BookName = "";
                var _BookType = "";
                var _BookAuthor = "";
                var _Publisher = "";
                var _Price = "";
                var _BookCase = "";
                var _Stock = "";
                var _Summary = "";
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

                    if (lists[i]["Summary"]) {
                        _Summary = lists[i]["Summary"];
                    }
                    else {
                        _Summary = "";
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

function UpdateBook() {
    var ISBN = $.trim($("#ISBN").val());
    var BookType = $("#BookType").find("option:selected").text();
    var BookName = $.trim($("#BookName").val());
    var BookAuthor = $.trim($("#BookAuthor").val());
    var Publisher = $.trim($("#Publisher").val());
    var Price = $.trim($("#Price").val());
    var BookCase = $("#BookCase").find("option:selected").text();
    var Stock = $.trim($("#Stock").val());
    var Summary = $.trim($("#Summary").val());
    if (ISBN == "" || BookType == "" || BookName == "" || BookAuthor == "" || Publisher == "" || Price == "" || Stock == "") {
        alert("请输入完整内容");
        return false;
    }
    if (BookType == "请选择" || BookCase == "请选择") {
        alert("请选择内容");
        return false;
    }
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/UpdateBook.ashx',
        data: {
            ISBN: ISBN,
            BookType: BookType,
            BookName: BookName,
            BookAuthor: BookAuthor,
            Publisher: Publisher,
            Price: Price,
            BookCase: BookCase,
            Stock: Stock,
            Summary: Summary,
            BookID: BookID
        },
        success: function (json) {
            if (json.MessageCode == '0') {

                alert("修改成功!");
                window.location.href = "BookList.htm";

            }
            else {
                alert("修改失败，请检查网络!");
            }
        },
        error: function () {
            alert("系统错误");
        }
    })
}

function exit() {
    if (confirm("是否确定取消修改图书？")) {
        window.location.href = "BookList.htm";
    }
}