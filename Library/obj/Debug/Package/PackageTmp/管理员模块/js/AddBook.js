function addBook() {
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
        url: '../../Handlers/InsertBook.ashx',
        data: {
            ISBN: ISBN,
            BookType: BookType,
            BookName: BookName,
            BookAuthor: BookAuthor,
            Publisher: Publisher,
            Price: Price,
            BookCase: BookCase,
            Stock: Stock,
            Summary: Summary
        },
        success: function (json) {
            if (json.MessageCode == '0') {
                $("#ISBN").attr("value", '');
                $("#BookName").attr("value", '');
                $("#BookAuthor").attr("value", '');
                $("#Publisher").attr("value", '');
                $("#Price").attr("value", '');
                $("#Stock").attr("value", '');
                $("#Summary").attr("value", '');
                $("#BookType").attr("value", '请选择');
                $("#BookCase").attr("value", '请选择');
                alert("新增成功!");

            }
            else {
                alert("新增失败，请检查网络!");
            }
        },
        error: function () {
            alert("系统错误");
        }
    })

}

function exit() {
    if (confirm("确定退出添加图书？")) {
        window.location.href = "BookList.htm";
    }
    else { 
    
    }
}


function searchBooktype() {
    var options = '';
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/SelectBookType.ashx',
        data: {

        },
        async:false,
        success: function (json) {
            if (json.MessageCode == "0") {
                var lists = json.list;
                for (var i = 0; i < lists.length; i++) {
                    if (lists[i]["TypeName"]) {
                        options += "<option values='" + lists[i]["TypeName"] + "'>" + lists[i]["TypeName"] + "</option>";
                    }
                    else {
                        options += "";
                    }
                }
            }
            else {
                options += "";
            }
        },
        error: function () {
            options += "";
        }
    })

    $('#BookType').html(options);
}

function searchBookCase() {
    var options = '';
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../../Handlers/SelectCase.ashx',
        data: {

        },
        async: false,
        success: function (json) {
            if (json.MessageCode == "0") {
                var lists = json.list;
                for (var i = 0; i < lists.length; i++) {
                    if (lists[i]["CaseName"]) {
                        options += "<option values='" + lists[i]["CaseName"] + "'>" + lists[i]["CaseName"] + "</option>";
                    }
                    else {
                        options += "";
                    }
                }
            }
            else {
                options += "";
            }
        },
        error: function () {
            options += "";
        }
    })

    $('#BookCase').html(options);
}