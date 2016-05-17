function SkipAddBookType() {
    if (confirm("是否确定添加新书架？")) {
        window.location.href = "AddCase.htm";
    }
}



function careateHtml(json) {
    $("#BookList").html("");


    var lists = json.list;

    var optionHtml = "";
    var CaseName = "";
    var CaseID = "";
    var CreatTime = "";


    optionHtml += ['<thead><tr><th><input name="" type="checkbox" value="" /></th><th>书架名</th><th>添加时间</thead><tbody>'].join('');
    for (var i = 0; i < lists.length; i++) {
        if (lists[i]["CaseName"])
            CaseName = lists[i]["CaseName"];
        else
            CaseName = "";

        if (lists[i]["CaseID"])
            CaseID = lists[i]["CaseID"];
        else
            CaseID = "";

        if (lists[i]["CreatTime"])
            CreatTime = lists[i]["CreatTime"];
        else
            CreatTime = "";
        optionHtml += [
                            '<tr><td><input name="checkBookTypeID" id="checkBookTypeID" type="checkbox" value="' + CaseID + '" /></td>',
                            '<td>' + CaseName + '</td>',
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
        url: "../../Handlers/SelectCase.ashx",
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
        var CaseID = "";
        $(":checkbox[name='checkBookTypeID'][checked]").each(function () {
            CaseID = $(this).val();
            alert(CaseID);
        });

        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '../../Handlers/DeleteCase.ashx',
            data: {
                CaseID: CaseID
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




