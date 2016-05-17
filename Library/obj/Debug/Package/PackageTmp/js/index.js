function temp() {
    window.location = "Login.htm";
}

function AddApointment() {
    var StudentID = $.trim($("#StudentID").val());
    var BookName = $.trim($("#BookName").val());
    if (StudentID == "" || BookName == "") {
        alert("请重新输入预约信息");
        return false;
    }

    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../Handlers/InsertAppointment.ashx',
        data: {
            StudentID: StudentID,
            BookName: BookName
        },
        success: function (json) {
            if (json.MessageCode == '0') {
                alert("预约成功");
                window.location.reload(true);
            }
            else {
                alert("预约失败，系统错误")
            }
        },
        error: function () {
            alert("系统错误");
        }


    })
}

function ShowTop15Book() {
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../Handlers/SelectTop15Book.ashx',
        data: {

        },
        success: function (json) {
            if (json.MessageCode == "0") {
                CreateRanklistHtml(json);
                //            alert(json);
            }
            else {
                alert("系统错误");
            }
        },
        error: function () {
            alert("请检查网络");
        }

    })
}
//配合查询排行使用
function CreateRanklistHtml(json) {
    var lists = json.list;

    var optionHtml = "";
    var _BookName = "";
    var _BookLoan = "";
    var borrowCount = json.borrowCount;

    //optionHtml += ['<div class="skill-grids">'].join('');

    for (var i = 0; i < lists.length; i++) {
        if (lists[i]["BookName"]) {
            _BookName = lists[i]["BookName"];
        }
        else {
            _BookName = "";
        }
        if (lists[i]["BookLoan"]) {
            _BookLoan = lists[i]["BookLoan"];
        }
        else {
            _BookLoan = "";
        }


        var percentage = (_BookLoan / borrowCount) * 100;
        optionHtml += ['<h6>' + _BookName + '</h6><div class="progress"><div class="progress-bar progress-bar" role="progressbar" aria-valuenow="95" aria-valuemin="0" aria-valuemax="100" style="width: ' + percentage + '%"><span class="sr-only">' + _BookLoan + '% Complete</span></div></div>'].join('');
    }

    //optionHtml += ['</div>'].join('');

    $("#Ranklist").append(optionHtml);
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


    optionHtml += ['<thead style="border:solid 1px #d8d8d8;width:1140px;"><tr><th width="10%">条形码<i class="sort"></i></th><th width="30%">书名</th><th width="30%">作者</th><th width="15%">出版社</th><th width="10%">书架名</th><th width="5%">现存量</th><th width="5%">操作</th></tr></thead><tbody>'].join('');
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


        optionHtml += [
                            '<td>' + _ISBN + '</td>',
                            '<td>' + _BookName + '</td>',
                            '<td>' + _BookAuthor + '</td>',
                            '<td>' + _Publisher + '</td>',
                            '<td>' + _BookCase + '</td>',
                            '<td>' + _Extant + '</td>',
                            '<td><a href="#" onclick="SkipBookSpecifics(' + _BookID + ')" class="tablelink">查看</a></td></tr>'
                             ].join('');
    }
    optionHtml += [' </tbody> '].join('');
    $("#BookList").append(optionHtml);
}

function conncetSearch(BookName, BookAuthor, startIndex, pageSize) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "../Handlers/SelectBook.ashx",

        data: {
            BookName: BookName,
            BookAuthor: BookAuthor,
            startIndex: startIndex,
            pageSize: pageSize
        },
        success: function (json) {
            //            alert(json.MessageCode);
            if (json.MessageCode == "0") {
                //                createPage(json);
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

function SearchBook(index, allNum) {
    var judge = $("#selectName").find("option:selected").text();
    var startIndex = index;
    var pageSize = allNum;
    var BookName = "";
    var BookAuthor = "";

    if (judge == "请选择") {
        conncetSearch(BookName, BookAuthor, startIndex, pageSize);
    }
    else if (judge == "书名") {


        BookName = $.trim($("#SearchName").val());
        conncetSearch(BookName, BookAuthor, startIndex, pageSize);
    }
    else {
        BookAuthor = $.trim($("#SearchName").val());
        conncetSearch(BookName, BookAuthor, startIndex, pageSize);
    }
}


function SearchAnnunciate() {
    var Display = 1;
 
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '../Handlers/SelectAnnunciate.ashx',
        data: {
            Display: Display
        },
        async:false,
        success: function (json) {
            if (json.MessageCode = "0") {
                createAnnunciate(json);
            }
            else {

            }
        },
        error: function () {

        }
    })
}

function createAnnunciate(json) {
    var lists = json.list;
    $("#AnnunciateList").html("");
    var optionHtml = "";
    var Information = "";
    var PublishTime = "";
    var Title = "";
    var AnnunciateID = "";
    var Count = json.count;
    optionHtml += ['<ul class="rslides" id="slider2">'].join(' ');

    for (var i = 0; i < lists.length; i += 2) {
        optionHtml += ['<li>'].join('');
        for (var j = i; j < i + 2 && j < lists.length; j++) {
            if (lists[j]["Information"]) {
                Information = lists[j]["Information"];
            }
            else {
                Information = "";
            }
            if (lists[j]["PublishTime"]) {
                PublishTime = lists[j]["PublishTime"];
            }
            else {
                PublishTime = "";
            }
            if (lists[j]["Title"]) {
                Title = lists[j]["Title"];
            }
            else {
                Title = "";
            }
            if (lists[j]["AnnunciateID"]) {
                AnnunciateID = lists[j]["AnnunciateID"];
            }
            else {
                AnnunciateID = "";
            }
            optionHtml += ['<div class="col-md-6 pricing-plans"><p>' + Information + '</p><div class="pic1"><span>' + PublishTime + '</span></div><div class="pic-info"><h5>' + Title + '</h5><a href="#" onclick="SkipAnnunciateSpecifics(' + AnnunciateID + ')">查看</a></div><div class="clearfix"></div></div>'].join('');
        }
        optionHtml += ['<div class="clearfix"></div></li>'].join('');
    }
    optionHtml += ['</ul>'].join('');

    $("#AnnunciateList").append(optionHtml);
}



function SkipAnnunciateSpecifics(AnnunciateID) {
    if (confirm("是否确定查看通告？")) {
        window.location.href = "Annunciate.htm?AnnunciateID=" + AnnunciateID;
//        window.location.href = "Annunciate.htm";
    }
}


function SkipBookSpecifics(BookID) {
//    if (confirm("是否确定查看图书？")) {
//        window.location.href = "Book.htm?BookID=" + BookID; 
    //    }
    window.location.href = "Book.htm?BookID=" + BookID; 
}
