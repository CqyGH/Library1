function SkipAddBookType() {
    if (confirm("是否确定添加新图书类型？")) {
        window.location.href = "AddBookType.htm";
    }
}



function careateHtml(json) {
    $("#BookList").html("");


    var lists = json.list;

    var optionHtml = "";
    var BookTypeID = "";
    var TypeName = "";
    var CreatTime = "";


    optionHtml += ['<thead><tr><th><input name="" type="checkbox" value="" /></th><th>类型名</th><th>添加时间</thead><tbody>'].join('');
    for (var i = 0; i < lists.length; i++) {
        if (lists[i]["BookTypeID"])
            BookTypeID = lists[i]["BookTypeID"];
        else
            BookTypeID = "";

        if (lists[i]["TypeName"])
            TypeName = lists[i]["TypeName"];
        else
            TypeName = "";

        if (lists[i]["CreatTime"])
            CreatTime = lists[i]["CreatTime"];
        else
            CreatTime = "";
        optionHtml += [
                            '<tr><td><input name="checkBookTypeID" id="checkBookTypeID" type="checkbox" value="' + BookTypeID + '" /></td>',
                            '<td>' + TypeName + '</td>',
                            '<td>' + CreatTime + '</td>',
                             ].join('');
    }
    optionHtml += [' </tbody> '].join('');
    $("#BookList").append(optionHtml);
}


function SearchBookType() {

        $.ajax({
            type: "POST",
            dataType: "json",
            url: "../../Handlers/SelectBookType.ashx",
            data: {
               
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


function deleteBookType() {
    //    var BookID = $("input[type='checkbox']").attr('value');
    //    alert(BookID);
    if (confirm("确定要删除该书？")) {
        var BookTypeID = "";
        $(":checkbox[name='checkBookTypeID'][checked]").each(function () {
            BookTypeID = $(this).val();
            alert(BookTypeID);
        });

        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '../../Handlers/DeleteBookType.ashx',
            data: {
                BookTypeID: BookTypeID
            },
            success: function (json) {
                if (json.MessageCode == '0') {
                    alert("删除成功");
                    SearchBookType();
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




