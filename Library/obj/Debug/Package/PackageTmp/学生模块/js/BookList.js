function SkipAddBook() {
    if (confirm("是否确定添加新书？")) {
        window.location.href = "AddBook.htm";    
     }
}

function createPage(json) {
    $("#pageList").html("");
    var lists = json.list;

    var MaxNum = json.count;//数据总数
    var pageNum = (MaxNum - 1) / 10 + 1;//页码总数
    var optionHtml = "";

    optionHtml += ['<ul class="paginList"></li>'].join('');
    for (var i = 1; i <= pageNum; i++) {
        optionHtml += ['<li class="paginItem"><a href="javascript:;" onclick="SearchBook(' + i + ', 10)">' + i + '</a></li>'].join('');
    }
    optionHtml += [' </ul>'].join('');
    $("#pageList").append(optionHtml);
 }

function careateHtml(json) {
    $("#BookList").html("");


    var lists = json.list;

    var optionHtml = "";
    var _BookID = "";
    var _ISBN = "";
    var _BookName = "";
    var _BookAuthor = "";
    var _Publisher = "";
    var _BookCase = "";
    var _Extant = "";
    var _Logout = "";

    optionHtml += ['<thead><tr><th ><input name="" type="checkbox" value="" /></th><th >条形码<i class="sort"><img src="images/px.gif" /></i></th> <th >书名</th><th >作者</th> <th >出版社</th><th >书架名</th><th >现存量</th><th>是否注销</th><th >操作</th></tr></thead><tbody>'].join('');
    for (var i = 0; i < lists.length; i++) {
        if (lists[i]["BookID"])
            _BookID = lists[i]["BookID"];
        else
            _BookID = "";

        if (lists[i]["ISBN"])
            _ISBN = lists[i]["ISBN"];
        else
            _ISBN = "";

        if (lists[i]["BookName"])
            _BookName = lists[i]["BookName"];
        else
            _BookName = "";

        if (lists[i]["BookAuthor"])
            _BookAuthor = lists[i]["BookAuthor"];
        else
            _BookAuthor = "";

        if (lists[i]["Publisher"])
            _Publisher = lists[i]["Publisher"];
        else
            _Publisher = "";

        if (lists[i]["BookCase"])
            _BookCase = lists[i]["BookCase"];
        else
            _BookCase = "";

        if (lists[i]["Extant"])
            _Extant = lists[i]["Extant"];
        else
            _Extant = "";

        if (lists[i]["Logout"])
            _Logout = lists[i]["Logout"];
        else
            _Logout = "";

        optionHtml += [
                            '<tr><td><input name="checkBookID" id="checkBookID" type="checkbox" value="' + _BookID + '" /></td>',
                            '<td>' + _ISBN + '</td>',
                            '<td>' + _BookName + '</td>',
                            '<td>' + _BookAuthor + '</td>',
                            '<td>' + _Publisher + '</td>',
                            '<td>' + _BookCase + '</td>',
                            '<td>' + _Extant + '</td>',
                            '<td>' + _Logout + '</td>',
                            '<td><a href="#" onclick="SkipBookSpecifics()" class="tablelink">查看</a></td></tr>'
                             ].join('');
    }
    optionHtml += [' </tbody> '].join('');
    $("#BookList").append(optionHtml);
}


function SearchBook(index, allNum) {
    var judge=  $("#selectName").find("option:selected").text();
    var startIndex=index;
    var pageSize=allNum;
     var BookName="";
     var BookAuthor="";


     if (judge == "请选择") {
         $.ajax({
             type: "POST",
             dataType: "json",
             url: "../../Handlers/SelectBook.ashx",
             data: {
                 BookName: BookName,
                 BookAuthor: BookAuthor,
                 startIndex: startIndex,
                 pageSize: pageSize
             },
             success: function (json) {
                 
                 if (json.MessageCode == "0") {
                     createPage(json);
                     careateHtml(json);


                     //                     var pageHtml = "";
                     //                     pageHtml += ['<ul class="paginList"> <li class="paginItem"><a href="javascript:;"><span class="pagepre"></span></a></li>'].join('');

                     //                     for (var i = 1; i <= _pageNum; i++) {
                     //                         pageHtml += ['<li class="paginItem" onclick="SearchBook(' + i + ', 10)">' + i + '</a></li>'].join('');
                     //                     }

                     //                     pageHtml += ['<li class="paginItem"><a href="javascript:;"><span class="pagenxt"></span></a></li></ul>'].join('');

                     //                     $("#page").append(pageHtml);


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
     else if (judge == "书名") {


         BookName = $.trim($("#SearchName").val());
        
         $.ajax({
             type: "POST",
             dataType: "json",
             url: "../../Handlers/SelectBook.ashx",
             data: {
                 BookName: BookName,
                 BookAuthor: BookAuthor,
                 startIndex: startIndex,
                 pageSize: pageSize
             },
             success: function (json) {
                 if (json.MessageCode == "0") {
//                     $("#indexNum").html('共' + json.count + '条数据，当前显示第' + startIndex + "页");
                     careateHtml(json);
                     createPage(json);
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
         BookAuthor = $.trim($("#SearchName").val());

         $.ajax({
             type: "POST",
             dataType: "json",
             url: "../../Handlers/SelectBook.ashx",
             data: {
                 BookName: BookName,
                 BookAuthor: BookAuthor,
                 startIndex: startIndex,
                 pageSize: pageSize
             },
             success: function (json) {
                 if (json.MessageCode == "0") {
                     $("#indexNum").html('共' + json.count + '条数据，当前显示第' + startIndex + "页");
                     careateHtml(json);
                     createPage(json);
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
}


function deleteBook() {
//    var BookID = $("input[type='checkbox']").attr('value');
//    alert(BookID);
    if (confirm("确定要删除该书？")) {
        var BookID = "";
        $(":checkbox[name='checkBookID'][checked]").each(function () {
            BookID = $(this).val();
            alert(BookID);
        });

        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '../../Handlers/DeleteBook.ashx',
            data: {
                BookID: BookID
            },
            success: function (json) {
                if (json.MessageCode == '0') {
                    alert("删除成功");
                    SearchBook(1, 10);
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



function SkipUpdateBook() {
    if (confirm("是否确定修改图书？")) {
        var BookID = ""
        $(":checkbox[name='checkBookID'][checked]").each(function () {
            BookID = $(this).val();
            alert(BookID);
        });
        if (BookID == "") {
            alert("请选择需要修改的书籍");
            return false;
        }
        else {
            window.location.href = "UpdateBook.htm?BookID=" + BookID; 
        }
    }
}


function SkipBookSpecifics() {
    if (confirm("是否确定查看图书？")) {
        var BookID = ""
        $(":checkbox[name='checkBookID'][checked]").each(function () {
            BookID = $(this).val();
            alert(BookID);
        });
        if (BookID == "") {
            alert("请选择需要查看的书籍");
            return false;
        }
        else {
            window.location.href = "Book.htm?BookID=" + BookID;
        }
    }
}

function SkipBorrow() {
    if (confirm("是否确定借阅该图书？")) {
        var BookID = ""
        $(":checkbox[name='checkBookID'][checked]").each(function () {
            BookID = $(this).val();
            alert(BookID);
        });
        if (BookID == "") {
            alert("请选择需要借阅的书籍");
            return false;
        }
        else {
            window.location.href = "Borrow.htm?BookID=" + BookID;
        }
    }
}